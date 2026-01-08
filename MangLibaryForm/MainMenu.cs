using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Contracts;
using DataAccessLayer.Repositories;
using MangLibaryForm;
using System;
using System.Drawing;
using System.Windows.Forms;
using BusinessLogicLayer.Services;
using DataAccessLayer.Repositories;
using DataAccessLayer.Entities;

namespace MangLibaryForm
{
    public partial class MainForm : Form
    {
        private Panel sidebarPanel;
        private Panel headerPanel;
        private Panel contentPanel;
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Member> _memberRepository;
        private readonly OperationsRepository _operationRepository;

        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IMemberService _memberService;
        
        public MainForm()
        {
            InitializeComponent();

            // Repositories
            _bookRepository = new BookRepository();
            _categoryRepository = new CategoryRepository();
            _memberRepository = new MemberRepository();
            _operationRepository = new OperationsRepository();

            // Services
            _bookService = new BookService(
                _bookRepository,
                _categoryRepository,
                _memberRepository,
                _operationRepository
               
            );

            _categoryService = new CategoryService(_categoryRepository);
            _memberService = new MemberService(_memberRepository,_bookRepository);
        }

        private void InitializeComponent()
        {
            // Form ayarları
            this.Text = "Kitabxana İdarəetmə Sistemi";
            this.Width = 1200;
            this.Height = 700;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.FormBorderStyle = FormBorderStyle.None;

            // Header Panel
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(41, 128, 185)
            };

            Label titleLabel = new Label
            {
                Text = "📚 KİTABXANA İDARƏETMƏ SİSTEMİ",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Padding = new Padding(30, 0, 0, 0)
            };

            Button closeButton = new Button
            {
                Text = "✕",
                Width = 50,
                Height = 50,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Dock = DockStyle.Right,
                Cursor = Cursors.Hand
            };
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 57, 43);
            closeButton.Click += (s, e) => Application.Exit();

            headerPanel.Controls.Add(titleLabel);
            headerPanel.Controls.Add(closeButton);

            // Sidebar Panel
            sidebarPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 280,
                BackColor = Color.FromArgb(44, 62, 80)
            };

            // Content Panel
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(240, 242, 245),
                Padding = new Padding(30)
            };

            Label welcomeLabel = new Label
            {
                Text = "Xoş gəlmisiniz! 👋\n\nKitabxana idarəetmə sisteminə xoş gəldiniz.\nYan menyudan əməliyyat seçin.",
                Font = new Font("Segoe UI", 19),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoSize = false,
                Size = new Size(600, 200),
                Location = new Point(50, 50),
                TextAlign = ContentAlignment.TopLeft
            };

            contentPanel.Controls.Add(welcomeLabel);


            // Menu buttons
            //CreateMenuButton("📚 Kitablar", "Kitabları idarə edin", 20, () => new BookForm().ShowDialog());
            //CreateMenuButton("📁 Kateqoriyalar", "Kateqoriyaları idarə edin", 120, () => new CategoryForm().ShowDialog());
            //CreateMenuButton("👤 Üzvlər", "Üzvləri idarə edin", 220, () => new MemberForm().ShowDialog());
            CreateMenuButton(
    "📚 Kitablar",
    "Kitabları idarə edin",
    20,
    () => new BookForm(
        _bookService,
        _categoryService,
        _memberService
    ).ShowDialog()
);

            CreateMenuButton(
                "📁 Kateqoriyalar",
                "Kateqoriyaları idarə edin",
                120,
                () => new CategoryForm(_categoryService).ShowDialog()
            );

            CreateMenuButton(
                "👤 Üzvlər",
                "Üzvləri idarə edin",
                220,
                () => new MemberForm(_memberService).ShowDialog()
            );

            // Panel divider shadow effect
            Panel shadowPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 3,
                BackColor = Color.FromArgb(30, 0, 0, 0)
            };

            // Form controls
            this.Controls.Add(contentPanel);
            this.Controls.Add(shadowPanel);
            this.Controls.Add(sidebarPanel);
            this.Controls.Add(headerPanel);

            // Mouse drag functionality
            SetupDragFunctionality();
        }

        private void CreateMenuButton(string title, string description, int top, Action onClick)
        {
            Panel buttonPanel = new Panel
            {
                Width = 260,
                Height = 80,
                Top = top,
                Left = 10,
                BackColor = Color.FromArgb(52, 73, 94),
                Cursor = Cursors.Hand
            };

            Label titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                Width = 240,
                Height = 30,
                Top = 12,
                Left = 15,
                BackColor = Color.Transparent
            };

            Label descLabel = new Label
            {
                Text = description,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(189, 195, 199),
                AutoSize = false,
                Width = 240,
                Height = 20,
                Top = 42,
                Left = 15,
                BackColor = Color.Transparent
            };

            buttonPanel.Controls.Add(titleLabel);
            buttonPanel.Controls.Add(descLabel);

            buttonPanel.MouseEnter += (s, e) => {
                buttonPanel.BackColor = Color.FromArgb(41, 128, 185);
                titleLabel.ForeColor = Color.White;
                descLabel.ForeColor = Color.FromArgb(236, 240, 241);
            };

            buttonPanel.MouseLeave += (s, e) => {
                buttonPanel.BackColor = Color.FromArgb(52, 73, 94);
                titleLabel.ForeColor = Color.White;
                descLabel.ForeColor = Color.FromArgb(189, 195, 199);
            };

            buttonPanel.Click += (s, e) => onClick();
            titleLabel.Click += (s, e) => onClick();
            descLabel.Click += (s, e) => onClick();

            sidebarPanel.Controls.Add(buttonPanel);
        }

        private void SetupDragFunctionality()
        {
            bool dragging = false;
            Point dragCursorPoint = Point.Empty;
            Point dragFormPoint = Point.Empty;

            MouseEventHandler mouseDownHandler = (s, e) => {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
            };

            MouseEventHandler mouseMoveHandler = (s, e) => {
                if (dragging)
                {
                    Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                    this.Location = Point.Add(dragFormPoint, new Size(diff));
                }
            };

            MouseEventHandler mouseUpHandler = (s, e) => {
                dragging = false;
            };

            headerPanel.MouseDown += mouseDownHandler;
            headerPanel.MouseMove += mouseMoveHandler;
            headerPanel.MouseUp += mouseUpHandler;
        }
    }
}