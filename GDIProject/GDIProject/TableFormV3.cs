//-----------------------------------------------------------------------
// <copyright file="TableFormV3.cs" company="ZinCorp.">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace GDIProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using DevExpress.XtraVerticalGrid;

    /// <summary>
    /// Table Form Class Version 3
    /// </summary>
    public class TableFormV3 : Form
    {
        /// <summary>
        /// figure start Point
        /// </summary>
        private int startX, startY;
        
        /// <summary>
        /// panel margin
        /// </summary>
        private int margin = 15;
        
        /// <summary>
        /// figure Count 
        /// </summary>
        private int counter = 0;
        
        /// <summary>
        /// Whether Hold Mouse
        /// </summary>
        private bool isHold = false;
        
        /// <summary>
        /// Menu List
        /// </summary>
        private List<string> menuList = new List<string>();
        
        /// <summary>
        /// Whether Resize
        /// </summary>
        private bool isResize = false;
        
        /// <summary>
        /// GDI Manager Instance
        /// </summary>
        private GDIManager gm = new GDIManager();

        /// <summary>
        /// Color Dialog
        /// </summary>
        private ColorDialog colorDialog = new ColorDialog();

        /// <summary>
        /// Initializes a new instance of the TableFormV3 class.
        /// </summary>
        public TableFormV3()
        {
            this.InitializeComponent();
            
            Point location = new Point();
            location.X = simpleButton1.Location.X;
            location.Y = simpleButton1.Location.Y + simpleButton1.Height + 3;

            // Rect Size Select
            rcSelector1.Location = location;
            rcSelector1.OnCloseEvent += new RCSelector.OnSelectEventHandler(this.RcSelector1_OnCloseEvent);
            rcSelector1.Hide();

            this.menuList.Add("크림 스파게티, 1");
            this.menuList.Add("로제 스파게티, 1");
            this.menuList.Add("토마토 스파게티, 2");
        }

        /// <summary>
        /// Rectangle Selector Close Event Handle
        /// </summary>
        /// <param name="row">row count</param>
        /// <param name="column">column count</param>
        private void RcSelector1_OnCloseEvent(int row, int column)
        {
            int row_count = row;
            int col_count = column;

            simpleButton1.Text = string.Format("{0} X {1}", row, column);

            this.InitPanel();

            this.counter = 0;

            int p_width = panelControl1.Width - (this.margin * col_count);
            int p_height = panelControl1.Height - (this.margin * row_count);

            // 개별 사각형 폭/높이
            int r_width = p_width / col_count;
            int r_height = p_height / row_count;

            for (int i = 0; i < row_count; i++)
            {
                for (int j = 0; j < col_count; j++)
                {                    
                    FigureInfo figure = new FigureInfo();
                    figure.Rectangle = new Rectangle(this.startX, this.startY, (int)r_width, (int)r_height);
                    figure.id = "Figure" + this.counter++;
                    figure.FigureType = 0;
                    figure.menuList = this.menuList;
                    figure.Font = new Font("Gulim", 10.0f, FontStyle.Regular);
                    this.gm.FigureList.Add(figure);

                    this.startX += r_width + this.margin;                    
                }

                this.startX = 0;
                this.startY = this.startY + r_height + this.margin;
            }

            if (this.gm.FigureList.Count > 0)
            {
                this.gm.SelectedFigure = this.gm.FigureList[0];
            }

            rcSelector1.Hide();

            panelControl1.Invalidate();
        }
                
        /// <summary>
        /// initialize Panel
        /// </summary>
        private void InitPanel()
        {
            this.gm.FigureList.Clear();            
            this.startX = 0;
            this.startY = 0;
            panelControl1.Invalidate();
        }

        /// <summary>
        /// Button Click Event
        /// </summary>
        /// <param name="sender">button sender</param>
        /// <param name="e">event arguments</param>
        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            rcSelector1.Show();
        }

        /// <summary>
        /// Panel OnPaint Event Handle
        /// </summary>
        /// <param name="sender">panel sender</param>
        /// <param name="e">paint event arguments</param>
        private void PanelControl1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                this.DrawFigureList(e);

                if (this.isResize && this.gm.SelectedFigure != null)
                {
                    this.DrawSizeRect(this.gm.SelectedFigure, e);
                }

                this.propertyGridControl1.SelectedObject = this.gm.SelectedFigure;
                this.propertyGridControl1.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
            }
        }

        /// <summary>
        /// Mouse move event handle on panel
        /// </summary>
        /// <param name="sender">mouse sender</param>
        /// <param name="e">mouse event arguments</param>
        private void PanelControl1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (!this.isHold)
                {
                    this.Cursor = this.gm.getMouseCursor(e.Location);
                }

                if (this.isHold)
                {
                    if (this.Cursor.Equals(Cursors.SizeAll))
                    {
                        this.gm.SelectedFigure.Rectangle = this.gm.RevisePosition(e);
                    }
                    else if (!this.Cursor.Equals(Cursors.Default))
                    {
                        this.gm.SelectedFigure.Rectangle = this.gm.ResizeRectangle(e);
                    }

                    panelControl1.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
            }
        }

        /// <summary>
        /// Mouse down event handle on panel
        /// </summary>
        /// <param name="sender">mouse sender</param>
        /// <param name="e">mouse event arguments</param>
        private void PanelControl1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.Cursor.Equals(Cursors.Default) && !this.CheckInside(e.Location))
                {                    
                    return;
                }
                
                this.gm.SelectedFigureList.Add(this.gm.SelectedFigure);

                if (this.Cursor.Equals(Cursors.Default) || this.Cursor.Equals(Cursors.SizeAll))
                {
                    this.gm.SelectedFigure = this.gm.isInside(e.Location);
                }

                if (this.gm.ShiftKeyDown)
                {
                    this.gm.SelectedFigureList.Add(this.gm.SelectedFigure);
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    this.gm.SelectedFigureList.Clear();
                }

                // Point Revise
                Point temp = e.Location;
                temp.Offset(-this.gm.SelectedFigure.Rectangle.X, -this.gm.SelectedFigure.Rectangle.Y);
                this.gm.RevisePoint = temp;

                this.isHold = true;                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
            }
        }
                
        /// <summary>
        /// mouse up event handle on panel
        /// </summary>
        /// <param name="sender">mouse sender</param>
        /// <param name="e">mouse event arguments</param>
        private void PanelControl1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (!this.isResize)
                {
                    this.gm.SelectedFigure = this.gm.isInside(e.Location);
                }

                this.isResize = true;

                if (this.gm.SelectedFigure != null && this.isResize)
                {
                    panelControl1.Invalidate();
                }

                this.isHold = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
            }
        }

        /// <summary>
        /// Draw Figure based on list
        /// </summary>
        /// <param name="e">paint event arguments</param>
        private void DrawFigureList(PaintEventArgs e)
        {
            foreach (FigureInfo figureitem in this.gm.FigureList)
            {
                if (figureitem != this.gm.SelectedFigure)
                {
                    this.DrawFigure(figureitem, e);
                }                     
            }

            this.DrawFigure(this.gm.SelectedFigure, e);            
        }

        /// <summary>
        /// Draw figure
        /// </summary>
        /// <param name="figureitem">figure info</param>
        /// <param name="e">paint event arguments</param>
        private void DrawFigure(FigureInfo figureitem, PaintEventArgs e)
        {
            if (figureitem == null)
            {
                return;
            }
                        
            switch (figureitem.FigureType)
            {
                case 0:                    
                    //// Draw Background
                    e.Graphics.FillRectangle(new SolidBrush(figureitem.BackgroundColor), figureitem.Rectangle);
                    //// Draw Border Line
                    e.Graphics.DrawRectangle(this.gm.SettingPen(figureitem.BorderColor, 1), figureitem.Rectangle);
                    break;
                case 1: //// Draw Circle     
                    e.Graphics.FillEllipse(new SolidBrush(figureitem.BackgroundColor), figureitem.Rectangle);
                    e.Graphics.DrawEllipse(this.gm.NormalPen, figureitem.Rectangle);                    
                    break;
                case 2: //// Draw Image
                    e.Graphics.DrawImage(figureitem.Image, figureitem.Rectangle.Location.X, figureitem.Rectangle.Location.Y, figureitem.Rectangle.Width, figureitem.Rectangle.Height);
                    break;
                case 3: //// Draw Horizontal Line
                    Point hlinePoint = new Point(figureitem.Rectangle.X + figureitem.Rectangle.Width, figureitem.Rectangle.Y);
                    e.Graphics.DrawLine(this.gm.NormalPen, figureitem.Rectangle.Location, hlinePoint);
                    break;
                case 4: //// Draw Vertical Line
                    Point vlinePoint = new Point(figureitem.Rectangle.X, figureitem.Rectangle.Y + figureitem.Rectangle.Height);
                    e.Graphics.DrawLine(this.gm.NormalPen, figureitem.Rectangle.Location, vlinePoint);
                    break;
            }

            // Draw Menu List
            int addMargin = 0;
            Point stringPoint = figureitem.Rectangle.Location;
            if (figureitem.menuList != null)
            {
                foreach (string menuitem in figureitem.menuList)
                {                    
                    addMargin += figureitem.Font.Height;
                    e.Graphics.DrawString(menuitem, figureitem.Font, new SolidBrush(Color.Red), stringPoint.X + 10, stringPoint.Y + addMargin);                    
                }
            }
        }

        /// <summary>
        /// Draw size rectangle
        /// </summary>
        /// <param name="target">figure info</param>
        /// <param name="e">paint event arguments</param>
        private void DrawSizeRect(FigureInfo target, PaintEventArgs e)
        {   
            Graphics g = e.Graphics;

            if (this.gm.SelectedFigureList.Count > 0)
            {
                foreach (FigureInfo fitem in this.gm.SelectedFigureList)
                {
                    this.DrawSizePoint(g, fitem.Rectangle);
                }
            }
            else
            {
                this.DrawSizePoint(g, this.gm.SelectedFigure.Rectangle);
            }
        }

        /// <summary>
        /// Draw size point of each rectangle
        /// </summary>
        /// <param name="g">Graphics instance</param>
        /// <param name="targetRect">target rectangle</param>
        private void DrawSizePoint(Graphics g, Rectangle targetRect)
        {
            foreach (Rectangle rectitem in this.gm.GetSizeRect(targetRect))
            {
                g.FillEllipse(new SolidBrush(Color.White), rectitem);
                g.DrawEllipse(this.gm.NormalPen, rectitem);
            }
        }

        /// <summary>
        /// Check pointer inside of figure
        /// </summary>
        /// <param name="point">point of mouse</param>
        /// <returns>return boolean value</returns>
        private bool CheckInside(Point point)
        {
            try
            {
                this.gm.SelectedFigure = this.gm.isInside(point);

                if (this.gm.SelectedFigure != null)
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
        
        /// <summary>
        /// Check Delete key and shift key state
        /// </summary>
        /// <param name="sender">mouse sender</param>
        /// <param name="e">key event argument</param>
        private void NewTableForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {   
                if (this.gm.SelectedFigureList.Count > 0)
                {
                    this.gm.SelectedFigure = null;
                    foreach (FigureInfo sitem in this.gm.SelectedFigureList)
                    {
                        this.gm.FigureList.Remove(sitem);
                    }
                }
                else
                {
                    if (this.gm.SelectedFigure != null)
                    {
                        this.gm.FigureList.Remove(this.gm.SelectedFigure);
                        this.gm.SelectedFigure = null;
                    }
                }

                this.gm.SelectedFigureList.Clear();

                this.panelControl1.Invalidate();
            }

            if (e.KeyCode == Keys.ShiftKey)
            {
                this.gm.ShiftKeyDown = true;
            }
        }

        /// <summary>
        /// Form Load Event Handle
        /// </summary>
        /// <param name="sender">form sender</param>
        /// <param name="e">event arguments</param>
        private void NewTableForm_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }
                
        /// <summary>
        /// Context Menu - Rectangle Select Handle
        /// </summary>
        /// <param name="sender">context menu sender</param>
        /// <param name="e">event arguments</param>
        private void 사각형ToolStripMenuItem_Click(object sender, EventArgs e)
        {                        
            this.SetFigureType(0, null);            
        }

        /// <summary>
        /// Context Menu - Circle Select Handle
        /// </summary>
        /// <param name="sender">context menu sender</param>
        /// <param name="e">event arguments</param>
        private void 원형ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SetFigureType(1, null);
        }
        
        /// <summary>
        /// Context Menu - Image Select Handle
        /// </summary>
        /// <param name="sender">context menu sender</param>
        /// <param name="e">event arguments</param>
        private void 이미지ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "jpg";
            ofd.Filter = "Images Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png";
            ofd.Multiselect = false;
            ofd.ShowDialog();

            if (ofd.FileName.Length > 0)
            {                
                this.SetFigureType(2, Image.FromFile(ofd.FileName));             
            }
        }

        /// <summary>
        /// Change Figure Type
        /// </summary>
        /// <param name="figureType">parameter Figure type number</param>
        /// <param name="image">setting image</param>
        private void SetFigureType(int figureType, Image image)
        {
            if (this.gm.SelectedFigure == null)
            {
                return;
            }

            foreach (FigureInfo fitem in this.gm.FigureList)
            {
                if (fitem.id.Equals(this.gm.SelectedFigure.id))
                {
                    fitem.FigureType = figureType;
                    this.gm.SelectedFigure.FigureType = figureType;
                    
                    if (image != null)
                    {
                        fitem.Image = image;
                    }
                }
            }

            this.panelControl1.Invalidate();
        }

        /// <summary>
        /// Change Figure Background Color
        /// </summary>
        /// <param name="color">setting color</param>
        private void SetFigureBackColor(Color color)
        {
            if (this.gm.SelectedFigure == null)
            {
                return;
            }

            foreach (FigureInfo fitem in this.gm.FigureList)
            {
                if (fitem.id.Equals(this.gm.SelectedFigure.id))
                {
                    fitem.BackgroundColor = color;                    
                }
            }

            foreach (FigureInfo fitem in this.gm.SelectedFigureList)
            {
                fitem.BackgroundColor = color;                
            }

            panelControl1.Invalidate();
        }

        /// <summary>
        /// Context Menu Handle - Delete
        /// </summary>
        /// <param name="sender">context menu</param>
        /// <param name="e">event arguments</param>
        private void 삭제ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.gm.SelectedFigure != null)
            {
                this.gm.FigureList.Remove(this.gm.SelectedFigure);
                this.gm.SelectedFigure = null;
            }

            panelControl1.Invalidate();
        }

        /// <summary>
        /// Context Menu handle - horizontal line
        /// </summary>
        /// <param name="sender">context menu</param>
        /// <param name="e">event arguments</param>
        private void 가로선ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SetFigureType(3, null);
        }

        /// <summary>
        /// Context Menu handle - vertical line
        /// </summary>
        /// <param name="sender">context menu</param>
        /// <param name="e">event arguments</param>
        private void 세로선ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SetFigureType(4, null);
        }
                
        /// <summary>
        /// Context Menu handle - Change background color
        /// </summary>
        /// <param name="sender">context menu</param>
        /// <param name="e">event arguments</param>
        private void 배경색상ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = this.colorDialog.ShowDialog();
            
            if (result == DialogResult.OK)
            {
                this.SetFigureBackColor(this.colorDialog.Color);
            }
        }

        /// <summary>
        /// Change shift key state
        /// </summary>
        /// <param name="sender">form sender</param>
        /// <param name="e">key event arguments</param>
        private void NewTableForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                this.gm.ShiftKeyDown = false;
            }            
        }
    }
}
