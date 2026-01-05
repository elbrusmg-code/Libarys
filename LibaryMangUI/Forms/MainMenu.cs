using System;
using System.Drawing;
using System.Windows.Forms;

namespace LibaryMangUI.Forms
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        // ================= EVENTS =================
        private void btnBooks_Click(object sender, EventArgs e)
        {
            BookMenu bookMenu = new BookMenu();
            bookMenu.ShowDialog();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            CategoryMenu categoryMenu = new CategoryMenu();
            categoryMenu.ShowDialog();
        }

        private void btnMembers_Click(object sender, EventArgs e)
        {
            MemberMenu memberMenu = new MemberMenu();
            memberMenu.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Proqramdan çıxmaq istədiyinizə əminsiniz?",
                "Çıxış",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // ================= HOVER =================
        private void Button_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn)
                btn.Font = new Font(btn.Font.FontFamily, btn.Font.Size + 1, btn.Font.Style);
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Font = btn == btnExit
                    ? new Font(btn.Font.FontFamily, 12F, btn.Font.Style)
                    : new Font(btn.Font.FontFamily, 14F, btn.Font.Style);
            }
        }
    }
}
