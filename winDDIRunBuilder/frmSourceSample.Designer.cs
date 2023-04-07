
namespace winDDIRunBuilder
{
    partial class frmSourceSample
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
            this.dgvSamples = new System.Windows.Forms.DataGridView();
            this.Include = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Sample = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RackName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSourcePlate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnGet = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSamples)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSamples
            // 
            this.dgvSamples.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvSamples.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvSamples.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSamples.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Include,
            this.Sample,
            this.RackName,
            this.Position});
            this.dgvSamples.Location = new System.Drawing.Point(25, 64);
            this.dgvSamples.Name = "dgvSamples";
            this.dgvSamples.RowHeadersWidth = 51;
            this.dgvSamples.RowTemplate.Height = 24;
            this.dgvSamples.Size = new System.Drawing.Size(716, 595);
            this.dgvSamples.TabIndex = 0;
            // 
            // Include
            // 
            this.Include.HeaderText = "Include";
            this.Include.MinimumWidth = 6;
            this.Include.Name = "Include";
            this.Include.Width = 125;
            // 
            // Sample
            // 
            this.Sample.HeaderText = "Sample";
            this.Sample.MinimumWidth = 6;
            this.Sample.Name = "Sample";
            this.Sample.Width = 125;
            // 
            // RackName
            // 
            this.RackName.HeaderText = "RackName";
            this.RackName.MinimumWidth = 6;
            this.RackName.Name = "RackName";
            this.RackName.Width = 125;
            // 
            // Position
            // 
            this.Position.HeaderText = "Position";
            this.Position.MinimumWidth = 6;
            this.Position.Name = "Position";
            this.Position.Width = 125;
            // 
            // lblSourcePlate
            // 
            this.lblSourcePlate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSourcePlate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSourcePlate.Location = new System.Drawing.Point(158, 19);
            this.lblSourcePlate.Name = "lblSourcePlate";
            this.lblSourcePlate.Size = new System.Drawing.Size(266, 33);
            this.lblSourcePlate.TabIndex = 32;
            this.lblSourcePlate.Text = "BCR230309AB";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 33);
            this.label1.TabIndex = 33;
            this.label1.Text = "Source Plate:";
            // 
            // btnGo
            // 
            this.btnGo.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.Image = global::winDDIRunBuilder.Properties.Resources.import32;
            this.btnGo.Location = new System.Drawing.Point(626, 12);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(115, 49);
            this.btnGo.TabIndex = 35;
            this.btnGo.Text = "Go";
            this.btnGo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnGet
            // 
            this.btnGet.FlatAppearance.BorderColor = System.Drawing.SystemColors.Highlight;
            this.btnGet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGet.Image = global::winDDIRunBuilder.Properties.Resources.load24;
            this.btnGet.Location = new System.Drawing.Point(482, 12);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(115, 49);
            this.btnGet.TabIndex = 34;
            this.btnGet.Text = "Re-Get Samples";
            this.btnGet.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // frmSourceSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 664);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSourcePlate);
            this.Controls.Add(this.dgvSamples);
            this.Name = "frmSourceSample";
            this.ShowIcon = false;
            this.Text = "Source Samples";
            this.Load += new System.EventHandler(this.frmSourceSample_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSamples)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSamples;
        private System.Windows.Forms.Label lblSourcePlate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Include;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sample;
        private System.Windows.Forms.DataGridViewTextBoxColumn RackName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Position;
    }
}