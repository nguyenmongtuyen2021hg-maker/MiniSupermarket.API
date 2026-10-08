using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniSupermarket.WinForms.Models;

namespace MiniSupermarket.WinForms
{
    public partial class FormProductManagement : Form
    {
        private List<ProductDto> _allProducts = new();
        private bool _isDataLoaded = false;

        public FormProductManagement()
        {
            InitializeComponent();
        }

        private async void FormProductManagement_Load(object sender, EventArgs e)
        {
            await ApiClientService.EnsureConnectionAsync();
            await LoadCategoriesToComboAsync();
            await LoadProductsAsync();
            _isDataLoaded = true;
        }

        private async Task LoadCategoriesToComboAsync()
        {
            try
            {
                var categories = await ApiClientService.Client.GetFromJsonAsync<List<CategoryDto>>("categories");
                if (categories != null && categories.Count > 0)
                {
                    // ComboBox chi tiết sản phẩm
                    cboCategory.DisplayMember = "CategoryName";
                    cboCategory.ValueMember = "CategoryId";
                    cboCategory.DataSource = new List<CategoryDto>(categories);

                    // ComboBox lọc nhóm hàng ở thanh tìm kiếm
                    var filterList = new List<CategoryDto>
                    {
                        new CategoryDto { CategoryId = 0, CategoryName = "-- Tất cả nhóm hàng --" }
                    };
                    filterList.AddRange(categories);

                    cboFilterCategory.DisplayMember = "CategoryName";
                    cboFilterCategory.ValueMember = "CategoryId";
                    cboFilterCategory.DataSource = filterList;
                    cboFilterCategory.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh mục sản phẩm: " + ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public async Task LoadProductsAsync()
        {
            try
            {
                var products = await ApiClientService.Client.GetFromJsonAsync<List<ProductDto>>("products");
                _allProducts = products ?? new();
                dgvProducts.DataSource = null;
                dgvProducts.DataSource = _allProducts;
                ConfigureGridColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ khi lấy danh sách sản phẩm: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGridColumns()
        {
            if (dgvProducts.Columns["ProductId"] != null)
            {
                dgvProducts.Columns["ProductId"].HeaderText = "Mã SP";
                dgvProducts.Columns["ProductId"].Width = 70;
            }
            if (dgvProducts.Columns["Barcode"] != null)
            {
                dgvProducts.Columns["Barcode"].HeaderText = "Mã Vạch";
                dgvProducts.Columns["Barcode"].Width = 120;
            }
            if (dgvProducts.Columns["ProductName"] != null)
            {
                dgvProducts.Columns["ProductName"].HeaderText = "Tên Sản Phẩm";
                dgvProducts.Columns["ProductName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dgvProducts.Columns["Price"] != null)
            {
                dgvProducts.Columns["Price"].HeaderText = "Đơn Giá (đ)";
                dgvProducts.Columns["Price"].Width = 110;
                dgvProducts.Columns["Price"].DefaultCellStyle.Format = "N0";
                dgvProducts.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvProducts.Columns["StockQuantity"] != null)
            {
                dgvProducts.Columns["StockQuantity"].HeaderText = "Tồn Kho";
                dgvProducts.Columns["StockQuantity"].Width = 85;
                dgvProducts.Columns["StockQuantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvProducts.Columns["CategoryName"] != null)
            {
                dgvProducts.Columns["CategoryName"].HeaderText = "Nhóm Hàng";
                dgvProducts.Columns["CategoryName"].Width = 180;
            }
            if (dgvProducts.Columns["CategoryId"] != null)
            {
                dgvProducts.Columns["CategoryId"].Visible = false;
            }
            if (dgvProducts.Columns["Category"] != null)
            {
                dgvProducts.Columns["Category"].Visible = false;
            }
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvProducts.Rows.Count)
            {
                var row = dgvProducts.Rows[e.RowIndex];
                txtId.Text = row.Cells["ProductId"].Value?.ToString() ?? string.Empty;
                txtBarcode.Text = row.Cells["Barcode"].Value?.ToString() ?? string.Empty;
                txtProductName.Text = row.Cells["ProductName"].Value?.ToString() ?? string.Empty;

                if (decimal.TryParse(row.Cells["Price"].Value?.ToString(), out decimal price))
                {
                    nudPrice.Value = Math.Min(Math.Max(price, nudPrice.Minimum), nudPrice.Maximum);
                }

                if (int.TryParse(row.Cells["StockQuantity"].Value?.ToString(), out int stock))
                {
                    nudStock.Value = Math.Min(Math.Max(stock, (int)nudStock.Minimum), (int)nudStock.Maximum);
                }

                if (int.TryParse(row.Cells["CategoryId"].Value?.ToString(), out int catId))
                {
                    cboCategory.SelectedValue = catId;
                }
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBarcode.Text) || string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Mã vạch và Tên sản phẩm không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int catId = GetSelectedCategoryId(cboCategory);
            if (catId <= 0) catId = 1;

            var newProd = new
            {
                Barcode = txtBarcode.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                Price = nudPrice.Value,
                StockQuantity = (int)nudStock.Value,
                CategoryId = catId
            };

            try
            {
                var res = await ApiClientService.Client.PostAsJsonAsync("products", newProd);
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Thêm mới sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadProductsAsync();
                    ClearInputs();
                }
                else
                {
                    var msg = await res.Content.ReadAsStringAsync();
                    MessageBox.Show("Thêm sản phẩm thất bại: " + msg, "Lỗi máy chủ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa trong bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = int.Parse(txtId.Text);
            int catId = GetSelectedCategoryId(cboCategory);
            if (catId <= 0) catId = 1;

            var updateProd = new
            {
                ProductId = id,
                Barcode = txtBarcode.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                Price = nudPrice.Value,
                StockQuantity = (int)nudStock.Value,
                CategoryId = catId
            };

            try
            {
                var res = await ApiClientService.Client.PutAsJsonAsync($"products/{id}", updateProd);
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cập nhật thông tin sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadProductsAsync();
                }
                else
                {
                    var msg = await res.Content.ReadAsStringAsync();
                    MessageBox.Show("Cập nhật thất bại: " + msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa trong bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = int.Parse(txtId.Text);
            var confirm = MessageBox.Show($"Xác nhận xóa sản phẩm: {txtProductName.Text} (ID = {id})?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    var res = await ApiClientService.Client.DeleteAsync($"products/{id}");
                    if (res.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Đã xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadProductsAsync();
                        ClearInputs();
                    }
                    else
                    {
                        var msg = await res.Content.ReadAsStringAsync();
                        MessageBox.Show("Xóa thất bại: " + msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void cboFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isDataLoaded)
            {
                ApplyFilter();
            }
        }

        private void ApplyFilter()
        {
            string keyword = txtSearchBarcode.Text.Trim().ToLower();
            int selectedCatId = GetSelectedCategoryId(cboFilterCategory);

            var filtered = _allProducts.Where(p =>
                (string.IsNullOrEmpty(keyword) || (p.ProductName != null && p.ProductName.ToLower().Contains(keyword)) || (p.Barcode != null && p.Barcode.Contains(keyword))) &&
                (selectedCatId == 0 || p.CategoryId == selectedCatId)
            ).ToList();

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = filtered;
            ConfigureGridColumns();
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            txtSearchBarcode.Clear();
            if (cboFilterCategory.Items.Count > 0)
            {
                cboFilterCategory.SelectedIndex = 0;
            }
            await LoadProductsAsync();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            txtId.Clear();
            txtBarcode.Clear();
            txtProductName.Clear();
            nudPrice.Value = 0;
            nudStock.Value = 0;
            if (cboCategory.Items.Count > 0)
            {
                cboCategory.SelectedIndex = 0;
            }
        }

        private int GetSelectedCategoryId(ComboBox cbo)
        {
            if (cbo.SelectedValue is int intVal)
            {
                return intVal;
            }
            if (cbo.SelectedItem is CategoryDto catDto)
            {
                return catDto.CategoryId;
            }
            if (cbo.SelectedValue != null && int.TryParse(cbo.SelectedValue.ToString(), out int parsed))
            {
                return parsed;
            }
            return 0;
        }
    }
}
