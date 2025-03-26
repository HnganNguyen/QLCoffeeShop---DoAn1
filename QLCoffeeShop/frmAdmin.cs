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
using DAL;
using DTO;
using QLCoffeeShop;

namespace QLCoffeeShop
{
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
            _showTable();
            _showTypeProduct();
            _showProduct();
            _loadTypeProduct();
            _filterProduct();
            _showIngredient();
            _loadTaiKhoan();
            _loadcbChucVu();
            _showDanhSachNhanVien();
            _loadDanhSachTinhLuong();
            _loadComboBox();
        }


        #region liên kết tabcontrl và button
        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            HandleButtonClick(sender, "tabPage1");
        }

        private void btnSanpham_Click(object sender, EventArgs e)
        {
            HandleButtonClick(sender, "tabPage2");
        }
        private void btnBan_Click(object sender, EventArgs e)
        {
            HandleButtonClick(sender, "tabPage3");
        }

        private void btnDoanhthu_Click(object sender, EventArgs e)
        {
            HandleButtonClick(sender, "tabPage4");
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            HandleButtonClick(sender, "tabPage5");
        }

        private TinhLuongBLL tinhLuongBLL = new TinhLuongBLL();
        //private BaoCaoBLL bllDoanhThu = new BaoCaoBLL();

        private void HandleButtonClick(object sender, string tabPageName)
        {
            // Đặt lại màu nền của tất cả các nút trong panel_btnSet
            foreach (Control ct in panel_btnSet.Controls)
            {
                if (ct is Button)
                    ct.BackColor = System.Drawing.Color.White;
            }

            // Đặt màu nền cho nút được nhấn
            Button btn = sender as Button;
            if (btn != null)
            {
                btn.BackColor = System.Drawing.Color.FromArgb(9, 132, 227);

                // Chuyển đổi tab trong TabControl
                foreach (TabPage tp in radAdminAccount.TabPages)
                {
                    if (tp.Name == tabPageName)
                    {
                        radAdminAccount.SelectTab(tp);
                        break;
                    }
                }
            }
        }
        #endregion

        #region quản lý bàn
        private void _showTable()
        {
            lstTable.Items.Clear();
            List<TableDTO> menulist = TableBLL.GetAllListTable();
            for (int i = 0; i < menulist.Count; i++)
            {
                ListViewItem listitem = new ListViewItem
                {
                    Text = "#" + (i + 1).ToString()
                };
                listitem.SubItems.Add(menulist[i].NameTable.ToString());
                listitem.SubItems.Add("B00" + menulist[i].ID.ToString());
                if (menulist[i].Status == 0)
                    listitem.SubItems.Add("Bàn trống");
                else listitem.SubItems.Add("Bàn đang có khách");
                listitem.Tag = menulist[i];
                lstTable.Items.Add(listitem);
            }
        }
        private void btnThemBan_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTenBan.Text == "")
                {
                    MessageBox.Show("Bạn không thể thêm nếu như để trống một trường dữ liệu nào.", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    TableDTO sp = new TableDTO
                    {
                        NameTable = txtTenBan.Text,
                        Status = 0
                    };
                    if (TableBLL.InsertTable(sp))
                    {
                        _showTable();
                        _deleteTextTable();
                        MessageBox.Show("Bạn đã thêm mới thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                        MessageBox.Show("Bạn thêm mới thất bại, thử lại.", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void btnXoaBan_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstTable.SelectedItems.Count > 0)
                {
                    TableDTO sp = lstTable.SelectedItems[0].Tag as TableDTO;

                    if (sp.Status == 0) // Chỉ xóa khi bàn trống
                    {
                        if (TableBLL.DeleteTable(sp))
                        {
                            btnXoaBan.Enabled = false;
                            MessageBox.Show("Bàn bạn chọn đã được xóa khỏi hệ thống", "Thông báo", MessageBoxButtons.OK);
                            _showTable();
                        }
                        else
                        {
                            MessageBox.Show("Xóa bàn thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bàn " + sp.ID + " hiện chưa được thanh toán, không thể xóa!", "Thông báo", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn bàn để thực hiện!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void _deleteTextTable()
        {
            txtTenBan.Text = "";
            txtMaBan.Text = "";
            btnXoaBan.Enabled = false;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            _deleteTextTable();
        }

        private void lstTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnXoaBan.Enabled = true;
            if (lstTable.SelectedItems.Count > 0)
            {
                ListViewItem lvw = lstTable.SelectedItems[0];
                TableDTO sp = (TableDTO)lvw.Tag;
                txtTenBan.Text = sp.NameTable;
                txtMaBan.Text = "B00" + sp.ID;
            }
        }
        #endregion

        #region Quản lý loại sản phẩm
        private void _showTypeProduct()
        {
            lstTypeProduct.Items.Clear();
            List<TypeProductDTO> typeList = TypeProductBLL.GetAllListTypeProduct();
            for (int i = 0; i < typeList.Count; i++)
            {
                ListViewItem listItem = new ListViewItem
                {
                    Text = (i + 1).ToString()
                };
                listItem.SubItems.Add(typeList[i].ID.ToString());
                listItem.SubItems.Add(typeList[i].Nametype);
                listItem.SubItems.Add(typeList[i].Status == 1 ? "Đang hoạt động" : "Khóa");
                listItem.Tag = typeList[i];
                lstTypeProduct.Items.Add(listItem);
            }
        }

        private void _clearTypeProductForm()
        {
            txtIDTypeProduct.Text = "";
            txtTypeProductName.Text = "";
            radHienType.Checked = true;
            btnDeTypeProduct.Enabled = false;
            btnEditTypeProduct.Enabled = false;
        }

        private void btnNewType_Click(object sender, EventArgs e)
        {
            _clearTypeProductForm();
        }

        private void btnAddTypeProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTypeProductName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                TypeProductDTO newType = new TypeProductDTO
                {
                    Nametype = txtTypeProductName.Text,
                    Status = radHienType.Checked ? 1 : 0
                };

                if (TypeProductBLL.InsertTypeProduct(newType))
                {
                    _showTypeProduct();
                    _clearTypeProductForm();
                    MessageBox.Show("Thêm loại sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thêm thất bại! Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeTypeProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstTypeProduct.SelectedItems.Count > 0)
                {
                    TypeProductDTO selectedType = lstTypeProduct.SelectedItems[0].Tag as TypeProductDTO;

                    DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa loại sản phẩm '{selectedType.Nametype}' không?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        if (TypeProductBLL.DeleteTypeProduct(selectedType))
                        {
                            _showTypeProduct();
                            _clearTypeProductForm();
                            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại! Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn loại sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditTypeProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstTypeProduct.SelectedItems.Count > 0)
                {
                    TypeProductDTO selectedType = lstTypeProduct.SelectedItems[0].Tag as TypeProductDTO;

                    selectedType.Nametype = txtTypeProductName.Text;
                    selectedType.Status = radHienType.Checked ? 1 : 0;

                    if (TypeProductBLL.UpdateTypeProduct(selectedType))
                    {
                        _showTypeProduct();
                        _clearTypeProductForm();
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại! Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn loại sản phẩm cần chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstTypeProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTypeProduct.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lstTypeProduct.SelectedItems[0];
                TypeProductDTO selectedType = (TypeProductDTO)selectedItem.Tag;

                txtIDTypeProduct.Text = selectedType.ID.ToString();
                txtTypeProductName.Text = selectedType.Nametype;
                radHienType.Checked = selectedType.Status == 1;
                radAnType.Checked = selectedType.Status == 0;

                btnDeTypeProduct.Enabled = true;
                btnEditTypeProduct.Enabled = true;
            }
        }
        #endregion

        #region Quản lý nguyên liệu

        private void _showIngredient()
        {
            lstNguyenLieu.Items.Clear();
            List<NguyenLieuDTO> list = NguyenLieuBLL.GetAllNguyenLieu();
            int stt = 1; // sTT tự tăng
            foreach (NguyenLieuDTO nl in list)
            {
                ListViewItem item = new ListViewItem(stt.ToString());
                item.SubItems.Add(nl.Ma.ToString());
                item.SubItems.Add(nl.Ten);
                item.SubItems.Add(nl.NgayTao.ToString("dd/MM/yyyy"));
                item.SubItems.Add(nl.GiaGoc.ToString());
                item.SubItems.Add(nl.GhiChu);
                lstNguyenLieu.Items.Add(item);
                stt++;
            }
            _calculateTotalPrice(); // tính tổng
        }

        private void btnLamMoiNL_Click(object sender, EventArgs e)
        {
            _clearIngredientForm();
        }

        private void btnLuuNL_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenNL.Text) || string.IsNullOrWhiteSpace(txtGiaNL.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NguyenLieuDTO nl = new NguyenLieuDTO
            {
                Ten = txtTenNL.Text,
                NgayTao = DateTime.Now,
                GiaGoc = double.Parse(txtGiaNL.Text),
                GhiChu = txtGhiChuNL.Text,
                MaTaiKhoan = Program.CurrentUserID // ⭐ Lấy mã tài khoản của người đăng nhập
            };

            NguyenLieuBLL.InsertNguyenLieu(nl);
            _showIngredient();
            _clearIngredientForm();
            _calculateTotalPrice();
        }

        private void btnXoaNL_Click(object sender, EventArgs e)
        {
            if (lstNguyenLieu.SelectedItems.Count > 0)
            {
                int ma = int.Parse(lstNguyenLieu.SelectedItems[0].SubItems[1].Text);
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nguyên liệu này?",
                                                      "Xác nhận xóa", MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    NguyenLieuBLL.DeleteNguyenLieu(ma);
                    _showIngredient();
                    _clearIngredientForm();
                    _calculateTotalPrice();
                }
            }
        }

        private void lstNguyenLieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstNguyenLieu.SelectedItems.Count > 0)
            {
                ListViewItem item = lstNguyenLieu.SelectedItems[0];
                txtMaNL.Text = item.SubItems[1].Text;
                txtTenNL.Text = item.SubItems[2].Text;
                txtGiaNL.Text = item.SubItems[4].Text;
                txtGhiChuNL.Text = item.SubItems[5].Text;
                btnXoaNL.Enabled = true;
            }
            else
            {
                btnXoaNL.Enabled = false;
            }
        }

        private void _clearIngredientForm()
        {
            txtMaNL.Text = "";
            txtTenNL.Text = "";
            txtGiaNL.Text = "";
            txtGhiChuNL.Text = "";
            btnXoaNL.Enabled = false;
        }

        private void _calculateTotalPrice() //hàm tính tổng
        {
            double total = 0;
            foreach (ListViewItem item in lstNguyenLieu.Items)
            {
                if (double.TryParse(item.SubItems[4].Text, out double price))
                {
                    total += price;
                }
            }
            txtTongGiaNL.Text = total.ToString("N0");
        }
        #endregion

        #region Quản lý sản phẩm
        void _loadTypeProduct()
        {
            List<TypeProductDTO> listType = TypeProductBLL.GetAllListTypeProduct();

            cbLoaiSP.DataSource = new List<TypeProductDTO>(listType);
            cbLoaiSP.DisplayMember = "NameType";
            cbLoaiSP.ValueMember = "ID";

            //phần này cho lọc
            listType.Insert(0, new TypeProductDTO { ID = 0, Nametype = "Tất cả" });
            cbLocLoaiSP.DataSource = listType;
            cbLocLoaiSP.DisplayMember = "NameType";
            cbLocLoaiSP.ValueMember = "ID";
        }

        void _showProduct()
        {
            lstProduct.Items.Clear();
            List<ProductDTO> listProduct = ProductBLL.GetAllListProduct();
            int stt = 1;
            foreach (ProductDTO product in listProduct)
            {
                ListViewItem item = new ListViewItem(stt.ToString());
                item.SubItems.Add(product.ID.ToString());
                item.SubItems.Add(product.NameProducts);
                item.SubItems.Add(product.SalePrice.ToString());
                item.SubItems.Add(product.Status == 1 ? "Đang hoạt động" : "Ngừng bán");
                item.SubItems.Add(TypeProductBLL.GetTypeNameByID(product.IDTypeProduct));
                lstProduct.Items.Add(item);
                stt++;
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!");
                return;
            }

            double price;
            if (!double.TryParse(txtDonGia.Text.Replace(",", "").Trim(), out price))
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số cho đơn giá!");
                return;
            }

            if (cbLoaiSP.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại sản phẩm!");
                return;
            }

            int typeID = (int)cbLoaiSP.SelectedValue;
            int status = radHien.Checked ? 1 : 0;

            ProductDTO newProduct = new ProductDTO(0, txtTenSP.Text, price, price, status, typeID);
            if (ProductBLL.AddProduct(newProduct))
            {
                MessageBox.Show("Thêm sản phẩm thành công!");
                _showProduct(); _clearTextProduct();
            }
            else
            {
                MessageBox.Show("Thêm sản phẩm thất bại!");
            }
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (lstProduct.SelectedItems.Count == 0)
                return;
            int id = int.Parse(lstProduct.SelectedItems[0].SubItems[1].Text);
            string name = txtTenSP.Text;
            string priceText = txtDonGia.Text.Replace(",", "").Trim();
            if (!double.TryParse(priceText, out double price))
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số cho đơn giá!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int typeID = (int)cbLoaiSP.SelectedValue;
            int status = radHien.Checked ? 1 : 0;

            ProductDTO updateProduct = new ProductDTO(id, name, price, price, status, typeID);

            if (ProductBLL.UpdateProduct(updateProduct))
            {
                MessageBox.Show("Cập nhật sản phẩm thành công!");
                _showProduct();
                _clearTextProduct();
            }
            else
            {
                MessageBox.Show("Cập nhật sản phẩm thất bại!");
            }
        }

        private void btnDeProduct_Click(object sender, EventArgs e)
        {
            if (lstProduct.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!");
                return;
            }

            if (!int.TryParse(lstProduct.SelectedItems[0].SubItems[1].Text, out int id))
            {
                MessageBox.Show("Lỗi: ID sản phẩm không hợp lệ!");
                return;
            }

            if (ProductBLL.DeleteProduct(id))
            {
                MessageBox.Show("Xóa sản phẩm thành công!");
                _showProduct();
                _clearTextProduct();
            }
            else
            {
                MessageBox.Show("Xóa sản phẩm thất bại!");
            }
        }

        private void lstProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstProduct.SelectedItems.Count == 0) return;
            ListViewItem item = lstProduct.SelectedItems[0];
            txtMaSP.Text = item.SubItems[1].Text;
            txtTenSP.Text = item.SubItems[2].Text;

            if (!double.TryParse(item.SubItems[3].Text.Replace(",", "").Trim(), out double price))
                txtDonGia.Text = "0";
            else
                txtDonGia.Text = price.ToString();
            if (item.SubItems[4].Text == "Đang hoạt động")
                radHien.Checked = true;
            else
                radAn.Checked = true;
            int? typeID = TypeProductBLL.GetIDByTypeName(item.SubItems[5].Text);

            if (typeID.HasValue)
                cbLoaiSP.SelectedValue = typeID.Value;
            else
                cbLoaiSP.SelectedIndex = -1;
        }


        private void _clearTextProduct()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            txtDonGia.Clear();
            cbLoaiSP.SelectedIndex = -1;
            radHien.Checked = true;
        }

        private void btnWatchProduct_Click(object sender, EventArgs e)
        {
            _clearTextProduct();
        }

        private void frmAdmin_Activated(object sender, EventArgs e) //sự kiện để load loại sp sau khi thêm
        {
            _loadTypeProduct();
        }

        //phần lọc
        void _filterProduct(int idLoaiSP = 0)
        {
            lstProduct.Items.Clear();
            List<ProductDTO> listProduct = ProductBLL.GetAllListProduct();

            // Nếu chọn loại khác "Tất cả", chỉ lấy sản phẩm của loại đã chọn
            if (idLoaiSP != 0)
            {
                listProduct = listProduct.Where(p => p.IDTypeProduct == idLoaiSP).ToList();
            }

            int stt = 1;
            foreach (ProductDTO product in listProduct)
            {
                ListViewItem item = new ListViewItem(stt.ToString());
                item.SubItems.Add(product.ID.ToString());
                item.SubItems.Add(product.NameProducts);
                item.SubItems.Add(product.SalePrice.ToString());
                item.SubItems.Add(product.Status == 1 ? "Đang hoạt động" : "Ngừng bán");
                item.SubItems.Add(TypeProductBLL.GetTypeNameByID(product.IDTypeProduct));
                lstProduct.Items.Add(item);
                stt++;
            }
        }

        private void cbLocLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLocLoaiSP.SelectedValue != null)
            {
                int idLoaiSP;
                if (int.TryParse(cbLocLoaiSP.SelectedValue.ToString(), out idLoaiSP))
                {
                    _filterProduct(idLoaiSP);
                }
            }
        }

        //phần tìm kiếm sp
        private void _searchProduct(string keyword)
        {
            lstProduct.Items.Clear();
            List<ProductDTO> listProduct = ProductBLL.SearchProductByName(keyword);

            int stt = 1;
            foreach (ProductDTO product in listProduct)
            {
                ListViewItem item = new ListViewItem(stt.ToString());
                item.SubItems.Add(product.ID.ToString());
                item.SubItems.Add(product.NameProducts);
                item.SubItems.Add(product.SalePrice.ToString());
                item.SubItems.Add(product.Status == 1 ? "Đang hoạt động" : "Ngừng bán");
                item.SubItems.Add(TypeProductBLL.GetTypeNameByID(product.IDTypeProduct));

                lstProduct.Items.Add(item);
                stt++;
            }
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            string keyword = txtSearchProduct.Text.Trim();
            _searchProduct(keyword);
        }
        #endregion

        #region Quản lý tài khoản
        private void _loadTaiKhoan()
        {
            lstAccount.Items.Clear();
            List<TaiKhoanDTO> listTaiKhoan = TaiKhoanBLL.GetAllTaiKhoan();
            for (int i = 0; i < listTaiKhoan.Count; i++)
            {
                ListViewItem listitem = new ListViewItem
                {
                    Text = "#" + (i + 1).ToString()
                };
                listitem.SubItems.Add(listTaiKhoan[i].ID.ToString());
                listitem.SubItems.Add(listTaiKhoan[i].TenTK.ToString());
                listitem.SubItems.Add(listTaiKhoan[i].SDT.ToString());
                listitem.SubItems.Add(listTaiKhoan[i].DiaChi.ToString());
                listitem.SubItems.Add(listTaiKhoan[i].Password.ToString());
                listitem.SubItems.Add(listTaiKhoan[i].CCCD.ToString());
                listitem.SubItems.Add(Position.GetName(listTaiKhoan[i].Quyen));
                listitem.SubItems.Add(listTaiKhoan[i].LuongByCa.ToString("###,### VNĐ"));
                if (listTaiKhoan[i].TrangThai == 0)
                {
                    listitem.SubItems.Add("Bị khóa");
                }
                else
                    listitem.SubItems.Add("Đã được mở khóa");

                listitem.Tag = listTaiKhoan[i];
                lstAccount.Items.Add(listitem);
            }
        }
        class ChucVu
        {
            public int ID { get; set; }

            public string Name { get; set; }
        }

        public static class Position
        {
            private static Dictionary<int, string> positon = new Dictionary<int, string>();

            public static string GetName(int id)
            {
                positon.Clear();

                positon.Add(0, "Quản lý");
                positon.Add(1, "Nhân Viên Bán Hàng");
                positon.Add(2, "Đầu bếp");
                positon.Add(3, "Phụ bếp");

                if (positon.ContainsKey(id))
                {
                    return positon[id];
                }

                return "Not found positon.";
            }
        }
        private void _loadcbChucVu()
        {
            List<ChucVu> listChucVu = new List<ChucVu>
        {
            new ChucVu(){ ID = 0, Name = Position.GetName(0) },
            new ChucVu(){ ID = 1, Name = Position.GetName(1) },
            new ChucVu(){ ID = 2, Name = Position.GetName(2) },
            new ChucVu(){ ID = 3, Name = Position.GetName(3) }
        };
            cbxChucVu.DataSource = listChucVu;
            cbxChucVu.DisplayMember = "Name";
            cbxChucVu.ValueMember = "ID";
            cbxChucVu.SelectedIndex = 0;
        }
        private void lstAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAccount.SelectedItems.Count == 0) return;
            ListViewItem selectedItem = lstAccount.SelectedItems[0];
            TaiKhoanDTO selectedAccount = (TaiKhoanDTO)selectedItem.Tag;
            txtNameAccount.Text = selectedAccount.ID.ToString();
            txtHoTen.Text = selectedAccount.TenTK;
            txtPassword.Text = selectedAccount.Password;
            txtSDT.Text = selectedAccount.SDT;
            txtAddress.Text = selectedAccount.DiaChi;
            txtCMND.Text = selectedAccount.CCCD;
            cbxChucVu.SelectedValue = selectedAccount.Quyen;
            txtSalary.Text = selectedAccount.LuongByCa.ToString();
            radHienAccount.Checked = selectedAccount.TrangThai == 1;
            radAnAccount.Checked = selectedAccount.TrangThai == 0;
            btnAddAccount.Enabled = false;
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if 

                    (string.IsNullOrWhiteSpace(txtNameAccount.Text) ||
                     string.IsNullOrWhiteSpace(txtHoTen.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) ||
                    string.IsNullOrWhiteSpace(txtSDT.Text) || string.IsNullOrWhiteSpace(txtAddress.Text) ||
                    string.IsNullOrWhiteSpace(txtCMND.Text) || cbxChucVu.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                TaiKhoanDTO newAccount = new TaiKhoanDTO
                {
                    ID = Convert.ToInt32(txtNameAccount.Text),
                    TenTK = txtHoTen.Text,
                    Password = txtPassword.Text,
                    SDT = txtSDT.Text,
                    DiaChi = txtAddress.Text,
                    CCCD = txtCMND.Text,
                    Quyen = Convert.ToInt32(cbxChucVu.SelectedValue),
                    LuongByCa = Convert.ToDouble(txtSalary.Text)
                };
                if (radHienAccount.Checked) // 0 không hoạt động 1: hoạt động//
                {
                    newAccount.TrangThai = 1;
                }
                else
                    newAccount.TrangThai = 0;
                if (TaiKhoanBLL.AddTaiKhoan(newAccount))
                {
                    txtNameAccount.Enabled = true;
                    btnAddAccount.Enabled = true;

                    _loadTaiKhoan();
                    _deleteTextAccount();
                    _showDanhSachNhanVien();

                    MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thêm tài khoản thất bại! Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNameAccount.Text) ||
                     string.IsNullOrWhiteSpace(txtHoTen.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) ||
                    string.IsNullOrWhiteSpace(txtSDT.Text) || string.IsNullOrWhiteSpace(txtAddress.Text) ||
                    string.IsNullOrWhiteSpace(txtCMND.Text) || cbxChucVu.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                TaiKhoanDTO updateAccount = new TaiKhoanDTO
                {
                    ID = Convert.ToInt32(txtNameAccount.Text),
                    TenTK = txtHoTen.Text,
                    Password = txtPassword.Text,
                    SDT = txtSDT.Text,
                    DiaChi = txtAddress.Text,
                    CCCD = txtCMND.Text,
                    Quyen = Convert.ToInt32(cbxChucVu.SelectedValue),
                    LuongByCa = Convert.ToDouble(txtSalary.Text)
                };

                updateAccount.TrangThai = radHienAccount.Checked ? 1 : 0;

                if (TaiKhoanBLL.UpdateTaiKhoan(updateAccount))
                {
                    _loadTaiKhoan();
                    _deleteTextAccount();
                    _showDanhSachNhanVien();
                    txtNameAccount.Enabled = true;
                    btnAddAccount.Enabled = true;

                    MessageBox.Show("Cập nhật tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cập nhật tài khoản thất bại! Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void _deleteTextAccount()
        {
            txtNameAccount.Text = "";
            txtHoTen.Text = "";
            txtPassword.Text = "";
            txtSDT.Text = "";
            txtAddress.Text = "";
            txtCMND.Text = "";
            txtSalary.Text = "";
            radHienAccount.Checked = true;
        }

        private void btnNewAccount_Click(object sender, EventArgs e)
        {
            _deleteTextAccount();
            txtNameAccount.Enabled = true;
            btnAddAccount.Enabled = true;

        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstAccount.SelectedItems.Count > 0)
                {
                    TaiKhoanDTO tk = lstAccount.SelectedItems[0].Tag as TaiKhoanDTO;
                    DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa tài khoản người này '{tk.TenTK}' không?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        if (TaiKhoanBLL.DeleteTaiKhoan(tk.ID))
                        {
                            btnDeleteAccount.Enabled = false;
                            _deleteTextAccount();
                            _loadTaiKhoan();
                            _showDanhSachNhanVien();
                            txtNameAccount.Enabled = true;
                            btnAddAccount.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Xóa tài khoản thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn tài khoản để thực hiện!", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void _searchAccount(string keyword)
        {
            lstAccount.Items.Clear();
            List<TaiKhoanDTO> listTaiKhoan = TaiKhoanBLL.SearchTaiKhoanByName(keyword);

            int stt = 1;
            foreach (TaiKhoanDTO account in listTaiKhoan)
            {
                ListViewItem item = new ListViewItem(stt.ToString());

                if (account.ID == 0)
                {
                    item.SubItems.Add("Admin");
                }
                else
                {
                    item.SubItems.Add(account.ID.ToString());
                }

                item.SubItems.Add(account.TenTK);
                item.SubItems.Add(account.SDT);
                item.SubItems.Add(account.DiaChi);
                item.SubItems.Add(account.Password);
                item.SubItems.Add(account.CCCD);
                item.SubItems.Add(Position.GetName(account.Quyen));
                item.SubItems.Add(account.LuongByCa.ToString("###,### VNĐ"));
                item.SubItems.Add(account.TrangThai == 1 ? "Đang hoạt động" : "Ngừng hoạt động");

                // 🔹 Gán Tag để tránh lỗi NullReferenceException
                item.Tag = account;

                lstAccount.Items.Add(item);
                stt++;
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string keyword = txtTimTK.Text.Trim();
            _searchAccount(keyword);
            txtNameAccount.Enabled = false;
        }
        private void btnLamMoiTK_Click(object sender, EventArgs e)
        {
            _loadTaiKhoan();
            txtTimTK.Clear();
            txtNameAccount.Enabled = true;
            btnAddAccount.Enabled = true;
        }
        #endregion

        #region Tính lương
        private void _showDanhSachNhanVien()
        {
            string query = "SELECT MATAIKHOAN, TEN FROM TAIKHOAN";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            cbxNhanVien.DataSource = data;
            cbxNhanVien.DisplayMember = "TEN";
            cbxNhanVien.ValueMember = "MATAIKHOAN";
        }

        private void btnThemLuong_Click(object sender, EventArgs e)
        {
            if (cbxNhanVien.SelectedItem == null || string.IsNullOrWhiteSpace(txtThang.Text) || string.IsNullOrWhiteSpace(txtCa.Text))
    {
        MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
        return;
    }

    int maTaiKhoan = Convert.ToInt32(cbxNhanVien.SelectedValue);
    int thang = Convert.ToInt32(txtThang.Text);
    int soCa = Convert.ToInt32(txtCa.Text);
    float luongCoBan = tinhLuongBLL.GetLuongCoBan(maTaiKhoan);
    float tongLuong = luongCoBan * soCa; // Tính tổng lương dựa trên số ca

    TinhLuongDTO luongMoi = new TinhLuongDTO
    {
        MaTaiKhoan = maTaiKhoan,
        Thang = thang,
        Nam = DateTime.Now.Year,
        Ca = soCa,
        Tong = tongLuong, // ✅ Lưu tổng lương vào DTO
        GhiChu = txtGhiChu.Text,
        TinhTrang = 0,
        NgayTao = DateTime.Now
    };

    if (tinhLuongBLL.ThemLuong(luongMoi))
    {
        // Cập nhật UI
        ListViewItem item = new ListViewItem(new string[]
        {
            tinhLuongBLL.GetTenNhanVien(maTaiKhoan),
            tinhLuongBLL.GetSDTNhanVien(maTaiKhoan),
            tinhLuongBLL.GetDiaChiNhanVien(maTaiKhoan),
            luongCoBan.ToString(),
            thang.ToString(),
            DateTime.Now.Year.ToString(),
            soCa.ToString(),
            tongLuong.ToString("N0"), // ✅ Hiển thị tổng lương
            "Chưa thanh toán"
        });
        lstLuongNhanVien.Items.Add(item);
        MessageBox.Show("Thêm lương thành công!");
        _loadDanhSachTinhLuong();
    }
    else
    {
        MessageBox.Show("Thêm lương thất bại!");
    }
        }

        private void _loadDanhSachTinhLuong(string keyword = "")
        {
            lstLuongNhanVien.Items.Clear();
            List<TinhLuongDTO> danhSachLuong = tinhLuongBLL.GetAllTinhLuong();

            foreach (TinhLuongDTO luong in danhSachLuong)
            {
                string tenNhanVien = tinhLuongBLL.GetTenNhanVien(luong.MaTaiKhoan);
                if (!string.IsNullOrEmpty(keyword) && !tenNhanVien.ToLower().Contains(keyword))
                {
                    continue; // Bỏ qua nhân viên không khớp từ khóa tìm kiếm
                }

                string sdt = tinhLuongBLL.GetSDTNhanVien(luong.MaTaiKhoan);
                string diaChi = tinhLuongBLL.GetDiaChiNhanVien(luong.MaTaiKhoan);
                float luongCoBan = tinhLuongBLL.GetLuongCoBan(luong.MaTaiKhoan);
                float tongLuong = luongCoBan * luong.Ca;

                ListViewItem item = new ListViewItem((lstLuongNhanVien.Items.Count + 1).ToString());
                item.SubItems.Add(luong.MaTaiKhoan.ToString());
                item.SubItems.Add(tenNhanVien);
                item.SubItems.Add(sdt);
                item.SubItems.Add(diaChi);
                item.SubItems.Add(luong.Thang.ToString());
                item.SubItems.Add(luong.Nam.ToString());
                item.SubItems.Add(luongCoBan.ToString("N0"));
                item.SubItems.Add(luong.Ca.ToString());
                item.SubItems.Add(tongLuong.ToString("N0"));
                item.SubItems.Add(luong.TinhTrang == 0 ? "Chưa thanh toán" : "Đã thanh toán");
                item.SubItems.Add(luong.GhiChu);

                lstLuongNhanVien.Items.Add(item);
            }
        }


        private void btnTinhLuong_Click(object sender, EventArgs e)
        {
            if (lstLuongNhanVien.SelectedItems.Count > 0)
            {
                int maTaiKhoan = Convert.ToInt32(lstLuongNhanVien.SelectedItems[0].SubItems[1].Text);
                int thang = Convert.ToInt32(lstLuongNhanVien.SelectedItems[0].SubItems[5].Text);
                int nam = Convert.ToInt32(lstLuongNhanVien.SelectedItems[0].SubItems[6].Text);
                if (tinhLuongBLL.ThanhToanLuong(maTaiKhoan, thang, nam))
                {
                    MessageBox.Show("Thanh toán thành công!");
                    _loadDanhSachTinhLuong(); 
                }
                else
                {
                    MessageBox.Show("Thanh toán thất bại!");
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            _loadDanhSachTinhLuong(keyword);
        }

        private void frmAdmin_Shown(object sender, EventArgs e)
        {
            txtThang.Text = DateTime.Now.Month.ToString();
        }
        #endregion

        #region Báo cáo doanh thu
        private void _loadComboBox()
        {
            // Tháng từ 1 đến 12
            for (int i = 1; i <= 12; i++)
            {
                cbxMonth.Items.Add(i);
            }

            // Năm từ 2020 đến năm hiện tại
            int namHienTai = DateTime.Now.Year;
            for (int i = 2020; i <= namHienTai; i++)
            {
                cbxYears.Items.Add(i);
            }

            // Chọn mặc định tháng hiện tại và năm hiện tại
            cbxMonth.SelectedItem = DateTime.Now.Month;
            cbxYears.SelectedItem = namHienTai;
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            int thang = Convert.ToInt32(cbxMonth.SelectedItem);
            int nam = Convert.ToInt32(cbxYears.SelectedItem);
            List<BaoCaoDTO> reports = BaoCaoBLL.Instance.LoadBaoCao(thang, nam);
            lstDoanhThu.Items.Clear();
            float sumDoanhThu = 0;
            int stt = 1; // Bắt đầu STT từ 1

            foreach (BaoCaoDTO report in reports)
            {
                ListViewItem item = new ListViewItem(stt.ToString()); // Cột STT
                item.SubItems.Add(report.Thang.ToString());
                item.SubItems.Add(report.Nam.ToString());
                item.SubItems.Add(report.TongTienBan.ToString());
                item.SubItems.Add(report.TongNguyenVatLieu.ToString());
                item.SubItems.Add(report.TongLuongNhanVien.ToString());
                item.SubItems.Add(report.TongDoanhThuThang.ToString());
                lstDoanhThu.Items.Add(item);
                sumDoanhThu += report.TongDoanhThuThang;
                stt++; // Tăng STT cho dòng tiếp theo
            }
            txtTongDoanhThu.Text = BaoCaoBLL.Instance.GetTongDoanhThuNam(nam).ToString();
        }

        private void btnLammoiDoanhthu_Click(object sender, EventArgs e)
        {
            lstDoanhThu.Items.Clear();
            txtTongDoanhThu.Clear();
            cbxMonth.SelectedItem = DateTime.Now.Month;
            cbxYears.SelectedItem = DateTime.Now.Year;
        }

        #endregion

        private void btnThoatAdmin_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmTrangChu frmTrangChu = new FrmTrangChu();
            frmTrangChu.Show();
        }
    }
}
 


