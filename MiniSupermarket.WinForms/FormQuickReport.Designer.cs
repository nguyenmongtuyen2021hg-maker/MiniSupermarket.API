namespace MiniSupermarket.WinForms
{
    partial class FormQuickReport
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelFilter = new Panel();
            btnRunReport = new Button();
            dtpReportDate = new DateTimePicker();
            labelDate = new Label();
            panelCards = new Panel();
            cardBestSeller = new Panel();
            lblBestSeller = new Label();
            labelCard3Title = new Label();
            cardRevenue = new Panel();
            lblTotalRevenue = new Label();
            labelCard2Title = new Label();
            cardOrders = new Panel();
            lblTotalOrders = new Label();
            labelCard1Title = new Label();
            panelFilter.SuspendLayout();
            panelCards.SuspendLayout();
            cardBestSeller.SuspendLayout();
            cardRevenue.SuspendLayout();
            cardOrders.SuspendLayout();
            SuspendLayout();
            // 
            // panelFilter
            // 
            panelFilter.BackColor = Color.White;
            panelFilter.BorderStyle = BorderStyle.FixedSingle;
            panelFilter.Controls.Add(btnRunReport);
            panelFilter.Controls.Add(dtpReportDate);
            panelFilter.Controls.Add(labelDate);
            panelFilter.Dock = DockStyle.Top;
            panelFilter.Location = new Point(20, 20);
            panelFilter.Name = "panelFilter";
            panelFilter.Padding = new Padding(15);
            panelFilter.Size = new Size(960, 65);
            panelFilter.TabIndex = 0;
            // 
            // btnRunReport
            // 
            btnRunReport.BackColor = Color.FromArgb(24, 30, 48);
            btnRunReport.FlatAppearance.BorderSize = 0;
            btnRunReport.FlatStyle = FlatStyle.Flat;
            btnRunReport.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRunReport.ForeColor = Color.White;
            btnRunReport.Location = new Point(340, 15);
            btnRunReport.Name = "btnRunReport";
            btnRunReport.Size = new Size(160, 32);
            btnRunReport.TabIndex = 2;
            btnRunReport.Text = "📊 Xem Báo Cáo";
            btnRunReport.UseVisualStyleBackColor = false;
            btnRunReport.Click += btnRunReport_Click;
            // 
            // dtpReportDate
            // 
            dtpReportDate.Format = DateTimePickerFormat.Short;
            dtpReportDate.Location = new Point(160, 20);
            dtpReportDate.Name = "dtpReportDate";
            dtpReportDate.Size = new Size(160, 23);
            dtpReportDate.TabIndex = 1;
            // 
            // labelDate
            // 
            labelDate.AutoSize = true;
            labelDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelDate.Location = new Point(15, 22);
            labelDate.Name = "labelDate";
            labelDate.Size = new Size(137, 19);
            labelDate.TabIndex = 0;
            labelDate.Text = "Chọn ngày báo cáo:";
            // 
            // panelCards
            // 
            panelCards.Controls.Add(cardBestSeller);
            panelCards.Controls.Add(cardRevenue);
            panelCards.Controls.Add(cardOrders);
            panelCards.Dock = DockStyle.Top;
            panelCards.Location = new Point(20, 85);
            panelCards.Name = "panelCards";
            panelCards.Padding = new Padding(0, 20, 0, 0);
            panelCards.Size = new Size(960, 200);
            panelCards.TabIndex = 1;
            // 
            // cardBestSeller
            // 
            cardBestSeller.BackColor = Color.FromArgb(255, 243, 224);
            cardBestSeller.BorderStyle = BorderStyle.FixedSingle;
            cardBestSeller.Controls.Add(lblBestSeller);
            cardBestSeller.Controls.Add(labelCard3Title);
            cardBestSeller.Location = new Point(640, 20);
            cardBestSeller.Name = "cardBestSeller";
            cardBestSeller.Padding = new Padding(15);
            cardBestSeller.Size = new Size(300, 160);
            cardBestSeller.TabIndex = 2;
            // 
            // lblBestSeller
            // 
            lblBestSeller.Dock = DockStyle.Fill;
            lblBestSeller.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblBestSeller.ForeColor = Color.FromArgb(230, 81, 0);
            lblBestSeller.Location = new Point(15, 45);
            lblBestSeller.Name = "lblBestSeller";
            lblBestSeller.Size = new Size(268, 98);
            lblBestSeller.TabIndex = 1;
            lblBestSeller.Text = "Mì Hảo Hảo (250 gói)";
            lblBestSeller.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelCard3Title
            // 
            labelCard3Title.Dock = DockStyle.Top;
            labelCard3Title.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelCard3Title.ForeColor = Color.FromArgb(230, 81, 0);
            labelCard3Title.Location = new Point(15, 15);
            labelCard3Title.Name = "labelCard3Title";
            labelCard3Title.Size = new Size(268, 30);
            labelCard3Title.TabIndex = 0;
            labelCard3Title.Text = "⭐ MẶT HÀNG BÁN CHẠY NHẤT";
            labelCard3Title.TextAlign = ContentAlignment.TopCenter;
            // 
            // cardRevenue
            // 
            cardRevenue.BackColor = Color.FromArgb(232, 245, 233);
            cardRevenue.BorderStyle = BorderStyle.FixedSingle;
            cardRevenue.Controls.Add(lblTotalRevenue);
            cardRevenue.Controls.Add(labelCard2Title);
            cardRevenue.Location = new Point(320, 20);
            cardRevenue.Name = "cardRevenue";
            cardRevenue.Padding = new Padding(15);
            cardRevenue.Size = new Size(300, 160);
            cardRevenue.TabIndex = 1;
            // 
            // lblTotalRevenue
            // 
            lblTotalRevenue.Dock = DockStyle.Fill;
            lblTotalRevenue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTotalRevenue.ForeColor = Color.FromArgb(46, 125, 50);
            lblTotalRevenue.Location = new Point(15, 45);
            lblTotalRevenue.Name = "lblTotalRevenue";
            lblTotalRevenue.Size = new Size(268, 98);
            lblTotalRevenue.TabIndex = 1;
            lblTotalRevenue.Text = "12,450,000 đ";
            lblTotalRevenue.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelCard2Title
            // 
            labelCard2Title.Dock = DockStyle.Top;
            labelCard2Title.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelCard2Title.ForeColor = Color.FromArgb(46, 125, 50);
            labelCard2Title.Location = new Point(15, 15);
            labelCard2Title.Name = "labelCard2Title";
            labelCard2Title.Size = new Size(268, 30);
            labelCard2Title.TabIndex = 0;
            labelCard2Title.Text = "💰 TỔNG DOANH THU";
            labelCard2Title.TextAlign = ContentAlignment.TopCenter;
            // 
            // cardOrders
            // 
            cardOrders.BackColor = Color.FromArgb(227, 242, 253);
            cardOrders.BorderStyle = BorderStyle.FixedSingle;
            cardOrders.Controls.Add(lblTotalOrders);
            cardOrders.Controls.Add(labelCard1Title);
            cardOrders.Location = new Point(0, 20);
            cardOrders.Name = "cardOrders";
            cardOrders.Padding = new Padding(15);
            cardOrders.Size = new Size(300, 160);
            cardOrders.TabIndex = 0;
            // 
            // lblTotalOrders
            // 
            lblTotalOrders.Dock = DockStyle.Fill;
            lblTotalOrders.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTotalOrders.ForeColor = Color.FromArgb(21, 101, 192);
            lblTotalOrders.Location = new Point(15, 45);
            lblTotalOrders.Name = "lblTotalOrders";
            lblTotalOrders.Size = new Size(268, 98);
            lblTotalOrders.TabIndex = 1;
            lblTotalOrders.Text = "48";
            lblTotalOrders.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelCard1Title
            // 
            labelCard1Title.Dock = DockStyle.Top;
            labelCard1Title.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelCard1Title.ForeColor = Color.FromArgb(21, 101, 192);
            labelCard1Title.Location = new Point(15, 15);
            labelCard1Title.Name = "labelCard1Title";
            labelCard1Title.Size = new Size(268, 30);
            labelCard1Title.TabIndex = 0;
            labelCard1Title.Text = "🧾 TỔNG SỐ HÓA ĐƠN";
            labelCard1Title.TextAlign = ContentAlignment.TopCenter;
            // 
            // FormQuickReport
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            ClientSize = new Size(1000, 600);
            Controls.Add(panelCards);
            Controls.Add(panelFilter);
            Name = "FormQuickReport";
            Padding = new Padding(20);
            Text = "Báo cáo Doanh thu & Hiệu suất Ca bán";
            Load += FormQuickReport_Load;
            panelFilter.ResumeLayout(false);
            panelFilter.PerformLayout();
            panelCards.ResumeLayout(false);
            cardBestSeller.ResumeLayout(false);
            cardRevenue.ResumeLayout(false);
            cardOrders.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelFilter;
        private Label labelDate;
        private DateTimePicker dtpReportDate;
        private Button btnRunReport;
        private Panel panelCards;
        private Panel cardOrders;
        private Label labelCard1Title;
        private Label lblTotalOrders;
        private Panel cardRevenue;
        private Label lblTotalRevenue;
        private Label labelCard2Title;
        private Panel cardBestSeller;
        private Label lblBestSeller;
        private Label labelCard3Title;
    }
}
