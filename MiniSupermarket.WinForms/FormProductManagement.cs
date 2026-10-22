using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniSupermarket.WinForms.Models;

namespace MiniSupermarket.WinForms
{
    public partial class FormProductManagement : Form
    {
        private const int PageSize = 5;
        private int _currentPage = 1;
        private int _totalPages;
        private int _totalItems;
        private bool _isDataLoaded;
        private bool _isReadOnlyForCashier;

        public FormProductManagement()
        {
            InitializeComponent();
        }

        private async void FormProductManagement_Load(object sender, EventArgs e)
        {
            _isReadOnlyForCashier = string.Equals(
                SessionManager.CurrentRole, "Cashier", StringComparison.OrdinalIgnoreCase);

            if (_isReadOnlyForCashier)
            {
                panelDetail.Visible = false;
                panelCenter.Dock = DockStyle.Fill;
                dgvProducts.ReadOnly = true;
                dgvProducts.AllowUserToAddRows = false;
                dgvProducts.AllowUserToDeleteRows = false;
                cboFilterCategory.Visible = false;
                btnSearch.Location = new System.Drawing.Point(310, 12);
                btnLoad.Location = new System.Drawing.Point(405, 12);
            }

            if (!await ApiClientService.EnsureConnectionAsync())
            {
                MessageBox.Show("Không kết nối được API. Hãy chạy MiniSupermarket.API rồi tải lại sản phẩm.",
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // API danh mục giới hạn Admin/Warehouse; cashier chỉ cần đọc danh sách sản phẩm.
            if (!_isReadOnlyForCashier)
            {
                await LoadCategoriesToComboAsync();
            }

            _isDataLoaded = true;
            await LoadProductsAsync();
        }

        private async Task LoadCategoriesToComboAsync()
        {
            try
            {
                using var response = await ApiClientService.Client.GetAsync("categories");
                if (!response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"API trả {(int)response.StatusCode}: {body}");
                }

                var categories = await response.Content.ReadFromJsonAsync<List<CategoryDto>>() ?? new();
                if (categories.Count == 0)
                {
                    cboCategory.DataSource = null;
                    cboFilterCategory.DataSource = null;
                    return;
                }

                cboCategory.DisplayMember = nameof(CategoryDto.CategoryName);
                cboCategory.ValueMember = nameof(CategoryDto.CategoryId);
                cboCategory.DataSource = new List<CategoryDto>(categories);

                var filterCategories = new List<CategoryDto>
                {
                    new() { CategoryId = 0, CategoryName = "-- Tất cả nhóm hàng --" }
                };
                filterCategories.AddRange(categories);
                cboFilterCategory.DisplayMember = nameof(CategoryDto.CategoryName);
                cboFilterCategory.ValueMember = nameof(CategoryDto.CategoryId);
                cboFilterCategory.DataSource = filterCategories;
                cboFilterCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh mục sản phẩm: " + ex.Message,
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public Task LoadProductsAsync() => LoadProductsPagedAsync();

        private async Task LoadProductsPagedAsync()
        {
            try
            {
                var keyword = Uri.EscapeDataString(txtSearchBarcode.Text.Trim());
                var categoryId = _isReadOnlyForCashier ? 0 : GetSelectedCategoryId(cboFilterCategory);
                var url = $"products/paged?keyword={keyword}&pageNumber={_currentPage}&pageSize={PageSize}&categoryId={categoryId}";

                using var response = await ApiClientService.Client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"API trả {(int)response.StatusCode}: {body}");
                }

                var result = await response.Content.ReadFromJsonAsync<PagedResultDto<ProductListItemDto>>();
                if (result == null)
                {
                    throw new InvalidOperationException("API không trả về dữ liệu phân trang.");
                }

                _currentPage = result.CurrentPage;
                _totalPages = result.TotalPages;
                _totalItems = result.TotalItems;
                dgvProducts.DataSource = null;
                dgvProducts.DataSource = result.Items;
                ConfigureGridColumns();
                UpdatePagingControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải sản phẩm: " + ex.Message,
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePagingControls()
        {
            lblPaging.Text = _totalPages == 0
                ? "Trang 0 / 0 (Tổng: 0 sản phẩm)"
                : $"Trang {_currentPage} / {_totalPages} (Tổng: {_totalItems} sản phẩm)";
            btnPrevious.Enabled = _currentPage > 1 && _totalPages > 0;
            btnNext.Enabled = _currentPage < _totalPages;
        }

        private void ConfigureGridColumns()
        {
            if (dgvProducts.Columns[nameof(ProductListItemDto.ProductId)] != null)
            {
                dgvProducts.Columns[nameof(ProductListItemDto.ProductId)].HeaderText = "Mã SP";
                dgvProducts.Columns[nameof(ProductListItemDto.ProductId)].Width = 70;
            }
            if (dgvProducts.Columns[nameof(ProductListItemDto.Barcode)] != null)
            {
                dgvProducts.Columns[nameof(ProductListItemDto.Barcode)].HeaderText = "Mã vạch";
                dgvProducts.Columns[nameof(ProductListItemDto.Barcode)].Width = 120;
            }
            if (dgvProducts.Columns[nameof(ProductListItemDto.ProductName)] != null)
            {
                dgvProducts.Columns[nameof(ProductListItemDto.ProductName)].HeaderText = "Tên sản phẩm";
                dgvProducts.Columns[nameof(ProductListItemDto.ProductName)].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dgvProducts.Columns[nameof(ProductListItemDto.Price)] != null)
            {
                dgvProducts.Columns[nameof(ProductListItemDto.Price)].HeaderText = "Đơn giá (đ)";
                dgvProducts.Columns[nameof(ProductListItemDto.Price)].Width = 110;
                dgvProducts.Columns[nameof(ProductListItemDto.Price)].DefaultCellStyle.Format = "N0";
                dgvProducts.Columns[nameof(ProductListItemDto.Price)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvProducts.Columns[nameof(ProductListItemDto.StockQuantity)] != null)
            {
                dgvProducts.Columns[nameof(ProductListItemDto.StockQuantity)].HeaderText = "Tồn kho";
                dgvProducts.Columns[nameof(ProductListItemDto.StockQuantity)].Width = 85;
                dgvProducts.Columns[nameof(ProductListItemDto.StockQuantity)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvProducts.Columns[nameof(ProductListItemDto.CategoryName)] != null)
            {
                dgvProducts.Columns[nameof(ProductListItemDto.CategoryName)].HeaderText = "Nhóm hàng";
                dgvProducts.Columns[nameof(ProductListItemDto.CategoryName)].Width = 180;
            }
            if (dgvProducts.Columns[nameof(ProductListItemDto.CategoryId)] != null)
            {
                dgvProducts.Columns[nameof(ProductListItemDto.CategoryId)].Visible = false;
            }
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_isReadOnlyForCashier || e.RowIndex < 0 || e.RowIndex >= dgvProducts.Rows.Count)
            {
                return;
            }

            var row = dgvProducts.Rows[e.RowIndex];
            txtId.Text = row.Cells[nameof(ProductListItemDto.ProductId)].Value?.ToString() ?? string.Empty;
            txtBarcode.Text = row.Cells[nameof(ProductListItemDto.Barcode)].Value?.ToString() ?? string.Empty;
            txtProductName.Text = row.Cells[nameof(ProductListItemDto.ProductName)].Value?.ToString() ?? string.Empty;

            if (decimal.TryParse(row.Cells[nameof(ProductListItemDto.Price)].Value?.ToString(), out var price))
            {
                nudPrice.Value = Math.Min(Math.Max(price, nudPrice.Minimum), nudPrice.Maximum);
            }
            if (int.TryParse(row.Cells[nameof(ProductListItemDto.StockQuantity)].Value?.ToString(), out var stock))
            {
                nudStock.Value = Math.Min(Math.Max(stock, (int)nudStock.Minimum), (int)nudStock.Maximum);
            }
            if (int.TryParse(row.Cells[nameof(ProductListItemDto.CategoryId)].Value?.ToString(), out var categoryId))
            {
                cboCategory.SelectedValue = categoryId;
            }
        }

        private async void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_currentPage <= 1) return;
            _currentPage--;
            await LoadProductsPagedAsync();
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (_currentPage >= _totalPages) return;
            _currentPage++;
            await LoadProductsPagedAsync();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            _currentPage = 1;
            await LoadProductsPagedAsync();
        }

        private async void cboFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isDataLoaded) return;
            _currentPage = 1;
            await LoadProductsPagedAsync();
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            _isDataLoaded = false;
            txtSearchBarcode.Clear();
            if (cboFilterCategory.Items.Count > 0)
            {
                cboFilterCategory.SelectedIndex = 0;
            }
            _currentPage = 1;
            _isDataLoaded = true;
            await LoadProductsPagedAsync();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (_isReadOnlyForCashier) { ShowCashierReadOnlyMessage(); return; }
            if (string.IsNullOrWhiteSpace(txtBarcode.Text) || string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Mã vạch và tên sản phẩm không được để trống!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var categoryId = GetSelectedCategoryId(cboCategory);
            if (categoryId <= 0)
            {
                MessageBox.Show("Vui lòng chọn nhóm hàng.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newProduct = new
            {
                Barcode = txtBarcode.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                Price = nudPrice.Value,
                StockQuantity = (int)nudStock.Value,
                CategoryId = categoryId
            };

            try
            {
                using var response = await ApiClientService.Client.PostAsJsonAsync("products", newProduct);
                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync("Thêm sản phẩm thất bại", response);
                    return;
                }

                MessageBox.Show("Thêm sản phẩm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _currentPage = 1;
                ClearInputs();
                await LoadProductsPagedAsync();
            }
            catch (Exception ex)
            {
                ShowConnectionError(ex);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_isReadOnlyForCashier) { ShowCashierReadOnlyMessage(); return; }
            if (!int.TryParse(txtId.Text, out var id))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa trong bảng.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var categoryId = GetSelectedCategoryId(cboCategory);
            if (categoryId <= 0)
            {
                MessageBox.Show("Vui lòng chọn nhóm hàng.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var updatedProduct = new
            {
                ProductId = id,
                Barcode = txtBarcode.Text.Trim(),
                ProductName = txtProductName.Text.Trim(),
                Price = nudPrice.Value,
                StockQuantity = (int)nudStock.Value,
                CategoryId = categoryId
            };

            try
            {
                using var response = await ApiClientService.Client.PutAsJsonAsync($"products/{id}", updatedProduct);
                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync("Cập nhật sản phẩm thất bại", response);
                    return;
                }

                MessageBox.Show("Cập nhật sản phẩm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadProductsPagedAsync();
            }
            catch (Exception ex)
            {
                ShowConnectionError(ex);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_isReadOnlyForCashier) { ShowCashierReadOnlyMessage(); return; }
            if (!int.TryParse(txtId.Text, out var id))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa trong bảng.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Xác nhận xóa sản phẩm: {txtProductName.Text} (ID = {id})?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using var response = await ApiClientService.Client.DeleteAsync($"products/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    await ShowApiErrorAsync("Xóa sản phẩm thất bại", response);
                    return;
                }

                MessageBox.Show("Đã xóa sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                await LoadProductsPagedAsync();
            }
            catch (Exception ex)
            {
                ShowConnectionError(ex);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (_isReadOnlyForCashier) { ShowCashierReadOnlyMessage(); return; }
            ClearInputs();
        }

        private static async Task ShowApiErrorAsync(string title, HttpResponseMessage response)
        {
            var body = await response.Content.ReadAsStringAsync();
            MessageBox.Show($"{title}. Mã lỗi {(int)response.StatusCode} ({response.StatusCode}).\n{body}",
                "Lỗi máy chủ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private static void ShowConnectionError(Exception ex)
        {
            MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message,
                "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void ShowCashierReadOnlyMessage()
        {
            MessageBox.Show("Tài khoản Cashier chỉ được xem sản phẩm, không được thêm, sửa hoặc xóa.",
                "Chỉ xem sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private static int GetSelectedCategoryId(ComboBox comboBox)
        {
            if (comboBox.SelectedValue is int value) return value;
            if (comboBox.SelectedItem is CategoryDto category) return category.CategoryId;
            return comboBox.SelectedValue != null && int.TryParse(comboBox.SelectedValue.ToString(), out var parsed)
                ? parsed
                : 0;
        }
    }
}
