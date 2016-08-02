using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GDIProject
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            gp.Width = 2;
            gp.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
        }

        Pen p = new Pen(Color.Black);
        Pen gp = new Pen(Color.Blue);
        Pen sp = new Pen(Color.Black);
        Rectangle guide_rect;
        Rectangle rect;

        
        List<Rectangle> _srList = new List<Rectangle>();
        List<Rectangle> rectList = new List<Rectangle>();

        private void simpleButton1_Click(object sender, EventArgs e)
        {            
            rect = new Rectangle(50, 50, 100, 200);
            figuretype = 1;
            //GetSizeRect(guide_rect);
            panelControl1.Invalidate();
        }

        public Rectangle GetGuidRect(Rectangle rect)
        {
            guide_rect = new Rectangle(rect.X - 6, rect.Y - 6, rect.Width + 14, rect.Height + 14);         

            return guide_rect;
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Rectangle rectitem in rectList)
            {
                //e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), rectitem);
                //e.Graphics.DrawRectangle(p, rectitem);
                switch (figuretype)
                {
                    case 1:
                        e.Graphics.FillEllipse(new SolidBrush(Color.White), rectitem);
                        e.Graphics.DrawEllipse(p, rect);
                        break;
                    case 2:
                        e.Graphics.FillRectangle(new SolidBrush(Color.White), rectitem);
                        e.Graphics.DrawRectangle(p, rect);
                        break;
                }
            }           

            //e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), rect);
            //e.Graphics.DrawRectangle(p, rect);

            switch (figuretype)
            {
                case 1:
                    e.Graphics.FillEllipse(new SolidBrush(Color.White), rect);
                    e.Graphics.DrawEllipse(p, rect);
                    break;
                case 2:
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), rect);
                    e.Graphics.DrawRectangle(p, rect);
                    break;
            }

            //System.Drawing.Image image = new Bitmap(@"C:\Users\zin\Desktop\EmployeeSample\p4.jpg");
            
            //e.Graphics.DrawImage(image, rect);
            

            if (!isDrawing)
            {
                if (rect.Width > 0 && rect.Height > 0)
                {
                    e.Graphics.DrawRectangle(gp, GetGuidRect(rect));
                    GetSizeRect(GetGuidRect(rect));

                    foreach (Rectangle rectitem in _srList)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.White), rectitem);
                        e.Graphics.DrawRectangle(p, rectitem);
                    }
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

        private int figuretype = 1;
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //// Hide Guide Line
            //rect = new Rectangle(-5,-5,-5,-5);
            //rectList.Clear();
            //panelControl1.Invalidate();

            figuretype = 2;
        }

        bool isDrawing = false;
        Point _startPos;
        Point _currentPos;
        private void panelControl1_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            _startPos = _currentPos = e.Location;            
        }

        private Rectangle getRectangle()
        {
            return new Rectangle(
                Math.Min(_startPos.X, _currentPos.X),
                Math.Min(_startPos.Y, _currentPos.Y),
                Math.Abs(_startPos.X - _currentPos.X),
                Math.Abs(_startPos.Y - _currentPos.Y));
        }
                
        private void panelControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                rect.Location = _currentPos;
                _currentPos = e.Location;                
                rect = getRectangle();
                panelControl1.Invalidate();
            }
            else
            {
                if (_srList.Count == 0) return;

                if (isInside(e.Location, _srList[0]) || isInside(e.Location, _srList[3]))
                {
                    this.Cursor = Cursors.SizeNWSE;
                }else if (isInside(e.Location, _srList[1]) || isInside(e.Location, _srList[2]))
                {
                    this.Cursor = Cursors.SizeNESW;
                } else if (isInside(e.Location, rect))
                {
                    Console.WriteLine("Rect ID : {0}", rect.X);
                    this.Cursor = Cursors.SizeAll;                
                }
                else
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        public bool isInside(Point mouse, Rectangle rect)
        {
            bool isInside = false;

            //foreach (Rectangle rect in _size_rectList)
            //{
            //    Console.WriteLine(string.Format("{0}. {1}, {2}, {3}, {4}, {5}", m.X, m.Y, rect.X, rect.Y, rect.Width, rect.Height));
            //    if (m.X > rect.X && m.X < rect.X + rect.Width && m.Y > rect.Y && m.Y < rect.Y + rect.Width)
            //    {                    
            //        isInside = true;
            //    }
            //}

            if (mouse.X > rect.X && mouse.X < rect.X + rect.Width && mouse.Y > rect.Y && mouse.Y < rect.Y + rect.Width)
            {
                isInside = true;
            }


            return isInside;
        }

        private void panelControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                _currentPos = e.Location;
                rect = getRectangle();
                rectList.Add(rect);
                panelControl1.Invalidate();
                isDrawing = false;
            }
        }

                
    }
}
