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
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm1()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        Point startPos;      // mouse-down position
        Point currentPos;    // current mouse position
        bool drawing;        // busy drawing
        List<Rectangle> rectangles = new List<Rectangle>();  // previous rectangles

        private Rectangle getRectangle()
        {
            return new Rectangle(
                Math.Min(startPos.X, currentPos.X),
                Math.Min(startPos.Y, currentPos.Y),
                Math.Abs(startPos.X - currentPos.X),
                Math.Abs(startPos.Y - currentPos.Y));
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            currentPos = startPos = e.Location;
            drawing = true;         
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                _curRect.Location = currentPos;
                currentPos = e.Location;

                panel1.Invalidate();
            }
            
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                drawing = false;
                var rc = getRectangle();                
                
                if(_curRect != null){
                    rc = _curRect;
                } else
                {
                    rc = getRectangle();
                }
                if (rc.Width > 0 && rc.Height > 0) rectangles.Add(rc);
                this.Invalidate();
            }
        }

        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen myDrawingPen = new Pen(Color.BlueViolet);
            myDrawingPen.Width = 3;
            myDrawingPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            Pen myCPen = new Pen(Color.Black);

            if (rectangles.Count > 0)
            {
                e.Graphics.DrawRectangles(myCPen, rectangles.ToArray());
                //e.Graphics.FillRectangle(new SolidBrush(Color.Black), rectangles[0]);
            }
            if (_curRect == null && drawing) e.Graphics.DrawRectangle(myDrawingPen, getRectangle());
            if (_curRect != null && drawing) e.Graphics.DrawRectangle(myDrawingPen, _curRect);

            
            this.Invalidate();
        }

        Rectangle _curRect;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _curRect = rectangles[0];
        }
    }

 
    public class DrawPoint
    {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }


}