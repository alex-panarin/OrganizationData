
namespace OrganizationData
{
    partial class OrganizationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrganizationForm));
            this.employeeDataGridView = new System.Windows.Forms.DataGridView();
            this.organizationDataGridView = new System.Windows.Forms.DataGridView();
            this.button_load = new System.Windows.Forms.Button();
            this.button_export = new System.Windows.Forms.Button();
            this.button_import = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.employeeDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.organizationDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // employeeDataGridView
            // 
            this.employeeDataGridView.AllowUserToAddRows = false;
            this.employeeDataGridView.AllowUserToDeleteRows = false;
            this.employeeDataGridView.AllowUserToResizeRows = false;
            this.employeeDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.employeeDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.employeeDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.employeeDataGridView.Location = new System.Drawing.Point(12, 263);
            this.employeeDataGridView.MultiSelect = false;
            this.employeeDataGridView.Name = "employeeDataGridView";
            this.employeeDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.employeeDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.employeeDataGridView.Size = new System.Drawing.Size(654, 201);
            this.employeeDataGridView.TabIndex = 1;
            // 
            // organizationDataGridView
            // 
            this.organizationDataGridView.AllowUserToAddRows = false;
            this.organizationDataGridView.AllowUserToDeleteRows = false;
            this.organizationDataGridView.AllowUserToResizeRows = false;
            this.organizationDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.organizationDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.organizationDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.organizationDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.organizationDataGridView.Location = new System.Drawing.Point(12, 62);
            this.organizationDataGridView.MultiSelect = false;
            this.organizationDataGridView.Name = "organizationDataGridView";
            this.organizationDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.organizationDataGridView.RowTemplate.DividerHeight = 1;
            this.organizationDataGridView.RowTemplate.ReadOnly = true;
            this.organizationDataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.organizationDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.organizationDataGridView.Size = new System.Drawing.Size(654, 176);
            this.organizationDataGridView.TabIndex = 0;
            // 
            // button_load
            // 
            this.button_load.Location = new System.Drawing.Point(12, 12);
            this.button_load.Name = "button_load";
            this.button_load.Size = new System.Drawing.Size(145, 23);
            this.button_load.TabIndex = 2;
            this.button_load.Text = "Загрузить из БД";
            this.button_load.UseVisualStyleBackColor = true;
            this.button_load.Click += new System.EventHandler(this.button_load_Click);
            // 
            // button_export
            // 
            this.button_export.Location = new System.Drawing.Point(191, 12);
            this.button_export.Name = "button_export";
            this.button_export.Size = new System.Drawing.Size(145, 23);
            this.button_export.TabIndex = 3;
            this.button_export.Text = "Экспорт в CSV";
            this.button_export.UseVisualStyleBackColor = true;
            this.button_export.Click += new System.EventHandler(this.button_export_Click);
            // 
            // button_import
            // 
            this.button_import.Location = new System.Drawing.Point(373, 12);
            this.button_import.Name = "button_import";
            this.button_import.Size = new System.Drawing.Size(145, 23);
            this.button_import.TabIndex = 4;
            this.button_import.Text = "Импорт из CSV";
            this.button_import.UseVisualStyleBackColor = true;
            this.button_import.Click += new System.EventHandler(this.button_import_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Список организаций";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 247);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Список сотрудников";
            // 
            // OrganizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 476);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_import);
            this.Controls.Add(this.button_export);
            this.Controls.Add(this.button_load);
            this.Controls.Add(this.employeeDataGridView);
            this.Controls.Add(this.organizationDataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OrganizationForm";
            this.Text = "Менеджер организаций";
            ((System.ComponentModel.ISupportInitialize)(this.employeeDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.organizationDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView employeeDataGridView;
        private System.Windows.Forms.DataGridView organizationDataGridView;
        private System.Windows.Forms.Button button_load;
        private System.Windows.Forms.Button button_export;
        private System.Windows.Forms.Button button_import;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

