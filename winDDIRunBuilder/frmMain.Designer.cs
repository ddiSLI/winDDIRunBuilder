
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.cbProtocolCd = new System.Windows.Forms.ComboBox();
            this.gbPlate = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblManualPlateId = new System.Windows.Forms.Label();
            this.btnPrintManuPlate = new System.Windows.Forms.Button();
            this.btnCreateManualPlate = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txbManualPlateDesc = new System.Windows.Forms.TextBox();
            this.txbManualPlateId = new System.Windows.Forms.TextBox();
            this.btnCloseMapping = new System.Windows.Forms.Button();
            this.dgvSamplePlate = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPlates = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txbBarcode = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.log = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cb_InProcess = new System.Windows.Forms.CheckBox();
            this.chkCompleted = new System.Windows.Forms.CheckBox();
            this.txbInitial = new System.Windows.Forms.TextBox();
            this.lblCurPlateId = new System.Windows.Forms.Label();
            this.lblJanusName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.wlReady = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ProcessedDB = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ProcessedWL = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MultiOutput = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ProcessType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnPrintPlateBarcode = new System.Windows.Forms.Button();
            this.fileWatcherBCR = new System.IO.FileSystemWatcher();
            this.btnPlateSample = new System.Windows.Forms.Button();
            this.btnExportEventLog = new System.Windows.Forms.Button();
            this.btnSampleStatus = new System.Windows.Forms.Button();
            this.btnQC = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.gbPlate.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSamplePlate)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlateSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileWatcherBCR)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(280, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Janus Protocol:";
            // 
            // cbProtocolCd
            // 
            this.cbProtocolCd.BackColor = System.Drawing.SystemColors.Info;
            this.cbProtocolCd.DropDownWidth = 300;
            this.cbProtocolCd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProtocolCd.FormattingEnabled = true;
            this.cbProtocolCd.Location = new System.Drawing.Point(279, 40);
            this.cbProtocolCd.Name = "cbProtocolCd";
            this.cbProtocolCd.Size = new System.Drawing.Size(139, 28);
            this.cbProtocolCd.TabIndex = 2;
            this.cbProtocolCd.SelectedIndexChanged += new System.EventHandler(this.cbProtocolCd_SelectedIndexChanged);
            // 
            // gbPlate
            // 
            this.gbPlate.Controls.Add(this.groupBox2);
            this.gbPlate.Controls.Add(this.btnCloseMapping);
            this.gbPlate.Controls.Add(this.dgvSamplePlate);
            this.gbPlate.Controls.Add(this.label3);
            this.gbPlate.Controls.Add(this.cbPlates);
            this.gbPlate.Controls.Add(this.button2);
            this.gbPlate.Controls.Add(this.button1);
            this.gbPlate.Controls.Add(this.lblMsg);
            this.gbPlate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPlate.Location = new System.Drawing.Point(7, 273);
            this.gbPlate.Name = "gbPlate";
            this.gbPlate.Size = new System.Drawing.Size(1085, 527);
            this.gbPlate.TabIndex = 2;
            this.gbPlate.TabStop = false;
            this.gbPlate.Text = "Plates";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblManualPlateId);
            this.groupBox2.Controls.Add(this.btnPrintManuPlate);
            this.groupBox2.Controls.Add(this.btnCreateManualPlate);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txbManualPlateDesc);
            this.groupBox2.Controls.Add(this.txbManualPlateId);
            this.groupBox2.Location = new System.Drawing.Point(0, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(166, 288);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            // 
            // lblManualPlateId
            // 
            this.lblManualPlateId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblManualPlateId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblManualPlateId.Location = new System.Drawing.Point(6, 201);
            this.lblManualPlateId.Name = "lblManualPlateId";
            this.lblManualPlateId.Size = new System.Drawing.Size(154, 28);
            this.lblManualPlateId.TabIndex = 45;
            this.lblManualPlateId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPrintManuPlate
            // 
            this.btnPrintManuPlate.Enabled = false;
            this.btnPrintManuPlate.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnPrintManuPlate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintManuPlate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintManuPlate.Image = global::winDDIRunBuilder.Properties.Resources.Print24;
            this.btnPrintManuPlate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintManuPlate.Location = new System.Drawing.Point(6, 230);
            this.btnPrintManuPlate.Name = "btnPrintManuPlate";
            this.btnPrintManuPlate.Size = new System.Drawing.Size(154, 48);
            this.btnPrintManuPlate.TabIndex = 43;
            this.btnPrintManuPlate.Text = "Print ManuPlate Bar Code";
            this.btnPrintManuPlate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrintManuPlate.UseVisualStyleBackColor = true;
            this.btnPrintManuPlate.Click += new System.EventHandler(this.btnPrintManuPlate_Click);
            // 
            // btnCreateManualPlate
            // 
            this.btnCreateManualPlate.Enabled = false;
            this.btnCreateManualPlate.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnCreateManualPlate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateManualPlate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateManualPlate.Image = global::winDDIRunBuilder.Properties.Resources.New32;
            this.btnCreateManualPlate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCreateManualPlate.Location = new System.Drawing.Point(7, 143);
            this.btnCreateManualPlate.Name = "btnCreateManualPlate";
            this.btnCreateManualPlate.Size = new System.Drawing.Size(154, 50);
            this.btnCreateManualPlate.TabIndex = 41;
            this.btnCreateManualPlate.Text = "Create Manual Plate";
            this.btnCreateManualPlate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCreateManualPlate.UseVisualStyleBackColor = true;
            this.btnCreateManualPlate.Click += new System.EventHandler(this.btnCreateManualPlate_Click);
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Location = new System.Drawing.Point(1, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(158, 23);
            this.label6.TabIndex = 42;
            this.label6.Text = "Manual Plate Desc:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Location = new System.Drawing.Point(2, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 23);
            this.label5.TabIndex = 41;
            this.label5.Text = "New Manual PlateId:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txbManualPlateDesc
            // 
            this.txbManualPlateDesc.BackColor = System.Drawing.SystemColors.Info;
            this.txbManualPlateDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbManualPlateDesc.Location = new System.Drawing.Point(2, 87);
            this.txbManualPlateDesc.MaxLength = 200;
            this.txbManualPlateDesc.Multiline = true;
            this.txbManualPlateDesc.Name = "txbManualPlateDesc";
            this.txbManualPlateDesc.Size = new System.Drawing.Size(158, 49);
            this.txbManualPlateDesc.TabIndex = 40;
            // 
            // txbManualPlateId
            // 
            this.txbManualPlateId.BackColor = System.Drawing.SystemColors.Info;
            this.txbManualPlateId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbManualPlateId.Location = new System.Drawing.Point(2, 28);
            this.txbManualPlateId.MaxLength = 20;
            this.txbManualPlateId.Name = "txbManualPlateId";
            this.txbManualPlateId.Size = new System.Drawing.Size(158, 24);
            this.txbManualPlateId.TabIndex = 39;
            // 
            // btnCloseMapping
            // 
            this.btnCloseMapping.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnCloseMapping.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseMapping.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseMapping.Image = global::winDDIRunBuilder.Properties.Resources.clearFilter32;
            this.btnCloseMapping.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCloseMapping.Location = new System.Drawing.Point(5, 471);
            this.btnCloseMapping.Name = "btnCloseMapping";
            this.btnCloseMapping.Size = new System.Drawing.Size(154, 50);
            this.btnCloseMapping.TabIndex = 21;
            this.btnCloseMapping.Text = "Close Mapping";
            this.btnCloseMapping.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCloseMapping.UseVisualStyleBackColor = true;
            this.btnCloseMapping.Click += new System.EventHandler(this.btnCloseMapping_Click);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSamplePlate.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSamplePlate.Location = new System.Drawing.Point(168, 80);
            this.dgvSamplePlate.Name = "dgvSamplePlate";
            this.dgvSamplePlate.RowHeadersVisible = false;
            this.dgvSamplePlate.RowHeadersWidth = 51;
            this.dgvSamplePlate.RowTemplate.Height = 24;
            this.dgvSamplePlate.Size = new System.Drawing.Size(911, 441);
            this.dgvSamplePlate.TabIndex = 0;
            this.dgvSamplePlate.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSamplePlate_CellContentClick);
            this.dgvSamplePlate.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSamplePlate_CellContentDoubleClick);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Location = new System.Drawing.Point(5, 19);
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
            this.cbPlates.Location = new System.Drawing.Point(5, 39);
            this.cbPlates.Name = "cbPlates";
            this.cbPlates.Size = new System.Drawing.Size(156, 28);
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
            // lblMsg
            // 
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(168, 19);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(911, 55);
            this.lblMsg.TabIndex = 12;
            this.lblMsg.Text = "[Status Msg]";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Location = new System.Drawing.Point(1098, 291);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 23);
            this.label4.TabIndex = 39;
            this.label4.Text = "Enter PlateId:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txbBarcode
            // 
            this.txbBarcode.BackColor = System.Drawing.SystemColors.Info;
            this.txbBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbBarcode.Location = new System.Drawing.Point(1098, 317);
            this.txbBarcode.Name = "txbBarcode";
            this.txbBarcode.Size = new System.Drawing.Size(155, 24);
            this.txbBarcode.TabIndex = 38;
            this.txbBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbBarcode_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvLogs);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.txbInitial);
            this.groupBox1.Controls.Add(this.lblCurPlateId);
            this.groupBox1.Controls.Add(this.lblJanusName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnGo);
            this.groupBox1.Controls.Add(this.dgvPlateSet);
            this.groupBox1.Controls.Add(this.cbProtocolCd);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.btnPrintPlateBarcode);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(7, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1085, 266);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input Source";
            // 
            // dgvLogs
            // 
            this.dgvLogs.AllowDrop = true;
            this.dgvLogs.AllowUserToAddRows = false;
            this.dgvLogs.AllowUserToDeleteRows = false;
            this.dgvLogs.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvLogs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogs.ColumnHeadersVisible = false;
            this.dgvLogs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.log});
            this.dgvLogs.Location = new System.Drawing.Point(548, 11);
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.RowHeadersVisible = false;
            this.dgvLogs.RowHeadersWidth = 8;
            this.dgvLogs.Size = new System.Drawing.Size(531, 59);
            this.dgvLogs.TabIndex = 51;
            // 
            // log
            // 
            this.log.HeaderText = "log";
            this.log.MinimumWidth = 8;
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.Width = 500;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cb_InProcess);
            this.groupBox3.Controls.Add(this.chkCompleted);
            this.groupBox3.Location = new System.Drawing.Point(426, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(116, 61);
            this.groupBox3.TabIndex = 50;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Schedule";
            // 
            // cb_InProcess
            // 
            this.cb_InProcess.AutoSize = true;
            this.cb_InProcess.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cb_InProcess.Checked = true;
            this.cb_InProcess.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_InProcess.Enabled = false;
            this.cb_InProcess.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_InProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_InProcess.Location = new System.Drawing.Point(15, 15);
            this.cb_InProcess.Name = "cb_InProcess";
            this.cb_InProcess.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cb_InProcess.Size = new System.Drawing.Size(98, 20);
            this.cb_InProcess.TabIndex = 48;
            this.cb_InProcess.Text = "In Process";
            this.cb_InProcess.UseVisualStyleBackColor = true;
            // 
            // chkCompleted
            // 
            this.chkCompleted.AutoSize = true;
            this.chkCompleted.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCompleted.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkCompleted.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCompleted.Location = new System.Drawing.Point(15, 38);
            this.chkCompleted.Name = "chkCompleted";
            this.chkCompleted.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkCompleted.Size = new System.Drawing.Size(100, 20);
            this.chkCompleted.TabIndex = 49;
            this.chkCompleted.Text = "Completed";
            this.chkCompleted.UseVisualStyleBackColor = true;
            this.chkCompleted.CheckedChanged += new System.EventHandler(this.chkCompleted_CheckedChanged);
            // 
            // txbInitial
            // 
            this.txbInitial.BackColor = System.Drawing.SystemColors.Info;
            this.txbInitial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbInitial.Location = new System.Drawing.Point(5, 80);
            this.txbInitial.MaxLength = 20;
            this.txbInitial.Name = "txbInitial";
            this.txbInitial.Size = new System.Drawing.Size(155, 26);
            this.txbInitial.TabIndex = 45;
            this.txbInitial.Text = "Initial Here";
            this.txbInitial.Click += new System.EventHandler(this.txbInitial_Click);
            this.txbInitial.Leave += new System.EventHandler(this.txbInitial_Leave);
            // 
            // lblCurPlateId
            // 
            this.lblCurPlateId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCurPlateId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCurPlateId.Location = new System.Drawing.Point(7, 171);
            this.lblCurPlateId.Name = "lblCurPlateId";
            this.lblCurPlateId.Size = new System.Drawing.Size(154, 28);
            this.lblCurPlateId.TabIndex = 44;
            this.lblCurPlateId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblJanusName
            // 
            this.lblJanusName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblJanusName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJanusName.Location = new System.Drawing.Point(170, 40);
            this.lblJanusName.Name = "lblJanusName";
            this.lblJanusName.Size = new System.Drawing.Size(104, 33);
            this.lblJanusName.TabIndex = 31;
            this.lblJanusName.Text = "Janus";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(170, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 24);
            this.label2.TabIndex = 30;
            this.label2.Text = "Janus Name";
            // 
            // btnGo
            // 
            this.btnGo.Enabled = false;
            this.btnGo.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.Image = global::winDDIRunBuilder.Properties.Resources.Go32;
            this.btnGo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGo.Location = new System.Drawing.Point(5, 113);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(154, 47);
            this.btnGo.TabIndex = 28;
            this.btnGo.Text = "    Go";
            this.btnGo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.BtnGo_Click);
            // 
            // dgvPlateSet
            // 
            this.dgvPlateSet.AllowUserToAddRows = false;
            this.dgvPlateSet.AllowUserToDeleteRows = false;
            this.dgvPlateSet.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvPlateSet.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPlateSet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
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
            this.SourceIsReady,
            this.wlReady,
            this.ProcessedDB,
            this.ProcessedWL,
            this.MultiOutput,
            this.ProcessType});
            this.dgvPlateSet.EnableHeadersVisualStyles = false;
            this.dgvPlateSet.GridColor = System.Drawing.SystemColors.Control;
            this.dgvPlateSet.Location = new System.Drawing.Point(168, 73);
            this.dgvPlateSet.Name = "dgvPlateSet";
            this.dgvPlateSet.RowHeadersVisible = false;
            this.dgvPlateSet.RowHeadersWidth = 51;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPlateSet.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPlateSet.RowTemplate.Height = 42;
            this.dgvPlateSet.Size = new System.Drawing.Size(911, 177);
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
            this.SourcePlate.Width = 138;
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
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.DestPlate.DefaultCellStyle = dataGridViewCellStyle3;
            this.DestPlate.HeaderText = "Destination Plate";
            this.DestPlate.MinimumWidth = 6;
            this.DestPlate.Name = "DestPlate";
            this.DestPlate.ReadOnly = true;
            this.DestPlate.Width = 168;
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
            // wlReady
            // 
            this.wlReady.HeaderText = "wlReady";
            this.wlReady.MinimumWidth = 6;
            this.wlReady.Name = "wlReady";
            this.wlReady.Visible = false;
            this.wlReady.Width = 125;
            // 
            // ProcessedDB
            // 
            this.ProcessedDB.HeaderText = "ProcessedDB";
            this.ProcessedDB.MinimumWidth = 6;
            this.ProcessedDB.Name = "ProcessedDB";
            this.ProcessedDB.Visible = false;
            this.ProcessedDB.Width = 125;
            // 
            // ProcessedWL
            // 
            this.ProcessedWL.HeaderText = "ProcessedWL";
            this.ProcessedWL.MinimumWidth = 6;
            this.ProcessedWL.Name = "ProcessedWL";
            this.ProcessedWL.Visible = false;
            this.ProcessedWL.Width = 125;
            // 
            // MultiOutput
            // 
            this.MultiOutput.HeaderText = "MultiOutput";
            this.MultiOutput.MinimumWidth = 6;
            this.MultiOutput.Name = "MultiOutput";
            this.MultiOutput.Visible = false;
            this.MultiOutput.Width = 125;
            // 
            // ProcessType
            // 
            this.ProcessType.HeaderText = "ProcessType";
            this.ProcessType.MinimumWidth = 6;
            this.ProcessType.Name = "ProcessType";
            this.ProcessType.Visible = false;
            this.ProcessType.Width = 125;
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
            // btnPrintPlateBarcode
            // 
            this.btnPrintPlateBarcode.Enabled = false;
            this.btnPrintPlateBarcode.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnPrintPlateBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintPlateBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintPlateBarcode.Image = global::winDDIRunBuilder.Properties.Resources.Print24;
            this.btnPrintPlateBarcode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintPlateBarcode.Location = new System.Drawing.Point(7, 202);
            this.btnPrintPlateBarcode.Name = "btnPrintPlateBarcode";
            this.btnPrintPlateBarcode.Size = new System.Drawing.Size(154, 56);
            this.btnPrintPlateBarcode.TabIndex = 19;
            this.btnPrintPlateBarcode.Text = "Print Plate Bar Code";
            this.btnPrintPlateBarcode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrintPlateBarcode.UseVisualStyleBackColor = true;
            this.btnPrintPlateBarcode.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // fileWatcherBCR
            // 
            this.fileWatcherBCR.EnableRaisingEvents = true;
            this.fileWatcherBCR.SynchronizingObject = this;
            this.fileWatcherBCR.Changed += new System.IO.FileSystemEventHandler(this.fileWatcherBCR_Changed);
            this.fileWatcherBCR.Created += new System.IO.FileSystemEventHandler(this.fileWatcherBCR_Created);
            // 
            // btnPlateSample
            // 
            this.btnPlateSample.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnPlateSample.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlateSample.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlateSample.Image = global::winDDIRunBuilder.Properties.Resources.Process32;
            this.btnPlateSample.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPlateSample.Location = new System.Drawing.Point(1098, 138);
            this.btnPlateSample.Name = "btnPlateSample";
            this.btnPlateSample.Size = new System.Drawing.Size(154, 48);
            this.btnPlateSample.TabIndex = 48;
            this.btnPlateSample.Text = "Plate  Samples";
            this.btnPlateSample.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPlateSample.UseVisualStyleBackColor = true;
            this.btnPlateSample.Click += new System.EventHandler(this.btnPlateSample_Click);
            // 
            // btnExportEventLog
            // 
            this.btnExportEventLog.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnExportEventLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportEventLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportEventLog.Image = global::winDDIRunBuilder.Properties.Resources.csv24;
            this.btnExportEventLog.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportEventLog.Location = new System.Drawing.Point(1098, 77);
            this.btnExportEventLog.Name = "btnExportEventLog";
            this.btnExportEventLog.Size = new System.Drawing.Size(154, 48);
            this.btnExportEventLog.TabIndex = 47;
            this.btnExportEventLog.Text = "Export Event log";
            this.btnExportEventLog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExportEventLog.UseVisualStyleBackColor = true;
            this.btnExportEventLog.Click += new System.EventHandler(this.btnExportEventLog_Click);
            // 
            // btnSampleStatus
            // 
            this.btnSampleStatus.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnSampleStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSampleStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSampleStatus.Image = global::winDDIRunBuilder.Properties.Resources.Report32;
            this.btnSampleStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSampleStatus.Location = new System.Drawing.Point(1099, 199);
            this.btnSampleStatus.Name = "btnSampleStatus";
            this.btnSampleStatus.Size = new System.Drawing.Size(154, 48);
            this.btnSampleStatus.TabIndex = 46;
            this.btnSampleStatus.Text = "Sample Status Report";
            this.btnSampleStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSampleStatus.UseVisualStyleBackColor = true;
            this.btnSampleStatus.Click += new System.EventHandler(this.btnSampleStatus_Click);
            // 
            // btnQC
            // 
            this.btnQC.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnQC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQC.Image = global::winDDIRunBuilder.Properties.Resources.qc;
            this.btnQC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQC.Location = new System.Drawing.Point(1098, 359);
            this.btnQC.Name = "btnQC";
            this.btnQC.Size = new System.Drawing.Size(154, 48);
            this.btnQC.TabIndex = 45;
            this.btnQC.Text = "Add QC/Export";
            this.btnQC.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnQC.UseVisualStyleBackColor = true;
            this.btnQC.Click += new System.EventHandler(this.btnQC_Click);
            // 
            // btnExit
            // 
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = global::winDDIRunBuilder.Properties.Resources.UserExit32;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(1098, 11);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(152, 51);
            this.btnExit.TabIndex = 20;
            this.btnExit.Text = "Exit";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1262, 350);
            this.Controls.Add(this.btnPlateSample);
            this.Controls.Add(this.btnExportEventLog);
            this.Controls.Add(this.btnSampleStatus);
            this.Controls.Add(this.btnQC);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbPlate);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txbBarcode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(200, 5);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DDI Run Builder -Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.gbPlate.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSamplePlate)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlateSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileWatcherBCR)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbProtocolCd;
        private System.Windows.Forms.GroupBox gbPlate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnPrintPlateBarcode;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnExit;
        private System.IO.FileSystemWatcher fileWatcherBCR;
        private System.Windows.Forms.ComboBox cbPlates;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgvPlateSet;
        private System.Windows.Forms.DataGridView dgvSamplePlate;
        private System.Windows.Forms.TextBox txbBarcode;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label lblJanusName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCloseMapping;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Included;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlateDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurWorkList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SourcePlateIsNew;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourcePlate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DestPlateIsNew;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestPlate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScannedPlate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SourceIsReady;
        private System.Windows.Forms.DataGridViewCheckBoxColumn wlReady;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ProcessedDB;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ProcessedWL;
        private System.Windows.Forms.DataGridViewCheckBoxColumn MultiOutput;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCreateManualPlate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txbManualPlateDesc;
        private System.Windows.Forms.TextBox txbManualPlateId;
        private System.Windows.Forms.Button btnPrintManuPlate;
        private System.Windows.Forms.Button btnQC;
        private System.Windows.Forms.Label lblCurPlateId;
        private System.Windows.Forms.Button btnSampleStatus;
        private System.Windows.Forms.Label lblManualPlateId;
        private System.Windows.Forms.TextBox txbInitial;
        private System.Windows.Forms.CheckBox cb_InProcess;
        private System.Windows.Forms.CheckBox chkCompleted;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.DataGridViewTextBoxColumn log;
        private System.Windows.Forms.Button btnExportEventLog;
        private System.Windows.Forms.Button btnPlateSample;
    }
}