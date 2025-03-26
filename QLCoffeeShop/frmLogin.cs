using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BLL;
namespace QLCoffeeShop
{
    public partial class Frm_DangNhap : Form
    {
        private Label lblNotification;

        public Frm_DangNhap()
        {
            InitializeComponent();
       
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMSTK.Text.Trim(), out int maTK))
            {
                label4.Text = "Mã tài khoản phải là số!";
                return;
            }
            string password = txtMatKhau.Text;

            TaiKhoanDTO account = TaiKhoanBLL.DangNhap(maTK, password);
            if (account != null)
            {
                Program.sTaiKhoan = account;
                Program.CurrentUserID = Program.sTaiKhoan.ID; // ⭐ Gán mã tài khoản vào biến toàn cục

                if (Program.sTaiKhoan.TrangThai == 1)
                {
                    if (Program.sTaiKhoan.Quyen == 0 || Program.sTaiKhoan.Quyen == 1)
                    {
                        XoaTruongDangNhap();
                        FrmTrangChu n = new FrmTrangChu();
                        Hide();
                        n.ShowDialog();
                    }
                    else
                    {
                        Close();
                    }
                }
                else
                {
                    Program.sTaiKhoan = null;
                    label4.Text = "Tài khoản của bạn đã bị khóa bởi người quản trị.";
                }
            }
            else
            {
                label4.Text = "Bạn nhập sai tài khoản hoặc mật khẩu. Vui lòng nhập lại!";
            }
        }
        private bool _login(int maTK, string password)
        {
            return TaiKhoanBLL.DangNhap(maTK, password) != null;
        }
        private void XoaTruongDangNhap()
        {
            txtMatKhau.Clear();
          
        }
 

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void hienmatkhauCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (hienmatkhauCheckbox.Checked)
            {
                txtMatKhau.UseSystemPasswordChar = false;
            }
            else
            {
                txtMatKhau.UseSystemPasswordChar = true;
            }
        }
    }
}
