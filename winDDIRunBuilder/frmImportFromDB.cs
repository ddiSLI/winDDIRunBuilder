using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using winDDIRunBuilder.Models;

namespace winDDIRunBuilder
{
    public partial class frmImportFromDB : Form
    {
        public List<Plate> GetPlates { get; set; }
        public string PlateId { get; set; } = "";
        public string SelectedPlateId { get; set; } = "";
        public string DBPlateId { get; set; }
        public frmImportFromDB()
        {
            InitializeComponent();
        }

        private void frmImportFromDB_Load(object sender, EventArgs e)
        {
            dgvPlates.Rows.Clear();
            //btnGo.Enabled = false;


            try
            {
                


            }

            catch (Exception ex)
            {


            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
