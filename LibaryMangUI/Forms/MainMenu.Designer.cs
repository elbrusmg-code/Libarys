namespace LibaryMangUI.Forms
{
    partial class MainMenu
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnBooks;
        private System.Windows.Forms.Button btnCategories;
        private System.Windows.Forms.Button btnMembers;
        private System.Windows.Forms.Button btnExit;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnBooks = new System.Windows.Forms.Button();
            this.btnCategories = new System.Windows.Forms.Button();
            this.btnMembers = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();

            // panelTop
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Size = new System.Drawing.Size(600, 80);

            // lblTitle
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Text = "📚 KİTABXANA İDARƏETMƏ SİSTEMİ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // btnBooks
            this.btnBooks.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnBooks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBooks.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnBooks.ForeColor = System.Drawing.Color.White;
            this.btnBooks.Location = new System.Drawing.Point(150, 120);
            this.btnBooks.Size = new System.Drawing.Size(300, 60);
            this.btnBooks.Text = "📚 Kitab İdarəetməsi";
            this.btnBooks.Click += new System.EventHandler(this.btnBooks_Click);
            this.btnBooks.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnBooks.MouseLeave += new System.EventHandler(this.Button_MouseLeave);

            // btnCategories
            this.btnCategories.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnCategories.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCategories.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnCategories.ForeColor = System.Drawing.Color.White;
            this.btnCategories.Location = new System.Drawing.Point(150, 200);
            this.btnCategories.Size = new System.Drawing.Size(300, 60);
            this.btnCategories.Text = "📁 Kateqoriya İdarəetməsi";
            this.btnCategories.Click += new System.EventHandler(this.btnCategories_Click);
            this.btnCategories.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnCategories.MouseLeave += new System.EventHandler(this.Button_MouseLeave);

            // btnMembers
            this.btnMembers.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.btnMembers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMembers.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnMembers.ForeColor = System.Drawing.Color.White;
            this.btnMembers.Location = new System.Drawing.Point(150, 280);
            this.btnMembers.Size = new System.Drawing.Size(300, 60);
            this.btnMembers.Text = "👥 Üzv İdarəetməsi";
            this.btnMembers.Click += new System.EventHandler(this.btnMembers_Click);
            this.btnMembers.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnMembers.MouseLeave += new System.EventHandler(this.Button_MouseLeave);

            // btnExit
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(150, 360);
            this.btnExit.Size = new System.Drawing.Size(300, 50);
            this.btnExit.Text = "🚪 Çıxış";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.btnExit.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnExit.MouseLeave += new System.EventHandler(this.Button_MouseLeave);

            // MainMenu
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnMembers);
            this.Controls.Add(this.btnCategories);
            this.Controls.Add(this.btnBooks);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kitabxana İdarəetmə Sistemi";

            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
