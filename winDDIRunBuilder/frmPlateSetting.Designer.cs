
namespace winDDIRunBuilder
{
    partial class frmPlateSetting
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.dgvPlateSet = new System.Windows.Forms.DataGridView();
            this.Included = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DestPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourcePlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourcePlateIsNew = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PlateDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlateId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblProtocol = new System.Windows.Forms.Label();
            this.fileWatcherBCR = new System.IO.FileSystemWatcher();
            this.lblMsg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlateSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileWatcherBCR)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Image = global::winDDIRunBuilder.Properties.Resources.UserExit32;
            this.btnCancel.Location = new System.Drawing.Point(630, 478);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(156, 49);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnGo
            // 
            this.btnGo.Enabled = false;
            this.btnGo.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.Image = global::winDDIRunBuilder.Properties.Resources.addFolder32;
            this.btnGo.Location = new System.Drawing.Point(408, 478);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(156, 49);
            this.btnGo.TabIndex = 21;
            this.btnGo.Text = "Go";
            this.btnGo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = global::winDDIRunBuilder.Properties.Resources.Print24;
            this.btnPrint.Location = new System.Drawing.Point(135, 478);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(199, 49);
            this.btnPrint.TabIndex = 23;
            this.btnPrint.Text = "Re-Print Plate Bar Code(s)";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dgvPlateSet
            // 
            this.dgvPlateSet.AllowUserToAddRows = false;
            this.dgvPlateSet.AllowUserToDeleteRows = false;
            this.dgvPlateSet.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvPlateSet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPlateSet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPlateSet.ColumnHeadersHeight = 48;
            this.dgvPlateSet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Included,
            this.DestPlate,
            this.SourcePlate,
            this.SourcePlateIsNew,
            this.PlateDesc,
            this.WorkList,
            this.PlateId});
            this.dgvPlateSet.EnableHeadersVisualStyles = false;
            this.dgvPlateSet.GridColor = System.Drawing.SystemColors.Control;
            this.dgvPlateSet.Location = new System.Drawing.Point(21, 61);
            this.dgvPlateSet.Name = "dgvPlateSet";
            this.dgvPlateSet.RowHeadersVisible = false;
            this.dgvPlateSet.RowHeadersWidth = 51;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPlateSet.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPlateSet.RowTemplate.Height = 42;
            this.dgvPlateSet.Size = new System.Drawing.Size(949, 380);
            this.dgvPlateSet.TabIndex = 25;
            this.dgvPlateSet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPlateSet_CellContentClick);
            // 
            // Included
            // 
            this.Included.HeaderText = "Included";
            this.Included.MinimumWidth = 6;
            this.Included.Name = "Included";
            this.Included.Width = 82;
            // 
            // DestPlate
            // 
            this.DestPlate.HeaderText = "Destination Plate";
            this.DestPlate.MinimumWidth = 6;
            this.DestPlate.Name = "DestPlate";
            this.DestPlate.ReadOnly = true;
            this.DestPlate.Width = 128;
            // 
            // SourcePlate
            // 
            this.SourcePlate.HeaderText = "Source Plate";
            this.SourcePlate.MinimumWidth = 6;
            this.SourcePlate.Name = "SourcePlate";
            this.SourcePlate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SourcePlate.Width = 128;
            // 
            // SourcePlateIsNew
            // 
            this.SourcePlateIsNew.HeaderText = "SourcePlate Is Ready";
            this.SourcePlateIsNew.MinimumWidth = 6;
            this.SourcePlateIsNew.Name = "SourcePlateIsNew";
            this.SourcePlateIsNew.Width = 108;
            // 
            // PlateDesc
            // 
            this.PlateDesc.HeaderText = "Description";
            this.PlateDesc.MinimumWidth = 6;
            this.PlateDesc.Name = "PlateDesc";
            this.PlateDesc.ReadOnly = true;
            this.PlateDesc.Width = 248;
            // 
            // WorkList
            // 
            this.WorkList.HeaderText = "Work List";
            this.WorkList.MinimumWidth = 6;
            this.WorkList.Name = "WorkList";
            this.WorkList.ReadOnly = true;
            this.WorkList.Width = 188;
            // 
            // PlateId
            // 
            this.PlateId.HeaderText = "PlateId";
            this.PlateId.MinimumWidth = 6;
            this.PlateId.Name = "PlateId";
            this.PlateId.ReadOnly = true;
            this.PlateId.Visible = false;
            this.PlateId.Width = 200;
            // 
            // lblProtocol
            // 
            this.lblProtocol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProtocol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProtocol.Location = new System.Drawing.Point(21, 16);
            this.lblProtocol.Name = "lblProtocol";
            this.lblProtocol.Size = new System.Drawing.Size(133, 45);
            this.lblProtocol.TabIndex = 26;
            this.lblProtocol.Text = "Protocol Name";
            this.lblProtocol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fileWatcherBCR
            // 
            this.fileWatcherBCR.EnableRaisingEvents = true;
            this.fileWatcherBCR.SynchronizingObject = this;
            this.fileWatcherBCR.Changed += new System.IO.FileSystemEventHandler(this.fileWatcherBCR_Changed);
            this.fileWatcherBCR.Created += new System.IO.FileSystemEventHandler(this.fileWatcherBCR_Created);
            // 
            // lblMsg
            // 
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(151, 16);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(819, 45);
            this.lblMsg.TabIndex = 27;
            this.lblMsg.Text = "Currently there is no BCR Samplefile.csv";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMsg.Click += new System.EventHandler(this.lblMsg_Click);
            // 
            // frmPlateSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(994, 563);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.dgvPlateSet);
            this.Controls.Add(this.lblProtocol);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmPlateSetting";
            this.Text = "ProtocolPlate Setting - DDI Run Builder";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmPlateSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlateSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileWatcherBCR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridView dgvPlateSet;
        private System.Windows.Forms.Label lblProtocol;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Included;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourcePlate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SourcePlateIsNew;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlateDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkList;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlateId;
        private System.IO.FileSystemWatcher fileWatcherBCR;
        private System.Windows.Forms.Label lblMsg;
    }
}