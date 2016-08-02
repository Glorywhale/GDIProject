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
    public partial class Form4 : Form
    {
        public Form4()
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

        GDIManager gm = new GDIManager();
        Pen p = new Pen(Color.Black);
        Pen gp = new Pen(Color.Blue);
        Pen sp = new Pen(Color.Black);        
        FigureInfo DrawFigure;        

        
        List<FigureInfo> figureList = new List<FigureInfo>();


                        
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
                    panelControl1.Paint +=new PaintEventHandler(panelControl1_DrawPaint);
                    break;
                case MouseModifyMode.SelectMode:
                    panelControl1.MouseDown += new MouseEventHandler(panelControl1_SelectMouseDown);
                    panelControl1.MouseMove += new MouseEventHandler(panelControl1_SelectMouseMove);
                    panelControl1.MouseUp += new MouseEventHandler(panelControl1_SelectMouseUp);
                    panelControl1.Paint += new PaintEventHandler(panelControl1_SelectPaint);
                    break;
            }
        }

        List<MouseEventHandler> mouseEventList = new List<MouseEventHandler>();
        public void InitializeMouseEventHandler()
        {
            mouseEventList.Add(panelControl1_DrawMouseDown);

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
                    panelControl1.Paint -= new PaintEventHandler(panelControl1_SelectPaint);
                    break;
                case MouseModifyMode.SelectMode:
                    panelControl1.MouseDown -= new MouseEventHandler(panelControl1_DrawMouseDown);
                    panelControl1.MouseMove -= new MouseEventHandler(panelControl1_DrawMouseMove);
                    panelControl1.MouseUp -= new MouseEventHandler(panelControl1_DrawMouseUp);
                    panelControl1.Paint -= new PaintEventHandler(panelControl1_DrawPaint);
                    break;
            }
        }

        

        private void panelControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Check ID
            foreach (FigureInfo fi in figureList)
            {
                if (gm.isInside(e.Location, fi.Rectangle))
                {
                    _currentFigure = fi;
                    ChangeEventHandler(MouseModifyMode.SelectMode);
                }
            }

            panelControl1_SelectMouseDown(null, e);
        }

        private void panelControl1_DrawPaint(object sender, PaintEventArgs e)
        {
            DrawFigureList(e);

            DrawCurrentFigure(e);                        
        }

        private void panelControl1_SelectPaint(object sender, PaintEventArgs e)
        {
            DrawFigureList(e);

            DrawCurrentFigure(e);

            // Size Modify Guide Line & Rectangle
            DrawSizeRect(DrawFigure, e);
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
                    e.Graphics.DrawRectangle(gp, gm.GetGuideRect(DrawFigure.Rectangle));
                    gm.GetSizeRect(gm.GetGuideRect(DrawFigure.Rectangle));

                    DrawSizeRect(DrawFigure, e);
                }
            }
        }

        private void DrawSizeRect(FigureInfo target, PaintEventArgs e)
        {
            if (DrawFigure == null) return;
            // Draw Guide Line Rectangle
            e.Graphics.DrawRectangle(gp, gm.GetGuideRect(DrawFigure.Rectangle));
            
            // Draw Resize Rectangle
            foreach (Rectangle figureitem in gm.GetSizeRect(DrawFigure.Rectangle))
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.Gray), figureitem);
                e.Graphics.DrawRectangle(p, figureitem);
            }
        }
        
        private void DrawCurrentFigure(PaintEventArgs e)
        {
            if (DrawFigure == null) return;
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

        private bool isDrawing = false;
        private Point _startPos;
        private Point _currentPos;
        private int setType = 0;
        private bool isFocus = false;
        private bool isHold = false;
        private FigureInfo _currentFigure;
        
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

            if (isDrawing && isHold)
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
                        
            gm.SelectedFigure = DrawFigure;

            addFigure = null;
            isDrawing = true;
            isFocus = true;
            isHold = false;            

            //ChangeEventHandler(MouseModifyMode.SelectMode);
            panelControl1.Invalidate();            
        }

        Point _revisePoint = new Point();
        private void panelControl1_SelectMouseDown(object sender, MouseEventArgs e)
        {
            DrawFigure = null;
            isDrawing = false;
            // Check ID
            foreach (FigureInfo fi in figureList)
            {
                if (gm.isInside(e.Location, fi.Rectangle))
                {
                    DrawFigure = fi;
                    isFocus = !isFocus;
                    //Console.WriteLine("Current Figure ID : {0}", fi.id);
                }
            }

            if (DrawFigure == null)
            {
                isHold = false;
                isFocus = false;
                DrawFigure = new FigureInfo();
                

                gm.SelectedFigure = DrawFigure;

                ChangeEventHandler(MouseModifyMode.DrawMode);
            }
            else
            {
                isHold = true;
                //int xpos = e.Location.X - _currentFigure.Rectangle.X;
                //int ypos = e.Location.Y - _currentFigure.Rectangle.Y;
                int xpos = e.Location.X - DrawFigure.Rectangle.X;
                int ypos = e.Location.Y - DrawFigure.Rectangle.Y;

                _revisePoint.X = xpos;
                _revisePoint.Y = ypos;
            }

            _startPos = _currentPos = e.Location;            
        }

        private void panelControl1_SelectMouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = gm.getMouseCursor(e.Location);
                
            if (isHold)
            {   
                //_currentFigure.Rectangle = RevisePosition(e);

                // resize Code
                if (this.Cursor.Equals(Cursors.SizeNESW))
                {                 
                    DrawFigure.Rectangle = ResizeRectangle(e);
                }
                else if (this.Cursor.Equals(Cursors.SizeNWSE))
                {                 
                    DrawFigure.Rectangle = ResizeRectangle(e);
                }
                else if (this.Cursor.Equals(Cursors.SizeAll))
                {                    
                    DrawFigure.Rectangle = RevisePosition(e);
                }
                else
                {
                    DrawFigure.Rectangle = RevisePosition(e);
                }
                
                panelControl1.Invalidate();
            }            
        }

        /// <summary>
        /// 사이즈 변경시 _startPos와 _currentPos 지정 지점에 다라 사이즈 변경 방향이 달라짐.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private Rectangle ResizeRectangle(MouseEventArgs e)
        {
            _startPos = DrawFigure.Rectangle.Location;
            _currentPos = e.Location;

            Rectangle rect = getRectangle();
            return rect;
        }

        private Rectangle RevisePosition(MouseEventArgs e)
        {
            //Rectangle rect = _currentFigure.Rectangle;
            Rectangle rect = DrawFigure.Rectangle;

            rect.X = e.X - _revisePoint.X;
            rect.Y = e.Y - _revisePoint.Y;

            return rect;
        }

        private void panelControl1_SelectMouseUp(object sender, MouseEventArgs e)
        {
            //_currentFigure = null;
            DrawFigure = null;
            isFocus = false;
            isHold = false;
        }
    }    
}
