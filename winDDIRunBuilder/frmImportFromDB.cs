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
        public bool IsSourcePlate { get; set; } = false;
        public bool IsSourceBCR { get; set; } = false;
        public ValidPlate SelectedDBPlate { get; set; }
        public List<PlateSample> SelectedPlateSamples { get; set; }
        List<DBPlate> DBPlates { get; set; }
        public string CurPlateId { get; set; } = "";
        public string CurPlateVersion { get; set; } = "";
        public frmImportFromDB()
        {
            InitializeComponent();
        }

        private void frmImportFromDB_Load(object sender, EventArgs e)
        {
            DBPlates = new List<DBPlate>();
            SelectedDBPlate = new ValidPlate();
            SelectedPlateSamples = new List<PlateSample>();

            dgvPlates.Rows.Clear();
            btnGo.Enabled = false;

            string plateName = "";

            try
            {
                LoadPlates(plateName);

                txbPlateId.Text = "";
                txbPlateId.Focus();

            }
            catch (Exception ex)
            {
                txbPlateId.Text = "";
                txbPlateId.Focus();

                string errMsg = "frmImportFromDB_Load met some issues:";
                errMsg = Environment.NewLine + ex.Message;
            }
            finally
            {
                txbPlateId.Text = "";
                this.ActiveControl = txbPlateId;
            }
        }

        private void LoadPlates(string plateId, string plateVersion = null)
        {
            DBPlates = new List<DBPlate>();
            dgvPlates.Rows.Clear();

            RepoSQL sqlService = new RepoSQL();

            try
            {
                DBPlates = sqlService.GetPlates(plateId);
                if (DBPlates != null && DBPlates.Count > 0)
                {
                    foreach (var plt in DBPlates)
                    {
                        dgvPlates.Rows.Add();
                        dgvPlates.Rows[dgvPlates.RowCount - 1].Cells["Plate"].Value = plt.PlateId;
                        dgvPlates.Rows[dgvPlates.RowCount - 1].Cells["StartPos"].Value = plt.StartPos;
                        dgvPlates.Rows[dgvPlates.RowCount - 1].Cells["EndPos"].Value = plt.EndPos;
                        dgvPlates.Rows[dgvPlates.RowCount - 1].Cells["ModifiedDate"].Value = plt.ModifiedDate;
                        dgvPlates.Rows[dgvPlates.RowCount - 1].Cells["Version"].Value = plt.PlateVersion;
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = "LoadPlates() met some issues:";
                errMsg = Environment.NewLine + ex.Message;
            }
        }

        private void dgvPlates_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //var senderGrid = (DataGridView)sender;

            //try
            //{
            //    if (e.RowIndex >= 0)
            //    {
            //        CurPlateId = (string)senderGrid.CurrentRow.Cells["Plate"].Value;
            //        CurPlateVersion = (string)senderGrid.CurrentRow.Cells["version"].Value;

            //        txbPlateId.Text = CurPlateId;

            //        btnGo.Enabled = true;
            //    }
            //    else
            //    {
            //        btnGo.Enabled = false;
            //    }

            //    txbPlateId.Focus();
            //}
            //catch (Exception ex)
            //{
            //    txbPlateId.Text = "";
            //    txbPlateId.Focus();

            //    string errMsg = "dgvPlateSet_CellContentClick() met some issues:";
            //    errMsg = Environment.NewLine + ex.Message;
            //}
        }

        private void txbPlateId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (!string.IsNullOrEmpty(txbPlateId.Text.Trim()))
                    {
                        LoadPlates(txbPlateId.Text.Trim());

                        if (dgvPlates != null && dgvPlates.Rows.Count > 0)
                        {
                            if (dgvPlates.Rows.Count == 1)
                            {
                                CurPlateId = txbPlateId.Text.Trim().ToUpper();
                                CurPlateVersion = (string)dgvPlates.Rows[0].Cells["Version"].Value;
                                btnGo.Enabled = true;
                            }
                            else
                            {
                                CurPlateId = "";
                                CurPlateVersion = "";
                                txbPlateId.Text = "";
                                btnGo.Enabled = false;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    txbPlateId.Text = "";
                    txbPlateId.Focus();

                    string errMsg = "{txbPlateId_KeyDown} met the following error: ";
                    errMsg += Environment.NewLine;
                    errMsg += ex.Message;

                }
                finally
                {
                    txbPlateId.Focus();
                }

            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            SelectedDBPlate = new ValidPlate();
            RepoSQL sqlService = new RepoSQL();
            ModelTransfer tranService = new ModelTransfer();

            try
            {
                CurPlateId= txbPlateId.Text.Trim().ToUpper();

                if (IsSourcePlate)
                {
                    List<DBPlate> sourcePlates = new List<DBPlate>();
                    DBPlate origialPlate = new DBPlate();
                    DBPlate lastPlate = new DBPlate();

                    sourcePlates = (List<DBPlate>)sqlService.GetPlates(CurPlateId).OrderBy(se => se.PlateVersion).ToList();
                    if (sourcePlates != null && sourcePlates.Count > 0)
                    {
                        origialPlate = sourcePlates.FirstOrDefault();
                        lastPlate = sourcePlates.LastOrDefault();

                        //Get Plate Samples
                        SelectedPlateSamples = sqlService.GetPlateSamples(CurPlateId);

                        SelectedDBPlate = tranService.DBPlate2ValidPlate(origialPlate, "SOURCE");
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show(
                            "The Source Plate must be existing. You entered an UN-Existing plate.",
                            "Source Plate",
                            MessageBoxButtons.OK);
                    }
                }
                else
                {
                    var selPlate = DBPlates.Where(db => db.PlateId.ToUpper() == CurPlateId && db.PlateVersion == CurPlateVersion).FirstOrDefault();
                    if (selPlate != null && selPlate.PlateId.Length > 0)
                    {
                        //Get Plate Samples
                        SelectedPlateSamples = sqlService.GetPlateSamples(CurPlateId);
                        SelectedDBPlate = tranService.DBPlate2ValidPlate(selPlate, "DEST");
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show(
                            "The Destination Plate must be existing. You entered an UN-Existing plate.",
                            "Destination Plate",
                            MessageBoxButtons.OK);

                    }
                }

                if (SelectedDBPlate != null && string.IsNullOrEmpty(SelectedDBPlate.PlateId) == false)
                {
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                string errMsg = "{btnGo_Click} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                
                txbPlateId.Text = "";
                txbPlateId.Focus();
            }
        }

        private void txbPlateId_TextChanged(object sender, EventArgs e)
        {
            if (txbPlateId.Text.Trim().Length > 0)
            {
                btnGo.Enabled = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


    }
}
