using System.Net.Http.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormCustomerManagement : Form
    {
        // Khởi tạo HttpClient kết nối trực tiếp đến Web API
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7118/api/")
        };

        public FormCustomerManagement()
        {
            InitializeComponent();
        }

        // Sự kiện Form load: Tự động tải danh sách khách hàng từ API
        private async void FormCustomerManagement_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        // Gọi API GET lấy danh sách và hiển thị lên DataGridView
        private async Task LoadDataAsync()
        {
            try
            {
                var customers = await _client.GetFromJsonAsync<List<CustomerDto>>("customers");
                dgvCustomers.DataSource = customers;
                ConfigureGridColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối Server: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Đổi tên tiêu đề cột DataGridView cho thân thiện
        private void ConfigureGridColumns()
        {
            if (dgvCustomers.Columns["CustomerId"] != null)
                dgvCustomers.Columns["CustomerId"].HeaderText = "Mã KH";
            if (dgvCustomers.Columns["CustomerName"] != null)
                dgvCustomers.Columns["CustomerName"].HeaderText = "Tên Khách Hàng";
            if (dgvCustomers.Columns["PhoneNumber"] != null)
                dgvCustomers.Columns["PhoneNumber"].HeaderText = "Số Điện Thoại";
            if (dgvCustomers.Columns["Address"] != null)
                dgvCustomers.Columns["Address"].HeaderText = "Địa Chỉ";
            if (dgvCustomers.Columns["RewardPoints"] != null)
                dgvCustomers.Columns["RewardPoints"].HeaderText = "Điểm Thưởng";
            if (dgvCustomers.Columns["MembershipRank"] != null)
                dgvCustomers.Columns["MembershipRank"].HeaderText = "Hạng Thẻ";
        }

        // Tải lại danh sách
        private async void btnLoad_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            await LoadDataAsync();
        }

        // Khi click vào 1 dòng trong bảng: Đổ dữ liệu lên các ô nhập
        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCustomers.Rows[e.RowIndex];
                txtCustomerId.Text = row.Cells["CustomerId"].Value?.ToString() ?? string.Empty;
                txtCustomerName.Text = row.Cells["CustomerName"].Value?.ToString() ?? string.Empty;
                txtPhoneNumber.Text = row.Cells["PhoneNumber"].Value?.ToString() ?? string.Empty;
                txtAddress.Text = row.Cells["Address"]?.Value?.ToString() ?? string.Empty;
                txtRewardPoints.Text = row.Cells["RewardPoints"].Value?.ToString() ?? "0";
                txtMembershipRank.Text = row.Cells["MembershipRank"].Value?.ToString() ?? "Chuẩn";
            }
        }

        // THÊM MỚI (POST /api/customers)
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text) || string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên khách hàng và Số điện thoại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int points = int.TryParse(txtRewardPoints.Text, out var p) ? p : 0;
            var newCustomer = new
            {
                CustomerName = txtCustomerName.Text.Trim(),
                PhoneNumber = txtPhoneNumber.Text.Trim(),
                Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim(),
                RewardPoints = points,
                MembershipRank = string.IsNullOrWhiteSpace(txtMembershipRank.Text) ? "Chuẩn" : txtMembershipRank.Text.Trim()
            };

            try
            {
                var response = await _client.PostAsJsonAsync("customers", newCustomer);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Thêm mới khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                    ClearInputs();
                }
                else
                {
                    var err = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Thêm mới thất bại: " + err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối Server: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // CẬP NHẬT (PUT /api/customers/{id})
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerId.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa trong bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = int.Parse(txtCustomerId.Text);
            int points = int.TryParse(txtRewardPoints.Text, out var p) ? p : 0;

            var updateCustomer = new
            {
                CustomerId = id,
                CustomerName = txtCustomerName.Text.Trim(),
                PhoneNumber = txtPhoneNumber.Text.Trim(),
                Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim(),
                RewardPoints = points,
                MembershipRank = string.IsNullOrWhiteSpace(txtMembershipRank.Text) ? "Chuẩn" : txtMembershipRank.Text.Trim()
            };

            try
            {
                var response = await _client.PutAsJsonAsync($"customers/{id}", updateCustomer);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cập nhật thông tin khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối Server: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // XÓA (DELETE /api/customers/{id})
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerId.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa trong bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = int.Parse(txtCustomerId.Text);
            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa khách hàng ID = {id} không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                var response = await _client.DeleteAsync($"customers/{id}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối Server: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // TÌM KIẾM (GET /api/customers/search?keyword=...)
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                await LoadDataAsync();
                return;
            }

            try
            {
                var result = await _client.GetFromJsonAsync<List<CustomerDto>>($"customers/search?keyword={Uri.EscapeDataString(keyword)}");
                dgvCustomers.DataSource = result;
                ConfigureGridColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa trắng các ô nhập
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            txtCustomerId.Clear();
            txtCustomerName.Clear();
            txtPhoneNumber.Clear();
            txtAddress.Clear();
            txtRewardPoints.Text = "0";
            txtMembershipRank.Text = "Chuẩn";
        }
    }

    // Lớp DTO nhận dữ liệu khách hàng từ API
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; }
        public int RewardPoints { get; set; }
        public string MembershipRank { get; set; } = string.Empty;
    }
}
