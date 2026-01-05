namespace LibaryMangUI.Forms
{
    partial class MemberMenu
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvMembers;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvMembers = new System.Windows.Forms.DataGridView();
            txtFullName = new System.Windows.Forms.TextBox();
            txtEmail = new System.Windows.Forms.TextBox();
            txtPhone = new System.Windows.Forms.TextBox();
            btnAdd = new System.Windows.Forms.Button();
            btnUpdate = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)dgvMembers).BeginInit();
            SuspendLayout();

            dgvMembers.Location = new System.Drawing.Point(12, 12);
            dgvMembers.Size = new System.Drawing.Size(600, 200);
            dgvMembers.ReadOnly = true;
            dgvMembers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvMembers.CellClick += dgvMembers_CellClick;

            txtFullName.Location = new System.Drawing.Point(12, 230);
            txtFullName.Width = 200;
            txtFullName.PlaceholderText = "Ad Soyad";

            txtEmail.Location = new System.Drawing.Point(220, 230);
            txtEmail.Width = 200;
            txtEmail.PlaceholderText = "Email";

            txtPhone.Location = new System.Drawing.Point(430, 230);
            txtPhone.Width = 180;
            txtPhone.PlaceholderText = "+994XXXXXXXXX";

            btnAdd.Text = "➕ Əlavə et";
            btnAdd.Location = new System.Drawing.Point(12, 270);
            btnAdd.Click += btnAdd_Click;

            btnUpdate.Text = "✏️ Yenilə";
            btnUpdate.Location = new System.Drawing.Point(120, 270);
            btnUpdate.Click += btnUpdate_Click;

            btnDelete.Text = "❌ Sil";
            btnDelete.Location = new System.Drawing.Point(220, 270);
            btnDelete.Click += btnDelete_Click;

            ClientSize = new System.Drawing.Size(624, 330);
            Controls.AddRange(new System.Windows.Forms.Control[]
            {
                dgvMembers, txtFullName, txtEmail, txtPhone, btnAdd, btnUpdate, btnDelete
            });
            Text = "Üzv İdarəetməsi";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)dgvMembers).EndInit();
            ResumeLayout(false);
        }
    }
}
