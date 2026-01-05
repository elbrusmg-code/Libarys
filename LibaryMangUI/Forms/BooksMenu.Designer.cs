namespace LibaryMangUI.Forms
{
    partial class BookMenu
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.TextBox txtISBN;
        private System.Windows.Forms.NumericUpDown numYear;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.ComboBox cmbMember;

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnBorrow;
        private System.Windows.Forms.Button btnReturn;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvBooks = new DataGridView();
            txtTitle = new TextBox();
            txtAuthor = new TextBox();
            txtISBN = new TextBox();
            numYear = new NumericUpDown();
            cmbCategory = new ComboBox();
            cmbMember = new ComboBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnBorrow = new Button();
            btnReturn = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numYear).BeginInit();
            SuspendLayout();
            // 
            // dgvBooks
            // 
            dgvBooks.ColumnHeadersHeight = 29;
            dgvBooks.Location = new Point(12, 12);
            dgvBooks.MultiSelect = false;
            dgvBooks.Name = "dgvBooks";
            dgvBooks.ReadOnly = true;
            dgvBooks.RowHeadersWidth = 51;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.Size = new Size(760, 220);
            dgvBooks.TabIndex = 0;
            dgvBooks.CellClick += dgvBooks_CellClick;
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(12, 250);
            txtTitle.Name = "txtTitle";
            txtTitle.PlaceholderText = "Kitab adı";
            txtTitle.Size = new Size(200, 27);
            txtTitle.TabIndex = 1;
            // 
            // txtAuthor
            // 
            txtAuthor.Location = new Point(220, 250);
            txtAuthor.Name = "txtAuthor";
            txtAuthor.PlaceholderText = "Müəllif";
            txtAuthor.Size = new Size(200, 27);
            txtAuthor.TabIndex = 2;
            // 
            // txtISBN
            // 
            txtISBN.Location = new Point(430, 250);
            txtISBN.Name = "txtISBN";
            txtISBN.PlaceholderText = "ISBN (13)";
            txtISBN.Size = new Size(160, 27);
            txtISBN.TabIndex = 3;
            // 
            // numYear
            // 
            numYear.Location = new Point(600, 250);
            numYear.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            numYear.Minimum = new decimal(new int[] { 1500, 0, 0, 0 });
            numYear.Name = "numYear";
            numYear.Size = new Size(120, 27);
            numYear.TabIndex = 4;
            numYear.Value = new decimal(new int[] { 1500, 0, 0, 0 });
            // 
            // cmbCategory
            // 
            cmbCategory.Location = new Point(12, 290);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(200, 28);
            cmbCategory.TabIndex = 5;
            // 
            // cmbMember
            // 
            cmbMember.Location = new Point(220, 290);
            cmbMember.Name = "cmbMember";
            cmbMember.Size = new Size(200, 28);
            cmbMember.TabIndex = 6;
            cmbMember.SelectedIndexChanged += cmbMember_SelectedIndexChanged;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(12, 330);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 7;
            btnAdd.Text = "➕ Əlavə et";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(120, 330);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 8;
            btnUpdate.Text = "✏️ Yenilə";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(220, 330);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "❌ Sil";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnBorrow
            // 
            btnBorrow.Location = new Point(320, 330);
            btnBorrow.Name = "btnBorrow";
            btnBorrow.Size = new Size(75, 23);
            btnBorrow.TabIndex = 10;
            btnBorrow.Text = "📕 Ver";
            btnBorrow.Click += btnBorrow_Click;
            // 
            // btnReturn
            // 
            btnReturn.Location = new Point(420, 330);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(75, 23);
            btnReturn.TabIndex = 11;
            btnReturn.Text = "📗 Qaytar";
            btnReturn.Click += btnReturn_Click;
            // 
            // BookMenu
            // 
            ClientSize = new Size(784, 380);
            Controls.Add(dgvBooks);
            Controls.Add(txtTitle);
            Controls.Add(txtAuthor);
            Controls.Add(txtISBN);
            Controls.Add(numYear);
            Controls.Add(cmbCategory);
            Controls.Add(cmbMember);
            Controls.Add(btnAdd);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnBorrow);
            Controls.Add(btnReturn);
            Name = "BookMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Kitab İdarəetməsi";
            ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();
            ((System.ComponentModel.ISupportInitialize)numYear).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
