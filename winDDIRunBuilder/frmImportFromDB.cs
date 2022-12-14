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
        public ValidPlate SelectedDBPlate { get; set; }
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
            var senderGrid = (DataGridView)sender;

            try
            {
                if (e.RowIndex >= 0)
                {
                    CurPlateId = (string)senderGrid.CurrentRow.Cells["Plate"].Value;
                    CurPlateVersion = (string)senderGrid.CurrentRow.Cells["version"].Value;

                    txbPlateId.Text = CurPlateId;

                    btnGo.Enabled = true;
                }
                else
                {
                    btnGo.Enabled = false;
                }

                txbPlateId.Focus();
            }
            catch (Exception ex)
            {
                txbPlateId.Text = "";
                txbPlateId.Focus();

                string errMsg = "dgvPlateSet_CellContentClick() met some issues:";
                errMsg = Environment.NewLine + ex.Message;
            }
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

            var selPlate = DBPlates.Where(db => db.PlateId == CurPlateId && db.PlateVersion == CurPlateVersion).FirstOrDefault();
            if (selPlate != null && selPlate.PlateId.Length > 0)
            {
                SelectedDBPlate.PlateId = selPlate.PlateId;
                SelectedDBPlate.StartWell = selPlate.StartPos;
                SelectedDBPlate.Offset = selPlate.OffSet;
                SelectedDBPlate.EndWell = selPlate.EndPos;
                SelectedDBPlate.Sample = selPlate.Sample;
                SelectedDBPlate.Diluent = selPlate.Diluent;
                SelectedDBPlate.Direction = selPlate.PlateRotated == true ? "1" : "0";
                SelectedDBPlate.PlateVersion = selPlate.PlateVersion;

                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
