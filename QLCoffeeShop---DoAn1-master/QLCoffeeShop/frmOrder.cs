using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DTO;
using BLL;
using System;

namespace QLCoffeeShop
{

    public partial class frmOrder : Form
    {
        public frmOrder()
        {
            InitializeComponent();
            LoadTable();
            _loadTypeProduct();
            timer1.Start();
                 

        }
        private void frmOrder_Load(object sender, EventArgs e)
        {
            _loadSanPham(ProductBLL.GetSanPhambyIDLoaiSP(0, 1));
            cbLoaiThucUong.ContextMenu = new ContextMenu();
            timer1.Enabled = true;
            btnThanhToan.Enabled = false;
        }

        private Table objTable; // Đối tượng bàn


        public void LoadTable()
        {
            flpTable.Controls.Clear();

            //load bàn lên trên panel
            List<TableDTO> tablelist = TableBLL.GetAllListTable();

            foreach (TableDTO item in tablelist)
            {
                Table tb = new Table();
                tb.lbl_Name1.Text = item.NameTable;
                tb.Click += Button_Click;
                tb.lblStatus1.Click += new EventHandler(Button_Click);
                tb.lbl_Name1.Click += new EventHandler(Button_Click);
                tb.lbl_Name1.Click += new EventHandler(Button_Click);
                tb.image.Click += new EventHandler(Button_Click);
                tb.Tag = item;
                tb.lbl_Name1.Text = item.NameTable;
                tb.lbl_Name1.Font = new Font("Arial", 12, FontStyle.Bold); // Tăng cỡ chữ
                tb.lbl_Name1.TextAlign = ContentAlignment.MiddleCenter; // Căn giữa
                tb.lbl_Name1.Dock = DockStyle.Top; // Đặt tên bàn ở trên cùng
                tb.lblStatus1.Dock = DockStyle.Bottom; // Đặt trạng thái ở dưới cùng

                if (BLL.TableBLL.GetStatusByIDTable(item.ID) == 0)// nếu trạng thái bàn = 0 thì bàn trống
                {
                    tb.BackColor = Color.FromArgb(174, 214, 241);
                    tb.lblStatus1.Text = "Trống";
                    tb.lblStatus1.ForeColor = Color.Green;
                }
                else
                {
                    tb.BackColor = Color.FromArgb(52, 152, 219);
                    tb.lblStatus1.Text = "Có người";
                    tb.lblStatus1.ForeColor = Color.Red;

                }

                flpTable.Controls.Add(tb);
            }
        }
        public void ShowBill(int id)
        {

            //load bill lên theo theo mã bàn
            lstBill.Items.Clear();

            List<MenuDTO> menulist = MenuBLL.GetListMenuByIDTable(id);
            double totalPrice = 0;
            for (int i = 0; i < menulist.Count; i++)
            {
                ListViewItem listitem = new ListViewItem
                {
                    Text = menulist[i].NameProduct.ToString()
                };
                listitem.SubItems.Add(menulist[i].Quantity.ToString());
                if (menulist[i].PriceBasic == 0)
                    listitem.SubItems.Add("Miễn phí");
                else
                    listitem.SubItems.Add(String.Format("{0:0,0}", menulist[i].PriceBasic) + " VNĐ");
                if (menulist[i].TotalPrice == 0)
                    listitem.SubItems.Add("Miễn phí");
                else
                    listitem.SubItems.Add(String.Format("{0:0,0}", menulist[i].TotalPrice) + " VNĐ");
                totalPrice += menulist[i].TotalPrice;
                listitem.Tag = menulist[i];
                listitem.SubItems.Add("#" + (i + 1).ToString());
                lstBill.Items.Add(listitem);
            }
            if (totalPrice > 0)
                txttotalPrice.Text = String.Format("{0:0,0}", totalPrice);
        }
        
        private void hoverClickButton(object sender) // Hiển thị màu khi click vào button
        {
            Table btnTableLast = null;
            if (sender.GetType() == typeof(Button))
            {
                btnTableLast = sender as Table;
            }
            else
            {
                btnTableLast = (Table)((Control)sender).Parent;
            }
            if (objTable != null)
            {
                Table btnTablePresent = objTable;
                int sttTable = TableBLL.GetStatusByIDTable((btnTablePresent.Tag as TableDTO).ID);
                if (sttTable == 1)
                {
                    btnTablePresent.BackColor = Color.LightPink;
                }
                else btnTablePresent.BackColor = Color.Aqua;
                btnTablePresent.ForeColor = Color.Black;
            }
            btnTableLast.BackColor = Color.LightBlue;
            btnTableLast.ForeColor = Color.White; // Mau chu
            objTable = btnTableLast;

        }
        Table choseTable;
        public void Button_Click(object sender, EventArgs e)
        {
            hoverClickButton(sender);
            txtHD.Text = "";
            txtBan.Text = "";

            // Viết hàm lấy thông tin bàn bằng mã
            Table btnTable = null;
            if (sender.GetType() == typeof(Button))
            {
                btnTable = sender as Table;
            }
            else
            {
                btnTable = (Table)((Control)sender).Parent;
            }
            choseTable = btnTable;

            lstBill.Tag = btnTable.Tag;
            // Kiểm tra trạng thai ở
            int idTable = (btnTable.Tag as TableDTO).ID;
            txtBan.Text = (btnTable.Tag as TableDTO).NameTable + "";
            btnThanhToan.Enabled = false;
            cbLoaiThucUong.Enabled = true;
            lstSanPham.Enabled = true;
            txttotalPrice.Text = "0";
            if (TableBLL.GetStatusByIDTable(idTable) == 1)
            {
                txtHD.Text = "HD00" + (string)BillBLL.GetIDBillNoPaymentByIDTable((int)idTable).ToString();
                btnThanhToan.Enabled = true;
                if (lstBill.Tag != null)
                {
                    ShowBill(idTable);
                }
            }
            else
            {
                lstBill.Items.Clear();
            }
        }
        private void _loadTypeProduct()
        {
            //load loại thức uống theo tên
            List<TypeProductDTO> listtype = TypeProductBLL.GetListTypeProductWithStatusOne(1);
            cbLoaiThucUong.DataSource = listtype;
            cbLoaiThucUong.DisplayMember = "NameType";
        }
     
        private void _loadSanPham(List<ProductDTO> listProduct) //load sản phẩm theo loại
        {
            lstSanPham.Items.Clear();
            for (int i = 0; i < listProduct.Count; i++)
            {
                ListViewItem item = new ListViewItem
                {
                    Text = "#" + (i + 1).ToString()
                };
                item.SubItems.Add(listProduct[i].NameProducts);
                if (listProduct[i].SalePrice == 0)
                    item.SubItems.Add("Miễn phí");
                else
                    item.SubItems.Add(listProduct[i].SalePrice.ToString("0,0 VNĐ"));
                item.Tag = listProduct[i];
                lstSanPham.Items.Add(item);
            }
        }


        private void cbLoaiThucUong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLoaiThucUong.SelectedIndex >= 0)
            {
                // Khi load loại loại thức uống thì sẽ gán loại theo cái thức uống.
                int id = 0;
                if (cbLoaiThucUong.SelectedItem == null)
                    return;
                TypeProductDTO typeProduct = cbLoaiThucUong.SelectedItem as TypeProductDTO;
                id = typeProduct.ID;
                _loadSanPham(ProductBLL.GetSanPhambyIDLoaiSP(id, 1));
            }
        }
       
        
        private TableDTO _createAddBillByIDTable(ProductDTO Product)
        {
            TableDTO table = lstBill.Tag as TableDTO;


            int idBill = BillBLL.GetIDBillNoPaymentByIDTable(table.ID);//lấy lên cái mã id của hóa đơn 

            // int idProduct = (cbProduct. SelectedItem as ProductDTO).ID;//thêm vào 1 gridview để hiển thị
            int idProduct = Product.ID;
            int quantity = 1;
            //kiểm tra hóa đơn có chưa hay
            if (idBill == -1)//nếu chưa thì tạo 1 hóa đơn mới với mã hóa đơn
            {
                quantity = ChiTietBillBLL.GetSoLuongSanPham(idBill, idProduct);
                // sau khi tạo xong 1 hóa đơn mới thì thêm vào bảng chi tiết hóa đơn với các trường tương ứng
                ChiTietBillBLL.InsertChiTietBill(BillBLL.GetIDBillMax(), idProduct, quantity + 1);
            }
            else//nếu đã có thì thêm nó vào cái cá bảng chi tiêt hóa đơn với các trường là mã hóa đơn, mã thức uống và số lượng
            {
                quantity = ChiTietBillBLL.GetSoLuongSanPham(idBill, idProduct);
                ChiTietBillBLL.InsertChiTietBill(idBill, idProduct, quantity + 1);
            }
            return table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TableDTO table = lstBill.Tag as TableDTO;
                int idBill = BillBLL.GetIDBillNoPaymentByIDTable(table.ID);

                RptThanhToan frm_TToan = new RptThanhToan();
                DateTime Time = DateTime.Now;
                frm_TToan.XuatHoaDon(idBill, "HÓA ĐƠN TẠM TÍNH", table.NameTable, Program.sTaiKhoan.TenTK, Time, string.Format("{0:0,0}", txttotalPrice.Text), "0", "0", "0", "0", true);

                frm_TToan.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lstSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSanPham.SelectedItems.Count > 0)
            {
                ProductDTO Product = lstSanPham.SelectedItems[0].Tag as ProductDTO;

                if (choseTable != null)
                {
                    if (objTable != null && objTable.Tag != null)
                    {
                        Table btnTable = objTable;

                        TableDTO table = objTable.Tag as TableDTO;
                        int idTable = table.ID;
                        if (TableBLL.GetStatusByIDTable(idTable) == 0)
                        {
                            DialogResult kq = MessageBox.Show("Bạn đang chọn bàn mới.\n Bạn có muốn tạo hóa đơn mới cho bàn này chứ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (kq == DialogResult.OK)
                            {
                                btnThanhToan.Enabled = true;
                                lstBill.Tag = choseTable.Tag;

                                TableBLL.UpdateStatusTable(1, idTable);

                                try
                                {
                                    BillBLL.InsertBill(DateTime.Now, 0.0, Program.sTaiKhoan.ID, idTable);
                                    lstBill.Items.Clear();
                                    txtHD.Text = "HD00" + BillBLL.GetIDBillNoPaymentByIDTable(idTable).ToString();
                                    btnTable.Text = table.NameTable + Environment.NewLine + "Có";
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                lstBill.Items.Clear();
                                btnThanhToan.Enabled = false;
                                cbLoaiThucUong.Enabled = false;
                                txtHD.Text = "";
                            }
                        }
                        table = _createAddBillByIDTable(Product);
                        ShowBill(table.ID);
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn bàn để thêm thức uống. Vui lòng chọn bàn để tiếp tục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtHD.Text = "";
            lstBill.Items.Clear();
            txtBan.Text = "";
            txttotalPrice.Text = "0";
            objTable = null;
            choseTable = null;
            btnThanhToan.Enabled = false;
            _loadTypeProduct();
            txtTuKhoa.Text = "";
            LoadTable();

        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                TableDTO table = lstBill.Tag as TableDTO;
                int idBill = BillBLL.GetIDBillNoPaymentByIDTable(table.ID);
                Frm_ThanhToan frm_ThanhToan = new Frm_ThanhToan("HÓA ĐƠN THANH TOÁN", table, idBill, txttotalPrice.Text);
                //Hide();
                frm_ThanhToan.ShowDialog();
                if (frm_ThanhToan._KetQua)
                {
                    ShowBill(table.ID);
                    LoadTable();
                    _loadTypeProduct();
                    btnThanhToan.Enabled = false;
                    cbLoaiThucUong.Enabled = false;
                    lstSanPham.Enabled = false;
                    txttotalPrice.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblNgayHienTai_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblNgayHienTai.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

   
    }
}