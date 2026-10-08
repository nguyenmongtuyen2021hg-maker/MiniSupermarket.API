using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormUserManagement : Form
    {
        public FormUserManagement()
        {
            InitializeComponent();
            cboRole.Items.AddRange(new string[] { "Admin", "Cashier", "Warehouse" });
            cboRole.SelectedIndex = 1; // Mặc định Cashier
        }

        private async void FormUserManagement_Load(object sender, EventArgs e)
        {
            await LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                var users = await ApiClientService.Client.GetFromJsonAsync<List<UserDto>>("users");
                dgvUsers.DataSource = users;
                ConfigureGridColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy danh sách tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGridColumns()
        {
            if (dgvUsers.Columns["Id"] != null)
                dgvUsers.Columns["Id"].HeaderText = "ID";
            if (dgvUsers.Columns["Username"] != null)
                dgvUsers.Columns["Username"].HeaderText = "Tên Đăng Nhập";
            if (dgvUsers.Columns["FullName"] != null)
                dgvUsers.Columns["FullName"].HeaderText = "Họ Và Tên";
            if (dgvUsers.Columns["Role"] != null)
                dgvUsers.Columns["Role"].HeaderText = "Vai Trò (Role)";
            if (dgvUsers.Columns["IsActive"] != null)
                dgvUsers.Columns["IsActive"].HeaderText = "Hoạt Động";
            if (dgvUsers.Columns["Password"] != null)
                dgvUsers.Columns["Password"].Visible = false;
        }

        private async void btnAddUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Tên đăng nhập và mật khẩu không được trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newUser = new
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text.Trim(),
                FullName = string.IsNullOrWhiteSpace(txtFullName.Text) ? txtUsername.Text.Trim() : txtFullName.Text.Trim(),
                Role = cboRole.SelectedItem?.ToString() ?? "Cashier",
                IsActive = true
            };

            try
            {
                var res = await ApiClientService.Client.PostAsJsonAsync("users", newUser);
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Tạo tài khoản mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadUsersAsync();
                    txtUsername.Clear();
                    txtPassword.Text = "123456";
                    txtFullName.Clear();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại trong hệ thống!", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            await LoadUsersAsync();
        }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
