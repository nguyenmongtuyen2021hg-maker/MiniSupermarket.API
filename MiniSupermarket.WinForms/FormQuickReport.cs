using System;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormQuickReport : Form
    {
        public FormQuickReport()
        {
            InitializeComponent();
        }

        private void FormQuickReport_Load(object sender, EventArgs e)
        {
            // Kiểm tra phân quyền cấp Action (Bài tập mở rộng)
            if (!string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Bạn không có quyền xem dữ liệu tài chính của siêu thị!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close();
                return;
            }

            LoadReportData();
        }

        private void btnRunReport_Click(object sender, EventArgs e)
        {
            LoadReportData();
        }

        private void LoadReportData()
        {
            DateTime date = dtpReportDate.Value;
            var rand = new Random(date.Day + date.Month * 31);

            int orders = rand.Next(35, 85);
            decimal revenue = orders * rand.Next(150000, 320000);
            string[] topItems = {
                "Mì Hảo Hảo Tôm Chua Cay (250 gói)",
                "Nước ngọt Coca-Cola 320ml (180 lon)",
                "Sữa tươi Vinamilk 180ml (140 hộp)",
                "Gạo thơm ST25 túi 5kg (45 bao)",
                "Dầu ăn Tường An 1L (70 chai)"
            };
            string best = topItems[rand.Next(topItems.Length)];

            lblTotalOrders.Text = orders.ToString("N0");
            lblTotalRevenue.Text = $"{revenue:N0} đ";
            lblBestSeller.Text = best;
        }
    }
}
