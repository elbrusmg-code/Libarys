using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace LibaryMangUI.Forms
{
    partial class CategoryMenu
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvCategories;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDescription;
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
            dgvCategories = new System.Windows.Forms.DataGridView();
            txtName = new System.Windows.Forms.TextBox();
            txtDescription = new System.Windows.Forms.TextBox();
            btnAdd = new System.Windows.Forms.Button();
            btnUpdate = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
            SuspendLayout();

            dgvCategories.Location = new System.Drawing.Point(12, 12);
            dgvCategories.Size = new System.Drawing.Size(560, 200);
            dgvCategories.ReadOnly = true;
            dgvCategories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.CellClick += dgvCategories_CellClick;

            txtName.Location = new System.Drawing.Point(12, 230);
            txtName.Width = 200;
            txtName.PlaceholderText = "Kateqoriya adı";

            txtDescription.Location = new System.Drawing.Point(220, 230);
            txtDescription.Width = 350;
            txtDescription.PlaceholderText = "Təsvir";

            btnAdd.Text = "➕ Əlavə et";
            btnAdd.Location = new System.Drawing.Point(12, 270);
            btnAdd.Click += btnAdd_Click;

            btnUpdate.Text = "✏️ Yenilə";
            btnUpdate.Location = new System.Drawing.Point(120, 270);
            btnUpdate.Click += btnUpdate_Click;

            btnDelete.Text = "❌ Sil";
            btnDelete.Location = new System.Drawing.Point(220, 270);
            btnDelete.Click += btnDelete_Click;

            ClientSize = new System.Drawing.Size(584, 330);
            Controls.AddRange(new System.Windows.Forms.Control[]
            {
                dgvCategories, txtName, txtDescription, btnAdd, btnUpdate, btnDelete
            });
            Text = "Kateqoriya İdarəetməsi";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
            ResumeLayout(false);
        }
    }
}
