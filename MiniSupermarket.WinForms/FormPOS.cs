using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormPOS : Form
    {
        private readonly List<CartItemDto> _cart = new();

        public FormPOS()
        {
            InitializeComponent();
            SetupCartGrid();
        }

        private void SetupCartGrid()
        {
            dgvCart.AutoGenerateColumns = false;
            dgvCart.Columns.Clear();
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductId", HeaderText = "Mã SP", Width = 80 });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "Tên Sản Phẩm", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UnitPrice", HeaderText = "Đơn Giá", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "SL", Width = 70 });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TotalPrice", HeaderText = "Thành Tiền", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
        }

        // Bắt sự kiện quét mã Barcode
        private async void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                string barcode = txtBarcode.Text.Trim();
                txtBarcode.Clear();
                await AddProductToCartByBarcodeAsync(barcode);
            }
        }

        private async Task AddProductToCartByBarcodeAsync(string barcode)
        {
            try
            {
                // Buổi 5: tra mã vạch qua ADO.NET SqlDataReader.
                using var response = await ApiClientService.Client.GetAsync(
                    $"ado-products/barcode/{Uri.EscapeDataString(barcode)}");
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Không tìm thấy sản phẩm có mã vạch này!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                response.EnsureSuccessStatusCode();
                var product = await response.Content.ReadFromJsonAsync<ProductPosDto>();
                if (product == null)
                {
                    MessageBox.Show("Không tìm thấy sản phẩm có mã vạch này!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (product.StockQuantity <= 0)
                {
                    MessageBox.Show("Sản phẩm đã hết hàng.", "Hết tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var existingItem = _cart.FirstOrDefault(c => c.ProductId == product.ProductId);
                if (existingItem != null)
                {
                    if (existingItem.Quantity >= product.StockQuantity)
                    {
                        MessageBox.Show("Số lượng trong giỏ đã bằng tồn kho.", "Không đủ tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    existingItem.Quantity++;
                }
                else
                {
                    _cart.Add(new CartItemDto
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        UnitPrice = product.Price,
                        Quantity = 1
                    });
                }

                UpdateCartDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tra cứu sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCartDisplay()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = _cart;

            decimal total = _cart.Sum(x => x.TotalPrice);
            lblTotalAmount.Text = $"{total:N0} đ";
            CalculateChange();
        }

        private void txtCashReceived_TextChanged(object sender, EventArgs e)
        {
            CalculateChange();
        }

        private async void txtCustomerPhone_Leave(object sender, EventArgs e)
        {
            var phone = txtCustomerPhone.Text.Trim();
            if (phone.Length == 0)
            {
                lblCustomerName.Text = "Khách vãng lai";
                return;
            }

            try
            {
                var customers = await ApiClientService.Client.GetFromJsonAsync<List<CustomerLookupDto>>(
                    $"customers/search?keyword={Uri.EscapeDataString(phone)}");
                var customer = customers?.FirstOrDefault(item => string.Equals(item.PhoneNumber, phone, StringComparison.Ordinal));
                lblCustomerName.Text = customer == null
                    ? "Không tìm thấy khách hàng"
                    : $"{customer.CustomerName} · {customer.MembershipRank} · {customer.RewardPoints} điểm";
            }
            catch (Exception ex)
            {
                lblCustomerName.Text = "Chưa tra cứu được khách hàng";
                MessageBox.Show("Không tra cứu được khách hàng: " + ex.Message,
                    "Lỗi tra cứu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CalculateChange()
        {
            decimal total = _cart.Sum(x => x.TotalPrice);
            if (decimal.TryParse(txtCashReceived.Text, out decimal cashReceived))
            {
                decimal change = cashReceived - total;
                lblChange.Text = change >= 0 ? $"{change:N0} đ" : "Chưa đủ tiền!";
                lblChange.ForeColor = change >= 0 ? Color.Green : Color.Red;
            }
            else
            {
                lblChange.Text = "0 đ";
            }
        }

        private async void btnCheckout_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var total = _cart.Sum(item => item.TotalPrice);
            if (!decimal.TryParse(txtCashReceived.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var cashReceived) || cashReceived < total)
            {
                MessageBox.Show("Tiền khách đưa chưa hợp lệ hoặc chưa đủ thanh toán.", "Chưa đủ tiền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var orderRequest = new
            {
                CashierUsername = SessionManager.CurrentUsername,
                CustomerPhone = txtCustomerPhone.Text.Trim(),
                Items = _cart.Select(i => new { i.ProductId, i.Quantity, i.UnitPrice }).ToList()
            };

            try
            {
                var response = await ApiClientService.Client.PostAsJsonAsync("checkout", orderRequest);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Thanh toán thành công và đã in hóa đơn!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _cart.Clear();
                    UpdateCartDisplay();
                    txtCashReceived.Text = "0";
                    txtCustomerPhone.Clear();
                    lblCustomerName.Text = "Khách vãng lai";
                }
                else
                {
                    MessageBox.Show("Thanh toán thất bại: " + await response.Content.ReadAsStringAsync(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (_cart.Count > 0)
            {
                var confirm = MessageBox.Show("Bạn có muốn hủy toàn bộ giỏ hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    _cart.Clear();
                    UpdateCartDisplay();
                }
            }
        }
    }

    public class CartItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
    }

    public class ProductPosDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }

    public class CustomerLookupDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string MembershipRank { get; set; } = string.Empty;
        public int RewardPoints { get; set; }
    }
}
