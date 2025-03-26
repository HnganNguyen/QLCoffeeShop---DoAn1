using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCoffeeShop
{
    public partial class FrmTrangChu : Form
    {
        public FrmTrangChu()
        {
            InitializeComponent();
            if (Program.sTaiKhoan.Quyen == 1)
            {
                btnAdmin.Visible = false;
            }
        }
        private void btnAdmin_Click(object sender, EventArgs e)
        {
            if (Program.sTaiKhoan.Quyen == 0)
            {
                frmAdmin n = new frmAdmin();
                Hide();
                n.ShowDialog();
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            frmOrder frm_Order = new frmOrder();
            Hide();
            frm_Order.ShowDialog();
            if (Program.sTaiKhoan.Quyen == 1)
            {
                btnAdmin.Visible = false;

            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có thực sự muốn đăng xuất không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (kq != DialogResult.Cancel)
            {
                this.Close();
                Frm_DangNhap frmDangNhap = new Frm_DangNhap();
                frmDangNhap.Show();
                Program.sTaiKhoan = null;
                
            }
        }

        private void FrmTrangChu_Load(object sender, EventArgs e)
        {

        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            frmDoiMatKhau frm = new frmDoiMatKhau(Program.sTaiKhoan);
            frm.ShowDialog();

        }
    }
}
