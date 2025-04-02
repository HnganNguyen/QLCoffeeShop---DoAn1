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
            private double _tongTien;
            private string _TenHD;
            public bool _KetQua = false;

            public Frm_ThanhToan(string TenHD, TableDTO tableDTO, int MaHD, string TongTien)
            {
                InitializeComponent();
                _TenHD = TenHD;
                txtMaHD.Text = "HD00" + MaHD.ToString();
                txtTongTien.Text = TongTien;
                txtGiaUuDai.Text = "0";

                _MaHD = MaHD;
                _tableDTO = tableDTO;
                double.TryParse(TongTien.Replace(",", ""), out _tongTien);

                timer1.Start();
            }
        private void UpdateThanhTienvsTienTon()
        {
            double giauudai, tongtien, stk, thanhtien, tienton;
            // Chuyển đổi an toàn
            if (!double.TryParse(txtGiaUuDai.Text.Replace(",", "").Trim(), out giauudai))
                giauudai = 0;

            if (!double.TryParse(txtSTK.Text.Replace(",", "").Trim(), out stk))
                stk = 0;

            tongtien = _tongTien;
            thanhtien = tongtien - giauudai;

            // Kiểm tra số tiền ưu đãi hợp lệ
            if (giauudai > tongtien)
            {
                MessageBox.Show("Vui lòng nhập số tiền giảm thấp hơn tổng giá trị hóa đơn.");
                txtGiaUuDai.Text = "0";
                giauudai = 0;
                thanhtien = tongtien;
            }

            // Tính tiền thối lại chính xác
            tienton = stk - thanhtien;

            // Cập nhật UI
            txtTienTon.Text = tienton >= 0 ? String.Format("{0:0,0}", tienton) : "0";
            txtThanhTien.Text = String.Format("{0:0,0}", thanhtien);
        }

            private void txtSTK_TextChanged(object sender, EventArgs e)
            {
                UpdateThanhTienvsTienTon();
        }

            private void btnXuatHD_Click(object sender, EventArgs e)
            {
                double giauudai, tongtien, stk, thanhtien, tienton;

                if (!double.TryParse(txtGiaUuDai.Text.Replace(",", "").Trim(), out giauudai))
                    giauudai = 0;

                if (!double.TryParse(txtSTK.Text.Replace(",", "").Trim(), out stk))
                    stk = 0;

                tongtien = _tongTien;
                thanhtien = tongtien - giauudai;

                if (giauudai > tongtien)
                {
                    MessageBox.Show("Vui lòng nhập số tiền giảm thấp hơn tổng giá trị hóa đơn.");
                    txtGiaUuDai.Text = "0";
                    giauudai = 0;
                    thanhtien = tongtien;
                }

                if (stk < thanhtien)
                {
                    MessageBox.Show("Khách hàng chưa thanh toán đủ số tiền trong hóa đơn.");
                    return;
                }

                // Tính toán tiền thối chính xác
                tienton = stk - thanhtien;
                txtTienTon.Text = String.Format("{0:0,0}", tienton);

                DialogResult kq = MessageBox.Show("Bạn có muốn thanh toán hay không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.Yes)
                {
                    // Xuất hóa đơn với tiền thối chính xác
                    DateTime Time = DateTime.Now;
                    RptThanhToan rptThanhToan = new RptThanhToan();
                    rptThanhToan.XuatHoaDon(_MaHD, _TenHD, _tableDTO.NameTable, Program.sTaiKhoan.TenTK, Time,
                        String.Format("{0:0,0}", _tongTien),
                        txtGiaUuDai.Text,
                        txtSTK.Text,
                        txtTienTon.Text,  // Đã sửa lỗi sai số tiền thối lại
                        String.Format("{0:0,0}", thanhtien), true);
                    rptThanhToan.ShowDialog();
                    // Cập nhật hóa đơn vào CSDL
                    BillBLL.UpdatetBill(_MaHD, tongtien, giauudai, stk, tienton, thanhtien, Time, Program.sTaiKhoan.ID);
                    TableBLL.UpdateStatusTable(0, _tableDTO.ID);

                    _KetQua = true;
                    Close();
            }
        }

            private void txtSTK_KeyPress(object sender, KeyPressEventArgs e)
            {
                // Chỉ cho nhập số và phím backspace
                e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == '\b');
            }

            private void timer1_Tick(object sender, EventArgs e)
            {
                lblThoiGian.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            }

            private void button1_Click(object sender, EventArgs e)
            {
                Close();
            }

        private void btnThoatoder_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private void txtGiaUuDai_TextChanged(object sender, EventArgs e)
        {
            UpdateThanhTienvsTienTon();
        }
    }
    }

