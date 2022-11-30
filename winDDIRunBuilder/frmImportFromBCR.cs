using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winDDIRunBuilder
{
    public partial class frmImportFromBCR : Form
    {
        public string CurProtocol { set; get; }
        public string DestPlate { set; get; }
        public ClientRunBuilder CurRunBuilder { set; get; }
        public List<InputFile> InputValues { get; private set; }

        public frmImportFromBCR()
        {
            InitializeComponent();
        }

        private void frmImportFromBCR_Load(object sender, EventArgs e)
        {
            lblProtocol.Text = CurProtocol;
            lblDestPlate.Text = DestPlate;

        }

       

        private void btnImport_Click(object sender, EventArgs e)
        {
            RepoSQLOracle repoService = new RepoSQLOracle();
            string importFile = "InputFile.csv";

            try
            {
                List<InputFile> sampleValues = new List<InputFile>();
                importFile = CurRunBuilder.ReadFilePath + importFile;

                if (lblDestPlate.Text.ToUpper() != txbPlateId.Text.Trim().ToUpper())
                {
                    lblMsg.ForeColor = Color.DarkRed;
                    lblMsg.Text = "The destination PlateId is not equal to entered PlateId!";
                }
                else
                {
                    lblMsg.ForeColor = Color.DarkGray;
                    lblMsg.Text = "The BCR-file is ready to go : " + importFile;

                    List<InputFile> rawValues = File.ReadAllLines(importFile)
                    .Skip(1)
                    .Select(v => InputFile.ReadInputFile(v))
                    .ToList();

                    dgvInputSource.Rows.Clear();
                    dgvInputSource.Refresh();
                    InputValues = new List<InputFile>();

                    if (rawValues.Count > 0)
                    {
                        //InputFileValues = runBuilder.GetSampleIds(values);
                        sampleValues = repoService.GetShortSamples(rawValues);
                        if (sampleValues.Count > 0)
                        {
                            string[] row;
                            foreach (var smp in sampleValues)
                            {
                                row = new string[] { smp.Position, "true", smp.ShortId, smp.FullSampleId };
                                dgvInputSource.Rows.Add(row);

                                InputValues.Add(new InputFile
                                {
                                    FullSampleId = smp.FullSampleId,
                                    ShortId = smp.ShortId,
                                    RackName = "BCR",
                                    Position = smp.Position,
                                    PlateId = lblDestPlate.Text
                                });
                            }

                            foreach (DataGridViewRow rw in dgvInputSource.Rows)
                            {
                                if (rw.Index % 2 == 0)
                                    // rw.DefaultCellStyle.BackColor = Color.GhostWhite;
                                    // rw.DefaultCellStyle.BackColor = Color.Honeydew;
                                    rw.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                            }

                            btnGo.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = "{btnImport_Click} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            InputValues = new List<InputFile>();
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }
    }
}
