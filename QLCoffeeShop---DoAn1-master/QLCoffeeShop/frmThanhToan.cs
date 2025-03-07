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

namespace QLCoffeeShop
{
    public partial class Frm_ThanhToan : Form
    {
        private int _MaHD;
        private TableDTO _tableDTO;
        private string _tongTien;
        private string _TenHD;
        public bool _KetQua = false;

        private void frmThanhToan_Load(object sender, EventArgs e)
        {
            txtSTK.ContextMenu = new ContextMenu();
            
        }
        public Frm_ThanhToan(string TenHD, TableDTO tableDTO, int MaHD, string TongTien)
        {
            InitializeComponent();
            _TenHD = TenHD;
            txtMaHD.Text = "HD00" + MaHD.ToString();
            txtTongTien.Text = TongTien.ToString();

            _MaHD = MaHD;
            _tableDTO = tableDTO;
            _tongTien = TongTien.Replace(",", "").ToString();
            timer1.Start();
            timer1.Enabled = true;
        }

        private void txtSTK_TextChanged(object sender, EventArgs e)
        {
            double giauudai = String.IsNullOrEmpty(txtPromotion.Text) ? 0 : Convert.ToDouble(txtPromotion.Text);
            double tongtien = String.IsNullOrEmpty(_tongTien) ? 0 : Convert.ToDouble(_tongTien);
            double thanhtien = tongtien - giauudai;
            if (txtSTK.Text.Equals(""))
            {
                txtSTK.Text = "0";
            }

            if (giauudai > tongtien)
            {
                MessageBox.Show("Vui lòng nhập số tiền giảm thấp hơn tổng giá trị hóa đơn.");
                return;
            }

            if (!txtSTK.Text.Equals(""))
            {
                if (txtSTK.Text.Length <= 8)
                {
                    double stk = String.IsNullOrEmpty(txtSTK.Text) ? 0 : Convert.ToDouble(txtSTK.Text);
                    double kt = (stk - thanhtien);

                    txtTienTon.Text = kt != 0 ? String.Format("{0:0,0}", kt) : "0";
                    txtThanhTien.Text = (thanhtien != 0) ? String.Format("{0:0,0}", thanhtien) : "0";
                }
                else
                {
                    txtSTK.Text = "0";
                    txtTienTon.Text = "0";
                    txtPromotion.Text = "0";
                    txtThanhTien.Text = "0";
                    MessageBox.Show("Vui lòng nhập số tiền nằm khoảng chừng số tiền khách hàng phải trả.");
                }
            }
            else
            {
                txtTienTon.Text = "0";
            }
        }

        private void txtHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXuatHD_Click(object sender, EventArgs e)
        {
            double giauudai = String.IsNullOrEmpty(txtPromotion.Text) ? 0 : Convert.ToDouble(txtPromotion.Text);
            double tongtien = String.IsNullOrEmpty(_tongTien) ? 0 : Convert.ToDouble(_tongTien);
            double thanhtien = tongtien - giauudai;

            if (!txtSTK.Text.Equals(""))
            {
                if (giauudai > tongtien)
                {
                    MessageBox.Show("Vui lòng nhập số tiền giảm thấp hơn tổng giá trị hóa đơn.");
                    return;
                }

                if (Convert.ToDouble(txtSTK.Text) < thanhtien)
                    MessageBox.Show("Hệ thống không cho phép khách hàng nợ, mong bạn thông cảm nhắc khách hàng thanh toán đúng số tiền trong hóa đơn.");
                else
                {
                    DialogResult kq = MessageBox.Show("Bạn có muốn thanh toán hay không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (kq != DialogResult.No)
                    {
                        //
                        // Hiển thị report
                        RptThanhToan rptThanhToan = new RptThanhToan();
                        DateTime Time = DateTime.Now;
                        rptThanhToan.XuatHoaDon(_MaHD, _TenHD, _tableDTO.NameTable, Program.sTaiKhoan.TenTK, Time, _tongTien, txtPromotion.Text, txtSTK.Text, txtTienTon.Text, thanhtien.ToString(), true);
                        //
                        rptThanhToan.ShowDialog();
                        BillBLL.UpdatetBill(_MaHD, tongtien, giauudai, Convert.ToDouble(txtSTK.Text), Convert.ToDouble(txtTienTon.Text), thanhtien, Time, Program.sTaiKhoan.ID);

                        TableBLL.UpdateStatusTable(0, _tableDTO.ID); // Cập nhật da

                        _KetQua = true;
                        Close();
                    }
                    else _KetQua = false;
                }
            }
            else MessageBox.Show("Nhập tiền khách cần thanh toán cho hóa đơn này!");


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblThoiGian.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void txtSTK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt32(e.KeyChar) >= 48 && Convert.ToInt32(e.KeyChar) <= 57) || Convert.ToInt32(e.KeyChar) == 8)
            {
                e.Handled = false;
            }
            else e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
