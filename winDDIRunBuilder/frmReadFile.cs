using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace winDDIRunBuilder
{
    public partial class frmReadFile : Form
    {
        private ClientRunBuilder pRunBuilder;
        public frmReadFile()
        {
            InitializeComponent();
        }

        private void frmReadFile_Load(object sender, EventArgs e)
        {
            pRunBuilder = new ClientRunBuilder();
        }

        private void btnReadFile_Click(object sender, EventArgs e)
        {
            List<InputFile> values = File.ReadAllLines(pRunBuilder.ReadFilePath)
                .Skip(1)
                .Select(v => InputFile.ReadInputFile(v))
                .ToList();

            //Close current form and open another;
            this.Hide();
            var frmMainForm = new frmHome();
            frmMainForm.runBuilder = pRunBuilder;
            frmMainForm.InputFileValues = values;
            frmMainForm.Closed += (s, args) => this.Close();
            frmMainForm.Show();
            //
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}
