
namespace winDDIRunBuilder
{
    partial class frmDDIRunBuilder
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvPlateSet = new System.Windows.Forms.DataGridView();
            this.Included = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DestPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourcePlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourcePlateIsNew = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PlateDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblPrompt = new System.Windows.Forms.Label();
            this.cbProtocolCd = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPlates = new System.Windows.Forms.ComboBox();
            this.dgvSamplePlate = new System.Windows.Forms.DataGridView();
            this.lblMsg = new System.Windows.Forms.Label();
            this.fileWatcherBCR = new System.IO.FileSystemWatcher();
            this.txbBarcode = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlateSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSamplePlate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileWatcherBCR)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPlateSet
            // 
            this.dgvPlateSet.AllowUserToAddRows = false;
            this.dgvPlateSet.AllowUserToDeleteRows = false;
            this.dgvPlateSet.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvPlateSet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPlateSet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPlateSet.ColumnHeadersHeight = 40;
            this.dgvPlateSet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Included,
            this.DestPlate,
            this.SourcePlate,
            this.SourcePlateIsNew,
            this.PlateDesc,
            this.WorkList});
            this.dgvPlateSet.EnableHeadersVisualStyles = false;
            this.dgvPlateSet.GridColor = System.Drawing.SystemColors.Control;
            this.dgvPlateSet.Location = new System.Drawing.Point(152, 54);
            this.dgvPlateSet.Name = "dgvPlateSet";
            this.dgvPlateSet.RowHeadersVisible = false;
            this.dgvPlateSet.RowHeadersWidth = 51;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPlateSet.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPlateSet.RowTemplate.Height = 42;
            this.dgvPlateSet.Size = new System.Drawing.Size(764, 181);
            this.dgvPlateSet.TabIndex = 26;
            this.dgvPlateSet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPlateSet_CellContentClick);
            // 
            // Included
            // 
            this.Included.HeaderText = "Included";
            this.Included.MinimumWidth = 6;
            this.Included.Name = "Included";
            this.Included.Width = 62;
            // 
            // DestPlate
            // 
            this.DestPlate.HeaderText = "Destination Plate";
            this.DestPlate.MinimumWidth = 6;
            this.DestPlate.Name = "DestPlate";
            this.DestPlate.ReadOnly = true;
            this.DestPlate.Width = 118;
            // 
            // SourcePlate
            // 
            this.SourcePlate.HeaderText = "Source Plate";
            this.SourcePlate.MinimumWidth = 6;
            this.SourcePlate.Name = "SourcePlate";
            this.SourcePlate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SourcePlate.Width = 118;
            // 
            // SourcePlateIsNew
            // 
            this.SourcePlateIsNew.HeaderText = "SourcePlate Is Ready";
            this.SourcePlateIsNew.MinimumWidth = 6;
            this.SourcePlateIsNew.Name = "SourcePlateIsNew";
            this.SourcePlateIsNew.Width = 80;
            // 
            // PlateDesc
            // 
            this.PlateDesc.HeaderText = "Description";
            this.PlateDesc.MinimumWidth = 6;
            this.PlateDesc.Name = "PlateDesc";
            this.PlateDesc.ReadOnly = true;
            this.PlateDesc.Width = 208;
            // 
            // WorkList
            // 
            this.WorkList.HeaderText = "Work List";
            this.WorkList.MinimumWidth = 6;
            this.WorkList.Name = "WorkList";
            this.WorkList.ReadOnly = true;
            this.WorkList.Width = 148;
            // 
            // lblPrompt
            // 
            this.lblPrompt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPrompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrompt.Location = new System.Drawing.Point(314, 2);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(602, 49);
            this.lblPrompt.TabIndex = 29;
            this.lblPrompt.Text = "Source File is ready to load. Or to get a existing batch to Modify.";
            this.lblPrompt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbProtocolCd
            // 
            this.cbProtocolCd.BackColor = System.Drawing.SystemColors.Info;
            this.cbProtocolCd.DropDownWidth = 300;
            this.cbProtocolCd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProtocolCd.FormattingEnabled = true;
            this.cbProtocolCd.Items.AddRange(new object[] {
            "Clone/Copy",
            "4 to 1 plate",
            "8 X 12"});
            this.cbProtocolCd.Location = new System.Drawing.Point(151, 23);
            this.cbProtocolCd.Name = "cbProtocolCd";
            this.cbProtocolCd.Size = new System.Drawing.Size(157, 28);
            this.cbProtocolCd.TabIndex = 27;
            this.cbProtocolCd.SelectedIndexChanged += new System.EventHandler(this.cbProtocolCd_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(152, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 20);
            this.label1.TabIndex = 28;
            this.label1.Text = "Janus Protocol Code:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::winDDIRunBuilder.Properties.Resources.ddilogo512;
            this.pictureBox1.Location = new System.Drawing.Point(18, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(103, 53);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = global::winDDIRunBuilder.Properties.Resources.UserExit32;
            this.btnExit.Location = new System.Drawing.Point(6, 379);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(126, 49);
            this.btnExit.TabIndex = 32;
            this.btnExit.Text = "Exit";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = global::winDDIRunBuilder.Properties.Resources.Print24;
            this.btnPrint.Location = new System.Drawing.Point(6, 73);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(126, 49);
            this.btnPrint.TabIndex = 31;
            this.btnPrint.Text = "Print Plate Bar Code";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Location = new System.Drawing.Point(152, 249);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 16);
            this.label3.TabIndex = 34;
            this.label3.Text = "Destination Plate:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // cbPlates
            // 
            this.cbPlates.BackColor = System.Drawing.SystemColors.Info;
            this.cbPlates.DropDownWidth = 300;
            this.cbPlates.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPlates.FormattingEnabled = true;
            this.cbPlates.Location = new System.Drawing.Point(152, 265);
            this.cbPlates.Name = "cbPlates";
            this.cbPlates.Size = new System.Drawing.Size(156, 28);
            this.cbPlates.TabIndex = 33;
            this.cbPlates.SelectedIndexChanged += new System.EventHandler(this.cbPlates_SelectedIndexChanged);
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
            this.dgvSamplePlate.Location = new System.Drawing.Point(152, 296);
            this.dgvSamplePlate.Name = "dgvSamplePlate";
            this.dgvSamplePlate.RowHeadersVisible = false;
            this.dgvSamplePlate.RowHeadersWidth = 51;
            this.dgvSamplePlate.RowTemplate.Height = 24;
            this.dgvSamplePlate.Size = new System.Drawing.Size(764, 310);
            this.dgvSamplePlate.TabIndex = 35;
            this.dgvSamplePlate.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSamplePlate_CellContentClick);
            // 
            // lblMsg
            // 
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(314, 249);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(602, 44);
            this.lblMsg.TabIndex = 36;
            this.lblMsg.Text = "[Status Msg]";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fileWatcherBCR
            // 
            this.fileWatcherBCR.EnableRaisingEvents = true;
            this.fileWatcherBCR.SynchronizingObject = this;
            this.fileWatcherBCR.Created += new System.IO.FileSystemEventHandler(this.fileWatcherBCR_Created);
            // 
            // txbBarcode
            // 
            this.txbBarcode.Location = new System.Drawing.Point(18, 448);
            this.txbBarcode.Name = "txbBarcode";
            this.txbBarcode.Size = new System.Drawing.Size(85, 20);
            this.txbBarcode.TabIndex = 37;
            // 
            // btnCreate
            // 
            this.btnCreate.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Image = global::winDDIRunBuilder.Properties.Resources.New32;
            this.btnCreate.Location = new System.Drawing.Point(6, 153);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(126, 49);
            this.btnCreate.TabIndex = 38;
            this.btnCreate.Text = "Create Worklist(s)";
            this.btnCreate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // frmDDIRunBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 634);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txbBarcode);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.dgvSamplePlate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbPlates);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblPrompt);
            this.Controls.Add(this.cbProtocolCd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dgvPlateSet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmDDIRunBuilder";
            this.Text = "DDI Run Builder - Home";
            this.Load += new System.EventHandler(this.frmDDIRunBuilder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlateSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSamplePlate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileWatcherBCR)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPlateSet;
        private System.Windows.Forms.Label lblPrompt;
        private System.Windows.Forms.ComboBox cbProtocolCd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbPlates;
        private System.Windows.Forms.DataGridView dgvSamplePlate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Included;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourcePlate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SourcePlateIsNew;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlateDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkList;
        private System.Windows.Forms.Label lblMsg;
        private System.IO.FileSystemWatcher fileWatcherBCR;
        private System.Windows.Forms.TextBox txbBarcode;
        private System.Windows.Forms.Button btnCreate;
    }
}