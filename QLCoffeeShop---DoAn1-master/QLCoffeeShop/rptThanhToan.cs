using DTO;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QLCoffeeShop
{
    public partial class RptThanhToan : Form
    {
        public RptThanhToan()
        {
            InitializeComponent();
      
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            rptXuatHD.RefreshReport();
        }
    
        public void XuatHoaDon(int MaHD, string TenHD, string Ban, string nhanvien, DateTime thoigian, string tongtien, string khuyenmai, string tienkhachhang, string tienton, string thanhtien, bool xemlaihoadon)
        {
            // Lấy danh sách sản phẩm theo hóa đơn
            List<MenuDTO> lstProduct = BLL.MenuBLL.GetListMenuByIDBill(MaHD);
            if (lstProduct == null || lstProduct.Count == 0)
            {
                MessageBox.Show("Lỗi: Không có dữ liệu sản phẩm nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!xemlaihoadon)
            {
                lstProduct = BLL.MenuBLL.GetReviewBill(MaHD);
            }
            rptXuatHD.LocalReport.ReportEmbeddedResource = "QLCoffeeShop.rptThanhToan.rdlc";
    
            // Kiểm tra và gán giá trị mặc định nếu cần
            if (string.IsNullOrEmpty(nhanvien)) nhanvien = "Không có nhân viên";

            if (tienton != "0")
                tienton = string.Format("{0:0,0}", Convert.ToDouble(tienton));

            if (tienkhachhang != "0")
                tienkhachhang = string.Format("{0:0,0}", Convert.ToDouble(tienkhachhang));

            if (tongtien != "0")
                tongtien = string.Format("{0:0,0}", Convert.ToDouble(tongtien));

            if (khuyenmai != "0")
                khuyenmai = string.Format("{0:0,0}", Convert.ToDouble(khuyenmai));

            if (thanhtien != "0")
                thanhtien = string.Format("{0:0,0}", Convert.ToDouble(thanhtien));

            // Xóa DataSources cũ trước khi thêm mới
            rptXuatHD.LocalReport.DataSources.Clear();
            rptXuatHD.LocalReport.DataSources.Add(new ReportDataSource("dtThanhToan", lstProduct));

            // Đặt giá trị cho các tham số
            ReportParameter[] parameters = new ReportParameter[]
            {
                    new ReportParameter("paraHoaDon", "HD00" + MaHD.ToString()),
                    new ReportParameter("paraNhanVien", nhanvien),
                    new ReportParameter("paraThoiGian", thoigian.ToString("dd/MM/yyyy HH:mm")),
                    new ReportParameter("paraTongTien", tongtien),
                    new ReportParameter("paraKhuyenMai", khuyenmai),
                    new ReportParameter("paraTienKhachTra", tienkhachhang),
                    new ReportParameter("paraTienTon", tienton),
                    new ReportParameter("paraThanhTien", thanhtien),
                    new ReportParameter("paraBan", Ban)
            };

            rptXuatHD.LocalReport.SetParameters(parameters);
            rptXuatHD.RefreshReport();
        }

        private void rptXuatHD_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }


    }


