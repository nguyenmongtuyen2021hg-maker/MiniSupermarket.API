using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormLogin : Form
    {
        // Danh sách 15 tài khoản mẫu theo đề bài Buổi 4 để chọn nhanh
        private readonly List<AccountSample> _sampleAccounts = new()
        {
            new("admin01", "123456", "Nguyễn Quản Trị", "Admin"),
            new("admin02", "123456", "Trần Giám Đốc", "Admin"),
            new("cashier01", "123456", "Lê Thu Ngân", "Cashier"),
            new("cashier02", "123456", "Phạm Bán Hàng", "Cashier"),
            new("cashier03", "123456", "Hoàng Thu Ngân", "Cashier"),
            new("cashier04", "123456", "Vũ Thị Quầy", "Cashier"),
            new("cashier05", "123456", "Đỗ Bán Lẻ", "Cashier"),
            new("ware01", "123456", "Ngô Quản Kho", "Warehouse"),
            new("ware02", "123456", "Bùi Kiểm Kê", "Warehouse"),
            new("ware03", "123456", "Dương Thủ Kho", "Warehouse"),
            new("ware04", "123456", "Lý Nhập Hàng", "Warehouse"),
            new("admin_backup", "123456", "Đặng Hỗ Trợ", "Admin"),
            new("cashier06", "123456", "Hồ Ca Chiều", "Cashier"),
            new("ware05", "123456", "Trương Vận Chuyển", "Warehouse"),
            new("supervisor", "123456", "Mai Giám Sát", "Admin")
        };

        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            cboQuickAccount.Items.Clear();
            foreach (var acc in _sampleAccounts)
            {
                cboQuickAccount.Items.Add($"{acc.Username} ({acc.Role}) - {acc.FullName}");
            }
            cboQuickAccount.SelectedIndex = 0; // Mặc định chọn admin01
        }

        private void cboQuickAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboQuickAccount.SelectedIndex >= 0)
            {
                var acc = _sampleAccounts[cboQuickAccount.SelectedIndex];
                txtUsername.Text = acc.Username;
                txtPassword.Text = acc.Password;
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "Đang đăng nhập...";

            try
            {
                // Thử gọi API đăng nhập backend
                var response = await ApiClientService.Client.PostAsJsonAsync("users/login", new { Username = username, Password = password });
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    if (data != null)
                    {
                        SessionManager.CurrentUsername = data.Username;
                        SessionManager.CurrentFullName = data.FullName;
                        SessionManager.CurrentRole = data.Role;
                        SessionManager.JwtToken = data.Token;

                        NavigateToMainShell();
                        return;
                    }
                }
            }
            catch
            {
                // Nếu chưa bật Web API, dùng cơ chế xác thực offline theo danh sách 15 tài khoản mẫu
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "ĐĂNG NHẬP";
            }

            // Kiểm tra theo danh sách 15 tài khoản chuẩn
            var localMatch = _sampleAccounts.FirstOrDefault(a =>
                string.Equals(a.Username, username, StringComparison.OrdinalIgnoreCase) && a.Password == password);

            if (localMatch != null)
            {
                SessionManager.CurrentUsername = localMatch.Username;
                SessionManager.CurrentFullName = localMatch.FullName;
                SessionManager.CurrentRole = localMatch.Role;
                SessionManager.JwtToken = "local-token";

                NavigateToMainShell();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NavigateToMainShell()
        {
            this.Hide();
            var mainShell = new FormMainShell();
            mainShell.FormClosed += (s, args) => this.Close();
            mainShell.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private record AccountSample(string Username, string Password, string FullName, string Role);

        private class LoginResponse
        {
            public string Username { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string Role { get; set; } = string.Empty;
            public string Token { get; set; } = string.Empty;
        }
    }
}
