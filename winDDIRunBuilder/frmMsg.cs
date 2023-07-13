using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winDDIRunBuilder
{
    public partial class frmMsg : Form
    {
        public string MyMsg { get; set; } = "";

        public frmMsg()
        {
            InitializeComponent();
        }

        private void frmMsg_Load(object sender, EventArgs e)
        {
            txbMsg.Text = MyMsg;

        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
