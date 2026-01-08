using BusinessLogicLayer.Dtos;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Contracts;
using DataAccessLayer.Entities;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MangLibaryForm
{
    public partial class BookForm : Form
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IMemberService _memberService;
        private DataGridView dgvBooks;
        private TextBox txtSearch, txtTitle, txtAuthor, txtISBN, txtYear;
        private ComboBox cmbCategory;
        private Button btnAdd, btnUpdate, btnDelete, btnBorrow, btnReturn, btnClear;

        private int selectedBookId = 0;

        public BookForm(IBookService bookService, ICategoryService categoryService, IMemberService memberService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));

            InitializeComponent();
            LoadBooks();
            LoadCategories();
        }



        private void InitializeComponent()
        {
            this.Text = "Kitab İdarəetməsi";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);

            // Header
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(41, 128, 185)
            };
            Label lblHeader = new Label
            {
                Text = "📚 KİTAB İDARƏETMƏSİ",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            headerPanel.Controls.Add(lblHeader);
            this.Controls.Add(headerPanel);

            // Left Panel - Form
            Panel leftPanel = new Panel
            {
                Location = new Point(20, 100),
                Size = new Size(350, 550),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblForm = new Label
            {
                Text = "Kitab Məlumatları",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };

            txtTitle = CreateTextBox(leftPanel, "Kitab adı:", 50);
            txtAuthor = CreateTextBox(leftPanel, "Müəllif:", 110);
            txtISBN = CreateTextBox(leftPanel, "ISBN:", 170);
            txtYear = CreateTextBox(leftPanel, "Nəşr ili:", 230);
           

            Label lblCategory = new Label
            {
                Text = "Kateqoriya:",
                Location = new Point(10, 290),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            cmbCategory = new ComboBox
            {
                Location = new Point(10, 315),
                Size = new Size(330, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Buttons
          
            btnAdd = CreateButton("➕ Əlavə Et", 360, Color.FromArgb(39, 174, 96));
            btnAdd.Click += BtnAdd_Click;

            btnUpdate = CreateButton("✏️ Yenilə", 410, Color.FromArgb(243, 156, 18));
            btnUpdate.Click += BtnUpdate_Click;

            btnDelete = CreateButton("🗑️ Sil", 460, Color.FromArgb(231, 76, 60));
            btnDelete.Click += BtnDelete_Click;

            btnClear = CreateButton("🔄 Təmizlə", 510, Color.FromArgb(149, 165, 166));
            btnClear.Click += (s, e) => ClearForm();

            leftPanel.Controls.AddRange(new Control[] {
                lblForm, txtTitle, txtAuthor, txtISBN, txtYear,
                lblCategory, cmbCategory, btnAdd, btnUpdate, btnDelete, btnClear
            });
            this.Controls.Add(leftPanel);

            // Right Panel - Grid
            Panel rightPanel = new Panel
            {
                Location = new Point(390, 100),
                Size = new Size(780, 550),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblSearch = new Label
            {
                Text = "🔍 Axtar:",
                Location = new Point(10, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };

            txtSearch = new TextBox
            {
                Location = new Point(80, 12),
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 11)
            };
            txtSearch.TextChanged += (s, e) => SearchBooks();

            btnBorrow = CreateButton("📤 Götür", 12, Color.FromArgb(52, 152, 219), 500, 60);
            btnBorrow.Click += BtnBorrow_Click;

            btnReturn = CreateButton("📥 Qaytar", 12, Color.FromArgb(46, 204, 113), 640, 60);
            btnReturn.Click += BtnReturn_Click;

            dgvBooks = new DataGridView
            {
                Location = new Point(10, 50),
                Size = new Size(760, 440),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ColumnHeadersHeight = 40
            };
            dgvBooks.SelectionChanged += DgvBooks_SelectionChanged;

            rightPanel.Controls.AddRange(new Control[] { lblSearch, txtSearch, dgvBooks, btnBorrow, btnReturn });
            this.Controls.Add(rightPanel);
        }

        private TextBox CreateTextBox(Panel parent, string label, int top)
        {
            Label lbl = new Label
            {
                Text = label,
                Location = new Point(10, top),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };

            TextBox txt = new TextBox
            {
                Location = new Point(10, top + 25),
                Size = new Size(330, 30),
                Font = new Font("Segoe UI", 11)
            };

            parent.Controls.Add(lbl);
            parent.Controls.Add(txt);

            return txt;
        }

        private Button CreateButton(string text, int top, Color backColor, int left = 10, int width = 330)
        {
            Button btn = new Button
            {
                Text = text,
                Location = new Point(left, top),
                Size = new Size(width, 40),
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void LoadBooks()
        {
            try
            {
                var books = _bookService.GetAll();
                var categories = _categoryService.GetAll();

                var bookDisplay = books.Select(b => new
                {
                    ID = b.Id,
                    Başlıq = b.Title,
                    Müəllif = b.Author,
                    ISBN = b.ISBN,
                    İl = b.PublishedYear,
                    Kateqoriya = categories.FirstOrDefault(c => c.Id == b.CategoryId)?.Name ?? "N/A",
                    Mövcud = b.IsAvailable ? "✓ Bəli" : "✗ Xeyr",
                    ÜzvID = b.MemberId?.ToString() ?? "-"
                }).ToList();

                dgvBooks.DataSource = bookDisplay;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xəta: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategories()
        {
            try
            {
                var categories = _categoryService.GetAll();
                cmbCategory.DisplayMember = "Name";
                cmbCategory.ValueMember = "Id";
                cmbCategory.DataSource = categories;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xəta: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchBooks()
        {
            try
            {
                var books = _bookService.Search(txtSearch.Text);
                var categories = _categoryService.GetAll();

                var bookDisplay = books.Select(b => new
                {
                    ID = b.Id,
                    Başlıq = b.Title,
                    Müəllif = b.Author,
                    ISBN = b.ISBN,
                    İl = b.PublishedYear,
                    Kateqoriya = categories.FirstOrDefault(c => c.Id == b.CategoryId)?.Name ?? "N/A",
                    Mövcud = b.IsAvailable ? "✓ Bəli" : "✗ Xeyr",
                    ÜzvID = b.MemberId?.ToString() ?? "-"
                }).ToList();

                dgvBooks.DataSource = bookDisplay;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xəta: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvBooks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                try
                {
                    int id = Convert.ToInt32(dgvBooks.SelectedRows[0].Cells["ID"].Value);
                    var book = _bookService.GetById(id);

                    selectedBookId = book.Id;
                    txtTitle.Text = book.Title;
                    txtAuthor.Text = book.Author;
                    txtISBN.Text = book.ISBN;
                    txtYear.Text = book.PublishedYear.ToString();
                    cmbCategory.SelectedValue = book.CategoryId;
                }
                catch { }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var dto = new BookCteateDto
                {
                    Title = txtTitle.Text,
                    Author = txtAuthor.Text,
                    ISBN = txtISBN.Text,
                    PublishedYear = int.Parse(txtYear.Text),
                    CategoryId = (int)cmbCategory.SelectedValue
                };

                _bookService.Add(dto);
                MessageBox.Show("Kitab uğurla əlavə edildi!", "Uğurlu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBooks();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xəta: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedBookId == 0)
                {
                    MessageBox.Show("Zəhmət olmasa kitab seçin!", "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dto = new BookUptadeDto
                {
                    Id = selectedBookId,
                    Title = txtTitle.Text,
                    Author = txtAuthor.Text,
                    ISBN = txtISBN.Text,
                    PublishedYear = int.Parse(txtYear.Text),
                    CategoryId = (int)cmbCategory.SelectedValue
                };

                _bookService.Update(dto);
                MessageBox.Show("Kitab uğurla yeniləndi!", "Uğurlu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBooks();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xəta: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedBookId == 0)
                {
                    MessageBox.Show("Zəhmət olmasa kitab seçin!", "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show("Əminsiniz?", "Təsdiq", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    _bookService.Delete(selectedBookId);
                    MessageBox.Show("Kitab uğurla silindi!", "Uğurlu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBooks();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xəta: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBorrow_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedBookId == 0)
                {
                    MessageBox.Show("Zəhmət olmasa kitab seçin!", "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string input = Microsoft.VisualBasic.Interaction.InputBox("Üzv ID daxil edin:", "Kitab Götür", "");
                if (string.IsNullOrEmpty(input)) return;

                int memberId = int.Parse(input);
                _bookService.BorrowBook(selectedBookId, memberId);
                MessageBox.Show("Kitab uğurla götürüldü!", "Uğurlu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xəta: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedBookId == 0)
                {
                    MessageBox.Show("Zəhmət olmasa kitab seçin!", "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _bookService.ReturnBook(selectedBookId);
                MessageBox.Show("Kitab uğurla qaytarıldı!", "Uğurlu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xəta: {ex.Message}", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
  

        private void ClearForm()
        {
            selectedBookId = 0;
            txtTitle.Clear();
            txtAuthor.Clear();
            txtISBN.Clear();
            txtYear.Clear();
            if (cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0;
        }
    }
}