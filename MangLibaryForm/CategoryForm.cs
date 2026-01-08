using BusinessLogicLayer.Dtos;
using BusinessLogicLayer.Services.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace MangLibaryForm
{
    public partial class CategoryForm : Form
    {

        private readonly ICategoryService _categoryService;

        private DataGridView dgvCategories;
        private TextBox txtName, txtSearch;
        private Button btnAdd, btnUpdate, btnDelete, btnClear;
        private TextBox txtDescription;

        private int selectedCategoryId = 0;
        public CategoryForm(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            InitializeCustomComponents();
            LoadCategories();
        }


        private void InitializeCustomComponents()
        {
            this.Text = "Kateqoriya İdarəetməsi";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);

            // LEFT PANEL
            Panel leftPanel = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(300, 420),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblTitle = new Label
            {
                Text = "Kateqoriya",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };

            txtName = CreateTextBox(leftPanel, "Kateqoriya adı:", 50);


            txtDescription = CreateTextBox(leftPanel, "Açıqlama:", 80);


            btnAdd = CreateButton("➕ Əlavə et", 150, Color.FromArgb(39, 174, 96));
            btnAdd.Click += BtnAdd_Click;

            btnUpdate = CreateButton("✏️ Yenilə", 200, Color.FromArgb(243, 156, 18));
            btnUpdate.Click += BtnUpdate_Click;

            btnDelete = CreateButton("🗑️ Sil", 250, Color.FromArgb(231, 76, 60));
            btnDelete.Click += BtnDelete_Click;

            btnClear = CreateButton("🔄 Təmizlə", 300, Color.FromArgb(149, 165, 166));
            btnClear.Click += (s, e) => ClearForm();

            leftPanel.Controls.AddRange(new Control[]
            {
        lblTitle, btnAdd, btnUpdate, btnDelete, btnClear
            });

            this.Controls.Add(leftPanel);

            // RIGHT PANEL
            Panel rightPanel = new Panel
            {
                Location = new Point(340, 20),
                Size = new Size(420, 420),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblSearch = new Label
            {
                Text = "🔍 Axtar:",
                Location = new Point(10, 15),
                AutoSize = true
            };

            txtSearch = new TextBox
            {
                Location = new Point(70, 12),
                Size = new Size(250, 25)
            };
            txtSearch.TextChanged += (s, e) => SearchCategories();

            dgvCategories = new DataGridView
            {
                Location = new Point(10, 50),
                Size = new Size(400, 350),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false
            };
            dgvCategories.SelectionChanged += DgvCategories_SelectionChanged;

            rightPanel.Controls.AddRange(new Control[]
            {
        lblSearch, txtSearch, dgvCategories
            });

            this.Controls.Add(rightPanel);
        }



        private TextBox CreateTextBox(Panel parent, string label, int top)
        {
            Label lbl = new Label
            {
                Text = label,
                Location = new Point(10, top),
                AutoSize = true
            };

            TextBox txt = new TextBox
            {
                Location = new Point(10, top + 25),
                Size = new Size(260, 25)
            };

            parent.Controls.Add(lbl);
            parent.Controls.Add(txt);
            return txt;
        }

        private Button CreateButton(string text, int top, Color color)
        {
            return new Button
            {
                Text = text,
                Location = new Point(10, top),
                Size = new Size(260, 35),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
        }




        private void LoadCategories()
        {
            dgvCategories.DataSource = _categoryService.GetAll()
                .Select(c => new { ID = c.Id, Ad = c.Name, Açıqlama = c.Description })
                .ToList();
        }

        private void SearchCategories()
        {
            dgvCategories.DataSource = _categoryService.Search(txtSearch.Text)
                .Select(c => new { ID = c.Id, Ad = c.Name, Açıqlama = c.Description })
                .ToList();
        }



        private void DgvCategories_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCategories.SelectedRows.Count > 0)
            {
                selectedCategoryId = Convert.ToInt32(dgvCategories.SelectedRows[0].Cells["ID"].Value);
                txtName.Text = dgvCategories.SelectedRows[0].Cells["Ad"].Value.ToString();
                txtDescription.Text = dgvCategories.SelectedRows[0].Cells["Açıqlama"].Value.ToString();
            }
        }




        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                _categoryService.Add(new CategoryCreateDto
                {
                    Name = txtName.Text,
                    Description = txtDescription.Text 
                });
                LoadCategories();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedCategoryId == 0) return;

            try
            {
                _categoryService.Update(new CategoryUpdateDto
                {
                    Id = selectedCategoryId,
                    Name = txtName.Text,
                    Description = txtDescription.Text
                });
                LoadCategories();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (selectedCategoryId == 0) return;

            try
            {
                _categoryService.Delete(selectedCategoryId);
                LoadCategories();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ClearForm()
        {
            selectedCategoryId = 0;
            txtName.Clear();
            txtDescription.Clear();
        }



































    }

}
