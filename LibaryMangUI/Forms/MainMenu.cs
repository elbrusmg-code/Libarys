using System;
using System.Drawing;
using System.Windows.Forms;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Contracts;
using DataAccessLayer.Repositories;

namespace LibaryMangUI.Forms
{
    public partial class MainForm : Form
    {
        private IBookService _bookService;
        private ICategoryService _categoryService;
        private IMemberService _memberService;

        public MainForm()
        {
            InitializeComponent();
            InitializeServices();
            CustomizeForm();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form
            this.ClientSize = new Size(900, 600);
            this.Text = "📚 Kitabxana İdarəetmə Sistemi";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);
            this.Font = new Font("Segoe UI", 10F);

            // Header Panel
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.FromArgb(30, 136, 229)
            };

            Label titleLabel = new Label
            {
                Text = "📚 KİTABXANA İDARƏETMƏ SİSTEMİ",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Size = new Size(900, 100),
                TextAlign = ContentAlignment.MiddleCenter
            };
            headerPanel.Controls.Add(titleLabel);

            // Main Panel
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(50, 30, 50, 30)
            };

            // Books Button
            Button btnBooks = CreateMenuButton("📚 Kitab İdarəetməsi", Color.FromArgb(76, 175, 80), 0);
            btnBooks.Click += (s, e) => OpenBookForm();

            // Categories Button
            Button btnCategories = CreateMenuButton("📁 Kateqoriya İdarəetməsi", Color.FromArgb(255, 152, 0), 1);
            btnCategories.Click += (s, e) => OpenCategoryForm();

            // Members Button
            Button btnMembers = CreateMenuButton("👥 Üzv İdarəetməsi", Color.FromArgb(33, 150, 243), 2);
            btnMembers.Click += (s, e) => OpenMemberForm();

            // Exit Button
            Button btnExit = CreateMenuButton("🚪 Çıxış", Color.FromArgb(244, 67, 54), 3);
            btnExit.Click += (s, e) => Application.Exit();

            mainPanel.Controls.AddRange(new Control[] { btnBooks, btnCategories, btnMembers, btnExit });

            this.Controls.Add(mainPanel);
            this.Controls.Add(headerPanel);
            this.ResumeLayout(false);
        }

        private Button CreateMenuButton(string text, Color backColor, int position)
        {
            Button btn = new Button
            {
                Text = text,
                Size = new Size(300, 80),
                Location = new Point(300, 50 + (position * 100)),
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.MouseEnter += (s, e) => btn.BackColor = ControlPaint.Light(backColor, 0.2f);
            btn.MouseLeave += (s, e) => btn.BackColor = backColor;
            return btn;
        }

        private void InitializeServices()
        {
            var bookRepository = new BookRepository();
            var categoryRepository = new CategoryRepository();
            var memberRepository = new MemberRepository();

            _bookService = new BookService(bookRepository, categoryRepository, memberRepository);
            _categoryService = new CategoryService(categoryRepository);
            _memberService = new MemberService(memberRepository);
        }

        private void CustomizeForm()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void OpenBookForm()
        {
            BookForm bookForm = new BookForm(_bookService, _categoryService);
            bookForm.ShowDialog();
        }

        private void OpenCategoryForm()
        {
            CategoryForm categoryForm = new CategoryForm(_categoryService);
            categoryForm.ShowDialog();
        }

        private void OpenMemberForm()
        {
            MemberMenu memberForm = new MemberForm(_memberService);
            memberForm.ShowDialog();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    internal class CategoryForm
    {
        private ICategoryService categoryService;

        public CategoryForm(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        internal void ShowDialog()
        {
            throw new NotImplementedException();
        }
    }
}