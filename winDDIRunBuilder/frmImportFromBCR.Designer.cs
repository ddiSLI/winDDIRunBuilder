
namespace winDDIRunBuilder
{
    partial class frmImportFromBCR
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
            this.dgvInputSource = new System.Windows.Forms.DataGridView();
            this.Seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ShortId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SampleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txbPlateId = new System.Windows.Forms.TextBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.lblProtocol = new System.Windows.Forms.Label();
            this.lblDestPlate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInputSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvInputSource
            // 
            this.dgvInputSource.AllowUserToAddRows = false;
            this.dgvInputSource.AllowUserToDeleteRows = false;
            this.dgvInputSource.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvInputSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvInputSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInputSource.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Seq,
            this.Selected,
            this.ShortId,
            this.SampleId});
            this.dgvInputSource.EnableHeadersVisualStyles = false;
            this.dgvInputSource.GridColor = System.Drawing.SystemColors.Control;
            this.dgvInputSource.Location = new System.Drawing.Point(24, 186);
            this.dgvInputSource.Name = "dgvInputSource";
            this.dgvInputSource.RowHeadersVisible = false;
            this.dgvInputSource.RowHeadersWidth = 51;
            this.dgvInputSource.RowTemplate.Height = 24;
            this.dgvInputSource.Size = new System.Drawing.Size(509, 390);
            this.dgvInputSource.TabIndex = 29;
            // 
            // Seq
            // 
            this.Seq.HeaderText = "Seq";
            this.Seq.MinimumWidth = 6;
            this.Seq.Name = "Seq";
            this.Seq.ReadOnly = true;
            this.Seq.Width = 42;
            // 
            // Selected
            // 
            this.Selected.HeaderText = "Sel";
            this.Selected.MinimumWidth = 6;
            this.Selected.Name = "Selected";
            this.Selected.ReadOnly = true;
            this.Selected.Width = 42;
            // 
            // ShortId
            // 
            this.ShortId.HeaderText = "Short Id";
            this.ShortId.MinimumWidth = 6;
            this.ShortId.Name = "ShortId";
            this.ShortId.ReadOnly = true;
            this.ShortId.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ShortId.Width = 108;
            // 
            // SampleId
            // 
            this.SampleId.HeaderText = "Sample Id";
            this.SampleId.MinimumWidth = 6;
            this.SampleId.Name = "SampleId";
            this.SampleId.ReadOnly = true;
            this.SampleId.Width = 180;
            // 
            // btnGo
            // 
            this.btnGo.Enabled = false;
            this.btnGo.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.Image = global::winDDIRunBuilder.Properties.Resources.addFolder32;
            this.btnGo.Location = new System.Drawing.Point(566, 191);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(156, 47);
            this.btnGo.TabIndex = 27;
            this.btnGo.Text = "Go";
            this.btnGo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 17);
            this.label1.TabIndex = 26;
            this.label1.Text = "Scan or Enter PlateId:";
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Image = global::winDDIRunBuilder.Properties.Resources.UserExit32;
            this.btnCancel.Location = new System.Drawing.Point(566, 273);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(156, 49);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txbPlateId
            // 
            this.txbPlateId.BackColor = System.Drawing.SystemColors.Info;
            this.txbPlateId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbPlateId.Location = new System.Drawing.Point(24, 104);
            this.txbPlateId.Name = "txbPlateId";
            this.txbPlateId.Size = new System.Drawing.Size(509, 30);
            this.txbPlateId.TabIndex = 34;
            // 
            // btnImport
            // 
            this.btnImport.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.Image = global::winDDIRunBuilder.Properties.Resources.BarcodeScanner32;
            this.btnImport.Location = new System.Drawing.Point(566, 53);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(156, 49);
            this.btnImport.TabIndex = 35;
            this.btnImport.Text = "Import From BCR";
            this.btnImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // lblProtocol
            // 
            this.lblProtocol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProtocol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProtocol.Location = new System.Drawing.Point(24, 39);
            this.lblProtocol.Name = "lblProtocol";
            this.lblProtocol.Size = new System.Drawing.Size(204, 31);
            this.lblProtocol.TabIndex = 36;
            this.lblProtocol.Text = "Protocol Name";
            this.lblProtocol.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDestPlate
            // 
            this.lblDestPlate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDestPlate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestPlate.Location = new System.Drawing.Point(267, 39);
            this.lblDestPlate.Name = "lblDestPlate";
            this.lblDestPlate.Size = new System.Drawing.Size(178, 31);
            this.lblDestPlate.TabIndex = 37;
            this.lblDestPlate.Text = "Plate Name";
            this.lblDestPlate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 17);
            this.label3.TabIndex = 38;
            this.label3.Text = "Current Protocol:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(264, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 17);
            this.label5.TabIndex = 40;
            this.label5.Text = "Destination Plate:";
            // 
            // lblMsg
            // 
            this.lblMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(24, 137);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(509, 46);
            this.lblMsg.TabIndex = 41;
            this.lblMsg.Text = "PromptMessage";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmImportFromBCR
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(750, 588);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblDestPlate);
            this.Controls.Add(this.lblProtocol);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.txbPlateId);
            this.Controls.Add(this.dgvInputSource);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmImportFromBCR";
            this.Text = "Import From BCR - DDI Run Builder";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmImportFromBCR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInputSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvInputSource;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txbPlateId;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label lblProtocol;
        private System.Windows.Forms.Label lblDestPlate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Seq;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selected;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShortId;
        private System.Windows.Forms.DataGridViewTextBoxColumn SampleId;
    }
}