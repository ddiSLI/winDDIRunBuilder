
namespace winDDIRunBuilder
{
    partial class frmPlateSample
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblPlate = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txbBarcode = new System.Windows.Forms.TextBox();
            this.txbEnterSampleId = new System.Windows.Forms.TextBox();
            this.dgvSamples = new System.Windows.Forms.DataGridView();
            this.SampleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Well = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifiedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Checked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ToPrint = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnUncheck = new System.Windows.Forms.Button();
            this.btnPrintLbl = new System.Windows.Forms.Button();
            this.btnGoBack = new System.Windows.Forms.Button();
            this.btnAddQC = new System.Windows.Forms.Button();
            this.btnCheckAll = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSamples)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPlate
            // 
            this.lblPlate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPlate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlate.Location = new System.Drawing.Point(221, 10);
            this.lblPlate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPlate.Name = "lblPlate";
            this.lblPlate.Size = new System.Drawing.Size(240, 38);
            this.lblPlate.TabIndex = 57;
            this.lblPlate.Text = "Current Plate:";
            // 
            // lblMsg
            // 
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(19, 56);
            this.lblMsg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(929, 52);
            this.lblMsg.TabIndex = 73;
            this.lblMsg.Text = "[Status Msg]";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(217, -44);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 18);
            this.label2.TabIndex = 71;
            this.label2.Text = "Assay:";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, -44);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 18);
            this.label1.TabIndex = 69;
            this.label1.Text = "PlateId:";
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 8);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(181, 15);
            this.label4.TabIndex = 77;
            this.label4.Text = "Enter PlateId:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txbBarcode
            // 
            this.txbBarcode.BackColor = System.Drawing.SystemColors.Info;
            this.txbBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbBarcode.Location = new System.Drawing.Point(19, 25);
            this.txbBarcode.Margin = new System.Windows.Forms.Padding(2);
            this.txbBarcode.Name = "txbBarcode";
            this.txbBarcode.Size = new System.Drawing.Size(182, 24);
            this.txbBarcode.TabIndex = 76;
            this.txbBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbBarcode_KeyDown);
            // 
            // txbEnterSampleId
            // 
            this.txbEnterSampleId.BackColor = System.Drawing.SystemColors.Info;
            this.txbEnterSampleId.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbEnterSampleId.Location = new System.Drawing.Point(477, 24);
            this.txbEnterSampleId.Margin = new System.Windows.Forms.Padding(2);
            this.txbEnterSampleId.Name = "txbEnterSampleId";
            this.txbEnterSampleId.Size = new System.Drawing.Size(181, 24);
            this.txbEnterSampleId.TabIndex = 78;
            this.txbEnterSampleId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbEnterSampleId_KeyDown);
            // 
            // dgvSamples
            // 
            this.dgvSamples.AllowUserToAddRows = false;
            this.dgvSamples.AllowUserToDeleteRows = false;
            this.dgvSamples.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvSamples.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSamples.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSamples.ColumnHeadersHeight = 40;
            this.dgvSamples.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SampleId,
            this.Well,
            this.Status,
            this.Type,
            this.ModifiedDate,
            this.Checked,
            this.ToPrint});
            this.dgvSamples.EnableHeadersVisualStyles = false;
            this.dgvSamples.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvSamples.Location = new System.Drawing.Point(19, 124);
            this.dgvSamples.Margin = new System.Windows.Forms.Padding(2);
            this.dgvSamples.Name = "dgvSamples";
            this.dgvSamples.RowHeadersVisible = false;
            this.dgvSamples.RowHeadersWidth = 51;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvSamples.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSamples.RowTemplate.Height = 42;
            this.dgvSamples.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSamples.Size = new System.Drawing.Size(929, 618);
            this.dgvSamples.TabIndex = 80;
            // 
            // SampleId
            // 
            this.SampleId.HeaderText = "SampleId";
            this.SampleId.MinimumWidth = 6;
            this.SampleId.Name = "SampleId";
            this.SampleId.ReadOnly = true;
            this.SampleId.Width = 218;
            // 
            // Well
            // 
            this.Well.HeaderText = "Well";
            this.Well.MinimumWidth = 6;
            this.Well.Name = "Well";
            this.Well.ReadOnly = true;
            this.Well.Width = 148;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 6;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Status.Width = 138;
            // 
            // Type
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.Type.DefaultCellStyle = dataGridViewCellStyle2;
            this.Type.HeaderText = "Sample Type";
            this.Type.MinimumWidth = 6;
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 168;
            // 
            // ModifiedDate
            // 
            this.ModifiedDate.HeaderText = "ModifiedDate";
            this.ModifiedDate.MinimumWidth = 6;
            this.ModifiedDate.Name = "ModifiedDate";
            this.ModifiedDate.ReadOnly = true;
            this.ModifiedDate.Visible = false;
            this.ModifiedDate.Width = 125;
            // 
            // Checked
            // 
            this.Checked.HeaderText = "Checked";
            this.Checked.Name = "Checked";
            this.Checked.ReadOnly = true;
            // 
            // ToPrint
            // 
            this.ToPrint.HeaderText = "To Print";
            this.ToPrint.MinimumWidth = 6;
            this.ToPrint.Name = "ToPrint";
            this.ToPrint.Width = 108;
            // 
            // btnUncheck
            // 
            this.btnUncheck.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnUncheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUncheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUncheck.Image = global::winDDIRunBuilder.Properties.Resources.uncheck24;
            this.btnUncheck.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUncheck.Location = new System.Drawing.Point(975, 198);
            this.btnUncheck.Margin = new System.Windows.Forms.Padding(2);
            this.btnUncheck.Name = "btnUncheck";
            this.btnUncheck.Size = new System.Drawing.Size(127, 51);
            this.btnUncheck.TabIndex = 49;
            this.btnUncheck.Text = "Uncheck All";
            this.btnUncheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUncheck.UseVisualStyleBackColor = true;
            this.btnUncheck.Click += new System.EventHandler(this.btnUncheck_Click);
            // 
            // btnPrintLbl
            // 
            this.btnPrintLbl.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnPrintLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintLbl.Image = global::winDDIRunBuilder.Properties.Resources.Print24;
            this.btnPrintLbl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintLbl.Location = new System.Drawing.Point(975, 272);
            this.btnPrintLbl.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrintLbl.Name = "btnPrintLbl";
            this.btnPrintLbl.Size = new System.Drawing.Size(127, 51);
            this.btnPrintLbl.TabIndex = 72;
            this.btnPrintLbl.Text = "Print Label(s)";
            this.btnPrintLbl.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrintLbl.UseVisualStyleBackColor = true;
            this.btnPrintLbl.Click += new System.EventHandler(this.btnPrintLbl_Click);
            // 
            // btnGoBack
            // 
            this.btnGoBack.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnGoBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoBack.Image = global::winDDIRunBuilder.Properties.Resources.Back32;
            this.btnGoBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoBack.Location = new System.Drawing.Point(975, 3);
            this.btnGoBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnGoBack.Name = "btnGoBack";
            this.btnGoBack.Size = new System.Drawing.Size(127, 51);
            this.btnGoBack.TabIndex = 68;
            this.btnGoBack.Text = "GoBack";
            this.btnGoBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGoBack.UseVisualStyleBackColor = true;
            this.btnGoBack.Click += new System.EventHandler(this.btnGoBack_Click);
            // 
            // btnAddQC
            // 
            this.btnAddQC.Enabled = false;
            this.btnAddQC.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnAddQC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddQC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddQC.Image = global::winDDIRunBuilder.Properties.Resources.Add32;
            this.btnAddQC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddQC.Location = new System.Drawing.Point(832, 3);
            this.btnAddQC.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddQC.Name = "btnAddQC";
            this.btnAddQC.Size = new System.Drawing.Size(116, 45);
            this.btnAddQC.TabIndex = 67;
            this.btnAddQC.Text = "Get Samples From File";
            this.btnAddQC.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddQC.UseVisualStyleBackColor = true;
            this.btnAddQC.Click += new System.EventHandler(this.btnAddQC_Click);
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnCheckAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckAll.Image = global::winDDIRunBuilder.Properties.Resources.check24;
            this.btnCheckAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCheckAll.Location = new System.Drawing.Point(975, 129);
            this.btnCheckAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(127, 51);
            this.btnCheckAll.TabIndex = 66;
            this.btnCheckAll.Text = "Select All";
            this.btnCheckAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCheckAll.UseVisualStyleBackColor = true;
            this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(477, 7);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 15);
            this.label3.TabIndex = 81;
            this.label3.Text = "Enter Sample Id:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // frmPlateSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 771);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvSamples);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblPlate);
            this.Controls.Add(this.txbBarcode);
            this.Controls.Add(this.btnUncheck);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnPrintLbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGoBack);
            this.Controls.Add(this.btnAddQC);
            this.Controls.Add(this.btnCheckAll);
            this.Controls.Add(this.txbEnterSampleId);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmPlateSample";
            this.Text = "frmPlateSample";
            this.Load += new System.EventHandler(this.frmPlateSample_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSamples)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblPlate;
        private System.Windows.Forms.Button btnUncheck;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnPrintLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGoBack;
        private System.Windows.Forms.Button btnAddQC;
        private System.Windows.Forms.Button btnCheckAll;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbBarcode;
        private System.Windows.Forms.TextBox txbEnterSampleId;
        private System.Windows.Forms.DataGridView dgvSamples;
        private System.Windows.Forms.DataGridViewTextBoxColumn SampleId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Well;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifiedDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Checked;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ToPrint;
        private System.Windows.Forms.Label label3;
    }
}