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
    public partial class SizeForm : Form
    {
        public SizeForm()
        {
            InitializeComponent();

            gp.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            gp.Width = 3;
            panelControl1.Invalidate();

            FigureInfo fi = new FigureInfo();
            fi.Rectangle = rect;
            fi.FigureType = 0;
            fi.id = "Rect1";
            fi.isEditable = true;

            gm.SelectedFigure = fi;
        }

        Pen p = new Pen(Color.Blue);
        Pen gp = new Pen(Color.Red);
        Pen sp = new Pen(Color.Black);
        GDIManager gm = new GDIManager();
        Rectangle rect = new Rectangle(50, 50, 200, 200);
        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.FillRectangle(new SolidBrush(Color.White), gm.SelectedFigure.Rectangle);
            g.DrawRectangle(p, gm.SelectedFigure.Rectangle);

            //g.DrawRectangle(gp, gm.GetGuideRect(gm.SelectedFigure.Rectangle));

            foreach (Rectangle rectitem in gm.GetSizeRect(gm.SelectedFigure.Rectangle))
            {
                g.FillEllipse(new SolidBrush(Color.White), rectitem);
                g.DrawEllipse(sp, rectitem);
            }
        }

        private void panelControl1_MouseDown(object sender, MouseEventArgs e)
        {
            Point temp = e.Location;
            temp.Offset(-gm.SelectedFigure.Rectangle.X, -gm.SelectedFigure.Rectangle.Y);
            gm.RevisePoint = temp;                        
            isHold = true;
        }

        bool isHold = false;
        private void panelControl1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if(!isHold) this.Cursor = gm.getMouseCursor(e.Location);
                                
                if (isHold)
                {
                    if (this.Cursor.Equals(Cursors.SizeAll))
                    {
                        gm.SelectedFigure.Rectangle = gm.RevisePosition(e);
                    }
                    else if(!this.Cursor.Equals(Cursors.Default))
                    {
                        gm.SelectedFigure.Rectangle = gm.ResizeRectangle(e);
                    }
                    
                    panelControl1.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
            }
        }

       
        private void panelControl1_MouseUp(object sender, MouseEventArgs e)
        {
            isHold = false;
        }

        

    }
}
