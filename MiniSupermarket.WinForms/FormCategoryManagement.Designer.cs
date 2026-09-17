namespace MiniSupermarket.WinForms
{
    partial class FormCategoryManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvCategories = new DataGridView();
            txtId = new TextBox();
            txtCategoryName = new TextBox();
            txtKeyword = new TextBox();
            btnLoad = new Button();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnSearch = new Button();
            information = new GroupBox();
            txtDescription = new TextBox();
            label2 = new Label();
            label1 = new Label();
            ID = new Label();
            List = new GroupBox();
            groupBox1 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
            information.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvCategories
            // 
            dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategories.Location = new Point(33, 78);
            dgvCategories.Name = "dgvCategories";
            dgvCategories.Size = new Size(356, 199);
            dgvCategories.TabIndex = 0;
            dgvCategories.CellContentClick += dgvCategories_CellContentClick;
            // 
            // txtId
            // 
            txtId.Location = new Point(21, 44);
            txtId.Name = "txtId";
            txtId.Size = new Size(173, 23);
            txtId.TabIndex = 1;
            txtId.Text = "1";
            // 
            // txtCategoryName
            // 
            txtCategoryName.Location = new Point(21, 96);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(173, 23);
            txtCategoryName.TabIndex = 2;
            txtCategoryName.Text = "Banh keo & Do an vat";
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(71, 9);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.Size = new Size(182, 23);
            txtKeyword.TabIndex = 4;
            txtKeyword.Text = "Nhap tu khoa...";
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(395, 8);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(64, 23);
            btnLoad.TabIndex = 5;
            btnLoad.Text = "Tai lai";
            btnLoad.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(416, 251);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(77, 26);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Them moi";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(513, 253);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(79, 24);
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "Cap nhat";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(613, 253);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(61, 25);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Xoa";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(318, 8);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(71, 23);
            btnSearch.TabIndex = 9;
            btnSearch.Text = "Tim kiem";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // information
            // 
            information.Controls.Add(txtDescription);
            information.Controls.Add(label2);
            information.Controls.Add(label1);
            information.Controls.Add(ID);
            information.Controls.Add(txtId);
            information.Controls.Add(txtCategoryName);
            information.Location = new Point(416, 57);
            information.Name = "information";
            information.Size = new Size(229, 187);
            information.TabIndex = 10;
            information.TabStop = false;
            information.Text = "Thong tin Nhom hang";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(21, 156);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(173, 23);
            txtDescription.TabIndex = 7;
            txtDescription.Text = "Cac loai snack, banh quy, keo deo, so-co-la";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 129);
            label2.Name = "label2";
            label2.Size = new Size(121, 15);
            label2.TabIndex = 6;
            label2.Text = "Mo Ta Mo ta chi tiet...";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 74);
            label1.Name = "label1";
            label1.Size = new Size(177, 15);
            label1.TabIndex = 5;
            label1.Text = "Ten Nhom hang Vi du: Banh keo";
            // 
            // ID
            // 
            ID.AutoSize = true;
            ID.Location = new Point(14, 21);
            ID.Name = "ID";
            ID.Size = new Size(38, 15);
            ID.TabIndex = 4;
            ID.Text = "Ma ID";
            // 
            // List
            // 
            List.Location = new Point(12, 46);
            List.Name = "List";
            List.Size = new Size(412, 341);
            List.TabIndex = 11;
            List.TabStop = false;
            List.Text = "Danh sach Nhom hang";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtKeyword);
            groupBox1.Location = new Point(12, 8);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(456, 32);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tim kiem";
            // 
            // FormCategoryManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSearch);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(btnLoad);
            Controls.Add(dgvCategories);
            Controls.Add(information);
            Controls.Add(List);
            Controls.Add(groupBox1);
            Name = "FormCategoryManagement";
            Text = "FormCategoryManagement";
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
            information.ResumeLayout(false);
            information.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvCategories;
        private TextBox txtId;
        private TextBox txtCategoryName;
        private TextBox txtKeyword;
        private Button btnLoad;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnSearch;
        private GroupBox information;
        private Label label1;
        private Label ID;
        private Label label2;
        private TextBox txtDescription;
        private GroupBox List;
        private GroupBox groupBox1;
    }
}