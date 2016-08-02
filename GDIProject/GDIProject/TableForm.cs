using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GDIProject
{
    public partial class TableForm : DevExpress.XtraEditors.XtraForm
    {
        public TableForm()
        {
            InitializeComponent();
            
            //this.DoubleBuffered = true;

            Point _loc = new Point();
            _loc.X = simpleButton1.Location.X;
            _loc.Y = simpleButton1.Location.Y + simpleButton1.Height + 3;

            p.Color = Color.Black;
            p.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            p.Width = 2;

            gp.Width = 2;
            gp.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;       

            rcSelector1.Location = _loc;
            rcSelector1.OnCloseEvent += new RCSelector.OnSelectEventHandler(rcSelector1_OnCloseEvent);
            rcSelector1.Hide();

            menuList.Add("크림 스파게티, 1");
            menuList.Add("로제 스파게티, 1");
            menuList.Add("토마토 스파게티, 2");
        }

        List<string> menuList = new List<string>();
        void rcSelector1_OnCloseEvent(int row, int column)
        {
            this.row_count = row;
            this.col_count = column;

            simpleButton1.Text = String.Format("{0} X {1}", row, column);

            InitPanel();

            counter = 0;
            if (row_count <= 0 && col_count <= 0) return;
            p_width = panelControl1.Width - margin * col_count;
            p_height = panelControl1.Height - margin * row_count;
            // 개별 사각형 폭/높이
            int r_width = p_width / col_count;
            int r_height = p_height / row_count;

            for (int i = 0; i < row_count; i++)
            {
                for (int j = 0; j < col_count; j++)
                {
                    //rectList.Add(new Rectangle(st_x, st_y, (int) r_width, (int) r_height));
                    FigureInfo figure = new FigureInfo();
                    figure.Rectangle = new Rectangle(st_x, st_y, (int)r_width, (int)r_height);
                    figure.id = "Figure" + counter++;
                    figure.FigureType = 0;
                    figure.menuList = menuList;
                    figureList.Add(figure);

                    st_x += r_width + margin;
                }
                st_x = 0;
                st_y = st_y + r_height + margin;
            }

            rcSelector1.Hide();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            rcSelector1.Show();
        }

        List<Rectangle> rectList = new List<Rectangle>();
        private void InitPanel()
        {
            figureList.Clear();
            rectList.Clear();
            st_x = 0;
            st_y = 0;
            panelControl1.Invalidate();
        }

        int col_count;
        int row_count;
        int p_width;
        int p_height;
        int st_x, st_y;
        int margin = 15;
        Pen p = new Pen(Color.Black);
        List<FigureInfo> figureList = new List<FigureInfo>();
        int counter = 0;
        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
            try
            {                  
                DrawFigureList(e);

                if (isResize)
                {
                    DrawSizeRect(SelectedFigure(), e);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
            }            
        }

        private void DrawFigureList(PaintEventArgs e)
        {            
            foreach (FigureInfo figureitem in figureList)
            {
                if (figureitem != gm.SelectedFigure)
                {
                    DrawFigure(figureitem, e);
                }
                //if (figureitem.menuList != null)
                //{
                //    Point stringPoint = figureitem.Rectangle.Location;
                //    int addMargin = 15;
                //    Font font = new Font("굴림", 10.0f, FontStyle.Regular);
                //    StringFormat stringFormat = new StringFormat(StringFormatFlags.DirectionVertical | StringFormatFlags.DirectionRightToLeft);

                //    foreach (string menu in figureitem.menuList)
                //    {
                //        addMargin += 15;                        
                //        e.Graphics.DrawString(menu, font, new SolidBrush(Color.Red), stringPoint.X + 10, stringPoint.Y + addMargin);
                //    }
                //    addMargin += 15;
                //    e.Graphics.DrawString(figureitem.id, font, new SolidBrush(Color.Red), stringPoint.X + 10, stringPoint.Y + addMargin);
                //}                
            }

            DrawFigure(gm.SelectedFigure, e);

        }

        private void DrawFigure(FigureInfo figureitem, PaintEventArgs e)
        {
            if (figureitem == null) return;

            Console.WriteLine("ID : {0}, Width : {1}, Height : {2}", figureitem.id, figureitem.Rectangle.Width, figureitem.Rectangle.Height);

            switch (figureitem.FigureType)
            {
                case 0:                    
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), figureitem.Rectangle);                    
                    e.Graphics.DrawRectangle(p, figureitem.Rectangle);
                    break;
                case 1:
                    e.Graphics.FillEllipse(new SolidBrush(Color.White), figureitem.Rectangle);
                    e.Graphics.DrawEllipse(p, figureitem.Rectangle);
                    break;
                case 2:
                    e.Graphics.DrawImage(figureitem.Image, figureitem.Rectangle.Location.X, figureitem.Rectangle.Location.Y, figureitem.Rectangle.Width, figureitem.Rectangle.Height);
                    break;
            }
        }

        public static void DrawRoundedRectangle(Graphics g,
        Rectangle r, int d, Pen p)
        {

            System.Drawing.Drawing2D.GraphicsPath gp =
                    new System.Drawing.Drawing2D.GraphicsPath();

            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            gp.AddLine(r.X, r.Y + r.Height - d, r.X, r.Y + d / 2);

            g.DrawPath(p, gp);
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            p.Color = colorEdit1.Color;
            panelControl1.Invalidate();
        }

        GDIManager gm = new GDIManager();        
        private void 삭제ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isEditable = true;
            figureList.Remove(SelectedFigure());

            panelControl1.Invalidate();
            isEditable = false;
        }

        Pen gp = new Pen(Color.Blue);
        private void DrawSizeRect(FigureInfo target, PaintEventArgs e)
        {
            if (SelectedFigure() == null) return;
            //// Draw Guide Line Rectangle
            //e.Graphics.DrawRectangle(gp, gm.GetGuideRect(SelectedFigure().Rectangle));

            //// Draw Resize Rectangle
            //foreach (Rectangle figureitem in gm.GetSizeRect(SelectedFigure().Rectangle))
            //{
            //    e.Graphics.FillRectangle(new SolidBrush(Color.Gray), figureitem);
            //    e.Graphics.DrawRectangle(p, figureitem);
            //}

            Graphics g = e.Graphics;

            g.FillRectangle(new SolidBrush(Color.White), gm.SelectedFigure.Rectangle);
            g.DrawRectangle(p, gm.SelectedFigure.Rectangle);
            
            foreach (Rectangle rectitem in gm.GetSizeRect(gm.SelectedFigure.Rectangle))
            {
                g.FillEllipse(new SolidBrush(Color.White), rectitem);
                g.DrawEllipse(p, rectitem);
            }
        }

        Point mouseLoc = new Point();
        bool isHold = false;
        
        private void panelControl1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                mouseLoc = e.Location;
               
                if (!isHold) this.Cursor = gm.getMouseCursor(e.Location);

                isEditable = true;
                if (isHold && gm.SelectedFigure.isEditable)
                {
                    //if (this.Cursor.Equals(Cursors.SizeNESW))
                    //{
                    //    isResize = true;
                    //    //SelectedFigure().Rectangle = ResizeRectangle(e);
                    //    gm.SelectedFigure.Rectangle = ResizeRectangle(e);
                    //}
                    //else if (this.Cursor.Equals(Cursors.SizeNWSE))
                    //{
                    //    isResize = true;
                    //    //SelectedFigure().Rectangle = ResizeRectangle(e);
                    //    gm.SelectedFigure.Rectangle = ResizeRectangle(e);
                    //}
                    //else if (this.Cursor.Equals(Cursors.SizeAll))
                    //{
                    //    isResize = false;
                    //    //SelectedFigure().Rectangle = RevisePosition(e);
                    //    gm.SelectedFigure.Rectangle = RevisePosition(e);
                    //}
                    //else
                    //{
                    //    //SelectedFigure().Rectangle = RevisePosition(e);
                    //    gm.SelectedFigure.Rectangle = RevisePosition(e);
                    //}
                    if (this.Cursor.Equals(Cursors.SizeAll))
                    {
                        isResize = false;
                        gm.SelectedFigure.Rectangle = gm.RevisePosition(e);
                    }
                    else if (!this.Cursor.Equals(Cursors.Default))
                    {
                        isResize = true;
                        gm.SelectedFigure.Rectangle = gm.ResizeRectangle(e);
                    }
                    else
                    {                     
                        gm.SelectedFigure.Rectangle = RevisePosition(e);
                    }

                    panelControl1.Invalidate();
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
            }            
        }

        Point _revisePoint = new Point();
        
        private Rectangle RevisePosition(MouseEventArgs e)
        {            
            Rectangle rect = gm.SelectedFigure.Rectangle;

            rect.X = e.X - _revisePoint.X;
            rect.Y = e.Y - _revisePoint.Y;

            return rect;
        }

        Point _startPos;
        Point _currentPos;

        /// <summary>
        /// 사이즈 변경시 _startPos와 _currentPos 지정 지점에 다라 사이즈 변경 방향이 달라짐.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private Rectangle ResizeRectangle(MouseEventArgs e)
        {
            _startPos = gm.SelectedFigure.Rectangle.Location;
            _currentPos = e.Location;

            //Rectangle rect = getRectangle();
            gm.SelectedFigure.Rectangle = getRectangle();
            //return rect;
            return gm.SelectedFigure.Rectangle;
        }

        private Rectangle getRectangle()
        {
            return new Rectangle(
                Math.Min(_startPos.X, _currentPos.X),
                Math.Min(_startPos.Y, _currentPos.Y),
                Math.Abs(_startPos.X - _currentPos.X),
                Math.Abs(_startPos.Y - _currentPos.Y));
        }

        private FigureInfo SelectedFigure()
        {
            FigureInfo selectFigure = new FigureInfo();
            
            foreach (FigureInfo fitem in figureList)
            {
                if (gm.isInside(mouseLoc, fitem.Rectangle))
                {
                    if(isEditable) selectFigure = fitem;
                }
            }

            Console.WriteLine(selectFigure.id);
            return selectFigure;
        }

        private void 사각형ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedFigure().FigureType = 0;
            panelControl1.Invalidate();
        }

        private void 원형ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedFigure().FigureType = 1;
            panelControl1.Invalidate();
        }

        private void 이미지그림ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "jpg";
            ofd.Filter = "Images Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png";
            ofd.Multiselect = false;
            ofd.ShowDialog();
            if (ofd.FileName.Length > 0)
            {
                SelectedFigure().FigureType = 2;
                SelectedFigure().Image = Image.FromFile(ofd.FileName);
                panelControl1.Invalidate();
            }
            
        }

        bool isResize = false;
        private void 사이즈변경ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isResize = true;

            isEditable = true;
            gm.SelectedFigure = SelectedFigure();
            gm.SelectedFigure.isEditable = true;            
            panelControl1.Invalidate();
            isEditable = false;
        }

        private void panelControl1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (!isHold)
                {
                    Console.WriteLine("Mouse Down");
                    isHold = true;
                    isEditable = true;
                    int xpos = e.Location.X - SelectedFigure().Rectangle.X;
                    int ypos = e.Location.Y - SelectedFigure().Rectangle.Y;

                    _revisePoint.X = xpos;
                    _revisePoint.Y = ypos;

                    gm.SelectedFigure = SelectedFigure();                    
                    gm.SelectedFigure.isEditable = true;
                    isEditable = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
            }
        }

        bool isEditable = false;
        private void panelControl1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                isHold = false;                
                gm.SelectedFigure.isEditable = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
            }
        }


    }
}