using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Linq;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibaryMangUI.Forms
{
    public partial class BookMenu : Form
    {
        private readonly BookService _bookService;
        private readonly CategoryService _categoryService;
        private readonly MemberService _memberService;

        private int selectedBookId = 0;

        public BookMenu()
        {
            InitializeComponent();

            _bookService = new BookService(
                new BookRepository(),
                new CategoryRepository(),
                new MemberRepository()
            );

            _categoryService = new CategoryService(new CategoryRepository());
            _memberService = new MemberService(new MemberRepository());

            LoadData();
        }

        private void LoadData()
        {
            dgvBooks.DataSource = _bookService.GetAll();
            dgvBooks.Columns["Id"].DisplayIndex = 0;
            cmbCategory.DataSource = _categoryService.GetAll();
            cmbCategory.DisplayMember = "Name";
            cmbCategory.ValueMember = "Id";


            cmbMember.DataSource = _memberService.GetAll()
                .Where(m => m.IsActive)
                .ToList();
            cmbMember.DisplayMember = "FullName";
            cmbMember.ValueMember = "Id";
        }

        private void dgvBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var book = dgvBooks.Rows[e.RowIndex].DataBoundItem as Book;
            if (book == null) return;

            selectedBookId = book.Id;

            txtTitle.Text = book.Title;
            txtAuthor.Text = book.Author;
            txtISBN.Text = book.ISBN;
            numYear.Value = book.PublishedYear;
            cmbCategory.SelectedValue = book.CategoryId;

            //if (book.MemberId.HasValue)
            //    cmbMember.SelectedValue = book.MemberId.Value;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                _bookService.Add(new Book
                {
                    Title = txtTitle.Text,
                    Author = txtAuthor.Text,
                    ISBN = txtISBN.Text,
                    PublishedYear = (int)numYear.Value,
                    CategoryId = (int)cmbCategory.SelectedValue
                });

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var book = _bookService.GetById(selectedBookId);
                _bookService.Update(new Book
                {
                    Id = selectedBookId,
                    Title = txtTitle.Text,
                    Author = txtAuthor.Text,
                    ISBN = txtISBN.Text,
                    PublishedYear = (int)numYear.Value,
                    CategoryId = (int)cmbCategory.SelectedValue,
                    
                    IsAvailable = book.IsAvailable,
                    //MemberId = book.MemberId
                   
                });

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedBookId == 0) return;
            _bookService.Delete(selectedBookId);
            LoadData();
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            if (selectedBookId == 0)
            {
                MessageBox.Show("Zəhmət olmasa kitab seçin!");
                return;
            }

            _bookService.BorrowBook(
                selectedBookId,
                (int)cmbMember.SelectedValue
            );

            LoadData();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (selectedBookId == 0)
            {
                MessageBox.Show("Zəhmət olmasa kitab seçin!");
                return;
            }

            _bookService.ReturnBook(selectedBookId);
            LoadData();
        }

        private void cmbMember_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
       
    }
}
