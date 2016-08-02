namespace WindowsFormsApplication1
{
    partial class PaintEX
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.그리기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.선ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.원ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.사각형ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.곡선ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.화면삭제ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.종료ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.속성ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.펜색ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.브러시색ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.색선택ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.널브러시ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.펜굵기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.굵은선ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.보통선ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.가는선ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.선지우기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.선ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.원ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.사각형ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.곡선ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.colorPen = new System.Windows.Forms.ColorDialog();
            this.colorBrush = new System.Windows.Forms.ColorDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.그리기ToolStripMenuItem,
            this.속성ToolStripMenuItem,
            this.선지우기ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(489, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 그리기ToolStripMenuItem
            // 
            this.그리기ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.선ToolStripMenuItem,
            this.원ToolStripMenuItem,
            this.사각형ToolStripMenuItem,
            this.곡선ToolStripMenuItem,
            this.화면삭제ToolStripMenuItem,
            this.종료ToolStripMenuItem});
            this.그리기ToolStripMenuItem.Name = "그리기ToolStripMenuItem";
            this.그리기ToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.그리기ToolStripMenuItem.Text = "그리기";
            // 
            // 선ToolStripMenuItem
            // 
            this.선ToolStripMenuItem.Name = "선ToolStripMenuItem";
            this.선ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.선ToolStripMenuItem.Text = "선";
            this.선ToolStripMenuItem.Click += new System.EventHandler(this.선ToolStripMenuItem_Click);
            // 
            // 원ToolStripMenuItem
            // 
            this.원ToolStripMenuItem.Name = "원ToolStripMenuItem";
            this.원ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.원ToolStripMenuItem.Text = "원";
            this.원ToolStripMenuItem.Click += new System.EventHandler(this.원ToolStripMenuItem_Click);
            // 
            // 사각형ToolStripMenuItem
            // 
            this.사각형ToolStripMenuItem.Name = "사각형ToolStripMenuItem";
            this.사각형ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.사각형ToolStripMenuItem.Text = "사각형";
            this.사각형ToolStripMenuItem.Click += new System.EventHandler(this.사각형ToolStripMenuItem_Click);
            // 
            // 곡선ToolStripMenuItem
            // 
            this.곡선ToolStripMenuItem.Name = "곡선ToolStripMenuItem";
            this.곡선ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.곡선ToolStripMenuItem.Text = "곡선";
            this.곡선ToolStripMenuItem.Click += new System.EventHandler(this.곡선ToolStripMenuItem_Click);
            // 
            // 화면삭제ToolStripMenuItem
            // 
            this.화면삭제ToolStripMenuItem.Name = "화면삭제ToolStripMenuItem";
            this.화면삭제ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.화면삭제ToolStripMenuItem.Text = "화면삭제";
            this.화면삭제ToolStripMenuItem.Click += new System.EventHandler(this.화면삭제ToolStripMenuItem_Click);
            // 
            // 종료ToolStripMenuItem
            // 
            this.종료ToolStripMenuItem.Name = "종료ToolStripMenuItem";
            this.종료ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.종료ToolStripMenuItem.Text = "종료";
            this.종료ToolStripMenuItem.Click += new System.EventHandler(this.종료ToolStripMenuItem_Click);
            // 
            // 속성ToolStripMenuItem
            // 
            this.속성ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.펜색ToolStripMenuItem,
            this.브러시색ToolStripMenuItem,
            this.펜굵기ToolStripMenuItem});
            this.속성ToolStripMenuItem.Name = "속성ToolStripMenuItem";
            this.속성ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.속성ToolStripMenuItem.Text = "속성";
            // 
            // 펜색ToolStripMenuItem
            // 
            this.펜색ToolStripMenuItem.Name = "펜색ToolStripMenuItem";
            this.펜색ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.펜색ToolStripMenuItem.Text = "펜색";
            this.펜색ToolStripMenuItem.Click += new System.EventHandler(this.펜색ToolStripMenuItem_Click);
            // 
            // 브러시색ToolStripMenuItem
            // 
            this.브러시색ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.색선택ToolStripMenuItem,
            this.널브러시ToolStripMenuItem});
            this.브러시색ToolStripMenuItem.Name = "브러시색ToolStripMenuItem";
            this.브러시색ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.브러시색ToolStripMenuItem.Text = "브러시색";
            // 
            // 색선택ToolStripMenuItem
            // 
            this.색선택ToolStripMenuItem.Name = "색선택ToolStripMenuItem";
            this.색선택ToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.색선택ToolStripMenuItem.Text = "색선택";
            this.색선택ToolStripMenuItem.Click += new System.EventHandler(this.색선택ToolStripMenuItem_Click);
            // 
            // 널브러시ToolStripMenuItem
            // 
            this.널브러시ToolStripMenuItem.Name = "널브러시ToolStripMenuItem";
            this.널브러시ToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.널브러시ToolStripMenuItem.Text = "널 브러시";
            this.널브러시ToolStripMenuItem.Click += new System.EventHandler(this.널브러시ToolStripMenuItem_Click);
            // 
            // 펜굵기ToolStripMenuItem
            // 
            this.펜굵기ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.굵은선ToolStripMenuItem,
            this.보통선ToolStripMenuItem,
            this.가는선ToolStripMenuItem});
            this.펜굵기ToolStripMenuItem.Name = "펜굵기ToolStripMenuItem";
            this.펜굵기ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.펜굵기ToolStripMenuItem.Text = "펜 굵기";
            // 
            // 굵은선ToolStripMenuItem
            // 
            this.굵은선ToolStripMenuItem.Name = "굵은선ToolStripMenuItem";
            this.굵은선ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.굵은선ToolStripMenuItem.Text = "굵은선";
            this.굵은선ToolStripMenuItem.Click += new System.EventHandler(this.굵은선ToolStripMenuItem_Click);
            // 
            // 보통선ToolStripMenuItem
            // 
            this.보통선ToolStripMenuItem.Name = "보통선ToolStripMenuItem";
            this.보통선ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.보통선ToolStripMenuItem.Text = "보통선";
            this.보통선ToolStripMenuItem.Click += new System.EventHandler(this.보통선ToolStripMenuItem_Click);
            // 
            // 가는선ToolStripMenuItem
            // 
            this.가는선ToolStripMenuItem.Name = "가는선ToolStripMenuItem";
            this.가는선ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.가는선ToolStripMenuItem.Text = "가는선";
            this.가는선ToolStripMenuItem.Click += new System.EventHandler(this.가는선ToolStripMenuItem_Click);
            // 
            // 선지우기ToolStripMenuItem
            // 
            this.선지우기ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.선ToolStripMenuItem1,
            this.원ToolStripMenuItem1,
            this.사각형ToolStripMenuItem1,
            this.곡선ToolStripMenuItem1});
            this.선지우기ToolStripMenuItem.Name = "선지우기ToolStripMenuItem";
            this.선지우기ToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.선지우기ToolStripMenuItem.Text = "지우기";
            // 
            // 선ToolStripMenuItem1
            // 
            this.선ToolStripMenuItem1.Name = "선ToolStripMenuItem1";
            this.선ToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.선ToolStripMenuItem1.Text = "선";
            this.선ToolStripMenuItem1.Click += new System.EventHandler(this.선ToolStripMenuItem1_Click);
            // 
            // 원ToolStripMenuItem1
            // 
            this.원ToolStripMenuItem1.Name = "원ToolStripMenuItem1";
            this.원ToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.원ToolStripMenuItem1.Text = "원";
            this.원ToolStripMenuItem1.Click += new System.EventHandler(this.원ToolStripMenuItem1_Click);
            // 
            // 사각형ToolStripMenuItem1
            // 
            this.사각형ToolStripMenuItem1.Name = "사각형ToolStripMenuItem1";
            this.사각형ToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.사각형ToolStripMenuItem1.Text = "사각형";
            this.사각형ToolStripMenuItem1.Click += new System.EventHandler(this.사각형ToolStripMenuItem1_Click);
            // 
            // 곡선ToolStripMenuItem1
            // 
            this.곡선ToolStripMenuItem1.Name = "곡선ToolStripMenuItem1";
            this.곡선ToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.곡선ToolStripMenuItem1.Text = "곡선";
            this.곡선ToolStripMenuItem1.Click += new System.EventHandler(this.곡선ToolStripMenuItem1_Click);
            // 
            // PaintEX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 468);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PaintEX";
            this.Text = "PaintEX";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 그리기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 선ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 원ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 사각형ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 곡선ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 화면삭제ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 종료ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 속성ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 펜색ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 브러시색ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 색선택ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 널브러시ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 펜굵기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 굵은선ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 보통선ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 가는선ToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorPen;
        private System.Windows.Forms.ColorDialog colorBrush;
        private System.Windows.Forms.ToolStripMenuItem 선지우기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 선ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 원ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 사각형ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 곡선ToolStripMenuItem1;


    }
}

