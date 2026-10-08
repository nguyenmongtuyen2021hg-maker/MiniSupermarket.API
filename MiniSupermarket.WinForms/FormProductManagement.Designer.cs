namespace MiniSupermarket.WinForms
{
    partial class FormProductManagement
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
            panelTop = new Panel();
            btnSearch = new Button();
            cboFilterCategory = new ComboBox();
            txtSearchBarcode = new TextBox();
            btnLoad = new Button();
            panelCenter = new Panel();
            dgvProducts = new DataGridView();
            panelDetail = new Panel();
            groupBoxActions = new GroupBox();
            btnClear = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            groupBoxInfo = new GroupBox();
            cboCategory = new ComboBox();
            labelCategory = new Label();
            nudStock = new NumericUpDown();
            labelStock = new Label();
            nudPrice = new NumericUpDown();
            labelPrice = new Label();
            txtProductName = new TextBox();
            labelName = new Label();
            txtBarcode = new TextBox();
            labelBarcode = new Label();
            txtId = new TextBox();
            labelId = new Label();
            panelTop.SuspendLayout();
            panelCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            panelDetail.SuspendLayout();
            groupBoxActions.SuspendLayout();
            groupBoxInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPrice).BeginInit();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.White;
            panelTop.BorderStyle = BorderStyle.FixedSingle;
            panelTop.Controls.Add(btnSearch);
            panelTop.Controls.Add(cboFilterCategory);
            panelTop.Controls.Add(txtSearchBarcode);
            panelTop.Controls.Add(btnLoad);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Padding = new Padding(15, 10, 15, 10);
            panelTop.Size = new Size(1000, 55);
            panelTop.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(540, 12);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(85, 29);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // cboFilterCategory
            // 
            cboFilterCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterCategory.FormattingEnabled = true;
            cboFilterCategory.Location = new Point(310, 15);
            cboFilterCategory.Name = "cboFilterCategory";
            cboFilterCategory.Size = new Size(220, 23);
            cboFilterCategory.TabIndex = 1;
            cboFilterCategory.SelectedIndexChanged += cboFilterCategory_SelectedIndexChanged;
            // 
            // txtSearchBarcode
            // 
            txtSearchBarcode.Location = new Point(15, 15);
            txtSearchBarcode.Name = "txtSearchBarcode";
            txtSearchBarcode.PlaceholderText = "Nhập tên hoặc mã vạch sản phẩm...";
            txtSearchBarcode.Size = new Size(280, 23);
            txtSearchBarcode.TabIndex = 0;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(635, 12);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(85, 29);
            btnLoad.TabIndex = 3;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // panelCenter
            // 
            panelCenter.Controls.Add(dgvProducts);
            panelCenter.Dock = DockStyle.Fill;
            panelCenter.Location = new Point(0, 55);
            panelCenter.Name = "panelCenter";
            panelCenter.Padding = new Padding(15);
            panelCenter.Size = new Size(670, 545);
            panelCenter.TabIndex = 1;
            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.Location = new Point(15, 15);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(640, 515);
            dgvProducts.TabIndex = 0;
            dgvProducts.CellClick += dgvProducts_CellClick;
            // 
            // panelDetail
            // 
            panelDetail.BackColor = Color.White;
            panelDetail.BorderStyle = BorderStyle.FixedSingle;
            panelDetail.Controls.Add(groupBoxActions);
            panelDetail.Controls.Add(groupBoxInfo);
            panelDetail.Dock = DockStyle.Right;
            panelDetail.Location = new Point(670, 55);
            panelDetail.Name = "panelDetail";
            panelDetail.Padding = new Padding(15);
            panelDetail.Size = new Size(330, 545);
            panelDetail.TabIndex = 2;
            // 
            // groupBoxActions
            // 
            groupBoxActions.Controls.Add(btnClear);
            groupBoxActions.Controls.Add(btnDelete);
            groupBoxActions.Controls.Add(btnUpdate);
            groupBoxActions.Controls.Add(btnAdd);
            groupBoxActions.Dock = DockStyle.Bottom;
            groupBoxActions.Location = new Point(15, 413);
            groupBoxActions.Name = "groupBoxActions";
            groupBoxActions.Size = new Size(298, 115);
            groupBoxActions.TabIndex = 1;
            groupBoxActions.TabStop = false;
            groupBoxActions.Text = "Thao tác";
            // 
            // btnClear
            // 
            btnClear.Location = new Point(155, 68);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(130, 32);
            btnClear.TabIndex = 3;
            btnClear.Text = "Làm mới";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.LightCoral;
            btnDelete.Location = new Point(15, 68);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(130, 32);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(155, 25);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(130, 32);
            btnUpdate.TabIndex = 1;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(15, 25);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(130, 32);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // groupBoxInfo
            // 
            groupBoxInfo.Controls.Add(cboCategory);
            groupBoxInfo.Controls.Add(labelCategory);
            groupBoxInfo.Controls.Add(nudStock);
            groupBoxInfo.Controls.Add(labelStock);
            groupBoxInfo.Controls.Add(nudPrice);
            groupBoxInfo.Controls.Add(labelPrice);
            groupBoxInfo.Controls.Add(txtProductName);
            groupBoxInfo.Controls.Add(labelName);
            groupBoxInfo.Controls.Add(txtBarcode);
            groupBoxInfo.Controls.Add(labelBarcode);
            groupBoxInfo.Controls.Add(txtId);
            groupBoxInfo.Controls.Add(labelId);
            groupBoxInfo.Dock = DockStyle.Top;
            groupBoxInfo.Location = new Point(15, 15);
            groupBoxInfo.Name = "groupBoxInfo";
            groupBoxInfo.Size = new Size(298, 385);
            groupBoxInfo.TabIndex = 0;
            groupBoxInfo.TabStop = false;
            groupBoxInfo.Text = "Thông tin chi tiết";
            // 
            // cboCategory
            // 
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.FormattingEnabled = true;
            cboCategory.Location = new Point(15, 335);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(270, 23);
            cboCategory.TabIndex = 11;
            // 
            // labelCategory
            // 
            labelCategory.AutoSize = true;
            labelCategory.Location = new Point(15, 315);
            labelCategory.Name = "labelCategory";
            labelCategory.Size = new Size(72, 15);
            labelCategory.TabIndex = 10;
            labelCategory.Text = "Nhóm hàng:";
            // 
            // nudStock
            // 
            nudStock.Location = new Point(15, 275);
            nudStock.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudStock.Name = "nudStock";
            nudStock.Size = new Size(270, 23);
            nudStock.TabIndex = 9;
            // 
            // labelStock
            // 
            labelStock.AutoSize = true;
            labelStock.Location = new Point(15, 255);
            labelStock.Name = "labelStock";
            labelStock.Size = new Size(79, 15);
            labelStock.TabIndex = 8;
            labelStock.Text = "Số lượng tồn:";
            // 
            // nudPrice
            // 
            nudPrice.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            nudPrice.Location = new Point(15, 215);
            nudPrice.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            nudPrice.Name = "nudPrice";
            nudPrice.Size = new Size(270, 23);
            nudPrice.TabIndex = 7;
            // 
            // labelPrice
            // 
            labelPrice.AutoSize = true;
            labelPrice.Location = new Point(15, 195);
            labelPrice.Name = "labelPrice";
            labelPrice.Size = new Size(50, 15);
            labelPrice.TabIndex = 6;
            labelPrice.Text = "Đơn giá:";
            // 
            // txtProductName
            // 
            txtProductName.Location = new Point(15, 155);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(270, 23);
            txtProductName.TabIndex = 5;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(15, 135);
            labelName.Name = "labelName";
            labelName.Size = new Size(83, 15);
            labelName.TabIndex = 4;
            labelName.Text = "Tên sản phẩm:";
            // 
            // txtBarcode
            // 
            txtBarcode.Location = new Point(15, 95);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(270, 23);
            txtBarcode.TabIndex = 3;
            // 
            // labelBarcode
            // 
            labelBarcode.AutoSize = true;
            labelBarcode.Location = new Point(15, 75);
            labelBarcode.Name = "labelBarcode";
            labelBarcode.Size = new Size(55, 15);
            labelBarcode.TabIndex = 2;
            labelBarcode.Text = "Mã vạch:";
            // 
            // txtId
            // 
            txtId.Location = new Point(15, 42);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(270, 23);
            txtId.TabIndex = 1;
            // 
            // labelId
            // 
            labelId.AutoSize = true;
            labelId.Location = new Point(15, 24);
            labelId.Name = "labelId";
            labelId.Size = new Size(82, 15);
            labelId.TabIndex = 0;
            labelId.Text = "Mã định danh:";
            // 
            // FormProductManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(panelCenter);
            Controls.Add(panelDetail);
            Controls.Add(panelTop);
            Name = "FormProductManagement";
            Text = "Quản lý Thông tin Sản phẩm & Kho hàng";
            Load += FormProductManagement_Load;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            panelDetail.ResumeLayout(false);
            groupBoxActions.ResumeLayout(false);
            groupBoxInfo.ResumeLayout(false);
            groupBoxInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPrice).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private TextBox txtSearchBarcode;
        private ComboBox cboFilterCategory;
        private Button btnSearch;
        private Button btnLoad;
        private Panel panelCenter;
        private DataGridView dgvProducts;
        private Panel panelDetail;
        private GroupBox groupBoxInfo;
        private TextBox txtId;
        private Label labelId;
        private TextBox txtBarcode;
        private Label labelBarcode;
        private TextBox txtProductName;
        private Label labelName;
        private NumericUpDown nudPrice;
        private Label labelPrice;
        private NumericUpDown nudStock;
        private Label labelStock;
        private ComboBox cboCategory;
        private Label labelCategory;
        private GroupBox groupBoxActions;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
    }
}
