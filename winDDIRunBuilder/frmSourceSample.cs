using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using winDDIRunBuilder.Models;

namespace winDDIRunBuilder
{
    public partial class frmSourceSample : Form
    {
        public string CurUniqueId { get; set; } = "";
        public string PlateId { get; set; } = "";
        public InputPlate BCRPlate { get; set; } = new InputPlate();
        public string BCRFilePath { get; set; } = "";
        public List<BCRSample> BCRSamples { get; set; }
        public List<ValidPlate> HisValidPlates { get; set; }
        public List<PlateSample> HisPlateSamples { get; set; }          //samples from history plates

        public frmSourceSample()
        {
            InitializeComponent();
        }

        private void frmSourceSample_Load(object sender, EventArgs e)
        {
            try
            {
                if(BCRSamples !=null & BCRSamples.Count > 0)
                {
                    LoadBCRSamples();

                }
                else if(HisPlateSamples !=null && HisPlateSamples.Count > 0)
                {
                    LoadHistorySamples();
                }
                else
                {

                }

            }
            catch (Exception ex)
            {

                string errMsg = "frmSourceSample_Load() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;

            }

        }

        private void LoadBCRSamples()
        {
            dgvSamples.Rows.Clear();

            foreach (var smp in BCRSamples)
            {
                dgvSamples.Rows.Add();
                dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["Include"].Value = true;
                dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["Sample"].Value = smp.ShortId;
                dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["RackName"].Value = "BCR";
                dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["Position"].Value = smp.Position;
            }

        }

        private void LoadHistorySamples()
        {
            dgvSamples.Rows.Clear();

            foreach (var smp in HisPlateSamples)
            {
                dgvSamples.Rows.Add();
                dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["Include"].Value = true;
                dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["Sample"].Value = smp.SampleId;
                dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["RackName"].Value = smp.PlateId;
                dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["Position"].Value = smp.Well;
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                if (BCRSamples != null & BCRSamples.Count > 0)
                {
                    UpdateBCRSamples();

                }
                else if (HisPlateSamples != null && HisPlateSamples.Count > 0)
                {
                    UpdateHistorySamples();
                }
                else
                {

                }

            }
            catch (Exception ex)
            {

                string errMsg = "frmSourceSample_Load() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;

            }
        }

        private void UpdateBCRSamples()
        {
            foreach (DataGridViewRow srw in dgvSamples.Rows)
            {
                if (Convert.ToBoolean(srw.Cells["Include"].Value) == false)
                {
                    BCRSamples.RemoveAll(bs => bs.ShortId.ToUpper() == srw.Cells["Sample"].Value.ToString().ToUpper());
                }
            }
        }

        private void UpdateHistorySamples()
        {
            foreach (DataGridViewRow srw in dgvSamples.Rows)
            {
                if (Convert.ToBoolean(srw.Cells["Include"].Value) == false)
                {
                    HisPlateSamples.RemoveAll(hs => hs.SampleId.ToUpper() == srw.Cells["Sample"].Value.ToString().ToUpper());
                }
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            BCRFile bcrFile = new BCRFile();
            RepoSQL sqlService = new RepoSQL();

            try
            {
                bcrFile.CurUniqueId = CurUniqueId;

                if (BCRSamples != null & BCRSamples.Count > 0)
                {
                    BCRSamples.Clear();

                    BCRSamples = bcrFile.BuildBCRPlateSamples(BCRFilePath);
                    BCRPlate = bcrFile.BCRPlate;
                    LoadBCRSamples();

                }
                else if (HisPlateSamples != null && HisPlateSamples.Count > 0)
                {
                    HisPlateSamples.Clear();
                    HisPlateSamples = sqlService.GetPlateSamples(PlateId);
                    LoadHistorySamples();
                }
                else
                {

                }

            }
            catch (Exception ex)
            {

                string errMsg = "btnGet_Click() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;

            }
        }


    }
}
