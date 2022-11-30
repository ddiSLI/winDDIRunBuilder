
namespace winDDIRunBuilder
{
    partial class frmHome
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
            this.gbOutputFormate = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.lblCreateBItem = new System.Windows.Forms.Label();
            this.lblCeateAItem = new System.Windows.Forms.Label();
            this.lblCreateB = new System.Windows.Forms.Label();
            this.lblCreateA = new System.Windows.Forms.Label();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvInputSource = new System.Windows.Forms.DataGridView();
            this.Seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShortId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SampleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Loaded = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnImportA = new System.Windows.Forms.Button();
            this.gbPlate = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.lblPrintMsg = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbTop = new System.Windows.Forms.GroupBox();
            this.btnReRead = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbProtoCd = new System.Windows.Forms.ComboBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.gbOutputFormate.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInputSource)).BeginInit();
            this.gbPlate.SuspendLayout();
            this.gbTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOutputFormate
            // 
            this.gbOutputFormate.Controls.Add(this.btnExit);
            this.gbOutputFormate.Controls.Add(this.btnCreate);
            this.gbOutputFormate.Controls.Add(this.lblCreateBItem);
            this.gbOutputFormate.Controls.Add(this.lblCeateAItem);
            this.gbOutputFormate.Controls.Add(this.lblCreateB);
            this.gbOutputFormate.Controls.Add(this.lblCreateA);
            this.gbOutputFormate.Controls.Add(this.radioButton6);
            this.gbOutputFormate.Controls.Add(this.radioButton5);
            this.gbOutputFormate.Controls.Add(this.radioButton4);
            this.gbOutputFormate.Controls.Add(this.radioButton3);
            this.gbOutputFormate.Controls.Add(this.radioButton2);
            this.gbOutputFormate.Controls.Add(this.radioButton1);
            this.gbOutputFormate.Location = new System.Drawing.Point(15, 563);
            this.gbOutputFormate.Name = "gbOutputFormate";
            this.gbOutputFormate.Size = new System.Drawing.Size(1121, 222);
            this.gbOutputFormate.TabIndex = 8;
            this.gbOutputFormate.TabStop = false;
            this.gbOutputFormate.Text = "Output Format";
            // 
            // btnExit
            // 
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = global::winDDIRunBuilder.Properties.Resources.UserExit;
            this.btnExit.Location = new System.Drawing.Point(927, 136);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(170, 64);
            this.btnExit.TabIndex = 16;
            this.btnExit.Text = "Exit";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Image = global::winDDIRunBuilder.Properties.Resources.FilesAdd48;
            this.btnCreate.Location = new System.Drawing.Point(722, 136);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(170, 64);
            this.btnCreate.TabIndex = 15;
            this.btnCreate.Text = "New";
            this.btnCreate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCreate.UseVisualStyleBackColor = true;
            // 
            // lblCreateBItem
            // 
            this.lblCreateBItem.AutoSize = true;
            this.lblCreateBItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateBItem.Location = new System.Drawing.Point(251, 189);
            this.lblCreateBItem.Name = "lblCreateBItem";
            this.lblCreateBItem.Size = new System.Drawing.Size(120, 20);
            this.lblCreateBItem.TabIndex = 14;
            this.lblCreateBItem.Text = "lblCreateBItem";
            // 
            // lblCeateAItem
            // 
            this.lblCeateAItem.AutoSize = true;
            this.lblCeateAItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCeateAItem.Location = new System.Drawing.Point(251, 136);
            this.lblCeateAItem.Name = "lblCeateAItem";
            this.lblCeateAItem.Size = new System.Drawing.Size(113, 20);
            this.lblCeateAItem.TabIndex = 13;
            this.lblCeateAItem.Text = "lblCeateAItem";
            // 
            // lblCreateB
            // 
            this.lblCreateB.AutoSize = true;
            this.lblCreateB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateB.Location = new System.Drawing.Point(56, 189);
            this.lblCreateB.Name = "lblCreateB";
            this.lblCreateB.Size = new System.Drawing.Size(176, 20);
            this.lblCreateB.TabIndex = 12;
            this.lblCreateB.Text = "[Analyzer Instructions]";
            // 
            // lblCreateA
            // 
            this.lblCreateA.AutoSize = true;
            this.lblCreateA.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateA.Location = new System.Drawing.Point(57, 136);
            this.lblCreateA.Name = "lblCreateA";
            this.lblCreateA.Size = new System.Drawing.Size(156, 20);
            this.lblCreateA.TabIndex = 11;
            this.lblCreateA.Text = "[Janus Instructions]";
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton6.Location = new System.Drawing.Point(1013, 43);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(84, 24);
            this.radioButton6.TabIndex = 10;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "Manual";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton5.Location = new System.Drawing.Point(803, 43);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(110, 24);
            this.radioButton5.TabIndex = 9;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "Kashi.CSV";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton4.Location = new System.Drawing.Point(595, 43);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(84, 24);
            this.radioButton4.TabIndex = 8;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "ABI750";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton3.Location = new System.Drawing.Point(450, 43);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(59, 24);
            this.radioButton3.TabIndex = 7;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Aus";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(265, 43);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(133, 24);
            this.radioButton2.TabIndex = 6;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Luminex GPP";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(60, 43);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(164, 24);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Luminex GA-MAP";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvInputSource);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnImportA);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 270);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1121, 282);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input Source";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // dgvInputSource
            // 
            this.dgvInputSource.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvInputSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvInputSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInputSource.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Seq,
            this.ShortId,
            this.SampleId,
            this.Loaded});
            this.dgvInputSource.EnableHeadersVisualStyles = false;
            this.dgvInputSource.GridColor = System.Drawing.SystemColors.Control;
            this.dgvInputSource.Location = new System.Drawing.Point(521, 34);
            this.dgvInputSource.Name = "dgvInputSource";
            this.dgvInputSource.RowHeadersVisible = false;
            this.dgvInputSource.RowHeadersWidth = 51;
            this.dgvInputSource.RowTemplate.Height = 24;
            this.dgvInputSource.Size = new System.Drawing.Size(579, 229);
            this.dgvInputSource.TabIndex = 18;
            // 
            // Seq
            // 
            this.Seq.HeaderText = "Seq";
            this.Seq.MinimumWidth = 6;
            this.Seq.Name = "Seq";
            this.Seq.ReadOnly = true;
            this.Seq.Width = 42;
            // 
            // ShortId
            // 
            this.ShortId.HeaderText = "Short Id";
            this.ShortId.MinimumWidth = 6;
            this.ShortId.Name = "ShortId";
            this.ShortId.ReadOnly = true;
            this.ShortId.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ShortId.Width = 125;
            // 
            // SampleId
            // 
            this.SampleId.HeaderText = "Sample Id";
            this.SampleId.MinimumWidth = 6;
            this.SampleId.Name = "SampleId";
            this.SampleId.ReadOnly = true;
            this.SampleId.Width = 248;
            // 
            // Loaded
            // 
            this.Loaded.HeaderText = "Loaded";
            this.Loaded.MinimumWidth = 6;
            this.Loaded.Name = "Loaded";
            this.Loaded.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Loaded.Width = 70;
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::winDDIRunBuilder.Properties.Resources.addDatabase32;
            this.btnSave.Location = new System.Drawing.Point(60, 199);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(408, 64);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnImportA
            // 
            this.btnImportA.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnImportA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportA.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportA.Image = global::winDDIRunBuilder.Properties.Resources.import24;
            this.btnImportA.Location = new System.Drawing.Point(60, 34);
            this.btnImportA.Name = "btnImportA";
            this.btnImportA.Size = new System.Drawing.Size(408, 64);
            this.btnImportA.TabIndex = 15;
            this.btnImportA.Text = "Import C:\\Output";
            this.btnImportA.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImportA.UseVisualStyleBackColor = true;
            this.btnImportA.Click += new System.EventHandler(this.btnImportA_Click);
            // 
            // gbPlate
            // 
            this.gbPlate.Controls.Add(this.textBox1);
            this.gbPlate.Controls.Add(this.btnPrint);
            this.gbPlate.Controls.Add(this.btnClear);
            this.gbPlate.Controls.Add(this.btnNew);
            this.gbPlate.Controls.Add(this.lblPrintMsg);
            this.gbPlate.Controls.Add(this.label2);
            this.gbPlate.Location = new System.Drawing.Point(12, 120);
            this.gbPlate.Name = "gbPlate";
            this.gbPlate.Size = new System.Drawing.Size(1117, 144);
            this.gbPlate.TabIndex = 6;
            this.gbPlate.TabStop = false;
            this.gbPlate.Text = "Plate";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Info;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(60, 51);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(408, 27);
            this.textBox1.TabIndex = 17;
            // 
            // btnPrint
            // 
            this.btnPrint.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = global::winDDIRunBuilder.Properties.Resources.PrintBar48;
            this.btnPrint.Location = new System.Drawing.Point(918, 26);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(170, 64);
            this.btnPrint.TabIndex = 16;
            this.btnPrint.Text = "Print Plate Bar Code";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Image = global::winDDIRunBuilder.Properties.Resources.Clear48;
            this.btnClear.Location = new System.Drawing.Point(725, 26);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(170, 64);
            this.btnClear.TabIndex = 15;
            this.btnClear.Text = "Clear";
            this.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Image = global::winDDIRunBuilder.Properties.Resources.New48;
            this.btnNew.Location = new System.Drawing.Point(536, 26);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(170, 64);
            this.btnNew.TabIndex = 14;
            this.btnNew.Text = "New";
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNew.UseVisualStyleBackColor = true;
            // 
            // lblPrintMsg
            // 
            this.lblPrintMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrintMsg.Location = new System.Drawing.Point(22, 103);
            this.lblPrintMsg.Name = "lblPrintMsg";
            this.lblPrintMsg.Size = new System.Drawing.Size(1075, 29);
            this.lblPrintMsg.TabIndex = 12;
            this.lblPrintMsg.Text = "[lblPrintMsg]";
            this.lblPrintMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Plate ID:";
            // 
            // gbTop
            // 
            this.gbTop.Controls.Add(this.btnReRead);
            this.gbTop.Controls.Add(this.label1);
            this.gbTop.Controls.Add(this.cbProtoCd);
            this.gbTop.Location = new System.Drawing.Point(12, 12);
            this.gbTop.Name = "gbTop";
            this.gbTop.Size = new System.Drawing.Size(1117, 100);
            this.gbTop.TabIndex = 5;
            this.gbTop.TabStop = false;
            this.gbTop.Text = "Proto";
            this.gbTop.Enter += new System.EventHandler(this.gbTop_Enter);
            // 
            // btnReRead
            // 
            this.btnReRead.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnReRead.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReRead.Image = global::winDDIRunBuilder.Properties.Resources.Downloads48;
            this.btnReRead.Location = new System.Drawing.Point(918, 26);
            this.btnReRead.Name = "btnReRead";
            this.btnReRead.Size = new System.Drawing.Size(170, 64);
            this.btnReRead.TabIndex = 13;
            this.btnReRead.Text = "Re-Read input file";
            this.btnReRead.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReRead.UseVisualStyleBackColor = true;
            this.btnReRead.Click += new System.EventHandler(this.btnReRead_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Janus Proto Code:";
            // 
            // cbProtoCd
            // 
            this.cbProtoCd.BackColor = System.Drawing.SystemColors.Info;
            this.cbProtoCd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProtoCd.FormattingEnabled = true;
            this.cbProtoCd.Location = new System.Drawing.Point(60, 41);
            this.cbProtoCd.Name = "cbProtoCd";
            this.cbProtoCd.Size = new System.Drawing.Size(408, 33);
            this.cbProtoCd.TabIndex = 2;
            this.cbProtoCd.SelectedIndexChanged += new System.EventHandler(this.cbProtoCd_SelectedIndexChanged);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Loaded";
            this.dataGridViewImageColumn1.Image = global::winDDIRunBuilder.Properties.Resources.Checked24;
            this.dataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn1.MinimumWidth = 6;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Width = 125;
            // 
            // frmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 797);
            this.Controls.Add(this.gbOutputFormate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbPlate);
            this.Controls.Add(this.gbTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmHome";
            this.Text = "Home - DDI Run Builder";
            this.Load += new System.EventHandler(this.frmHome_Load);
            this.gbOutputFormate.ResumeLayout(false);
            this.gbOutputFormate.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInputSource)).EndInit();
            this.gbPlate.ResumeLayout(false);
            this.gbPlate.PerformLayout();
            this.gbTop.ResumeLayout(false);
            this.gbTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOutputFormate;
        private System.Windows.Forms.Label lblCreateBItem;
        private System.Windows.Forms.Label lblCeateAItem;
        private System.Windows.Forms.Label lblCreateB;
        private System.Windows.Forms.Label lblCreateA;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbPlate;
        private System.Windows.Forms.Label lblPrintMsg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbTop;
        private System.Windows.Forms.Button btnReRead;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbProtoCd;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnImportA;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.DataGridView dgvInputSource;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Seq;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShortId;
        private System.Windows.Forms.DataGridViewTextBoxColumn SampleId;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Loaded;
    }
}