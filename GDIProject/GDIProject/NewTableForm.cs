using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraVerticalGrid;
using System.Drawing.Drawing2D;

namespace GDIProject
{
    public partial class NewTableForm : Form
    {
        int col_count;
        int row_count;
        int p_width;
        int p_height;
        int st_x, st_y;
        int margin = 15;
        Pen p = new Pen(Color.Black);
        Pen sp = new Pen(Color.Black);
        Pen ep = new Pen(Color.Red);
        int counter = 0;
        bool isHold = false;
        List<string> menuList = new List<string>();
        bool isResize = false;
        GDIManager gm = new GDIManager();
        public NewTableForm()
        {
            InitializeComponent();

            Point _loc = new Point();
            _loc.X = simpleButton1.Location.X;
            _loc.Y = simpleButton1.Location.Y + simpleButton1.Height + 3;

            p.Color = Color.Black;
            p.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            p.Width = 2;

            sp.Color = Color.Black;
            sp.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            sp.Width = 2;

            rcSelector1.Location = _loc;
            rcSelector1.OnCloseEvent += new RCSelector.OnSelectEventHandler(rcSelector1_OnCloseEvent);
            rcSelector1.Hide();

            menuList.Add("크림 스파게티, 1");
            menuList.Add("로제 스파게티, 1");
            menuList.Add("토마토 스파게티, 2");
        }

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
                    FigureInfo figure = new FigureInfo();
                    figure.Rectangle = new Rectangle(st_x, st_y, (int)r_width, (int)r_height);
                    figure.id = "Figure" + counter++;
                    figure.FigureType = 0;
                    figure.menuList = menuList;
                    figure.Font = new Font("Gulim", 10.0f, FontStyle.Regular);
                    gm.FigureList.Add(figure);

                    st_x += r_width + margin;                    
                }
                st_x = 0;
                st_y = st_y + r_height + margin;
            }

            if (gm.FigureList.Count > 0) gm.SelectedFigure = gm.FigureList[0];

            rcSelector1.Hide();

            panelControl1.Invalidate();
        }

        List<Rectangle> rectList = new List<Rectangle>();
        private void InitPanel()
        {
            gm.FigureList.Clear();
            rectList.Clear();
            st_x = 0;
            st_y = 0;
            panelControl1.Invalidate();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            rcSelector1.Show();

        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                //if (gm.SelectedFigure == null) return;
                
                DrawFigureList(e);

                if (isResize)
                {
                    DrawSizeRect(gm.SelectedFigure, e);
                }

                propertyGridControl1.SelectedObject = gm.SelectedFigure;
                propertyGridControl1.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
            }
        }

        private void panelControl1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (!isHold) this.Cursor = gm.getMouseCursor(e.Location);

                if (isHold)
                {
                    if (this.Cursor.Equals(Cursors.SizeAll))
                    {
                        gm.SelectedFigure.Rectangle = gm.RevisePosition(e);
                    }
                    else if (!this.Cursor.Equals(Cursors.Default))
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

        private void panelControl1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {                
                if (this.Cursor.Equals(Cursors.Default) && !CheckInside(e.Location))
                {                    
                    return;
                }
                
                gm.SelectedFigureList.Add(gm.SelectedFigure);
                
                if (this.Cursor.Equals(Cursors.Default) || this.Cursor.Equals(Cursors.SizeAll)) gm.SelectedFigure = gm.isInside(e.Location);
 
                if (gm.ShiftKeyDown)
                {
                    gm.SelectedFigureList.Add(gm.SelectedFigure);
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    gm.SelectedFigureList.Clear();
                }

                // Point Revise
                Point temp = e.Location;
                temp.Offset(-gm.SelectedFigure.Rectangle.X, -gm.SelectedFigure.Rectangle.Y);
                gm.RevisePoint = temp;
                //
                isHold = true;                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
            }
        }
                
        private void panelControl1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if(!isResize) gm.SelectedFigure = gm.isInside(e.Location);

                isResize = true;

                if (gm.SelectedFigure != null && isResize)
                {
                    panelControl1.Invalidate();
                }

                isHold = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
            }
        }

        private void DrawFigureList(PaintEventArgs e)
        {
            foreach (FigureInfo figureitem in gm.FigureList)
            {
                if (figureitem != gm.SelectedFigure)
                {
                    DrawFigure(figureitem, e);
                }                     
            }
                        
            DrawFigure(gm.SelectedFigure, e);
            
        }

        private void DrawFigure(FigureInfo figureitem, PaintEventArgs e)
        {
            if (figureitem == null) return;

            //Console.WriteLine("ID : {0}, Width : {1}, Height : {2}", figureitem.id, figureitem.Rectangle.Width, figureitem.Rectangle.Height);            
            switch (figureitem.FigureType)
            {
                case 0:                    
                    //// Draw Background
                    e.Graphics.FillRectangle(new SolidBrush(figureitem.BackgroundColor), figureitem.Rectangle);
                    //// Draw Border Line
                    e.Graphics.DrawRectangle(gm.SettingPen(figureitem.BorderColor, 1), figureitem.Rectangle);
                    break;
                case 1:
                    Matrix mt = new Matrix();
                    mt.RotateAt(45, figureitem.Rectangle.Location, MatrixOrder.Append);
                    e.Graphics.Transform = mt;
                    e.Graphics.FillEllipse(new SolidBrush(figureitem.BackgroundColor), figureitem.Rectangle);
                    e.Graphics.DrawEllipse(ep, figureitem.Rectangle);
                    mt = new Matrix();
                    e.Graphics.Transform = mt;
                    break;
                case 2:
                    e.Graphics.DrawImage(figureitem.Image, figureitem.Rectangle.Location.X, figureitem.Rectangle.Location.Y, figureitem.Rectangle.Width, figureitem.Rectangle.Height);
                    break;
                case 3: // Draw Horizontal Line
                    Point HlinePoint = new Point(figureitem.Rectangle.X + figureitem.Rectangle.Width, figureitem.Rectangle.Y);                    
                    e.Graphics.DrawLine(p, figureitem.Rectangle.Location, HlinePoint);
                    break;
                case 4: // Draw Vertical Line
                    Point VlinePoint = new Point(figureitem.Rectangle.X, figureitem.Rectangle.Y + figureitem.Rectangle.Height);
                    e.Graphics.DrawLine(p, figureitem.Rectangle.Location, VlinePoint);
                    break;
            }

            // Draw Menu List
            int addMargin = 0;
            Point stringPoint = figureitem.Rectangle.Location;
            if (figureitem.menuList != null)
            {
                foreach (string menuitem in figureitem.menuList)
                {
                    //Font font = new Font("Gulim", 10.0f, FontStyle.Regular);
                    addMargin += figureitem.Font.Height;
                    e.Graphics.DrawString(menuitem, figureitem.Font, new SolidBrush(Color.Red), stringPoint.X + 10, stringPoint.Y + addMargin);                    
                }
            }
        }

        private void DrawSizeRect(FigureInfo target, PaintEventArgs e)
        {
            if (gm.SelectedFigure == null) return;
            
            Graphics g = e.Graphics;

            if (gm.SelectedFigureList.Count > 0)
            {
                foreach (FigureInfo fitem in gm.SelectedFigureList)
                {
                    DrawSizePoint(g, fitem.Rectangle);
                }
            }
            else
            {
                DrawSizePoint(g, gm.SelectedFigure.Rectangle);
            }
        }

        private void DrawSizePoint(Graphics g, Rectangle targetRect)
        {
            foreach (Rectangle rectitem in gm.GetSizeRect(targetRect))
            {
                g.FillEllipse(new SolidBrush(Color.White), rectitem);
                g.DrawEllipse(p, rectitem);
            }
        }

        public bool CheckInside(Point point)
        {
            try
            {
                gm.SelectedFigure = gm.isInside(point);
                if (gm.SelectedFigure != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
                return false;
            }
        }
        
        private void NewTableForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {   
                if (gm.SelectedFigureList.Count > 0)
                {
                    gm.SelectedFigure = null;
                    foreach (FigureInfo sitem in gm.SelectedFigureList)
                    {
                        gm.FigureList.Remove(sitem);
                    }
                }
                else
                {
                    if (gm.SelectedFigure != null)
                    {
                        gm.FigureList.Remove(gm.SelectedFigure);
                        gm.SelectedFigure = null;
                    }
                }

                gm.SelectedFigureList.Clear();

                panelControl1.Invalidate();
            }

            if (e.KeyCode == Keys.ShiftKey)
            {
                gm.ShiftKeyDown = true;
            }
        }

        private void NewTableForm_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        private void 도형변경ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void 사각형ToolStripMenuItem_Click(object sender, EventArgs e)
        {                        
            SetFigureType(0, null);            
        }

        private void 원형ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetFigureType(1, null);
        }
        
        private void 이미지ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "jpg";
            ofd.Filter = "Images Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png";
            ofd.Multiselect = false;
            ofd.ShowDialog();
            if (ofd.FileName.Length > 0)
            {                
                SetFigureType(2, Image.FromFile(ofd.FileName));             
            }
        }

        private void SetFigureType(int figureType, Image image)
        {
            if (gm.SelectedFigure == null) return;
            
            foreach (FigureInfo fitem in gm.FigureList)
            {
                if (fitem.id.Equals(gm.SelectedFigure.id))
                {
                    fitem.FigureType = figureType;
                    gm.SelectedFigure.FigureType = figureType;
                    if (image != null) fitem.Image = image;
                }
            }

            panelControl1.Invalidate();
        }

        private void SetFigureBackColor(Color color)
        {
            if (gm.SelectedFigure == null) return;

            foreach (FigureInfo fitem in gm.FigureList)
            {
                if (fitem.id.Equals(gm.SelectedFigure.id))
                {
                    fitem.BackgroundColor = color;                    
                }
            }

            foreach (FigureInfo fitem in gm.SelectedFigureList)
            {
                fitem.BackgroundColor = color;                
            }

            panelControl1.Invalidate();
        }

        private void 삭제ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gm.SelectedFigure != null)
            {
                gm.FigureList.Remove(gm.SelectedFigure);
                gm.SelectedFigure = null;
            }
            panelControl1.Invalidate();
        }

        private void 가로선ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetFigureType(3, null);
        }

        private void 세로선ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetFigureType(4, null);
        }

        ColorDialog colorDialog = new ColorDialog();
        private void 배경색상ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog.ShowDialog();
            
            if(result == DialogResult.OK)
            {
                SetFigureBackColor(colorDialog.Color);
            }
        }

        private void NewTableForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                gm.ShiftKeyDown = false;
            }            
        }


    }
}
