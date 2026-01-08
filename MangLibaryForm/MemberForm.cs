using BusinessLogicLayer.Dtos;
using BusinessLogicLayer.Services.Contracts;
using DataAccessLayer.Entities;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MangLibaryForm
{
    public partial class MemberForm : Form
    {
        private readonly IMemberService _memberService;

        private DataGridView dgvMembers;
        private TextBox txtName, txtEmail, txtPhone, txtSearch;
        private Button btnAdd, btnUpdate, btnDelete, btnClear;

        private int selectedMemberId = 0;

        public MemberForm(IMemberService memberService)
        {
            _memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));
            InitializeCusComponent();
            LoadMembers();
        }

        private void InitializeCusComponent()
        {
            this.Text = "Üzv İdarəetməsi";
            this.Size = new Size(1100, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);

            // HEADER
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(41, 128, 185)
            };

            Label lblHeader = new Label
            {
                Text = "👤 ÜZV İDARƏETMƏSİ",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            headerPanel.Controls.Add(lblHeader);
            this.Controls.Add(headerPanel);

            // LEFT PANEL
            Panel leftPanel = new Panel
            {
                Location = new Point(20, 100),
                Size = new Size(350, 500),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblForm = new Label
            {
                Text = "Üzv Məlumatları",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };

            txtName = CreateTextBox(leftPanel, "Ad Soyad:", 50);
            txtEmail = CreateTextBox(leftPanel, "Email:", 110);
            txtPhone = CreateTextBox(leftPanel, "Telefon:", 170);

            // BUTTON PANEL (aşağı hissə)
            Panel buttonPanel = new Panel
            {
                Location = new Point(10, 240),
                Size = new Size(330, 200)
            };

            btnAdd = CreateButton("➕ Əlavə Et", 0, Color.FromArgb(39, 174, 96));
            btnAdd.Click += BtnAdd_Click;

            btnUpdate = CreateButton("✏️ Yenilə", 45, Color.FromArgb(243, 156, 18));
            btnUpdate.Click += BtnUpdate_Click;

            btnDelete = CreateButton("🗑️ Sil", 90, Color.FromArgb(231, 76, 60));
            btnDelete.Click += BtnDelete_Click;

            btnClear = CreateButton("🔄 Təmizlə", 135, Color.FromArgb(149, 165, 166));
            btnClear.Click += (s, e) => ClearForm();

            buttonPanel.Controls.AddRange(new Control[]
            {
                btnAdd, btnUpdate, btnDelete, btnClear
            });

            leftPanel.Controls.AddRange(new Control[]
            {
                lblForm, txtName, txtEmail, txtPhone, buttonPanel
            });

            this.Controls.Add(leftPanel);

            // RIGHT PANEL
            Panel rightPanel = new Panel
            {
                Location = new Point(390, 100),
                Size = new Size(680, 500),
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
                Size = new Size(350, 30),
                Font = new Font("Segoe UI", 11)
            };
            txtSearch.TextChanged += (s, e) => SearchMembers();

            dgvMembers = new DataGridView
            {
                Location = new Point(10, 55),
                Size = new Size(660, 420),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true,
                AllowUserToAddRows = false,
                MultiSelect = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            dgvMembers.SelectionChanged += DgvMembers_SelectionChanged;

            rightPanel.Controls.AddRange(new Control[]
            {
                lblSearch, txtSearch, dgvMembers
            });

            this.Controls.Add(rightPanel);
        }

        // ================= METHODS =================

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

        private Button CreateButton(string text, int top, Color backColor)
        {
            Button btn = new Button
            {
                Text = text,
                Location = new Point(0, top),
                Size = new Size(330, 40),
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void LoadMembers()
        {
            var members = _memberService.GetAll();

            dgvMembers.DataSource = members.Select(m => new
            {
                ID = m.Id,
                AdSoyad = m.FullName,
                 Email  = m.Email,
                Telefon = m.PhoneNumber
            }).ToList();
        }

        private void SearchMembers()
        {
            var members = _memberService.Search(txtSearch.Text);

            dgvMembers.DataSource = members.Select(m => new
            {
                ID = m.Id,
                
                AdSoyad = m.FullName,
                Email = m.Email,
                Telefon = m.PhoneNumber
            }).ToList();
        }

        private void DgvMembers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMembers.SelectedRows.Count > 0)
            {
                selectedMemberId = Convert.ToInt32(dgvMembers.SelectedRows[0].Cells["ID"].Value);
                txtName.Text = dgvMembers.SelectedRows[0].Cells["AdSoyad"].Value.ToString();
                txtEmail.Text = dgvMembers.SelectedRows[0].Cells["Email"].Value.ToString();
                txtPhone.Text = dgvMembers.SelectedRows[0].Cells["Telefon"].Value.ToString();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var dto = new MemberCreateDto
                {
                    FullName = txtName.Text,
                    Email = txtEmail.Text,
                    PhoneNumber = txtPhone.Text
                };

                _memberService.Add(dto);
                MessageBox.Show("Üzv uğurla əlavə edildi!", "Uğurlu",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadMembers();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedMemberId == 0)
                {
                    MessageBox.Show("Zəhmət olmasa üzv seçin!", "Xəbərdarlıq",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dto = new MemberUpdateDto
                {
                    Id = selectedMemberId,
                    FullName = txtName.Text,
                    Email = txtEmail.Text,
                    PhoneNumber = txtPhone.Text
                };

                _memberService.Update(dto);
                MessageBox.Show("Üzv uğurla yeniləndi!", "Uğurlu",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadMembers();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Xəta",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (selectedMemberId == 0) return;

            _memberService.Delete(selectedMemberId);
            LoadMembers();
            ClearForm();
        }

        private void ClearForm()
        {
            selectedMemberId = 0;
            txtName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
        }
    }
}
