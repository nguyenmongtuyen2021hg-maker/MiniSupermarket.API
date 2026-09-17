namespace MiniSupermarket.WinForms
{
    partial class FormRoleManagement
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
            dgvRoles = new DataGridView();
            txtId = new TextBox();
            txtRoleName = new TextBox();
            txtDescription = new TextBox();
            btnLoad = new Button();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvRoles).BeginInit();
            SuspendLayout();
            // 
            // dgvRoles
            // 
            dgvRoles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRoles.Location = new Point(35, 96);
            dgvRoles.Name = "dgvRoles";
            dgvRoles.Size = new Size(340, 209);
            dgvRoles.TabIndex = 0;
            // 
            // txtId
            // 
            txtId.Location = new Point(444, 83);
            txtId.Name = "txtId";
            txtId.Size = new Size(100, 23);
            txtId.TabIndex = 1;
            // 
            // txtRoleName
            // 
            txtRoleName.Location = new Point(444, 147);
            txtRoleName.Name = "txtRoleName";
            txtRoleName.Size = new Size(100, 23);
            txtRoleName.TabIndex = 2;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(444, 209);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(100, 23);
            txtDescription.TabIndex = 3;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(286, 39);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(75, 23);
            btnLoad.TabIndex = 4;
            btnLoad.Text = "button1";
            btnLoad.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(381, 250);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(65, 23);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "button2";
            btnAdd.UseVisualStyleBackColor = true;
            //btnAdd.Click += button2_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(452, 250);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(62, 23);
            btnUpdate.TabIndex = 6;
            btnUpdate.Text = "button3";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(520, 250);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(59, 23);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "button4";
            btnDelete.UseVisualStyleBackColor = true;
            //btnDelete.Click += button4_Click;
            // 
            // FormRoleManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(btnLoad);
            Controls.Add(txtDescription);
            Controls.Add(txtRoleName);
            Controls.Add(txtId);
            Controls.Add(dgvRoles);
            Name = "FormRoleManagement";
            Text = "FormRoleManagement";
            ((System.ComponentModel.ISupportInitialize)dgvRoles).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvRoles;
        private TextBox txtId;
        private TextBox txtRoleName;
        private TextBox txtDescription;
        private Button btnLoad;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
    }
}