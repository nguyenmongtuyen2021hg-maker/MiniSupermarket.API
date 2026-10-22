namespace MiniSupermarket.WinForms
{
    partial class FormLogin
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
            panelCard = new Panel();
            labelQuickSelect = new Label();
            cboQuickAccount = new ComboBox();
            btnExit = new Button();
            btnLogin = new Button();
            txtPassword = new TextBox();
            labelPassword = new Label();
            txtUsername = new TextBox();
            labelUsername = new Label();
            lblSubtitle = new Label();
            lblHeader = new Label();
            panelCard.SuspendLayout();
            SuspendLayout();
            // 
            // panelCard
            // 
            panelCard.BackColor = Color.White;
            panelCard.BorderStyle = BorderStyle.FixedSingle;
            panelCard.Controls.Add(labelQuickSelect);
            panelCard.Controls.Add(cboQuickAccount);
            panelCard.Controls.Add(btnExit);
            panelCard.Controls.Add(btnLogin);
            panelCard.Controls.Add(txtPassword);
            panelCard.Controls.Add(labelPassword);
            panelCard.Controls.Add(txtUsername);
            panelCard.Controls.Add(labelUsername);
            panelCard.Controls.Add(lblSubtitle);
            panelCard.Controls.Add(lblHeader);
            panelCard.Location = new Point(40, 30);
            panelCard.Name = "panelCard";
            panelCard.Size = new Size(420, 440);
            panelCard.TabIndex = 0;
            // 
            // labelQuickSelect
            // 
            labelQuickSelect.AutoSize = true;
            labelQuickSelect.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            labelQuickSelect.ForeColor = Color.FromArgb(64, 64, 64);
            labelQuickSelect.Location = new Point(35, 95);
            labelQuickSelect.Name = "labelQuickSelect";
            labelQuickSelect.Size = new Size(187, 15);
            labelQuickSelect.TabIndex = 9;
            labelQuickSelect.Text = "Chọn nhanh tài khoản mẫu kiểm thử:";
            // 
            // cboQuickAccount
            // 
            cboQuickAccount.DropDownStyle = ComboBoxStyle.DropDownList;
            cboQuickAccount.FormattingEnabled = true;
            cboQuickAccount.Location = new Point(35, 115);
            cboQuickAccount.Name = "cboQuickAccount";
            cboQuickAccount.Size = new Size(350, 23);
            cboQuickAccount.TabIndex = 8;
            cboQuickAccount.SelectedIndexChanged += cboQuickAccount_SelectedIndexChanged;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.FromArgb(224, 224, 224);
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Segoe UI", 10F);
            btnExit.Location = new Point(220, 360);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(165, 40);
            btnExit.TabIndex = 7;
            btnExit.Text = "Thoát";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(24, 30, 48);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(35, 360);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(165, 40);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "ĐĂNG NHẬP";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 11F);
            txtPassword.Location = new Point(35, 275);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(350, 27);
            txtPassword.TabIndex = 5;
            txtPassword.Text = "123456";
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelPassword.Location = new Point(35, 250);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(75, 19);
            labelPassword.TabIndex = 4;
            labelPassword.Text = "Mật khẩu:";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 11F);
            txtUsername.Location = new Point(35, 195);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(350, 27);
            txtUsername.TabIndex = 3;
            txtUsername.Text = "admin01";
            // 
            // labelUsername
            // 
            labelUsername.AutoSize = true;
            labelUsername.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelUsername.Location = new Point(35, 170);
            labelUsername.Name = "labelUsername";
            labelUsername.Size = new Size(111, 19);
            labelUsername.TabIndex = 2;
            labelUsername.Text = "Tên đăng nhập:";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblSubtitle.ForeColor = Color.Gray;
            lblSubtitle.Location = new Point(70, 55);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(279, 17);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Hệ thống Phân quyền Quản lý Siêu thị Mini POS";
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(24, 30, 48);
            lblHeader.Location = new Point(78, 20);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(262, 30);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "🛒 TẠP HÓA NGỌC MAI";
            // 
            // FormLogin
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            ClientSize = new Size(500, 505);
            Controls.Add(panelCard);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập Hệ thống - Tiệm Tạp hóa Ngọc Mai";
            Load += FormLogin_Load;
            panelCard.ResumeLayout(false);
            panelCard.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelCard;
        private Label lblHeader;
        private Label lblSubtitle;
        private TextBox txtPassword;
        private Label labelPassword;
        private TextBox txtUsername;
        private Label labelUsername;
        private Button btnLogin;
        private Button btnExit;
        private Label labelQuickSelect;
        private ComboBox cboQuickAccount;
    }
}
