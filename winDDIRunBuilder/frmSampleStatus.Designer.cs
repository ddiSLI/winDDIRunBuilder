
namespace winDDIRunBuilder
{
    partial class frmSampleStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSampleStatus));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.dgvSamples = new System.Windows.Forms.DataGridView();
            this.btnUnReject = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.cmbModifiedBy = new System.Windows.Forms.ComboBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvDBTests = new System.Windows.Forms.DataGridView();
            this.Sel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DBTestCd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DBTestName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnReject = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SampleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DBTest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifiedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifiedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlateId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlateVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSamples)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDBTests)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMsg
            // 
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(9, 218);
            this.lblMsg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(978, 39);
            this.lblMsg.TabIndex = 41;
            this.lblMsg.Text = "[...message...]";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(9, 5);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(978, 29);
            this.lblStatus.TabIndex = 43;
            this.lblStatus.Text = "Total Samples:";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvSamples
            // 
            this.dgvSamples.AllowUserToAddRows = false;
            this.dgvSamples.AllowUserToDeleteRows = false;
            this.dgvSamples.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.UnReject,
            this.Status,
            this.SampleId,
            this.DBTest,
            this.ModifiedDate,
            this.ModifiedBy,
            this.PlateId,
            this.PlateVersion,
            this.Id});
            this.dgvSamples.EnableHeadersVisualStyles = false;
            this.dgvSamples.GridColor = System.Drawing.SystemColors.Control;
            this.dgvSamples.Location = new System.Drawing.Point(9, 261);
            this.dgvSamples.Margin = new System.Windows.Forms.Padding(2);
            this.dgvSamples.Name = "dgvSamples";
            this.dgvSamples.RowHeadersVisible = false;
            this.dgvSamples.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvSamples.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSamples.RowTemplate.Height = 42;
            this.dgvSamples.Size = new System.Drawing.Size(978, 484);
            this.dgvSamples.TabIndex = 44;
            this.dgvSamples.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSamples_CellContentClick);
            // 
            // btnUnReject
            // 
            this.btnUnReject.Enabled = false;
            this.btnUnReject.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnUnReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnReject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnReject.Image = ((System.Drawing.Image)(resources.GetObject("btnUnReject.Image")));
            this.btnUnReject.Location = new System.Drawing.Point(839, 162);
            this.btnUnReject.Margin = new System.Windows.Forms.Padding(2);
            this.btnUnReject.Name = "btnUnReject";
            this.btnUnReject.Size = new System.Drawing.Size(149, 47);
            this.btnUnReject.TabIndex = 42;
            this.btnUnReject.Text = "Process UnReject";
            this.btnUnReject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUnReject.UseVisualStyleBackColor = true;
            this.btnUnReject.Click += new System.EventHandler(this.btnUnReject_Click);
            // 
            // btnGo
            // 
            this.btnGo.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.Image = ((System.Drawing.Image)(resources.GetObject("btnGo.Image")));
            this.btnGo.Location = new System.Drawing.Point(839, 99);
            this.btnGo.Margin = new System.Windows.Forms.Padding(2);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(149, 46);
            this.btnGo.TabIndex = 40;
            this.btnGo.Text = "Go / Refresh";
            this.btnGo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnExit
            // 
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(837, 11);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(149, 47);
            this.btnExit.TabIndex = 39;
            this.btnExit.Text = "Back";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cmbModifiedBy
            // 
            this.cmbModifiedBy.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.cmbModifiedBy.DropDownWidth = 300;
            this.cmbModifiedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbModifiedBy.FormattingEnabled = true;
            this.cmbModifiedBy.Location = new System.Drawing.Point(9, 98);
            this.cmbModifiedBy.Margin = new System.Windows.Forms.Padding(2);
            this.cmbModifiedBy.Name = "cmbModifiedBy";
            this.cmbModifiedBy.Size = new System.Drawing.Size(233, 28);
            this.cmbModifiedBy.TabIndex = 47;
            this.cmbModifiedBy.SelectedIndexChanged += new System.EventHandler(this.cmbModifiedBy_SelectedIndexChanged);
            // 
            // cmbStatus
            // 
            this.cmbStatus.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.cmbStatus.DropDownWidth = 300;
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(11, 30);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(2);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(231, 28);
            this.cmbStatus.TabIndex = 56;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 8);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(231, 20);
            this.label4.TabIndex = 58;
            this.label4.Text = "Filter By Status";
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 79);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(233, 20);
            this.label5.TabIndex = 59;
            this.label5.Text = "Filter By ModifiedBy";
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(261, 8);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(547, 20);
            this.label6.TabIndex = 60;
            this.label6.Text = "Filter By Test(s)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 750);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(999, 39);
            this.panel1.TabIndex = 61;
            // 
            // dgvDBTests
            // 
            this.dgvDBTests.AllowUserToAddRows = false;
            this.dgvDBTests.AllowUserToDeleteRows = false;
            this.dgvDBTests.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDBTests.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDBTests.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDBTests.ColumnHeadersHeight = 24;
            this.dgvDBTests.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sel,
            this.DBTestCd,
            this.DBTestName});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDBTests.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDBTests.EnableHeadersVisualStyles = false;
            this.dgvDBTests.GridColor = System.Drawing.SystemColors.Control;
            this.dgvDBTests.Location = new System.Drawing.Point(261, 32);
            this.dgvDBTests.Margin = new System.Windows.Forms.Padding(2);
            this.dgvDBTests.Name = "dgvDBTests";
            this.dgvDBTests.RowHeadersVisible = false;
            this.dgvDBTests.RowHeadersWidth = 51;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDBTests.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDBTests.RowTemplate.Height = 30;
            this.dgvDBTests.Size = new System.Drawing.Size(547, 180);
            this.dgvDBTests.TabIndex = 62;
            // 
            // Sel
            // 
            this.Sel.HeaderText = "Sel";
            this.Sel.MinimumWidth = 6;
            this.Sel.Name = "Sel";
            this.Sel.Width = 50;
            // 
            // DBTestCd
            // 
            this.DBTestCd.HeaderText = "DBTest Code";
            this.DBTestCd.Name = "DBTestCd";
            this.DBTestCd.ReadOnly = true;
            this.DBTestCd.Width = 160;
            // 
            // DBTestName
            // 
            this.DBTestName.HeaderText = "DBTest Name";
            this.DBTestName.MinimumWidth = 6;
            this.DBTestName.Name = "DBTestName";
            this.DBTestName.ReadOnly = true;
            this.DBTestName.Width = 300;
            // 
            // UnReject
            // 
            this.UnReject.HeaderText = "Un-Reject";
            this.UnReject.MinimumWidth = 6;
            this.UnReject.Name = "UnReject";
            this.UnReject.Width = 82;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 120;
            // 
            // SampleId
            // 
            this.SampleId.HeaderText = "SampleId";
            this.SampleId.MinimumWidth = 6;
            this.SampleId.Name = "SampleId";
            this.SampleId.ReadOnly = true;
            this.SampleId.Width = 148;
            // 
            // DBTest
            // 
            this.DBTest.HeaderText = "Test";
            this.DBTest.MinimumWidth = 6;
            this.DBTest.Name = "DBTest";
            this.DBTest.ReadOnly = true;
            this.DBTest.Width = 328;
            // 
            // ModifiedDate
            // 
            this.ModifiedDate.HeaderText = "ModifiedDate";
            this.ModifiedDate.MinimumWidth = 6;
            this.ModifiedDate.Name = "ModifiedDate";
            this.ModifiedDate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ModifiedDate.Width = 168;
            // 
            // ModifiedBy
            // 
            this.ModifiedBy.HeaderText = "ModifiedBy";
            this.ModifiedBy.Name = "ModifiedBy";
            this.ModifiedBy.ReadOnly = true;
            // 
            // PlateId
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.PlateId.DefaultCellStyle = dataGridViewCellStyle2;
            this.PlateId.HeaderText = "PlateId";
            this.PlateId.MinimumWidth = 6;
            this.PlateId.Name = "PlateId";
            this.PlateId.ReadOnly = true;
            this.PlateId.Visible = false;
            this.PlateId.Width = 168;
            // 
            // PlateVersion
            // 
            this.PlateVersion.HeaderText = "PlateVersion";
            this.PlateVersion.MinimumWidth = 6;
            this.PlateVersion.Name = "PlateVersion";
            this.PlateVersion.Visible = false;
            this.PlateVersion.Width = 125;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.MinimumWidth = 6;
            this.Id.Name = "Id";
            this.Id.Visible = false;
            this.Id.Width = 125;
            // 
            // frmSampleStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(999, 789);
            this.Controls.Add(this.dgvDBTests);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.cmbModifiedBy);
            this.Controls.Add(this.dgvSamples);
            this.Controls.Add(this.btnUnReject);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.btnExit);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmSampleStatus";
            this.Text = "Sample Status Report - DDI RunBuilder";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmSampleStatus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSamples)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDBTests)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnUnReject;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DataGridView dgvSamples;
        private System.Windows.Forms.ComboBox cmbModifiedBy;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvDBTests;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Sel;
        private System.Windows.Forms.DataGridViewTextBoxColumn DBTestCd;
        private System.Windows.Forms.DataGridViewTextBoxColumn DBTestName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn UnReject;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn SampleId;
        private System.Windows.Forms.DataGridViewTextBoxColumn DBTest;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifiedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifiedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlateId;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlateVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
    }
}