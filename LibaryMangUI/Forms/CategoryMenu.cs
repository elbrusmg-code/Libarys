using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Windows.Forms;

namespace LibaryMangUI.Forms
{
    public partial class CategoryMenu : Form
    {
        private readonly CategoryService _categoryService;
        private int selectedId = 0;

        public CategoryMenu()
        {
            InitializeComponent();
            _categoryService = new CategoryService(new CategoryRepository());
            LoadData();
        }

        private void LoadData()
        {
            dgvCategories.DataSource = _categoryService.GetAll();
            dgvCategories.Columns["Id"].DisplayIndex = 0;
        }

        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var category = dgvCategories.Rows[e.RowIndex].DataBoundItem as Category;
            selectedId = category.Id;
            txtName.Text = category.Name;
            txtDescription.Text = category.Description;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _categoryService.Add(new Category
            {
                Name = txtName.Text,
                Description = txtDescription.Text
            });
            LoadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var category = _categoryService.GetById(selectedId);
            category.Name = txtName.Text;
            category.Description = txtDescription.Text;
            _categoryService.Update(category);
            LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _categoryService.Delete(selectedId);
            LoadData();
        }
    }
}
