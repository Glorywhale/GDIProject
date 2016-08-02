namespace GDIProject
{
    partial class TableFormV3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.rcSelector1 = new GDIProject.RCSelector();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.도형변경ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.사각형ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.원형ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.이미지ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.가로선ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.세로선ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.배경색상ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.삭제ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertyGridControl1 = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(12, 12);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(177, 40);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "테이블 구성";
            this.simpleButton1.Click += new System.EventHandler(this.SimpleButton1_Click);
            // 
            // rcSelector1
            // 
            this.rcSelector1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.rcSelector1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rcSelector1.Location = new System.Drawing.Point(195, 12);
            this.rcSelector1.Name = "rcSelector1";
            this.rcSelector1.Size = new System.Drawing.Size(218, 265);
            this.rcSelector1.TabIndex = 5;
            // 
            // panelControl1
            // 
            this.panelControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.panelControl1.Location = new System.Drawing.Point(12, 58);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1101, 757);
            this.panelControl1.TabIndex = 4;
            this.panelControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelControl1_Paint);
            this.panelControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelControl1_MouseDown);
            this.panelControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelControl1_MouseMove);
            this.panelControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelControl1_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.도형변경ToolStripMenuItem,
            this.배경색상ToolStripMenuItem,
            this.삭제ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(159, 76);
            // 
            // 도형변경ToolStripMenuItem
            // 
            this.도형변경ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.사각형ToolStripMenuItem,
            this.원형ToolStripMenuItem,
            this.이미지ToolStripMenuItem,
            this.가로선ToolStripMenuItem,
            this.세로선ToolStripMenuItem});
            this.도형변경ToolStripMenuItem.Name = "도형변경ToolStripMenuItem";
            this.도형변경ToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.도형변경ToolStripMenuItem.Text = "도형 변경";            
            // 
            // 사각형ToolStripMenuItem
            // 
            this.사각형ToolStripMenuItem.Name = "사각형ToolStripMenuItem";
            this.사각형ToolStripMenuItem.Size = new System.Drawing.Size(128, 24);
            this.사각형ToolStripMenuItem.Text = "사각형";
            this.사각형ToolStripMenuItem.Click += new System.EventHandler(this.사각형ToolStripMenuItem_Click);
            // 
            // 원형ToolStripMenuItem
            // 
            this.원형ToolStripMenuItem.Name = "원형ToolStripMenuItem";
            this.원형ToolStripMenuItem.Size = new System.Drawing.Size(128, 24);
            this.원형ToolStripMenuItem.Text = "원형";
            this.원형ToolStripMenuItem.Click += new System.EventHandler(this.원형ToolStripMenuItem_Click);
            // 
            // 이미지ToolStripMenuItem
            // 
            this.이미지ToolStripMenuItem.Name = "이미지ToolStripMenuItem";
            this.이미지ToolStripMenuItem.Size = new System.Drawing.Size(128, 24);
            this.이미지ToolStripMenuItem.Text = "이미지";
            this.이미지ToolStripMenuItem.Click += new System.EventHandler(this.이미지ToolStripMenuItem_Click);
            // 
            // 가로선ToolStripMenuItem
            // 
            this.가로선ToolStripMenuItem.Name = "가로선ToolStripMenuItem";
            this.가로선ToolStripMenuItem.Size = new System.Drawing.Size(128, 24);
            this.가로선ToolStripMenuItem.Text = "가로 선";
            this.가로선ToolStripMenuItem.Click += new System.EventHandler(this.가로선ToolStripMenuItem_Click);
            // 
            // 세로선ToolStripMenuItem
            // 
            this.세로선ToolStripMenuItem.Name = "세로선ToolStripMenuItem";
            this.세로선ToolStripMenuItem.Size = new System.Drawing.Size(128, 24);
            this.세로선ToolStripMenuItem.Text = "세로 선";
            this.세로선ToolStripMenuItem.Click += new System.EventHandler(this.세로선ToolStripMenuItem_Click);
            // 
            // 배경색상ToolStripMenuItem
            // 
            this.배경색상ToolStripMenuItem.Name = "배경색상ToolStripMenuItem";
            this.배경색상ToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.배경색상ToolStripMenuItem.Text = "배경색 변경";
            this.배경색상ToolStripMenuItem.Click += new System.EventHandler(this.배경색상ToolStripMenuItem_Click);
            // 
            // 삭제ToolStripMenuItem
            // 
            this.삭제ToolStripMenuItem.Name = "삭제ToolStripMenuItem";
            this.삭제ToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.삭제ToolStripMenuItem.Text = "삭제";
            this.삭제ToolStripMenuItem.Click += new System.EventHandler(this.삭제ToolStripMenuItem_Click);
            // 
            // propertyGridControl1
            // 
            this.propertyGridControl1.Location = new System.Drawing.Point(1120, 58);
            this.propertyGridControl1.Name = "propertyGridControl1";
            this.propertyGridControl1.Size = new System.Drawing.Size(436, 757);
            this.propertyGridControl1.TabIndex = 7;
            // 
            // NewTableForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1568, 827);
            this.Controls.Add(this.propertyGridControl1);
            this.Controls.Add(this.rcSelector1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.simpleButton1);
            this.Name = "NewTableForm";
            this.Text = "NewTableForm";
            this.Load += new System.EventHandler(this.NewTableForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NewTableForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.NewTableForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private RCSelector rcSelector1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 도형변경ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 삭제ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 사각형ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 원형ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 이미지ToolStripMenuItem;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControl1;
        private System.Windows.Forms.ToolStripMenuItem 가로선ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 세로선ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 배경색상ToolStripMenuItem;
    }
}