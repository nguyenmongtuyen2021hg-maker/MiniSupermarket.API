using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormQuickReport : Form
    {
        public FormQuickReport()
        {
            InitializeComponent();
        }

        private async void FormQuickReport_Load(object sender, EventArgs e)
        {
            // Kiểm tra phân quyền cấp Action (Bài tập mở rộng)
            if (!string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Bạn không có quyền xem dữ liệu tài chính của siêu thị!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close();
                return;
            }

            await LoadReportDataAsync();
        }

        private async void btnRunReport_Click(object sender, EventArgs e)
        {
            await LoadReportDataAsync();
        }

        private async Task LoadReportDataAsync()
        {
            btnRunReport.Enabled = false;
            lblTotalOrders.Text = "…";
            lblTotalRevenue.Text = "…";
            lblBestSeller.Text = "Đang tải báo cáo...";

            try
            {
                var date = dtpReportDate.Value.ToString("yyyy-MM-dd");
                var report = await ApiClientService.Client.GetFromJsonAsync<QuickReportDto>(
                    $"orders/report?date={date}");

                if (report == null)
                {
                    throw new InvalidOperationException("API không trả về dữ liệu báo cáo.");
                }

                lblTotalOrders.Text = report.OrderCount.ToString("N0");
                lblTotalRevenue.Text = $"{report.TotalRevenue:N0} đ";
                lblBestSeller.Text = report.BestSeller;
            }
            catch (Exception ex)
            {
                lblTotalOrders.Text = "—";
                lblTotalRevenue.Text = "—";
                lblBestSeller.Text = "Không tải được báo cáo";
                MessageBox.Show("Không tải được báo cáo doanh thu từ database: " + ex.Message,
                    "Lỗi báo cáo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRunReport.Enabled = true;
            }
        }

        private sealed class QuickReportDto
        {
            public int OrderCount { get; set; }
            public decimal TotalRevenue { get; set; }
            public string BestSeller { get; set; } = string.Empty;
        }
    }
}
