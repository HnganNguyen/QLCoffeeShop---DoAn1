using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QLCoffeeShop
{
    public partial class frmDoiMatKhau : Form
    {
        private TaiKhoanDTO currentAccount;

        public frmDoiMatKhau(TaiKhoanDTO account)
        {
            InitializeComponent();
            currentAccount = account;
            _LoadAccount();
            DisplayCurrentAccount();
        }

        private void _LoadAccount()
        {
            List<TaiKhoanDTO> listtype = TaiKhoanBLL.GetAllTaiKhoan();
            cbNhanVien.DataSource = listtype;
            cbNhanVien.DisplayMember = "TenTK";
            cbNhanVien.ValueMember = "ID";
        }

        private void DisplayCurrentAccount()
        {
            if (currentAccount != null)
            {
                cbNhanVien.SelectedValue = currentAccount.ID;
                cbNhanVien.Enabled = false; // Disable combobox to prevent changing the account
            }
        }

        private void ClearForm()
        {
            textBoxMKcu.Clear();
            txtMKnew.Clear();
            txtXacNhap.Clear();
        }

        private void btnDoi_Click_1(object sender, EventArgs e)
        {
            if (cbNhanVien.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int userId = (int)cbNhanVien.SelectedValue;
            string oldPassword = textBoxMKcu.Text.Trim();
            string newPassword = txtMKnew.Text.Trim();
            string confirmPassword = txtXacNhap.Text.Trim();

            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TaiKhoanDTO account = TaiKhoanBLL.GetTaiKhoanById(userId);
            if (account == null)
            {
                MessageBox.Show("Tài khoản không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (account.Password.Trim() != oldPassword)
            {
                MessageBox.Show("Mật khẩu cũ không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đổi mật khẩu?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }
            account.Password = newPassword;
            if (TaiKhoanBLL.UpdateTaiKhoan(account))
            {
                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu thất bại! Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBoxMKcu.UseSystemPasswordChar = false;
                txtMKnew.UseSystemPasswordChar = false;
                txtXacNhap.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxMKcu.UseSystemPasswordChar = true;
                txtMKnew.UseSystemPasswordChar = true;
                txtXacNhap.UseSystemPasswordChar = true;
            }
        }

        private void btnThoatoder_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
