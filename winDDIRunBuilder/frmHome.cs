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
    public partial class frmHome : Form
    {

        public List<InputFile> InputFileValues { get; set; }
        public ClientRunBuilder runBuilder =new ClientRunBuilder();

        private bool pIsFrmLoaded = false;

        public frmHome()
        {
            InitializeComponent();


            //dgvInputSource.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
            //dgvInputSource.EnableHeadersVisualStyles = false;

        }

        private void frmHome_Load(object sender, EventArgs e)
        {

            try
            {
                CombItem cbPrptpCd = new CombItem();
                
                if (InputFileValues.Count>0)
                {
                    var newObj= InputFileValues.Select(s => new { Guid = s.Position.PadRight(10) + s.RackName.PadRight(20) + s.ShortId, Value = s.ShortId });

                    cbProtoCd.SelectedItem = null;
                    cbProtoCd.SelectedText = "--select--";

                    cbProtoCd.DataSource = newObj.ToList();
                    cbProtoCd.DisplayMember = "Guid";
                    cbProtoCd.ValueMember = "Value";

                    cbProtoCd.SelectedItem = null;
                    cbProtoCd.SelectedText = "--select--";

                    pIsFrmLoaded = true;

                }
                else
                {
                    cbProtoCd.Text = "There is NO Proto Code";
                }


            }
            catch (Exception ex)
            {
                string errMsg = "{frmHome_Load} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;


            }

        }

        private void btnReRead_Click(object sender, EventArgs e)
        {

        }

        private void cbProtoCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(pIsFrmLoaded)
                {
                    string ind = cbProtoCd.SelectedIndex.ToString();
                    string protoCd = cbProtoCd.SelectedValue.ToString();
                }



            }
            catch (Exception ex)
            {
                string errMsg = "{cbProtoCd_SelectedIndexChanged} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }
        }

        [Obsolete]
        private void btnImportA_Click(object sender, EventArgs e)
        {
            ServiceRunBuilder runBuilder =new ServiceRunBuilder();
            List<InputFile> fullSamples = new List<InputFile>();

            try
            {

                dgvInputSource.Rows.Clear();

                if (InputFileValues.Count>0)
                {
                    fullSamples = runBuilder.GetSampleIds(InputFileValues);
                    if (fullSamples.Count > 0)
                    {
                        string[] row;
                        foreach (var smp in fullSamples)
                        {
                            row = new string[] { smp.ShortId, smp.FullSampleId};
                            dgvInputSource.Rows.Add(row);
                            //tbxInputSource.Text += smp.ShortId + ",".PadRight(10) + smp.FullSampleId;
                            //tbxInputSource.Text += Environment.NewLine;
                        }

                        foreach(DataGridViewRow rw in dgvInputSource.Rows)
                        {
                            if (rw.Index % 2 == 0)
                               // rw.DefaultCellStyle.BackColor = Color.GhostWhite;
                            rw.DefaultCellStyle.BackColor = Color.Honeydew;

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                string errMsg = "{btnImportA_Click} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }
        }
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void gbTop_Enter(object sender, EventArgs e)
        {

        }
    }
}
