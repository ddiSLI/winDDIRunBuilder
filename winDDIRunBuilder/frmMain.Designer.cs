
namespace winDDIRunBuilder
{
    partial class frmMain
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
            this.lblPrompt = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbProtocolCd = new System.Windows.Forms.ComboBox();
            this.gbPlate = new System.Windows.Forms.GroupBox();
            this.dgvSamplePlate = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPlates = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCreateWorklist = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.txbBarcode = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPrintPlateBarcode = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.dgvPlateSet = new System.Windows.Forms.DataGridView();
            this.Included = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PlateDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurWorkList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourcePlateIsNew = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SourcePlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DestPlateIsNew = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DestPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScannedPlate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceIsReady = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.fileWatcherBCR = new System.IO.FileSystemWatcher();
            this.gbPlate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSamplePlate)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlateSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileWatcherBCR)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPrompt
            // 
            this.lblPrompt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPrompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrompt.Location = new System.Drawing.Point(340, 17);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(671, 54);
            this.lblPrompt.TabIndex = 13;
            this.lblPrompt.Text = "Source File is ready to load. Or to get a existing batch to Modify.";
            this.lblPrompt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(178, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Janus Protocol Code:";
            // 
            // cbProtocolCd
            // 
            this.cbProtocolCd.BackColor = System.Drawing.SystemColors.Info;
            this.cbProtocolCd.DropDownWidth = 300;
            this.cbProtocolCd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProtocolCd.FormattingEnabled = true;
            this.cbProtocolCd.Location = new System.Drawing.Point(178, 40);
            this.cbProtocolCd.Name = "cbProtocolCd";
            this.cbProtocolCd.Size = new System.Drawing.Size(156, 33);
            this.cbProtocolCd.TabIndex = 2;
            this.cbProtocolCd.SelectedIndexChanged += new System.EventHandler(this.cbProtocolCd_SelectedIndexChanged);
            // 
            // gbPlate
            // 
            this.gbPlate.Controls.Add(this.dgvSamplePlate);
            this.gbPlate.Controls.Add(this.label3);
            this.gbPlate.Controls.Add(this.cbPlates);
            this.gbPlate.Controls.Add(this.button2);
            this.gbPlate.Controls.Add(this.button1);
            this.gbPlate.Controls.Add(this.btnClear);
            this.gbPlate.Controls.Add(this.btnCreateWorklist);
            this.gbPlate.Controls.Add(this.lblMsg);
            this.gbPlate.Controls.Add(this.txbBarcode);
            this.gbPlate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPlate.Location = new System.Drawing.Point(7, 269);
            this.gbPlate.Name = "gbPlate";
            this.gbPlate.Size = new System.Drawing.Size(1017, 528);
            this.gbPlate.TabIndex = 2;
            this.gbPlate.TabStop = false;
            this.gbPlate.Text = "Plates";
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
            this.dgvSamplePlate.Location = new System.Drawing.Point(177, 74);
            this.dgvSamplePlate.Name = "dgvSamplePlate";
            this.dgvSamplePlate.RowHeadersVisible = false;
            this.dgvSamplePlate.RowHeadersWidth = 51;
            this.dgvSamplePlate.RowTemplate.Height = 24;
            this.dgvSamplePlate.Size = new System.Drawing.Size(833, 447);
            this.dgvSamplePlate.TabIndex = 0;
            this.dgvSamplePlate.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSamplePlate_CellContentClick);
            this.dgvSamplePlate.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvSamplePlate_CellPainting);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Location = new System.Drawing.Point(5, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 20);
            this.label3.TabIndex = 31;
            this.label3.Text = "Destination Plate:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // cbPlates
            // 
            this.cbPlates.BackColor = System.Drawing.SystemColors.Info;
            this.cbPlates.DropDownWidth = 300;
            this.cbPlates.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPlates.FormattingEnabled = true;
            this.cbPlates.Location = new System.Drawing.Point(5, 50);
            this.cbPlates.Name = "cbPlates";
            this.cbPlates.Size = new System.Drawing.Size(156, 33);
            this.cbPlates.TabIndex = 30;
            this.cbPlates.SelectedIndexChanged += new System.EventHandler(this.cbPlates_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = global::winDDIRunBuilder.Properties.Resources.BarcodeScanner32;
            this.button2.Location = new System.Drawing.Point(2, -265);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(199, 49);
            this.button2.TabIndex = 20;
            this.button2.Text = "Import From BCR";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::winDDIRunBuilder.Properties.Resources.searchLocation32;
            this.button1.Location = new System.Drawing.Point(2, -198);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(199, 49);
            this.button1.TabIndex = 22;
            this.button1.Text = "Import From DB\r\n";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Image = global::winDDIRunBuilder.Properties.Resources.clearFilter32;
            this.btnClear.Location = new System.Drawing.Point(0, 240);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(156, 49);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "Clear";
            this.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCreateWorklist
            // 
            this.btnCreateWorklist.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnCreateWorklist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateWorklist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateWorklist.Image = global::winDDIRunBuilder.Properties.Resources.New32;
            this.btnCreateWorklist.Location = new System.Drawing.Point(7, 102);
            this.btnCreateWorklist.Name = "btnCreateWorklist";
            this.btnCreateWorklist.Size = new System.Drawing.Size(154, 49);
            this.btnCreateWorklist.TabIndex = 17;
            this.btnCreateWorklist.Text = "Create Worklist(s)";
            this.btnCreateWorklist.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCreateWorklist.UseVisualStyleBackColor = true;
            this.btnCreateWorklist.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(177, 19);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(833, 55);
            this.lblMsg.TabIndex = 12;
            this.lblMsg.Text = "[Status Msg]";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txbBarcode
            // 
            this.txbBarcode.Location = new System.Drawing.Point(33, 207);
            this.txbBarcode.Name = "txbBarcode";
            this.txbBarcode.Size = new System.Drawing.Size(85, 27);
            this.txbBarcode.TabIndex = 38;
            this.txbBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbBarcode_KeyDown);
            // 
            // btnExit
            // 
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = global::winDDIRunBuilder.Properties.Resources.UserExit32;
            this.btnExit.Location = new System.Drawing.Point(7, 214);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(154, 49);
            this.btnExit.TabIndex = 20;
            this.btnExit.Text = "Exit";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPrintPlateBarcode
            // 
            this.btnPrintPlateBarcode.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnPrintPlateBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintPlateBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintPlateBarcode.Image = global::winDDIRunBuilder.Properties.Resources.Print24;
            this.btnPrintPlateBarcode.Location = new System.Drawing.Point(7, 152);
            this.btnPrintPlateBarcode.Name = "btnPrintPlateBarcode";
            this.btnPrintPlateBarcode.Size = new System.Drawing.Size(154, 49);
            this.btnPrintPlateBarcode.TabIndex = 19;
            this.btnPrintPlateBarcode.Text = "Print Plate Bar Code";
            this.btnPrintPlateBarcode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrintPlateBarcode.UseVisualStyleBackColor = true;
            this.btnPrintPlateBarcode.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGo);
            this.groupBox1.Controls.Add(this.dgvPlateSet);
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.lblPrompt);
            this.groupBox1.Controls.Add(this.cbProtocolCd);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.btnPrintPlateBarcode);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(7, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1017, 274);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input Source";
            // 
            // btnGo
            // 
            this.btnGo.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.Image = global::winDDIRunBuilder.Properties.Resources.import32;
            this.btnGo.Location = new System.Drawing.Point(7, 90);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(154, 49);
            this.btnGo.TabIndex = 28;
            this.btnGo.Text = "Go";
            this.btnGo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
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
            this.dgvPlateSet.ColumnHeadersHeight = 40;
            this.dgvPlateSet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Included,
            this.PlateDesc,
            this.CurWorkList,
            this.SourcePlateIsNew,
            this.SourcePlate,
            this.DestPlateIsNew,
            this.DestPlate,
            this.ScannedPlate,
            this.SourceIsReady});
            this.dgvPlateSet.EnableHeadersVisualStyles = false;
            this.dgvPlateSet.GridColor = System.Drawing.SystemColors.Control;
            this.dgvPlateSet.Location = new System.Drawing.Point(178, 88);
            this.dgvPlateSet.Name = "dgvPlateSet";
            this.dgvPlateSet.RowHeadersVisible = false;
            this.dgvPlateSet.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPlateSet.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPlateSet.RowTemplate.Height = 42;
            this.dgvPlateSet.Size = new System.Drawing.Size(833, 177);
            this.dgvPlateSet.TabIndex = 27;
            this.dgvPlateSet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPlateSet_CellContentClick);
            // 
            // Included
            // 
            this.Included.HeaderText = "Included";
            this.Included.MinimumWidth = 6;
            this.Included.Name = "Included";
            this.Included.Width = 62;
            // 
            // PlateDesc
            // 
            this.PlateDesc.HeaderText = "Description";
            this.PlateDesc.MinimumWidth = 6;
            this.PlateDesc.Name = "PlateDesc";
            this.PlateDesc.ReadOnly = true;
            this.PlateDesc.Width = 180;
            // 
            // CurWorkList
            // 
            this.CurWorkList.HeaderText = "Work List";
            this.CurWorkList.MinimumWidth = 6;
            this.CurWorkList.Name = "CurWorkList";
            this.CurWorkList.ReadOnly = true;
            this.CurWorkList.Width = 148;
            // 
            // SourcePlateIsNew
            // 
            this.SourcePlateIsNew.HeaderText = "SourcePlate Is New";
            this.SourcePlateIsNew.MinimumWidth = 6;
            this.SourcePlateIsNew.Name = "SourcePlateIsNew";
            this.SourcePlateIsNew.Width = 84;
            // 
            // SourcePlate
            // 
            this.SourcePlate.HeaderText = "Source Plate";
            this.SourcePlate.MinimumWidth = 6;
            this.SourcePlate.Name = "SourcePlate";
            this.SourcePlate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SourcePlate.Width = 118;
            // 
            // DestPlateIsNew
            // 
            this.DestPlateIsNew.HeaderText = "DestPlate Is New";
            this.DestPlateIsNew.MinimumWidth = 6;
            this.DestPlateIsNew.Name = "DestPlateIsNew";
            this.DestPlateIsNew.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DestPlateIsNew.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.DestPlateIsNew.Width = 84;
            // 
            // DestPlate
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.DestPlate.DefaultCellStyle = dataGridViewCellStyle2;
            this.DestPlate.HeaderText = "Destination Plate";
            this.DestPlate.MinimumWidth = 6;
            this.DestPlate.Name = "DestPlate";
            this.DestPlate.ReadOnly = true;
            this.DestPlate.Width = 118;
            // 
            // ScannedPlate
            // 
            this.ScannedPlate.HeaderText = "ScannedPlate";
            this.ScannedPlate.MinimumWidth = 6;
            this.ScannedPlate.Name = "ScannedPlate";
            this.ScannedPlate.Visible = false;
            this.ScannedPlate.Width = 125;
            // 
            // SourceIsReady
            // 
            this.SourceIsReady.HeaderText = "SourceIsReady";
            this.SourceIsReady.MinimumWidth = 6;
            this.SourceIsReady.Name = "SourceIsReady";
            this.SourceIsReady.Visible = false;
            this.SourceIsReady.Width = 125;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::winDDIRunBuilder.Properties.Resources.ddilogo512;
            this.pictureBox1.Location = new System.Drawing.Point(6, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(103, 53);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // fileWatcherBCR
            // 
            this.fileWatcherBCR.EnableRaisingEvents = true;
            this.fileWatcherBCR.SynchronizingObject = this;
            this.fileWatcherBCR.Changed += new System.IO.FileSystemEventHandler(this.fileWatcherBCR_Changed);
            this.fileWatcherBCR.Created += new System.IO.FileSystemEventHandler(this.fileWatcherBCR_Created);
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1029, 800);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbPlate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DDI Run Builder -Main";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.gbPlate.ResumeLayout(false);
            this.gbPlate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSamplePlate)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlateSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileWatcherBCR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbProtocolCd;
        private System.Windows.Forms.GroupBox gbPlate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnPrintPlateBarcode;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnCreateWorklist;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblPrompt;
        private System.IO.FileSystemWatcher fileWatcherBCR;
        private System.Windows.Forms.ComboBox cbPlates;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgvPlateSet;
        private System.Windows.Forms.DataGridView dgvSamplePlate;
        private System.Windows.Forms.TextBox txbBarcode;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Included;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlateDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurWorkList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SourcePlateIsNew;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourcePlate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DestPlateIsNew;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScannedPlate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SourceIsReady;
    }
}