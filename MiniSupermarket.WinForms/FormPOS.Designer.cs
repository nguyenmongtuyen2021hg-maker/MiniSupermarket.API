namespace MiniSupermarket.WinForms
{
    partial class FormPOS
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
            panelLeft = new Panel();
            dgvCart = new DataGridView();
            panelBarcode = new Panel();
            lblBarcodePrompt = new Label();
            txtBarcode = new TextBox();
            panelRight = new Panel();
            btnClearCart = new Button();
            btnCheckout = new Button();
            lblChange = new Label();
            labelChangeTitle = new Label();
            txtCashReceived = new TextBox();
            labelCashPrompt = new Label();
            lblTotalAmount = new Label();
            labelTotalPrompt = new Label();
            groupBoxCustomer = new GroupBox();
            lblCustomerName = new Label();
            txtCustomerPhone = new TextBox();
            labelPhonePrompt = new Label();
            panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            panelBarcode.SuspendLayout();
            panelRight.SuspendLayout();
            groupBoxCustomer.SuspendLayout();
            SuspendLayout();
            // 
            // panelLeft
            // 
            panelLeft.Controls.Add(dgvCart);
            panelLeft.Controls.Add(panelBarcode);
            panelLeft.Dock = DockStyle.Fill;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Padding = new Padding(15);
            panelLeft.Size = new Size(650, 600);
            panelLeft.TabIndex = 0;
            // 
            // dgvCart
            // 
            dgvCart.AllowUserToAddRows = false;
            dgvCart.AllowUserToDeleteRows = false;
            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCart.Dock = DockStyle.Fill;
            dgvCart.Location = new Point(15, 75);
            dgvCart.MultiSelect = false;
            dgvCart.Name = "dgvCart";
            dgvCart.ReadOnly = true;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.Size = new Size(620, 510);
            dgvCart.TabIndex = 1;
            // 
            // panelBarcode
            // 
            panelBarcode.Controls.Add(lblBarcodePrompt);
            panelBarcode.Controls.Add(txtBarcode);
            panelBarcode.Dock = DockStyle.Top;
            panelBarcode.Location = new Point(15, 15);
            panelBarcode.Name = "panelBarcode";
            panelBarcode.Size = new Size(620, 60);
            panelBarcode.TabIndex = 0;
            // 
            // lblBarcodePrompt
            // 
            lblBarcodePrompt.AutoSize = true;
            lblBarcodePrompt.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblBarcodePrompt.Location = new Point(0, 5);
            lblBarcodePrompt.Name = "lblBarcodePrompt";
            lblBarcodePrompt.Size = new Size(260, 17);
            lblBarcodePrompt.TabIndex = 0;
            lblBarcodePrompt.Text = "🔍 Quét hoặc nhập mã vạch sản phẩm (Enter):";
            // 
            // txtBarcode
            // 
            txtBarcode.Dock = DockStyle.Bottom;
            txtBarcode.Font = new Font("Segoe UI", 12F);
            txtBarcode.Location = new Point(0, 25);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.PlaceholderText = "Nhập mã vạch (ví dụ: 893456789001) rồi nhấn Enter...";
            txtBarcode.Size = new Size(620, 29);
            txtBarcode.TabIndex = 1;
            txtBarcode.KeyDown += txtBarcode_KeyDown;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.White;
            panelRight.BorderStyle = BorderStyle.FixedSingle;
            panelRight.Controls.Add(btnClearCart);
            panelRight.Controls.Add(btnCheckout);
            panelRight.Controls.Add(lblChange);
            panelRight.Controls.Add(labelChangeTitle);
            panelRight.Controls.Add(txtCashReceived);
            panelRight.Controls.Add(labelCashPrompt);
            panelRight.Controls.Add(lblTotalAmount);
            panelRight.Controls.Add(labelTotalPrompt);
            panelRight.Controls.Add(groupBoxCustomer);
            panelRight.Dock = DockStyle.Right;
            panelRight.Location = new Point(650, 0);
            panelRight.Name = "panelRight";
            panelRight.Padding = new Padding(15);
            panelRight.Size = new Size(350, 600);
            panelRight.TabIndex = 1;
            // 
            // btnClearCart
            // 
            btnClearCart.BackColor = Color.FromArgb(255, 128, 128);
            btnClearCart.Dock = DockStyle.Bottom;
            btnClearCart.FlatAppearance.BorderSize = 0;
            btnClearCart.FlatStyle = FlatStyle.Flat;
            btnClearCart.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClearCart.ForeColor = Color.White;
            btnClearCart.Location = new Point(15, 543);
            btnClearCart.Name = "btnClearCart";
            btnClearCart.Size = new Size(318, 40);
            btnClearCart.TabIndex = 8;
            btnClearCart.Text = "❌ HỦY GIỎ HÀNG";
            btnClearCart.UseVisualStyleBackColor = false;
            btnClearCart.Click += btnClearCart_Click;
            // 
            // btnCheckout
            // 
            btnCheckout.BackColor = Color.FromArgb(46, 125, 50);
            btnCheckout.Dock = DockStyle.Bottom;
            btnCheckout.FlatAppearance.BorderSize = 0;
            btnCheckout.FlatStyle = FlatStyle.Flat;
            btnCheckout.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCheckout.ForeColor = Color.White;
            btnCheckout.Location = new Point(15, 483);
            btnCheckout.Name = "btnCheckout";
            btnCheckout.Size = new Size(318, 50);
            btnCheckout.TabIndex = 7;
            btnCheckout.Text = "💳 THANH TOÁN (F9)";
            btnCheckout.UseVisualStyleBackColor = false;
            btnCheckout.Click += btnCheckout_Click;
            // 
            // lblChange
            // 
            lblChange.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblChange.ForeColor = Color.Green;
            lblChange.Location = new Point(15, 410);
            lblChange.Name = "lblChange";
            lblChange.Size = new Size(318, 30);
            lblChange.TabIndex = 6;
            lblChange.Text = "0 đ";
            lblChange.TextAlign = ContentAlignment.MiddleRight;
            // 
            // labelChangeTitle
            // 
            labelChangeTitle.AutoSize = true;
            labelChangeTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelChangeTitle.Location = new Point(15, 390);
            labelChangeTitle.Name = "labelChangeTitle";
            labelChangeTitle.Size = new Size(123, 19);
            labelChangeTitle.TabIndex = 5;
            labelChangeTitle.Text = "Tiền thừa trả lại:";
            // 
            // txtCashReceived
            // 
            txtCashReceived.Font = new Font("Segoe UI", 13F);
            txtCashReceived.Location = new Point(15, 340);
            txtCashReceived.Name = "txtCashReceived";
            txtCashReceived.Size = new Size(318, 31);
            txtCashReceived.TabIndex = 4;
            txtCashReceived.Text = "0";
            txtCashReceived.TextAlign = HorizontalAlignment.Right;
            txtCashReceived.TextChanged += txtCashReceived_TextChanged;
            // 
            // labelCashPrompt
            // 
            labelCashPrompt.AutoSize = true;
            labelCashPrompt.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelCashPrompt.Location = new Point(15, 315);
            labelCashPrompt.Name = "labelCashPrompt";
            labelCashPrompt.Size = new Size(111, 19);
            labelCashPrompt.TabIndex = 3;
            labelCashPrompt.Text = "Tiền khách đưa:";
            // 
            // lblTotalAmount
            // 
            lblTotalAmount.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTotalAmount.ForeColor = Color.FromArgb(198, 40, 40);
            lblTotalAmount.Location = new Point(15, 240);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(318, 45);
            lblTotalAmount.TabIndex = 2;
            lblTotalAmount.Text = "0 đ";
            lblTotalAmount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // labelTotalPrompt
            // 
            labelTotalPrompt.AutoSize = true;
            labelTotalPrompt.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            labelTotalPrompt.Location = new Point(15, 215);
            labelTotalPrompt.Name = "labelTotalPrompt";
            labelTotalPrompt.Size = new Size(168, 20);
            labelTotalPrompt.TabIndex = 1;
            labelTotalPrompt.Text = "TỔNG TIỀN PHẢI TRẢ:";
            // 
            // groupBoxCustomer
            // 
            groupBoxCustomer.Controls.Add(lblCustomerName);
            groupBoxCustomer.Controls.Add(txtCustomerPhone);
            groupBoxCustomer.Controls.Add(labelPhonePrompt);
            groupBoxCustomer.Dock = DockStyle.Top;
            groupBoxCustomer.Font = new Font("Segoe UI", 9.5F);
            groupBoxCustomer.Location = new Point(15, 15);
            groupBoxCustomer.Name = "groupBoxCustomer";
            groupBoxCustomer.Size = new Size(318, 170);
            groupBoxCustomer.TabIndex = 0;
            groupBoxCustomer.TabStop = false;
            groupBoxCustomer.Text = "Khách hàng thân thiết";
            // 
            // lblCustomerName
            // 
            lblCustomerName.AutoSize = true;
            lblCustomerName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCustomerName.ForeColor = Color.FromArgb(41, 100, 180);
            lblCustomerName.Location = new Point(15, 115);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(107, 19);
            lblCustomerName.TabIndex = 2;
            lblCustomerName.Text = "Khách vãng lai";
            // 
            // txtCustomerPhone
            // 
            txtCustomerPhone.Location = new Point(15, 65);
            txtCustomerPhone.Name = "txtCustomerPhone";
            txtCustomerPhone.PlaceholderText = "Nhập SĐT khách hàng...";
            txtCustomerPhone.Size = new Size(285, 24);
            txtCustomerPhone.TabIndex = 1;
            // 
            // labelPhonePrompt
            // 
            labelPhonePrompt.AutoSize = true;
            labelPhonePrompt.Location = new Point(15, 40);
            labelPhonePrompt.Name = "labelPhonePrompt";
            labelPhonePrompt.Size = new Size(149, 17);
            labelPhonePrompt.TabIndex = 0;
            labelPhonePrompt.Text = "Số điện thoại tích điểm:";
            // 
            // FormPOS
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(panelLeft);
            Controls.Add(panelRight);
            Name = "FormPOS";
            Text = "Hệ thống Quầy Bán hàng & Thu ngân (POS)";
            panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            panelBarcode.ResumeLayout(false);
            panelBarcode.PerformLayout();
            panelRight.ResumeLayout(false);
            panelRight.PerformLayout();
            groupBoxCustomer.ResumeLayout(false);
            groupBoxCustomer.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelLeft;
        private DataGridView dgvCart;
        private Panel panelBarcode;
        private Label lblBarcodePrompt;
        private TextBox txtBarcode;
        private Panel panelRight;
        private GroupBox groupBoxCustomer;
        private TextBox txtCustomerPhone;
        private Label labelPhonePrompt;
        private Label lblCustomerName;
        private Label labelTotalPrompt;
        private Label lblTotalAmount;
        private Label labelCashPrompt;
        private TextBox txtCashReceived;
        private Label labelChangeTitle;
        private Label lblChange;
        private Button btnCheckout;
        private Button btnClearCart;
    }
}
