
namespace winDDIRunBuilder
{
    partial class frmImportFromDB
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
            this.label3 = new System.Windows.Forms.Label();
            this.dgvPlates = new System.Windows.Forms.DataGridView();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txbPlateId = new System.Windows.Forms.TextBox();
            this.Plate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartPos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndPos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifiedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlates)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(11, 40);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 27);
            this.label3.TabIndex = 35;
            this.label3.Text = "Plate Id:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvPlates
            // 
            this.dgvPlates.AllowUserToAddRows = false;
            this.dgvPlates.AllowUserToDeleteRows = false;
            this.dgvPlates.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvPlates.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvPlates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Plate,
            this.StartPos,
            this.EndPos,
            this.ModifiedDate,
            this.Version});
            this.dgvPlates.EnableHeadersVisualStyles = false;
            this.dgvPlates.GridColor = System.Drawing.SystemColors.Control;
            this.dgvPlates.Location = new System.Drawing.Point(11, 83);
            this.dgvPlates.Margin = new System.Windows.Forms.Padding(2);
            this.dgvPlates.Name = "dgvPlates";
            this.dgvPlates.RowHeadersVisible = false;
            this.dgvPlates.RowHeadersWidth = 51;
            this.dgvPlates.RowTemplate.Height = 24;
            this.dgvPlates.Size = new System.Drawing.Size(431, 360);
            this.dgvPlates.TabIndex = 37;
            this.dgvPlates.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPlates_CellContentClick);
            // 
            // btnGo
            // 
            this.btnGo.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.Image = global::winDDIRunBuilder.Properties.Resources.addFolder32;
            this.btnGo.Location = new System.Drawing.Point(489, 41);
            this.btnGo.Margin = new System.Windows.Forms.Padding(2);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(125, 38);
            this.btnGo.TabIndex = 36;
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
            this.btnExit.Image = global::winDDIRunBuilder.Properties.Resources.UserExit32;
            this.btnExit.Location = new System.Drawing.Point(489, 107);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(125, 40);
            this.btnExit.TabIndex = 38;
            this.btnExit.Text = "Cancel";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txbPlateId
            // 
            this.txbPlateId.BackColor = System.Drawing.SystemColors.Info;
            this.txbPlateId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbPlateId.Location = new System.Drawing.Point(169, 41);
            this.txbPlateId.Margin = new System.Windows.Forms.Padding(2);
            this.txbPlateId.Name = "txbPlateId";
            this.txbPlateId.Size = new System.Drawing.Size(202, 26);
            this.txbPlateId.TabIndex = 39;
            this.txbPlateId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbPlateId_KeyDown);
            // 
            // Plate
            // 
            this.Plate.HeaderText = "Plate";
            this.Plate.MinimumWidth = 6;
            this.Plate.Name = "Plate";
            this.Plate.ReadOnly = true;
            this.Plate.Width = 128;
            // 
            // StartPos
            // 
            this.StartPos.HeaderText = "StartPos";
            this.StartPos.MinimumWidth = 6;
            this.StartPos.Name = "StartPos";
            this.StartPos.ReadOnly = true;
            this.StartPos.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.StartPos.Width = 60;
            // 
            // EndPos
            // 
            this.EndPos.HeaderText = "EndPos";
            this.EndPos.MinimumWidth = 6;
            this.EndPos.Name = "EndPos";
            this.EndPos.ReadOnly = true;
            this.EndPos.Width = 60;
            // 
            // ModifiedDate
            // 
            this.ModifiedDate.HeaderText = "ModifiedDate";
            this.ModifiedDate.Name = "ModifiedDate";
            // 
            // Version
            // 
            this.Version.HeaderText = "Version";
            this.Version.Name = "Version";
            this.Version.Width = 50;
            // 
            // frmImportFromDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 479);
            this.Controls.Add(this.txbPlateId);
            this.Controls.Add(this.dgvPlates);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmImportFromDB";
            this.Text = "Import Plate from DB";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmImportFromDB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlates)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvPlates;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selected;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txbPlateId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Plate;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartPos;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndPos;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifiedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
    }
}