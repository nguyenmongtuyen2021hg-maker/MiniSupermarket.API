namespace MiniSupermarket.WinForms
{
    partial class FormCustomerManagement
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
            dgvCustomers = new DataGridView();
            groupBoxSearch = new GroupBox();
            btnSearch = new Button();
            txtSearch = new TextBox();
            btnLoad = new Button();
            groupBoxInfo = new GroupBox();
            labelRank = new Label();
            txtMembershipRank = new TextBox();
            labelPoints = new Label();
            txtRewardPoints = new TextBox();
            labelAddress = new Label();
            txtAddress = new TextBox();
            labelPhone = new Label();
            txtPhoneNumber = new TextBox();
            labelName = new Label();
            txtCustomerName = new TextBox();
            labelId = new Label();
            txtCustomerId = new TextBox();
            groupBoxActions = new GroupBox();
            btnClear = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            groupBoxList = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            groupBoxSearch.SuspendLayout();
            groupBoxInfo.SuspendLayout();
            groupBoxActions.SuspendLayout();
            groupBoxList.SuspendLayout();
            SuspendLayout();
            // 
            // dgvCustomers
            // 
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Dock = DockStyle.Fill;
            dgvCustomers.Location = new Point(3, 19);
            dgvCustomers.MultiSelect = false;
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.ReadOnly = true;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.Size = new Size(514, 388);
            dgvCustomers.TabIndex = 0;
            dgvCustomers.CellClick += dgvCustomers_CellClick;
            // 
            // groupBoxSearch
            // 
            groupBoxSearch.Controls.Add(btnSearch);
            groupBoxSearch.Controls.Add(txtSearch);
            groupBoxSearch.Controls.Add(btnLoad);
            groupBoxSearch.Location = new Point(12, 12);
            groupBoxSearch.Name = "groupBoxSearch";
            groupBoxSearch.Size = new Size(520, 60);
            groupBoxSearch.TabIndex = 1;
            groupBoxSearch.TabStop = false;
            groupBoxSearch.Text = "Tìm kiếm khách hàng";
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(330, 20);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(85, 27);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(15, 23);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Nhập tên hoặc SĐT khách hàng...";
            txtSearch.Size = new Size(300, 23);
            txtSearch.TabIndex = 1;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(425, 20);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(85, 27);
            btnLoad.TabIndex = 3;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // groupBoxInfo
            // 
            groupBoxInfo.Controls.Add(labelRank);
            groupBoxInfo.Controls.Add(txtMembershipRank);
            groupBoxInfo.Controls.Add(labelPoints);
            groupBoxInfo.Controls.Add(txtRewardPoints);
            groupBoxInfo.Controls.Add(labelAddress);
            groupBoxInfo.Controls.Add(txtAddress);
            groupBoxInfo.Controls.Add(labelPhone);
            groupBoxInfo.Controls.Add(txtPhoneNumber);
            groupBoxInfo.Controls.Add(labelName);
            groupBoxInfo.Controls.Add(txtCustomerName);
            groupBoxInfo.Controls.Add(labelId);
            groupBoxInfo.Controls.Add(txtCustomerId);
            groupBoxInfo.Location = new Point(545, 12);
            groupBoxInfo.Name = "groupBoxInfo";
            groupBoxInfo.Size = new Size(300, 370);
            groupBoxInfo.TabIndex = 2;
            groupBoxInfo.TabStop = false;
            groupBoxInfo.Text = "Thông tin khách hàng";
            // 
            // labelRank
            // 
            labelRank.AutoSize = true;
            labelRank.Location = new Point(15, 310);
            labelRank.Name = "labelRank";
            labelRank.Size = new Size(111, 15);
            labelRank.TabIndex = 11;
            labelRank.Text = "Hạng thẻ thành viên:";
            // 
            // txtMembershipRank
            // 
            txtMembershipRank.Location = new Point(15, 328);
            txtMembershipRank.Name = "txtMembershipRank";
            txtMembershipRank.Size = new Size(265, 23);
            txtMembershipRank.TabIndex = 10;
            txtMembershipRank.Text = "Chuẩn";
            // 
            // labelPoints
            // 
            labelPoints.AutoSize = true;
            labelPoints.Location = new Point(15, 250);
            labelPoints.Name = "labelPoints";
            labelPoints.Size = new Size(79, 15);
            labelPoints.TabIndex = 9;
            labelPoints.Text = "Điểm tích lũy:";
            // 
            // txtRewardPoints
            // 
            txtRewardPoints.Location = new Point(15, 268);
            txtRewardPoints.Name = "txtRewardPoints";
            txtRewardPoints.Size = new Size(265, 23);
            txtRewardPoints.TabIndex = 8;
            txtRewardPoints.Text = "0";
            // 
            // labelAddress
            // 
            labelAddress.AutoSize = true;
            labelAddress.Location = new Point(15, 190);
            labelAddress.Name = "labelAddress";
            labelAddress.Size = new Size(46, 15);
            labelAddress.TabIndex = 7;
            labelAddress.Text = "Địa chỉ:";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(15, 208);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(265, 23);
            txtAddress.TabIndex = 6;
            // 
            // labelPhone
            // 
            labelPhone.AutoSize = true;
            labelPhone.Location = new Point(15, 135);
            labelPhone.Name = "labelPhone";
            labelPhone.Size = new Size(79, 15);
            labelPhone.TabIndex = 5;
            labelPhone.Text = "Số điện thoại:";
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Location = new Point(15, 153);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(265, 23);
            txtPhoneNumber.TabIndex = 4;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(15, 80);
            labelName.Name = "labelName";
            labelName.Size = new Size(93, 15);
            labelName.TabIndex = 3;
            labelName.Text = "Tên khách hàng:";
            // 
            // txtCustomerName
            // 
            txtCustomerName.Location = new Point(15, 98);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(265, 23);
            txtCustomerName.TabIndex = 2;
            // 
            // labelId
            // 
            labelId.AutoSize = true;
            labelId.Location = new Point(15, 25);
            labelId.Name = "labelId";
            labelId.Size = new Size(92, 15);
            labelId.TabIndex = 1;
            labelId.Text = "Mã khách hàng:";
            // 
            // txtCustomerId
            // 
            txtCustomerId.Location = new Point(15, 43);
            txtCustomerId.Name = "txtCustomerId";
            txtCustomerId.ReadOnly = true;
            txtCustomerId.Size = new Size(265, 23);
            txtCustomerId.TabIndex = 0;
            // 
            // groupBoxActions
            // 
            groupBoxActions.Controls.Add(btnClear);
            groupBoxActions.Controls.Add(btnDelete);
            groupBoxActions.Controls.Add(btnUpdate);
            groupBoxActions.Controls.Add(btnAdd);
            groupBoxActions.Location = new Point(545, 390);
            groupBoxActions.Name = "groupBoxActions";
            groupBoxActions.Size = new Size(300, 100);
            groupBoxActions.TabIndex = 3;
            groupBoxActions.TabStop = false;
            groupBoxActions.Text = "Chức năng";
            // 
            // btnClear
            // 
            btnClear.Location = new Point(155, 60);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(125, 30);
            btnClear.TabIndex = 3;
            btnClear.Text = "Làm mới ô";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.LightCoral;
            btnDelete.Location = new Point(15, 60);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(125, 30);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(155, 22);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(125, 30);
            btnUpdate.TabIndex = 1;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(15, 22);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(125, 30);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // groupBoxList
            // 
            groupBoxList.Controls.Add(dgvCustomers);
            groupBoxList.Location = new Point(12, 80);
            groupBoxList.Name = "groupBoxList";
            groupBoxList.Size = new Size(520, 410);
            groupBoxList.TabIndex = 4;
            groupBoxList.TabStop = false;
            groupBoxList.Text = "Danh sách khách hàng thân thiết";
            // 
            // FormCustomerManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(860, 505);
            Controls.Add(groupBoxList);
            Controls.Add(groupBoxActions);
            Controls.Add(groupBoxInfo);
            Controls.Add(groupBoxSearch);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormCustomerManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý Khách hàng - MiniSupermarket";
            Load += FormCustomerManagement_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            groupBoxSearch.ResumeLayout(false);
            groupBoxSearch.PerformLayout();
            groupBoxInfo.ResumeLayout(false);
            groupBoxInfo.PerformLayout();
            groupBoxActions.ResumeLayout(false);
            groupBoxList.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvCustomers;
        private GroupBox groupBoxSearch;
        private Button btnSearch;
        private TextBox txtSearch;
        private Button btnLoad;
        private GroupBox groupBoxInfo;
        private Label labelRank;
        private TextBox txtMembershipRank;
        private Label labelPoints;
        private TextBox txtRewardPoints;
        private Label labelAddress;
        private TextBox txtAddress;
        private Label labelPhone;
        private TextBox txtPhoneNumber;
        private Label labelName;
        private TextBox txtCustomerName;
        private Label labelId;
        private TextBox txtCustomerId;
        private GroupBox groupBoxActions;
        private Button btnClear;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnAdd;
        private GroupBox groupBoxList;
    }
}
