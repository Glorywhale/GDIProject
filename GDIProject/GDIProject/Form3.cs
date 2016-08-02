using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GDIProject;

namespace GDIProject
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            gp.Width = 2;
            gp.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;                       
            p.Width = 2;
            panelControl1.MouseDoubleClick += new MouseEventHandler(panelControl1_MouseDoubleClick);

            DrawFigure = new FigureInfo();
            DrawFigure.FigureType = 1;

            ChangeEventHandler(MouseModifyMode.DrawMode);
        }

        Pen p = new Pen(Color.Black);
        Pen gp = new Pen(Color.Blue);
        Pen sp = new Pen(Color.Black);
        Rectangle guide_figure;
        FigureInfo DrawFigure;        

        List<Rectangle> _srList = new List<Rectangle>();
        List<FigureInfo> figureList = new List<FigureInfo>();

        public Rectangle GetGuideRect(Rectangle figure)
        {
            guide_figure = new Rectangle(figure.X - 6, figure.Y - 6, figure.Width + 14, figure.Height + 14);

            return guide_figure;
        }
                        
        private void ChangeEventHandler(MouseModifyMode mode)
        {
            lbl_status.Text = mode == 0 ? "Draw" : "Select";
            ClearMouseEvent(mode);
            switch (mode)
            {
                case MouseModifyMode.DrawMode:
                    panelControl1.MouseDown += new MouseEventHandler(panelControl1_DrawMouseDown);
                    panelControl1.MouseMove += new MouseEventHandler(panelControl1_DrawMouseMove);
                    panelControl1.MouseUp += new MouseEventHandler(panelControl1_DrawMouseUp);
                    break;
                case MouseModifyMode.SelectMode:
                    panelControl1.MouseDown += new MouseEventHandler(panelControl1_SelectMouseDown);
                    panelControl1.MouseMove += new MouseEventHandler(panelControl1_SelectMouseMove);
                    panelControl1.MouseUp += new MouseEventHandler(panelControl1_SelectMouseUp);
                    break;
            }
        }

        private void ClearMouseEvent(MouseModifyMode current_mode)
        {
            switch (current_mode)
            {
                case MouseModifyMode.Default:

                    break;
                case MouseModifyMode.DrawMode:
                    panelControl1.MouseDown -= new MouseEventHandler(panelControl1_SelectMouseDown);
                    panelControl1.MouseMove -= new MouseEventHandler(panelControl1_SelectMouseMove);
                    panelControl1.MouseUp -= new MouseEventHandler(panelControl1_SelectMouseUp);                    
                    break;
                case MouseModifyMode.SelectMode:
                    panelControl1.MouseDown -= new MouseEventHandler(panelControl1_DrawMouseDown);
                    panelControl1.MouseMove -= new MouseEventHandler(panelControl1_DrawMouseMove);
                    panelControl1.MouseUp -= new MouseEventHandler(panelControl1_DrawMouseUp);                
                    break;
            }
        }

        

        private void panelControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Check ID
            foreach (FigureInfo fi in figureList)
            {
                if (isInside(e.Location, fi.Rectangle))
                {
                    _currentFigure = fi;
                    ChangeEventHandler(MouseModifyMode.SelectMode);
                }
            }

            panelControl1_SelectMouseDown(null, e);
        }



        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
            //if (figure == null) return;
            
            DrawFigureList(e);

            DrawCurrentFigure(e);

            if (!isDrawing)
            {
                if (DrawFigure.Rectangle.Width > 0 && DrawFigure.Rectangle.Height > 0)
                {
                    e.Graphics.DrawRectangle(gp, GetGuideRect(DrawFigure.Rectangle));
                    GetSizeRect(GetGuideRect(DrawFigure.Rectangle));

                    foreach (Rectangle figureitem in _srList)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.White), figureitem);
                        e.Graphics.DrawRectangle(p, figureitem);
                    }
                }
            }
        }
        
        private void DrawCurrentFigure(PaintEventArgs e)
        {
            switch (DrawFigure.FigureType)
            {
                case 0:
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), DrawFigure.Rectangle);
                    e.Graphics.DrawRectangle(p, DrawFigure.Rectangle);
                    break;
                case 1:
                    e.Graphics.FillEllipse(new SolidBrush(Color.White), DrawFigure.Rectangle);
                    e.Graphics.DrawEllipse(p, DrawFigure.Rectangle);
                    break;
            }
        }

        private void DrawFigureList(PaintEventArgs e)
        {            
            foreach (FigureInfo figureitem in figureList)
            {
                int counter = 0;
                switch (figureitem.FigureType)
                {
                    case 0:
                        Console.WriteLine("Draw " + figureitem.id + counter++);
                        e.Graphics.FillRectangle(new SolidBrush(Color.White), figureitem.Rectangle);
                        e.Graphics.DrawRectangle(p, figureitem.Rectangle);
                        
                        break;
                    case 1:
                        e.Graphics.FillEllipse(new SolidBrush(Color.White), figureitem.Rectangle);
                        e.Graphics.DrawEllipse(p, figureitem.Rectangle);
                        break;
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        int g_size = 12;
        public void GetSizeRect(Rectangle item)
        {
            _srList.Clear();
            _srList.Add(new Rectangle(item.X - g_size / 2, item.Y - g_size / 2, g_size, g_size)); // 상좌
            _srList.Add(new Rectangle(item.X + item.Width - g_size / 2, item.Y - g_size / 2, g_size, g_size)); // 상우
            _srList.Add(new Rectangle(item.X - g_size / 2, item.Y + item.Height - g_size / 2, g_size, g_size)); // 하좌
            _srList.Add(new Rectangle(item.X + item.Width - g_size / 2, item.Y + item.Height - g_size / 2, g_size, g_size)); // 하우
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            setType = 0;
        }


        private Rectangle getRectangle()
        {
            return new Rectangle(
                Math.Min(_startPos.X, _currentPos.X),
                Math.Min(_startPos.Y, _currentPos.Y),
                Math.Abs(_startPos.X - _currentPos.X),
                Math.Abs(_startPos.Y - _currentPos.Y));
        }

        bool isDrawing = false;
        Point _startPos;
        Point _currentPos;
        int setType = 0;        
        bool isFocus = false;
        bool isHold = false;
        private FigureInfo _currentFigure;

        public bool isInside(Point mouse, Rectangle rectangle)
        {
            bool isInside = false;

            Rectangle _rect = rectangle;

            if (mouse.X > _rect.X && mouse.X < _rect.X + _rect.Width && mouse.Y > _rect.Y && mouse.Y < _rect.Y + _rect.Width)
            {
                isInside = true;
            }

            return isInside;
        }

        int counter = 0;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            setType = 1;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DrawFigure = null;
            figureList.Clear();
            panelControl1.Invalidate();
        }

        private void panelControl1_DrawMouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            _startPos = _currentPos = e.Location;
            if (DrawFigure == null)
            {
                DrawFigure = new FigureInfo();
            }
            DrawFigure.FigureType = setType;
        }

        private void panelControl1_DrawMouseMove(object sender, MouseEventArgs e)
        {
            if (DrawFigure == null) return;

            if (isDrawing)
            {
                _currentPos = e.Location;
                DrawFigure.Rectangle = getRectangle();
                panelControl1.Invalidate();
            }
        }

        private void panelControl1_DrawMouseUp(object sender, MouseEventArgs e)
        {
            _currentPos = e.Location;            
            FigureInfo addFigure = new FigureInfo();
            addFigure.id = DrawFigure.FigureType == 0 ? "Rectangle" + counter++ : "Ellipse" + counter++;
            addFigure.Rectangle = getRectangle();

            figureList.Add(addFigure);

            addFigure = null;
            isDrawing = false;
            isFocus = true;
            isHold = false;            

            ChangeEventHandler(MouseModifyMode.SelectMode);
            panelControl1.Invalidate();            
        }

        Point _revisePoint = new Point();
        private void panelControl1_SelectMouseDown(object sender, MouseEventArgs e)
        {
            _currentFigure = null;
            isDrawing = false;
            // Check ID
            foreach (FigureInfo fi in figureList)
            {
                if (isInside(e.Location, fi.Rectangle))
                {
                    _currentFigure = fi;
                    isFocus = !isFocus;
                    Console.WriteLine("Current Figure ID : {0}", fi.id);
                }
            }

            if (_currentFigure == null)
            {
                isHold = false;
                isFocus = false;
                ChangeEventHandler(MouseModifyMode.DrawMode);
            }
            else
            {
                isHold = true;
                int xpos = e.Location.X - _currentFigure.Rectangle.X;
                int ypos = e.Location.Y - _currentFigure.Rectangle.Y;

                _revisePoint.X = xpos;
                _revisePoint.Y = ypos;
            }
            _startPos = _currentPos = e.Location;            
        }

        private Point RevisePosition(Point startPoint, Point selectPoint)
        {
            //Console.WriteLine(selectPoint.X + "," + selectPoint.Y + "," + startPoint.X + ", " + startPoint.Y);
            Point results = new Point();
            int x_pos = selectPoint.X - startPoint.X;
            int y_pos = selectPoint.Y - startPoint.Y;
            //int x_pos = Math.Min(selectPoint.X, startPoint.X) - Math.Max(selectPoint.X, startPoint.X);
            //int y_pos = Math.Min(selectPoint.Y, startPoint.Y) - Math.Max(selectPoint.Y, startPoint.Y);

            //Console.WriteLine(selectPoint.X + "," + startPoint.X + "," + x_pos + ", " + y_pos);

            results.X = selectPoint.X + x_pos;
            results.Y = selectPoint.Y + y_pos;
            
            return results;
        }

        private void panelControl1_SelectMouseMove(object sender, MouseEventArgs e)
        {
            if (_srList.Count == 0 || DrawFigure == null) return;

            if (isInside(e.Location, _srList[0]) || isInside(e.Location, _srList[3]))
            {
                this.Cursor = Cursors.SizeNWSE;
            }
            else if (isInside(e.Location, _srList[1]) || isInside(e.Location, _srList[2]))
            {
                this.Cursor = Cursors.SizeNESW;
            }
            else if (isInside(e.Location, DrawFigure.Rectangle))
            {                
                this.Cursor = Cursors.SizeAll;
            }
            else
            {                
                this.Cursor = Cursors.Default;
            }
            
            if (isHold)
            {   
                _currentFigure.Rectangle = RevisePosition(e);
                panelControl1.Invalidate();
            }            
        }

        private Rectangle RevisePosition(MouseEventArgs e)
        {
            Rectangle rect = _currentFigure.Rectangle;
            
            rect.X = e.X - _revisePoint.X;
            rect.Y = e.Y - _revisePoint.Y;

            return rect;
        }

        private void panelControl1_SelectMouseUp(object sender, MouseEventArgs e)
        {
            _currentFigure = null;
            isFocus = false;
            isHold = false;
        }
    }

    
}
