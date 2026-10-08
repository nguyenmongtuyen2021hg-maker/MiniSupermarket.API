namespace MiniSupermarket.WinForms
{
    partial class FormUserManagement
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
            panelMain = new Panel();
            dgvUsers = new DataGridView();
            panelDetail = new Panel();
            btnLoad = new Button();
            btnAddUser = new Button();
            cboRole = new ComboBox();
            labelRole = new Label();
            txtPassword = new TextBox();
            labelPassword = new Label();
            txtFullName = new TextBox();
            labelFullName = new Label();
            txtUsername = new TextBox();
            labelUsername = new Label();
            lblTitle = new Label();
            panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            panelDetail.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.Controls.Add(dgvUsers);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(15);
            panelMain.Size = new Size(660, 600);
            panelMain.TabIndex = 0;
            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Dock = DockStyle.Fill;
            dgvUsers.Location = new Point(15, 15);
            dgvUsers.MultiSelect = false;
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(630, 570);
            dgvUsers.TabIndex = 0;
            // 
            // panelDetail
            // 
            panelDetail.BackColor = Color.White;
            panelDetail.BorderStyle = BorderStyle.FixedSingle;
            panelDetail.Controls.Add(btnLoad);
            panelDetail.Controls.Add(btnAddUser);
            panelDetail.Controls.Add(cboRole);
            panelDetail.Controls.Add(labelRole);
            panelDetail.Controls.Add(txtPassword);
            panelDetail.Controls.Add(labelPassword);
            panelDetail.Controls.Add(txtFullName);
            panelDetail.Controls.Add(labelFullName);
            panelDetail.Controls.Add(txtUsername);
            panelDetail.Controls.Add(labelUsername);
            panelDetail.Controls.Add(lblTitle);
            panelDetail.Dock = DockStyle.Right;
            panelDetail.Location = new Point(660, 0);
            panelDetail.Name = "panelDetail";
            panelDetail.Padding = new Padding(20);
            panelDetail.Size = new Size(340, 600);
            panelDetail.TabIndex = 1;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(20, 395);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(295, 35);
            btnLoad.TabIndex = 10;
            btnLoad.Text = "Tải lại danh sách";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // btnAddUser
            // 
            btnAddUser.BackColor = Color.FromArgb(24, 30, 48);
            btnAddUser.FlatAppearance.BorderSize = 0;
            btnAddUser.FlatStyle = FlatStyle.Flat;
            btnAddUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddUser.ForeColor = Color.White;
            btnAddUser.Location = new Point(20, 345);
            btnAddUser.Name = "btnAddUser";
            btnAddUser.Size = new Size(295, 40);
            btnAddUser.TabIndex = 9;
            btnAddUser.Text = "Thêm tài khoản mới";
            btnAddUser.UseVisualStyleBackColor = false;
            btnAddUser.Click += btnAddUser_Click;
            // 
            // cboRole
            // 
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.FormattingEnabled = true;
            cboRole.Location = new Point(20, 290);
            cboRole.Name = "cboRole";
            cboRole.Size = new Size(295, 23);
            cboRole.TabIndex = 8;
            // 
            // labelRole
            // 
            labelRole.AutoSize = true;
            labelRole.Location = new Point(20, 270);
            labelRole.Name = "labelRole";
            labelRole.Size = new Size(100, 15);
            labelRole.TabIndex = 7;
            labelRole.Text = "Vai trò (Phân quyền):";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(20, 225);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(295, 23);
            txtPassword.TabIndex = 6;
            txtPassword.Text = "123456";
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(20, 205);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(60, 15);
            labelPassword.TabIndex = 5;
            labelPassword.Text = "Mật khẩu:";
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(20, 160);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(295, 23);
            txtFullName.TabIndex = 4;
            // 
            // labelFullName
            // 
            labelFullName.AutoSize = true;
            labelFullName.Location = new Point(20, 140);
            labelFullName.Name = "labelFullName";
            labelFullName.Size = new Size(83, 15);
            labelFullName.TabIndex = 3;
            labelFullName.Text = "Tên nhân viên:";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(20, 95);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(295, 23);
            txtUsername.TabIndex = 2;
            // 
            // labelUsername
            // 
            labelUsername.AutoSize = true;
            labelUsername.Location = new Point(20, 75);
            labelUsername.Name = "labelUsername";
            labelUsername.Size = new Size(89, 15);
            labelUsername.TabIndex = 1;
            labelUsername.Text = "Tên đăng nhập:";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(207, 21);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🛡 Cấp tài khoản nhân sự";
            // 
            // FormUserManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(panelMain);
            Controls.Add(panelDetail);
            Name = "FormUserManagement";
            Text = "Quản trị Tài khoản & Phân quyền Hệ thống";
            Load += FormUserManagement_Load;
            panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            panelDetail.ResumeLayout(false);
            panelDetail.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMain;
        private DataGridView dgvUsers;
        private Panel panelDetail;
        private Label lblTitle;
        private TextBox txtUsername;
        private Label labelUsername;
        private TextBox txtFullName;
        private Label labelFullName;
        private TextBox txtPassword;
        private Label labelPassword;
        private ComboBox cboRole;
        private Label labelRole;
        private Button btnAddUser;
        private Button btnLoad;
    }
}
