﻿
namespace winDDIRunBuilder
{
    partial class frmQC
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvSamplePlate = new System.Windows.Forms.DataGridView();
            this.dgvQCSamples = new System.Windows.Forms.DataGridView();
            this.Include = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Sample = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WellX = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.WellY = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Prefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HarvestId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txbPlateId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbExportFormat = new System.Windows.Forms.ComboBox();
            this.txbExportPath = new System.Windows.Forms.TextBox();
            this.btnSetLocation = new System.Windows.Forms.Button();
            this.btnGetQC = new System.Windows.Forms.Button();
            this.btnGoBack = new System.Windows.Forms.Button();
            this.btnSent = new System.Windows.Forms.Button();
            this.btnAddQC = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbAssay = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSamplePlate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQCSamples)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSamplePlate
            // 
            this.dgvSamplePlate.AllowUserToAddRows = false;
            this.dgvSamplePlate.AllowUserToDeleteRows = false;
            this.dgvSamplePlate.AllowUserToOrderColumns = true;
            this.dgvSamplePlate.AllowUserToResizeColumns = false;
            this.dgvSamplePlate.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvSamplePlate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvSamplePlate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSamplePlate.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvSamplePlate.Location = new System.Drawing.Point(23, 270);
            this.dgvSamplePlate.Name = "dgvSamplePlate";
            this.dgvSamplePlate.RowHeadersVisible = false;
            this.dgvSamplePlate.RowHeadersWidth = 51;
            this.dgvSamplePlate.RowTemplate.Height = 24;
            this.dgvSamplePlate.Size = new System.Drawing.Size(1175, 546);
            this.dgvSamplePlate.TabIndex = 1;
            this.dgvSamplePlate.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSamplePlate_CellContentClick);
            this.dgvSamplePlate.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvSamplePlate_CellPainting);
            // 
            // dgvQCSamples
            // 
            this.dgvQCSamples.AllowUserToAddRows = false;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvQCSamples.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvQCSamples.ColumnHeadersHeight = 28;
            this.dgvQCSamples.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Include,
            this.Sample,
            this.WellX,
            this.WellY,
            this.Prefix,
            this.HarvestId});
            this.dgvQCSamples.Location = new System.Drawing.Point(23, 65);
            this.dgvQCSamples.Name = "dgvQCSamples";
            this.dgvQCSamples.RowHeadersVisible = false;
            this.dgvQCSamples.RowHeadersWidth = 51;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvQCSamples.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvQCSamples.RowTemplate.Height = 24;
            this.dgvQCSamples.Size = new System.Drawing.Size(589, 163);
            this.dgvQCSamples.TabIndex = 2;
            // 
            // Include
            // 
            this.Include.HeaderText = "Include";
            this.Include.MinimumWidth = 6;
            this.Include.Name = "Include";
            this.Include.Width = 80;
            // 
            // Sample
            // 
            this.Sample.HeaderText = "Sample";
            this.Sample.MinimumWidth = 6;
            this.Sample.Name = "Sample";
            this.Sample.Width = 150;
            // 
            // WellX
            // 
            this.WellX.HeaderText = "WellX";
            this.WellX.MinimumWidth = 6;
            this.WellX.Name = "WellX";
            this.WellX.Width = 90;
            // 
            // WellY
            // 
            this.WellY.HeaderText = "WellY";
            this.WellY.MinimumWidth = 6;
            this.WellY.Name = "WellY";
            this.WellY.Width = 90;
            // 
            // Prefix
            // 
            this.Prefix.HeaderText = "Prefix";
            this.Prefix.MinimumWidth = 6;
            this.Prefix.Name = "Prefix";
            this.Prefix.Visible = false;
            this.Prefix.Width = 125;
            // 
            // HarvestId
            // 
            this.HarvestId.HeaderText = "HarvestId";
            this.HarvestId.MinimumWidth = 6;
            this.HarvestId.Name = "HarvestId";
            this.HarvestId.Visible = false;
            this.HarvestId.Width = 125;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 22);
            this.label1.TabIndex = 52;
            this.label1.Text = "PlateId:";
            // 
            // txbPlateId
            // 
            this.txbPlateId.BackColor = System.Drawing.SystemColors.Info;
            this.txbPlateId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbPlateId.Location = new System.Drawing.Point(23, 32);
            this.txbPlateId.Name = "txbPlateId";
            this.txbPlateId.Size = new System.Drawing.Size(187, 27);
            this.txbPlateId.TabIndex = 53;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(245, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 22);
            this.label2.TabIndex = 54;
            this.label2.Text = "Assay:";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(358, 22);
            this.label3.TabIndex = 57;
            this.label3.Text = "Export Format:";
            // 
            // cbExportFormat
            // 
            this.cbExportFormat.BackColor = System.Drawing.SystemColors.Info;
            this.cbExportFormat.DropDownWidth = 228;
            this.cbExportFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbExportFormat.FormattingEnabled = true;
            this.cbExportFormat.Items.AddRange(new object[] {
            "Gen5",
            "Luminex GA-MAP",
            "Luminex GPP",
            "Aus",
            "ABI750",
            "Kashi.CSV",
            "Manual"});
            this.cbExportFormat.Location = new System.Drawing.Point(15, 43);
            this.cbExportFormat.Name = "cbExportFormat";
            this.cbExportFormat.Size = new System.Drawing.Size(358, 28);
            this.cbExportFormat.TabIndex = 58;
            this.cbExportFormat.SelectedIndexChanged += new System.EventHandler(this.cbExportFormat_SelectedIndexChanged);
            // 
            // txbExportPath
            // 
            this.txbExportPath.BackColor = System.Drawing.SystemColors.Info;
            this.txbExportPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbExportPath.Location = new System.Drawing.Point(15, 102);
            this.txbExportPath.Multiline = true;
            this.txbExportPath.Name = "txbExportPath";
            this.txbExportPath.Size = new System.Drawing.Size(358, 48);
            this.txbExportPath.TabIndex = 59;
            this.txbExportPath.TextChanged += new System.EventHandler(this.txbExportPath_TextChanged);
            // 
            // btnSetLocation
            // 
            this.btnSetLocation.Location = new System.Drawing.Point(14, 79);
            this.btnSetLocation.Name = "btnSetLocation";
            this.btnSetLocation.Size = new System.Drawing.Size(360, 23);
            this.btnSetLocation.TabIndex = 60;
            this.btnSetLocation.Text = "Set Export Location";
            this.btnSetLocation.UseVisualStyleBackColor = true;
            this.btnSetLocation.Click += new System.EventHandler(this.btnSetLocation_Click);
            // 
            // btnGetQC
            // 
            this.btnGetQC.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnGetQC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetQC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetQC.Image = global::winDDIRunBuilder.Properties.Resources.qc;
            this.btnGetQC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGetQC.Location = new System.Drawing.Point(441, 10);
            this.btnGetQC.Name = "btnGetQC";
            this.btnGetQC.Size = new System.Drawing.Size(171, 48);
            this.btnGetQC.TabIndex = 56;
            this.btnGetQC.Text = "Get QC Plate";
            this.btnGetQC.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGetQC.UseVisualStyleBackColor = true;
            this.btnGetQC.Click += new System.EventHandler(this.btnGetQC_Click);
            // 
            // btnGoBack
            // 
            this.btnGoBack.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnGoBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoBack.Image = global::winDDIRunBuilder.Properties.Resources.Back32;
            this.btnGoBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoBack.Location = new System.Drawing.Point(618, 11);
            this.btnGoBack.Name = "btnGoBack";
            this.btnGoBack.Size = new System.Drawing.Size(154, 48);
            this.btnGoBack.TabIndex = 50;
            this.btnGoBack.Text = "GoBack";
            this.btnGoBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGoBack.UseVisualStyleBackColor = true;
            this.btnGoBack.Click += new System.EventHandler(this.btnGoBack_Click);
            // 
            // btnSent
            // 
            this.btnSent.Enabled = false;
            this.btnSent.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnSent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSent.Image = global::winDDIRunBuilder.Properties.Resources.export32;
            this.btnSent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSent.Location = new System.Drawing.Point(19, 158);
            this.btnSent.Name = "btnSent";
            this.btnSent.Size = new System.Drawing.Size(154, 50);
            this.btnSent.TabIndex = 49;
            this.btnSent.Text = "Sent To Instrument";
            this.btnSent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSent.UseVisualStyleBackColor = true;
            this.btnSent.Click += new System.EventHandler(this.btnSent_Click);
            // 
            // btnAddQC
            // 
            this.btnAddQC.Enabled = false;
            this.btnAddQC.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnAddQC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddQC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddQC.Image = global::winDDIRunBuilder.Properties.Resources.Add32;
            this.btnAddQC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddQC.Location = new System.Drawing.Point(618, 167);
            this.btnAddQC.Name = "btnAddQC";
            this.btnAddQC.Size = new System.Drawing.Size(154, 48);
            this.btnAddQC.TabIndex = 48;
            this.btnAddQC.Text = "Add QC";
            this.btnAddQC.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddQC.UseVisualStyleBackColor = true;
            this.btnAddQC.Click += new System.EventHandler(this.btnAddQC_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = global::winDDIRunBuilder.Properties.Resources.Add32;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(618, 107);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(154, 32);
            this.btnAdd.TabIndex = 47;
            this.btnAdd.Text = "Add A Sample";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(23, 231);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(1175, 36);
            this.lblMsg.TabIndex = 61;
            this.lblMsg.Text = "[Status Msg]";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSetLocation);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbExportFormat);
            this.groupBox1.Controls.Add(this.txbExportPath);
            this.groupBox1.Controls.Add(this.btnSent);
            this.groupBox1.Location = new System.Drawing.Point(804, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 217);
            this.groupBox1.TabIndex = 62;
            this.groupBox1.TabStop = false;
            // 
            // cmbAssay
            // 
            this.cmbAssay.BackColor = System.Drawing.SystemColors.Info;
            this.cmbAssay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAssay.FormattingEnabled = true;
            this.cmbAssay.Location = new System.Drawing.Point(245, 32);
            this.cmbAssay.Name = "cmbAssay";
            this.cmbAssay.Size = new System.Drawing.Size(190, 28);
            this.cmbAssay.TabIndex = 63;
            // 
            // frmQC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 822);
            this.Controls.Add(this.cmbAssay);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnGetQC);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txbPlateId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGoBack);
            this.Controls.Add(this.btnAddQC);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvQCSamples);
            this.Controls.Add(this.dgvSamplePlate);
            this.Name = "frmQC";
            this.Text = "frmQC";
            this.Load += new System.EventHandler(this.frmQC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSamplePlate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQCSamples)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSamplePlate;
        private System.Windows.Forms.DataGridView dgvQCSamples;
        private System.Windows.Forms.Button btnAddQC;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSent;
        private System.Windows.Forms.Button btnGoBack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbPlateId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGetQC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbExportFormat;
        private System.Windows.Forms.TextBox txbExportPath;
        private System.Windows.Forms.Button btnSetLocation;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Include;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sample;
        private System.Windows.Forms.DataGridViewComboBoxColumn WellX;
        private System.Windows.Forms.DataGridViewComboBoxColumn WellY;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbAssay;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prefix;
        private System.Windows.Forms.DataGridViewTextBoxColumn HarvestId;
    }
}