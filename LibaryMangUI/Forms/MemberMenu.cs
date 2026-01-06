using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Windows.Forms;

namespace LibaryMangUI.Forms
{
    public partial class MemberMenu : Form
    {
        private readonly MemberService _memberService;
        private int selectedId = 0;

        public MemberMenu()
        {
            InitializeComponent();
            _memberService = new MemberService(new MemberRepository());
            LoadData();
        }

        private void LoadData()
        {
            dgvMembers.DataSource = _memberService.GetAll();
            dgvMembers.Columns["Id"].DisplayIndex = 0;
        }

        private void dgvMembers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var member = dgvMembers.Rows[e.RowIndex].DataBoundItem as Member;
            selectedId = member.Id;
            txtFullName.Text = member.FullName;
            txtEmail.Text = member.Email;
            txtPhone.Text = member.PhoneNumber;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _memberService.Add(new Member
            {
                FullName = txtFullName.Text,
                Email = txtEmail.Text,
                PhoneNumber = txtPhone.Text
            });
            LoadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var member = _memberService.GetById(selectedId);
            member.FullName = txtFullName.Text;
            member.Email = txtEmail.Text;
            member.PhoneNumber = txtPhone.Text;
            _memberService.Update(member);
            LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _memberService.Delete(selectedId);
            LoadData();
        }
    }
}
