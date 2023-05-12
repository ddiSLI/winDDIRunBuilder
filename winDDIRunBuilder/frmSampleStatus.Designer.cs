
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cklDbTests = new System.Windows.Forms.CheckedListBox();
            this.cbDept = new System.Windows.Forms.ComboBox();
            this.lblDept = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownMonths = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblTolSamples = new System.Windows.Forms.Label();
            this.dgvSamples = new System.Windows.Forms.DataGridView();
            this.UnReject = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SampleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DBTest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifiedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlateId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlateVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnUnReject = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMonths)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSamples)).BeginInit();
            this.SuspendLayout();
            // 
            // cklDbTests
            // 
            this.cklDbTests.BackColor = System.Drawing.SystemColors.Info;
            this.cklDbTests.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cklDbTests.FormattingEnabled = true;
            this.cklDbTests.Location = new System.Drawing.Point(301, 37);
            this.cklDbTests.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cklDbTests.Name = "cklDbTests";
            this.cklDbTests.Size = new System.Drawing.Size(249, 88);
            this.cklDbTests.TabIndex = 1;
            this.cklDbTests.SelectedIndexChanged += new System.EventHandler(this.cklDbTests_SelectedIndexChanged);
            // 
            // cbDept
            // 
            this.cbDept.BackColor = System.Drawing.SystemColors.Info;
            this.cbDept.DropDownWidth = 300;
            this.cbDept.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDept.FormattingEnabled = true;
            this.cbDept.Location = new System.Drawing.Point(10, 37);
            this.cbDept.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbDept.Name = "cbDept";
            this.cbDept.Size = new System.Drawing.Size(196, 28);
            this.cbDept.TabIndex = 4;
            this.cbDept.SelectedIndexChanged += new System.EventHandler(this.cbDept_SelectedIndexChanged);
            // 
            // lblDept
            // 
            this.lblDept.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDept.Location = new System.Drawing.Point(10, 16);
            this.lblDept.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(195, 20);
            this.lblDept.TabIndex = 5;
            this.lblDept.Text = "Department:";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(10, 93);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "In Recent Week(s):";
            // 
            // numericUpDownMonths
            // 
            this.numericUpDownMonths.BackColor = System.Drawing.SystemColors.Info;
            this.numericUpDownMonths.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownMonths.Location = new System.Drawing.Point(10, 113);
            this.numericUpDownMonths.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numericUpDownMonths.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMonths.Name = "numericUpDownMonths";
            this.numericUpDownMonths.Size = new System.Drawing.Size(194, 26);
            this.numericUpDownMonths.TabIndex = 8;
            this.numericUpDownMonths.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownMonths.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(301, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "DBTest Type:";
            // 
            // lblMsg
            // 
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(9, 150);
            this.lblMsg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(540, 53);
            this.lblMsg.TabIndex = 41;
            this.lblMsg.Text = "[Status Msg]";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTolSamples
            // 
            this.lblTolSamples.AutoSize = true;
            this.lblTolSamples.Location = new System.Drawing.Point(10, 541);
            this.lblTolSamples.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTolSamples.Name = "lblTolSamples";
            this.lblTolSamples.Size = new System.Drawing.Size(77, 13);
            this.lblTolSamples.TabIndex = 43;
            this.lblTolSamples.Text = "Total Samples:";
            // 
            // dgvSamples
            // 
            this.dgvSamples.AllowUserToAddRows = false;
            this.dgvSamples.AllowUserToDeleteRows = false;
            this.dgvSamples.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvSamples.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSamples.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSamples.ColumnHeadersHeight = 40;
            this.dgvSamples.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UnReject,
            this.SampleId,
            this.DBTest,
            this.ModifiedDate,
            this.PlateId,
            this.PlateVersion,
            this.Id});
            this.dgvSamples.EnableHeadersVisualStyles = false;
            this.dgvSamples.GridColor = System.Drawing.SystemColors.Control;
            this.dgvSamples.Location = new System.Drawing.Point(9, 212);
            this.dgvSamples.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvSamples.Name = "dgvSamples";
            this.dgvSamples.RowHeadersVisible = false;
            this.dgvSamples.RowHeadersWidth = 51;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvSamples.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvSamples.RowTemplate.Height = 42;
            this.dgvSamples.Size = new System.Drawing.Size(765, 321);
            this.dgvSamples.TabIndex = 44;
            // 
            // UnReject
            // 
            this.UnReject.HeaderText = "Un-Reject";
            this.UnReject.MinimumWidth = 6;
            this.UnReject.Name = "UnReject";
            this.UnReject.Width = 82;
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
            this.DBTest.HeaderText = "DBTest";
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
            // PlateId
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.PlateId.DefaultCellStyle = dataGridViewCellStyle5;
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
            // btnUnReject
            // 
            this.btnUnReject.Enabled = false;
            this.btnUnReject.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnUnReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnReject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnReject.Image = global::winDDIRunBuilder.Properties.Resources.Process32;
            this.btnUnReject.Location = new System.Drawing.Point(649, 143);
            this.btnUnReject.Margin = new System.Windows.Forms.Padding(2);
            this.btnUnReject.Name = "btnUnReject";
            this.btnUnReject.Size = new System.Drawing.Size(125, 47);
            this.btnUnReject.TabIndex = 42;
            this.btnUnReject.Text = "Process UnReject";
            this.btnUnReject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUnReject.UseVisualStyleBackColor = true;
            this.btnUnReject.Click += new System.EventHandler(this.btnUnReject_Click);
            // 
            // btnGo
            // 
            this.btnGo.Enabled = false;
            this.btnGo.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.Image = global::winDDIRunBuilder.Properties.Resources.Go32;
            this.btnGo.Location = new System.Drawing.Point(649, 74);
            this.btnGo.Margin = new System.Windows.Forms.Padding(2);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(125, 46);
            this.btnGo.TabIndex = 40;
            this.btnGo.Text = "Go";
            this.btnGo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnExit
            // 
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = global::winDDIRunBuilder.Properties.Resources.Back32;
            this.btnExit.Location = new System.Drawing.Point(649, 11);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(125, 47);
            this.btnExit.TabIndex = 39;
            this.btnExit.Text = "Back";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmSampleStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(789, 571);
            this.Controls.Add(this.dgvSamples);
            this.Controls.Add(this.lblTolSamples);
            this.Controls.Add(this.btnUnReject);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownMonths);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbDept);
            this.Controls.Add(this.lblDept);
            this.Controls.Add(this.cklDbTests);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmSampleStatus";
            this.Text = "Sample Status Report - DDI RunBuilder";
            this.Load += new System.EventHandler(this.frmSampleStatus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMonths)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSamples)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckedListBox cklDbTests;
        private System.Windows.Forms.ComboBox cbDept;
        private System.Windows.Forms.Label lblDept;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownMonths;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnUnReject;
        private System.Windows.Forms.Label lblTolSamples;
        private System.Windows.Forms.DataGridView dgvSamples;
        private System.Windows.Forms.DataGridViewCheckBoxColumn UnReject;
        private System.Windows.Forms.DataGridViewTextBoxColumn SampleId;
        private System.Windows.Forms.DataGridViewTextBoxColumn DBTest;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifiedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlateId;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlateVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
    }
}