namespace GDIProject
{
    partial class TableForm
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.색상변경ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.사각형ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.원형ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.이미지그림ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.사이즈변경ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.삭제ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.colorEdit1 = new DevExpress.XtraEditors.ColorEdit();
            this.rcSelector1 = new GDIProject.RCSelector();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.panelControl1.Location = new System.Drawing.Point(13, 59);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1237, 782);
            this.panelControl1.TabIndex = 0;
            this.panelControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl1_Paint);
            this.panelControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelControl1_MouseDown);
            this.panelControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelControl1_MouseMove);
            this.panelControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelControl1_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.색상변경ToolStripMenuItem,
            this.사이즈변경ToolStripMenuItem,
            this.삭제ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(159, 76);
            // 
            // 색상변경ToolStripMenuItem
            // 
            this.색상변경ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.사각형ToolStripMenuItem,
            this.원형ToolStripMenuItem,
            this.이미지그림ToolStripMenuItem});
            this.색상변경ToolStripMenuItem.Name = "색상변경ToolStripMenuItem";
            this.색상변경ToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.색상변경ToolStripMenuItem.Text = "모양 변경";
            // 
            // 사각형ToolStripMenuItem
            // 
            this.사각형ToolStripMenuItem.Name = "사각형ToolStripMenuItem";
            this.사각형ToolStripMenuItem.Size = new System.Drawing.Size(161, 24);
            this.사각형ToolStripMenuItem.Text = "사각형";
            this.사각형ToolStripMenuItem.Click += new System.EventHandler(this.사각형ToolStripMenuItem_Click);
            // 
            // 원형ToolStripMenuItem
            // 
            this.원형ToolStripMenuItem.Name = "원형ToolStripMenuItem";
            this.원형ToolStripMenuItem.Size = new System.Drawing.Size(161, 24);
            this.원형ToolStripMenuItem.Text = "원형";
            this.원형ToolStripMenuItem.Click += new System.EventHandler(this.원형ToolStripMenuItem_Click);
            // 
            // 이미지그림ToolStripMenuItem
            // 
            this.이미지그림ToolStripMenuItem.Name = "이미지그림ToolStripMenuItem";
            this.이미지그림ToolStripMenuItem.Size = new System.Drawing.Size(161, 24);
            this.이미지그림ToolStripMenuItem.Text = "이미지, 그림";
            this.이미지그림ToolStripMenuItem.Click += new System.EventHandler(this.이미지그림ToolStripMenuItem_Click);
            // 
            // 사이즈변경ToolStripMenuItem
            // 
            this.사이즈변경ToolStripMenuItem.Name = "사이즈변경ToolStripMenuItem";
            this.사이즈변경ToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.사이즈변경ToolStripMenuItem.Text = "사이즈 변경";
            this.사이즈변경ToolStripMenuItem.Click += new System.EventHandler(this.사이즈변경ToolStripMenuItem_Click);
            // 
            // 삭제ToolStripMenuItem
            // 
            this.삭제ToolStripMenuItem.Name = "삭제ToolStripMenuItem";
            this.삭제ToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.삭제ToolStripMenuItem.Text = "삭제";
            this.삭제ToolStripMenuItem.Click += new System.EventHandler(this.삭제ToolStripMenuItem_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(12, 13);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(177, 40);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "테이블 구성";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // colorEdit1
            // 
            this.colorEdit1.EditValue = System.Drawing.Color.Empty;
            this.colorEdit1.Location = new System.Drawing.Point(418, 21);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorEdit1.Size = new System.Drawing.Size(100, 24);
            this.colorEdit1.TabIndex = 0;
            this.colorEdit1.EditValueChanged += new System.EventHandler(this.colorEdit1_EditValueChanged);
            // 
            // rcSelector1
            // 
            this.rcSelector1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.rcSelector1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rcSelector1.Location = new System.Drawing.Point(196, 16);
            this.rcSelector1.Name = "rcSelector1";
            this.rcSelector1.Size = new System.Drawing.Size(216, 265);
            this.rcSelector1.TabIndex = 3;
            // 
            // TableForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1262, 853);
            this.Controls.Add(this.colorEdit1);
            this.Controls.Add(this.rcSelector1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.panelControl1);
            this.Name = "TableForm";
            this.Text = "TableForm";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.colorEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private RCSelector rcSelector1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 색상변경ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 사이즈변경ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 삭제ToolStripMenuItem;
        private DevExpress.XtraEditors.ColorEdit colorEdit1;
        private System.Windows.Forms.ToolStripMenuItem 사각형ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 원형ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 이미지그림ToolStripMenuItem;
    }
}