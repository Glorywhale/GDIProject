using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GDIProject
{
    public partial class RCSelector : UserControl
    {
        public RCSelector()
        {
            InitializeComponent();
            panelControl1.Width = this.Width;
            panelControl1.Height = this.Height;

            InitTable();
        }

        private void panelControl1_MouseMove(object sender, MouseEventArgs e)
        {
            _mouse = e.Location;
            panelControl1.Invalidate();
        }

        private void panelControl1_MouseLeave(object sender, EventArgs e)
        {

        }

        int st_x = 0, st_y = 20;
        int margin = 5;
        List<Rectangle> _rectList = new List<Rectangle>();
        int row_count = 8;
        int col_count = 8;
        int width = 20;
        int height = 20;
        private void InitTable()
        {
            _rectList.Clear();
            Rectangle rect = new Rectangle(st_x, st_y, width, height);
            
            for (int i = 0; i < row_count; i++)
            {
                for (int j = 0; j < col_count; j++)                
                {                    
                    rect = new Rectangle(st_x, st_y, width, height);
                    _rectList.Add(rect);

                    st_x = st_x + width + margin;
                }
                st_x = 0;
                st_y = st_y + height + margin;
            }
        }

        Pen p = new Pen(Color.Black);
        Point _mouse = new Point();
        int last_row, last_col;
        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
            for(int i = 0; i < _rectList.Count; i++)
            {
                if (_rectList[i].X < _mouse.X && _rectList[i].Y < _mouse.Y)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.SkyBlue), _rectList[i]);

                    last_row = i/8 + 1;
                    last_col = i%8 + 1;
                    
                }
                e.Graphics.DrawRectangle(p, _rectList[i]);                
            }

            label1.Text = last_row.ToString() + " X " + last_col.ToString();
        }

        public delegate void OnSelectEventHandler(int row, int column);
        public event OnSelectEventHandler OnCloseEvent;
        private void panelControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (last_row > 1 && last_col > 1)
            {
                if (OnCloseEvent != null) OnCloseEvent(last_row, last_col);
                this.Hide();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
