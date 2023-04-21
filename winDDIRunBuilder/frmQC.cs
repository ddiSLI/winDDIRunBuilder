using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using winDDIRunBuilder.Models;
using static winDDIRunBuilder.ClientBackend;
using static winDDIRunBuilder.Models.InputPlate;

namespace winDDIRunBuilder
{
    public partial class frmQC : Form
    {
        public string PlateId { get; set; } = "";
        public DBPlate CurDBPlate { get; set; } = new DBPlate();
        public InputPlate QCInputPlate { get; set; } = new InputPlate();

        public string CurExportPath { get; set; }

        private string pPlateSizeX = "";
        private string pPlateSizeY = "";
        public bool IsRotated { get; set; } = false;
        private string pAlpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private List<string> WellXs { get; set; }
        private List<string> WellYs { get; set; }

        private bool pHasQC = false;

        public frmQC()
        {
            InitializeComponent();
        }

        private void frmQC_Load(object sender, EventArgs e)
        {
            string assay = "";
            List<string> assays = new List<string>();
            RepoSQL sqlService = new RepoSQL();
            List<DBPlate> dbPlates = new List<DBPlate>();

            try
            {
                //Testing
                //PlateId = "eBeta230228AH";
                //
                //

                if (!string.IsNullOrEmpty(PlateId))
                {
                    dbPlates = sqlService.GetPlates(PlateId).OrderByDescending(p => p.PlateVersion).ToList();
                    CurDBPlate = dbPlates.FirstOrDefault();
                    txbPlateId.Text = PlateId;
                    txbExportPath.Text = CurExportPath;
                }

                if (CurDBPlate.PlateRotated)
                {
                    pPlateSizeX = CurDBPlate.SizeEndWell.Substring(1);
                    pPlateSizeY = CurDBPlate.SizeEndWell.Substring(0, 1);
                }
                else
                {
                    pPlateSizeX = CurDBPlate.SizeEndWell.Substring(1);
                    pPlateSizeY = CurDBPlate.SizeEndWell.Substring(0, 1);
                }


                SetWellXY();




                //Get DBTests
                assay = ConfigurationManager.AppSettings["Assay"];
                assays = assay.Split(',').ToList();

                //Add DBTest to Control
                cmbAssay.Items.AddRange(assays.ToArray());

                cmbAssay.SelectedItem = CurDBPlate.PlateName;

                cbExportFormat.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                string msgErr = ex.Message;


            }

        }

        private void btnGetQC_Click(object sender, EventArgs e)
        {
            RepoSQL sqlService = new RepoSQL();
            string lastSampleWell = "";

            try
            {
                string qcPlate = cmbAssay.SelectedItem.ToString();
                List<QCSample> qcSamples = new List<QCSample>();
                MapPlateSamples mapPlateSamples = new MapPlateSamples();
                DataTable dtPlateSamples = new DataTable();

                if (!string.IsNullOrEmpty(PlateId) && !string.IsNullOrEmpty(qcPlate))
                {
                    mapPlateSamples.GetMapAnyPlateSamples(PlateId);
                    dtPlateSamples = mapPlateSamples.PlateSampleMapTable;
                    MapPlates(dtPlateSamples);
                    lastSampleWell = mapPlateSamples.LastSampleWell;

                    qcSamples = sqlService.GetQCSamples(qcPlate);
                    LoadQCSamples(qcSamples, lastSampleWell);

                }

                if (pHasQC)
                {
                    btnAddQC.Enabled = false;
                }
                else
                {
                    btnAddQC.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                string errMsg = "btnGetQC_Click() met some issues:";
                errMsg += ex.Message;

                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = errMsg;

            }

        }

        private void LoadQCSamples(List<QCSample> qcSamples, string lastSampleWell)
        {

            int iRw = 0;
            string wellX = "";
            string wellY = "";

            try
            {
                dgvQCSamples.Rows.Clear();

                if (qcSamples.Count > 0)
                {
                    DataGridViewComboBoxCell cbCell = new DataGridViewComboBoxCell();
                    foreach (var qs in qcSamples)
                    {
                        dgvQCSamples.Rows.Add();
                        iRw = dgvQCSamples.Rows.Count - 1;
                        dgvQCSamples.Rows[iRw].Cells["Include"].Value = true;
                        dgvQCSamples.Rows[iRw].Cells["Sample"].Value = qs.Sample;
                        dgvQCSamples.Rows[iRw].Cells["Prefix"].Value = qs.Prefix;
                        dgvQCSamples.Rows[iRw].Cells["HarvestId"].Value = qs.HarvestId;

                        if (qs.Well.ToUpper() == "END")
                        {
                            if (CurDBPlate.PlateRotated)
                            {
                                if ((Char.Parse(lastSampleWell.Substring(0, 1)) <= Char.Parse(pPlateSizeX)) &&
                                    (Convert.ToInt32(lastSampleWell.Substring(1)) < Convert.ToInt32(pPlateSizeY)))
                                {
                                    wellX = lastSampleWell.Substring(0, 1);
                                    //wellY = (Convert.ToInt32(lastSampleWell.Substring(1)) + 1).ToString();
                                    wellY = ((char)((int)Char.Parse(lastSampleWell.Substring(1)) + 1)).ToString();
                                }
                                else
                                {
                                    if (Convert.ToInt32(lastSampleWell.Substring(1)) == Convert.ToInt32(pPlateSizeY))
                                    {
                                        wellX = (Char.Parse(lastSampleWell.Substring(0, 1)) + 1).ToString();
                                        wellY = "1";
                                    }
                                }
                            }
                            else
                            {
                                if (Char.Parse(lastSampleWell.Substring(0, 1)) < Char.Parse(pPlateSizeY))
                                {
                                    wellX = lastSampleWell.Substring(1);
                                    wellY = ((char)((int)Char.Parse(lastSampleWell.Substring(0, 1)) + 1)).ToString();

                                }
                                else
                                {
                                    if (Convert.ToInt32(lastSampleWell.Substring(1)) < Convert.ToInt32(pPlateSizeX))
                                    {
                                        wellX = (Convert.ToInt32(lastSampleWell.Substring(1)) + 1).ToString();
                                        wellY = "A";
                                    }
                                }
                            }
                        }
                        else
                        {
                            wellX = qs.Well.Substring(1);
                            wellY = qs.Well.Substring(0, 1);
                        }

                        cbCell = new DataGridViewComboBoxCell();
                        cbCell = (DataGridViewComboBoxCell)dgvQCSamples.Rows[iRw].Cells["WellX"];
                        cbCell.Items.AddRange(WellXs.ToArray());
                        dgvQCSamples.Rows[iRw].Cells["WellX"].Value = wellX;

                        cbCell = new DataGridViewComboBoxCell();
                        cbCell = (DataGridViewComboBoxCell)dgvQCSamples.Rows[iRw].Cells["WellY"];
                        cbCell.Items.AddRange(WellYs.ToArray());
                        dgvQCSamples.Rows[iRw].Cells["WellY"].Value = wellY;
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = "LoadQCSamples() met some issues:";
                errMsg += ex.Message;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = errMsg;
            }
        }

        private List<QCSample> GetQCSamples(string plate)
        {
            List<QCSample> qcSamples = new List<QCSample>();
            QCSample qcSmp = new QCSample();

            qcSmp.Plate = "eBeta";
            qcSmp.Sample = "eBHIGH032322";
            qcSmp.PlateDesc = "Plate position fixed";
            qcSamples.Add(qcSmp);

            qcSmp = new QCSample();
            qcSmp.Plate = "eBeta";
            qcSmp.Sample = "eBNORM032322";
            qcSmp.PlateDesc = "Plate position fixed";
            qcSamples.Add(qcSmp);

            qcSmp = new QCSample();
            qcSmp.Plate = "eBeta";
            qcSmp.Sample = "GUSBP221203";
            qcSmp.PlateDesc = "Plate position fixed";
            qcSamples.Add(qcSmp);

            qcSmp = new QCSample();
            qcSmp.Plate = "eBeta";
            qcSmp.Sample = "FSIgAP2B101122";
            qcSmp.PlateDesc = "Position is variable but always last filled well on plate";
            qcSamples.Add(qcSmp);


            return qcSamples;

        }

        private void btnAddQC_Click(object sender, EventArgs e)
        {
            string sample = "";
            string wellX = "";
            string wellY = "";
            int iRw = 0;

            foreach (DataGridViewRow dr in dgvQCSamples.Rows)
            {
                if (Convert.ToBoolean(dr.Cells["Include"].Value))
                {
                    // sample = dr.Cells["Sample"].Value.ToString() + "_QC";
                    //sample = dr.Cells["Prefix"].Value.ToString() + "_QC";
                    sample = dr.Cells["HarvestId"].Value.ToString() + "_QC";
                    wellX = dr.Cells["WellX"].Value.ToString();
                    wellY = dr.Cells["WellY"].Value.ToString();

                    if (!string.IsNullOrEmpty(wellX) && !string.IsNullOrEmpty(wellY))
                    {
                        if (IsRotated)
                        {
                            iRw = pAlpha.IndexOf(wellY);
                            dgvSamplePlate.Rows[iRw].Cells[wellX].Value = sample;
                        }
                        else
                        {
                            iRw = pAlpha.IndexOf(wellY);
                            dgvSamplePlate.Rows[iRw].Cells[wellX].Value = sample;
                        }
                    }
                }
            }
        }

        private void MapPlates(DataTable dtPlateSamples, bool pIsRotated = false)
        {
            string curWell = "";

            dgvSamplePlate.Rows.Clear();
            dgvSamplePlate.Columns.Clear();

            if (dtPlateSamples != null && dtPlateSamples.Rows.Count > 0)
            {
                foreach (DataColumn col in dtPlateSamples.Columns)
                {
                    if (col.ColumnName == "0")
                    {
                        DataGridViewTextBoxColumn dgvCol = new DataGridViewTextBoxColumn();
                        dgvCol.Name = col.ColumnName;
                        dgvCol.Width = 28;
                        dgvSamplePlate.Columns.Add(dgvCol);
                    }
                    else
                    {
                        DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                        dgvBtn.Name = col.ColumnName;
                        dgvBtn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        dgvBtn.Width = 70;
                        dgvSamplePlate.Columns.Add(dgvBtn);
                    }
                }

                dgvSamplePlate.RowTemplate.Height = 50;
                foreach (DataRow tRw in dtPlateSamples.Rows)
                {
                    dgvSamplePlate.Rows.Add();
                    for (int tCol = 0; tCol < dtPlateSamples.Columns.Count; tCol++)
                    {
                        if (tRw[tCol] != null && tRw[tCol].ToString().IndexOf("QC") >= 0)
                        {
                            pHasQC = true;
                        }

                        if (pIsRotated)
                        {
                            //Rotated
                            if (tCol == dgvSamplePlate.ColumnCount - 1)
                            {
                                dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Value = tRw[tCol];
                            }
                            else
                            {
                                dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Value = tRw[tCol];

                                //Put info to Tag
                                if (tRw[tCol] == null)
                                {
                                    dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Tag = "Empty Sample";
                                }
                                else
                                {
                                    curWell = dgvSamplePlate.Columns[tCol].Name;
                                    curWell += dgvSamplePlate.RowCount.ToString();
                                    dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Tag = "Well is " + curWell;
                                }
                            }
                        }
                        else
                        {
                            // Not Rotated
                            if (tCol == 0)
                            {
                                dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Value = tRw[tCol];
                            }
                            else
                            {
                                dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Value = tRw[tCol];

                                //Put info to Tag
                                if (tRw[tCol] == null)
                                {
                                    dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Tag = "Empty Sample";
                                }
                                else
                                {
                                    curWell = dgvSamplePlate[0, dgvSamplePlate.RowCount - 1].Value.ToString();
                                    curWell += dgvSamplePlate.Columns[tCol].Name;
                                    dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Tag = "Well is " + curWell;
                                }

                                //var sampleName = InputFileValues.Where(smp => smp.PlateId.ToUpper() == pSelectedPlatePage.ToUpper() &&
                                //                                         smp.ShortId == tRw[tCol].ToString()).Select(smp => smp.FullSampleId).FirstOrDefault();
                                //if (sampleName == null || string.IsNullOrEmpty(sampleName.ToString()))
                                //{
                                //    dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Tag = "empty sample";
                                //}
                                //else
                                //{
                                //    dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Tag = sampleName.ToString();
                                //}
                            }

                        }

                    }
                }
            }

        }

        private void dgvSamplePlate_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            bool pIsRotated = false;

            if (e.RowIndex < 0)
                return;

            //I supposed your button column is at index 0
            //if (e.ColumnIndex > 0 && string.IsNullOrEmpty(e.Value.ToString()) == false)
            if (string.IsNullOrEmpty(e.Value.ToString()) == false)
            {
                //if ((pIsRotated == false && e.ColumnIndex > 0) || (pIsRotated && e.ColumnIndex < dgvSamplePlate.ColumnCount - 1))
                if (pIsRotated == false && e.ColumnIndex > 0)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    var w = Properties.Resources.tubeBlue64.Width / 3;  //Properties.Resources.tubeBlue64.Width;
                    var h = Properties.Resources.tube64.Height / 3;     // Properties.Resources.tube64.Height;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 30; // e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 5; // e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    if (e.Value.ToString().IndexOf("QC") >= 0)
                    {
                        e.Graphics.DrawImage(Properties.Resources.tubePurple64, new Rectangle(x, y, w, h));
                    }
                    else
                    {
                        e.Graphics.DrawImage(Properties.Resources.tubeBlue64, new Rectangle(x, y, w, h));
                    }



                    e.Handled = true;
                }
                else if (pIsRotated && e.ColumnIndex < dgvSamplePlate.ColumnCount - 1)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    var w = Properties.Resources.tubeBlue64.Width / 3;  //Properties.Resources.tubeBlue64.Width;
                    var h = Properties.Resources.tube64.Height / 3;     // Properties.Resources.tube64.Height;
                    var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 30; // e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 5; // e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    e.Graphics.DrawImage(Properties.Resources.tubeBlue64, new Rectangle(x, y, w, h));
                    e.Handled = true;
                }

            }
        }

        private void dgvSamplePlate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            //string sourcePlate = "";
            //string destPlate = "";

            string cellValue = "";
            string cellX = "";
            string cellY = "";

            try
            {
                if (e.RowIndex >= 0)
                {
                    //bool included = Convert.ToBoolean(senderGrid.CurrentRow.Cells["Included"].Value);
                    //bool isReadySourcePlate = Convert.ToBoolean(senderGrid.CurrentRow.Cells["SourcePlateIsNew"].Value);

                    //sourcePlate = (string)senderGrid.CurrentRow.Cells["SourcePlate"].Value;
                    //destPlate = (string)senderGrid.CurrentRow.Cells["DestPlate"].Value;

                    if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                    {
                        senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        lblMsg.ForeColor = Color.DarkBlue;
                        lblMsg.Text = "Tube Sample: " + senderGrid.CurrentCell.Value + "; [ " + senderGrid.CurrentCell.Tag + " ]";
                        //lblMsg.Text = "Tube -" + senderGrid.CurrentCell.Value + " sample is : [ " + senderGrid.CurrentCell.Tag + " ]";
                    }

                    if (IsRotated)
                    {
                        cellX = senderGrid.CurrentRow.Cells[senderGrid.Columns.Count - 1].ToString();
                        cellY = senderGrid.Columns[e.ColumnIndex].Name;


                    }
                    else
                    {
                        cellX = senderGrid.Columns[e.ColumnIndex].Name;
                        cellY = senderGrid.CurrentRow.Cells[0].Value.ToString();

                        if (string.IsNullOrEmpty(senderGrid.CurrentRow.Cells[e.ColumnIndex].Value.ToString()))
                        {
                            cellValue = "";
                            dgvQCSamples.CurrentRow.Cells["WellX"].Value = cellX;
                            dgvQCSamples.CurrentRow.Cells["WellY"].Value = cellY;
                        }
                        else
                        {
                            cellValue = senderGrid.CurrentRow.Cells[e.ColumnIndex].Value.ToString();
                            if (cellValue.IndexOf("QC") >= 0)
                            {
                                dgvQCSamples.CurrentRow.Cells["WellX"].Value = cellX;
                                dgvQCSamples.CurrentRow.Cells["WellY"].Value = cellY;
                            }
                        }


                    }




                }

            }
            catch (Exception ex)
            {
                string errMsg = "dgvSamplePlate_CellContentClick() met some issues:";
                errMsg += ex.Message;

                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = errMsg;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dgvQCSamples.Rows.Add();

            DataGridViewComboBoxCell cbCell = new DataGridViewComboBoxCell();
            cbCell = (DataGridViewComboBoxCell)dgvQCSamples.Rows[dgvQCSamples.Rows.Count - 1].Cells["WellX"];
            cbCell.Items.AddRange(WellXs.ToArray());

            cbCell = new DataGridViewComboBoxCell();
            cbCell = (DataGridViewComboBoxCell)dgvQCSamples.Rows[dgvQCSamples.Rows.Count - 1].Cells["WellY"];
            cbCell.Items.AddRange(WellYs.ToArray());
        }

        private void SetWellXY(string sizeX = "A1", string sizeY = "H12", bool isRotated = false)
        {
            string wx = "";
            string wy = "";

            WellXs = new List<string>();
            WellYs = new List<string>();

            if (isRotated)
            {
                wy = sizeY.Substring(0, 1);
                for (char chrX = 'A'; chrX <= char.Parse(wy); chrX++)
                {
                    WellXs.Add(chrX.ToString());
                }

                wx = sizeY.Substring(1);
                for (int intY = 1; intY <= Convert.ToInt32(wx); intY++)
                {
                    WellXs.Add(intY.ToString());
                }

            }
            else
            {
                wy = sizeY.Substring(0, 1);
                for (char chrY = 'A'; chrY <= char.Parse(wy); chrY++)
                {
                    WellYs.Add(chrY.ToString());
                }

                wx = sizeY.Substring(1);
                for (int intX = 1; intX <= Convert.ToInt32(wx); intX++)
                {
                    WellXs.Add(intX.ToString());
                }
            }
        }

        private void btnSetLocation_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                try
                {
                    txbExportPath.Text = folderDlg.SelectedPath;
                }
                catch (IOException ioEx)
                {
                    string errMsg = "btnSetLocation_Click() met some issues:";
                    errMsg += ioEx.Message;

                    lblMsg.ForeColor = Color.Red;
                    lblMsg.Text = errMsg;
                }
            }

        }

        private void btnSent_Click(object sender, EventArgs e)
        {
            string resultSaveSample = "";
            string resultUpdPlate = "";
            string exportFile = "";
            DateTime curDateTime = DateTime.Now;
            string curDate = "";
            string curTime = "";
            List<PlateSample> rawQCSamples = new List<PlateSample>();
            List<OutputPlateSample> qcSamples = new List<OutputPlateSample>();
            List<PlateSample> pltQCSamples = new List<PlateSample>();
            RepoSQL sqlService = new RepoSQL();
            List<DtoWorklist> dtoworklist = new List<DtoWorklist>();

            try
            {
                curDate = curDateTime.Month.ToString("0#") + curDateTime.Day.ToString("0#") + curDateTime.Year.ToString("##");
                curTime= curDateTime.Hour.ToString("0#") + curDateTime.Minute.ToString("0#") + curDateTime.Second.ToString("##");

                if (!string.IsNullOrEmpty(cbExportFormat.SelectedItem.ToString()) && !string.IsNullOrEmpty(txbExportPath.Text))
                {
                    if (txbExportPath.Text.Trim().LastIndexOf("\\")== txbExportPath.Text.Trim().Length - 1)
                    {
                        exportFile = txbExportPath.Text.Trim();
                    }
                    else
                    {
                        exportFile = txbExportPath.Text.Trim() + "\\";
                    }
                    

                    //if (cbExportFormat.SelectedIndex == 0)
                    //{

                    //}
                    //else if (cbExportFormat.SelectedIndex == 1)
                    //{
                    //    //Luminex GPP
                    //}
                    //else if (cbExportFormat.SelectedIndex == 2)
                    //{
                    //    //Aus
                    //}
                    //else if (cbExportFormat.SelectedIndex == 3)
                    //{
                    //    //ABI750
                    //}
                    //else if (cbExportFormat.SelectedIndex == 4)
                    //{
                    //    //Kashi.CSV
                    //}
                    //else if (cbExportFormat.SelectedIndex == 5)
                    //{
                    //    //Manual
                    //}
                    //else
                    //{
                    //    exportFile = txbExportPath.Text.Trim() + "/" + CurDBPlate.PlateId + ".CSV";
                    //}

                    if (!string.IsNullOrEmpty(cbExportFormat.SelectedItem.ToString()))
                    {
                        //Gem5
                        exportFile += cbExportFormat.SelectedItem.ToString().Trim();
                        exportFile += "_" + CurDBPlate.PlateId;
                        exportFile += "_" + curDate;
                        exportFile += "_" + curTime;
                        exportFile += ".CSV";
                    }
                    

                    //Collect plateSamples
                    rawQCSamples = MakePlateQCSamples();
                    qcSamples = MakeQCSamples();

                    //Call back serviceAPI
                    //Tanslate QC Samples
                    //dtoworklist = ProcessQCSamples(rawQCSamples);

                    //
                    //pltQCSamples = GetQCPlateSamples(dtoworklist);

                    if (pHasQC == false)
                    {
                        //Add QC samples
                        resultSaveSample = sqlService.AddSamples(qcSamples, Environment.UserName);
                        //resultSaveSample = sqlService.AddPlateQCSamples(rawQCSamples, Environment.UserName);

                        //Update Plate Status
                        if (resultSaveSample == "SUCCESS")
                        {
                            resultUpdPlate = sqlService.UpdatePlateQC(CurDBPlate.PlateId, CurDBPlate.PlateVersion, hasQC: true, Environment.UserName);
                        }
                    }

                    //Sent QC Samples
                    if (WriteExport(CurDBPlate.PlateId, rawQCSamples, exportFile) == "SUCCESS")
                    {
                        lblMsg.Text = "The Following samples exported to " + exportFile;
                        lblMsg.ForeColor = Color.DarkBlue;
                    }

                }

            }
            catch (IOException ioEx)
            {
                string errMsg = "btnSent_Click() met some issues:";
                errMsg += ioEx.Message;

                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = errMsg;
            }

        }

        private List<PlateSample> GetQCPlateSamples(List<DtoWorklist> dtoWorklist)
        {
            List<PlateSample> qcPlateSamples = new List<PlateSample>();
            PlateSample qcSmp = new PlateSample();
            ModelTransfer mTrans = new ModelTransfer();
            string[] wellXY;

            try
            {
                if (dtoWorklist != null && dtoWorklist.Count > 0)
                {
                    foreach (var wl in dtoWorklist)
                    {
                        qcSmp = new PlateSample();
                        qcSmp.PlateId = CurDBPlate.PlateId;
                        if (wl.Attributes.ContainsKey("QCId") && !string.IsNullOrEmpty(wl.Attributes["QCId"]))
                        {
                            qcSmp.SampleId = wl.Attributes["QCId"];
                            qcSmp.SampleType = "QC";
                        }
                        else
                        {
                            qcSmp.SampleId = wl.Attributes["SampleId"];
                            qcSmp.SampleType = "";
                        }

                        wellXY = wl.SourceWellId.Split(',');
                        //wellXY = wl.DestWellId.Split(',');
                        if (CurDBPlate.PlateRotated)
                        {
                            qcSmp.Well = pAlpha.Substring(int.Parse(wellXY[0]), 1) + wellXY[1];
                        }
                        else
                        {
                            qcSmp.Well = pAlpha.Substring(int.Parse(wellXY[1]), 1) + wellXY[0];
                        }


                        qcPlateSamples.Add(qcSmp);
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = "QC.GetQCPlateSamples() met some issues:";
                errMsg += ex.Message;

                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = errMsg;
            }

            return qcPlateSamples;
        }

        private List<OutputPlateSample> MakeQCSamples()
        {
            List<OutputPlateSample> plateQCSamples = new List<OutputPlateSample>();
            OutputPlateSample pltSmp = new OutputPlateSample();
            int rows = 0;
            int cols = 0;
            string sample = "";
            string well = "";


            try
            {
                if (dgvSamplePlate != null && dgvSamplePlate.Rows.Count > 0)
                {
                    rows = dgvSamplePlate.Rows.Count;
                    cols = dgvSamplePlate.ColumnCount;

                    if (IsRotated)
                    {
                        for (int cl = cols - 2; cl >= 0; cl--)
                        {
                            for (int rw = 0; rw < rows; rw++)
                            {
                                if (dgvSamplePlate[cl, rw].Value != null && !string.IsNullOrEmpty(dgvSamplePlate[cl, rw].Value.ToString()))
                                {
                                    pltSmp = new OutputPlateSample();
                                    pltSmp.DestPlateId = CurDBPlate.PlateId;
                                    pltSmp.DestPlateVersion = CurDBPlate.PlateVersion;

                                    well = dgvSamplePlate.Columns[cl].Name;
                                    well += dgvSamplePlate[cols - 1, rw].Value.ToString();

                                    sample = dgvSamplePlate[cl, rw].Value.ToString();

                                    if (sample.IndexOf("QC") >= 0)
                                    {
                                        pltSmp.SampleId = sample.Remove(sample.IndexOf("_QC"));
                                        pltSmp.SampleType = "QC";

                                        pltSmp.DestWellId = well;

                                        pltSmp.Sequence = 0;
                                        pltSmp.SourcePlateId = "";
                                        pltSmp.SourcePlateVersion = "";
                                        pltSmp.SourceWellId = "";

                                        plateQCSamples.Add(pltSmp);

                                    }

                                }
                            }
                        }
                    }
                    else if (IsRotated == false)
                    {
                        for (int cl = 1; cl < cols; cl++)
                        {
                            for (int rw = 0; rw < rows; rw++)
                            {
                                if (dgvSamplePlate[cl, rw].Value != null && !string.IsNullOrEmpty(dgvSamplePlate[cl, rw].Value.ToString()))
                                {
                                    pltSmp = new OutputPlateSample();
                                    pltSmp.DestPlateId = CurDBPlate.PlateId;
                                    pltSmp.DestPlateVersion = CurDBPlate.PlateVersion;

                                    well = dgvSamplePlate[0, rw].Value.ToString();
                                    well += dgvSamplePlate.Columns[cl].Name;
                                    sample = dgvSamplePlate[cl, rw].Value.ToString();

                                    if (sample.IndexOf("QC") >= 0)
                                    {
                                        pltSmp.SampleId = sample.Remove(sample.IndexOf("_QC"));
                                        pltSmp.SampleType = "QC";

                                        pltSmp.DestWellId = well;

                                        pltSmp.Sequence = 0;
                                        pltSmp.SourcePlateId = "";
                                        pltSmp.SourcePlateVersion = "";
                                        pltSmp.SourceWellId = "";

                                        plateQCSamples.Add(pltSmp);
                                    }
                                }
                            }
                        }
                    }

                    if (plateQCSamples != null && plateQCSamples.Count > 0)
                    {
                        plateQCSamples.LastOrDefault().SampleType = "QCEND";
                    }

                }
            }
            catch (Exception ex)
            {
                string errMsg = "MakePlateQCSamples() met some issues:";
                errMsg += ex.Message;

                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = errMsg;
            }

            return plateQCSamples;

        }

        public List<PlateSample> MakePlateQCSamples()
        {
            List<PlateSample> plateQCSamples = new List<PlateSample>();
            PlateSample pltSmp = new PlateSample();
            int rows = 0;
            int cols = 0;
            string sample = "";
            string well = "";


            try
            {
                if (dgvSamplePlate != null && dgvSamplePlate.Rows.Count > 0)
                {
                    rows = dgvSamplePlate.Rows.Count;
                    cols = dgvSamplePlate.ColumnCount;

                    if (IsRotated)
                    {
                        for (int cl = cols - 2; cl >= 0; cl--)
                        {
                            for (int rw = 0; rw < rows; rw++)
                            {
                                if (dgvSamplePlate[cl, rw].Value != null && !string.IsNullOrEmpty(dgvSamplePlate[cl, rw].Value.ToString()))
                                {
                                    pltSmp = new PlateSample();
                                    pltSmp.PlateId = CurDBPlate.PlateId;
                                    pltSmp.PlateVersion = CurDBPlate.PlateVersion;

                                    well = dgvSamplePlate.Columns[cl].Name;
                                    well += dgvSamplePlate[cols - 1, rw].Value.ToString();

                                    sample = dgvSamplePlate[cl, rw].Value.ToString();

                                    if (sample.IndexOf("QC") >= 0)
                                    {
                                        pltSmp.SampleId = sample.Remove(sample.IndexOf("_QC"));
                                        pltSmp.SampleType = "QC";
                                    }
                                    else
                                    {
                                        pltSmp.SampleId = sample;
                                        pltSmp.SampleType = "";
                                    }
                                    pltSmp.Well = well;

                                    plateQCSamples.Add(pltSmp);
                                }
                            }
                        }
                    }
                    else if (IsRotated == false)
                    {
                        for (int cl = 1; cl < cols; cl++)
                        {
                            for (int rw = 0; rw < rows; rw++)
                            {
                                if (dgvSamplePlate[cl, rw].Value != null && !string.IsNullOrEmpty(dgvSamplePlate[cl, rw].Value.ToString()))
                                {
                                    pltSmp = new PlateSample();
                                    pltSmp.PlateId = CurDBPlate.PlateId;
                                    pltSmp.PlateVersion = CurDBPlate.PlateVersion;

                                    well = dgvSamplePlate[0, rw].Value.ToString();
                                    well += dgvSamplePlate.Columns[cl].Name;
                                    sample = dgvSamplePlate[cl, rw].Value.ToString();

                                    if (sample.IndexOf("QC") >= 0)
                                    {
                                        pltSmp.SampleId = sample.Remove(sample.IndexOf("_QC"));
                                        pltSmp.SampleType = "QC";
                                    }
                                    else
                                    {
                                        pltSmp.SampleId = sample;
                                        pltSmp.SampleType = "";
                                    }
                                    pltSmp.Well = well;

                                    plateQCSamples.Add(pltSmp);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = "MakePlateQCSamples() met some issues:";
                errMsg += ex.Message;

                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = errMsg;
            }

            return plateQCSamples;

        }

        public string WriteExport(string plateId, List<PlateSample> plateSamples, string exportPath)
        {
            string actionResult = "NA";
            string exportArchivePath = "";

            string itemValue = "";
            string extValue = "";

            string plateFileItems = "SampleID, Well, SampleType ";

            try
            {

                using (var writer = new StreamWriter(exportPath))
                {
                    //writer.WriteLine(plateFileItems);


                    foreach (var item in plateSamples)
                    {
                        //itemValue = plateId + ",";
                        itemValue = item.SampleId.Replace("-", "") + ",";
                        //itemValue += item.Well + ",";
                        //itemValue += item.SampleType + "";

                        itemValue += extValue;

                        writer.WriteLine(itemValue);
                        itemValue = "";
                    }
                }

                //Archive
                //File.Copy(exportPath, exportArchivePath, true);

                actionResult = "SUCCESS";
            }
            catch (Exception ex)
            {
                string errMsg = "{WriteExport()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                actionResult = "ERROR:" + errMsg;
            }

            return actionResult;
        }

        private List<DtoWorklist> ProcessQCSamples(List<PlateSample> rawQCSamples)
        {
            List<DtoWorklist> dtoWorklist = new List<DtoWorklist>();
            ClientBackend backService = new ClientBackend();
            InputPlate sourcePlate = new InputPlate();
            InputPlate virtualDestPlate = new InputPlate();
            ValidPlate findSourcePlate = new ValidPlate();
            List<InputFile> plateSamples = new List<InputFile>();
            string hasSourcePlate = "";
            string hasVirtualPlate = "";
            string hasSamples = "";

            try
            {
                findSourcePlate.PlateId = CurDBPlate.PlateId;
                findSourcePlate.PlateName = CurDBPlate.PlateName;
                findSourcePlate.ExcludeWells = CurDBPlate.ExcludeWells;
                findSourcePlate.SizeStartWell = CurDBPlate.SizeStartWell;
                findSourcePlate.SizeEndWell = CurDBPlate.SizeEndWell;

                //Get History Source Plate
                sourcePlate = GetHisDestPlate(findSourcePlate);

                //Create Source Plate
                hasSourcePlate = backService.CreatePlate(sourcePlate);

                //Create Virtual DestPlate
                virtualDestPlate = (InputPlate)sourcePlate.Clone();
                virtualDestPlate.Name = sourcePlate.Name + "VT";
                hasVirtualPlate = backService.CreatePlate(virtualDestPlate);

                //Add Plate QC samples
                if (hasSourcePlate == "YES" && hasVirtualPlate == "YES")
                {
                    plateSamples = GetSourcePlateSamples(rawQCSamples);

                    hasSamples = backService.AddSamples(CurDBPlate.PlateId, plateSamples, isQC: true);
                    if (!string.IsNullOrEmpty(backService.ErrMsg))
                    {
                        lblMsg.ForeColor = Color.DarkRed;
                        lblMsg.Text = backService.ErrMsg;
                    }
                }

                //Process new destination Plate
                dtoWorklist = (List<DtoWorklist>)backService.GetWorklist(sourcePlate.Name, virtualDestPlate.Name, "lookup_alias,lookup_qc");
                if (dtoWorklist == null && dtoWorklist.Count == 0)
                {
                    lblMsg.ForeColor = Color.DarkRed;
                    lblMsg.Text = "There is no available QC samples.";
                }

            }
            catch (Exception ex)
            {
                string errMsg = "ProcessQCSamples() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;

                lblMsg.ForeColor = Color.DarkRed;
                lblMsg.Text = errMsg;
            }

            return dtoWorklist;
        }

        private List<InputFile> GetSourcePlateSamples(List<PlateSample> qcPlateSampls)
        {
            List<InputFile> inputSamples = new List<InputFile>();
            InputFile sample = new InputFile();
            try
            {
                //Get samples from the history plate
                if (qcPlateSampls != null && qcPlateSampls.Count > 0)
                {
                    foreach (var hSmp in qcPlateSampls)
                    {
                        sample = new InputFile();
                        sample.ShortId = hSmp.SampleId;    // scanner read sample label
                        sample.RackName = hSmp.PlateId;
                        sample.SampleType = hSmp.SampleType;
                        sample.Position = hSmp.Well;
                        sample.Well = hSmp.Well;
                        if (CurDBPlate.PlateRotated)
                        {
                            sample.WellX = pAlpha.IndexOf(hSmp.Well.Substring(0, 1)).ToString();
                            sample.WellY = hSmp.Well.Substring(1);
                        }
                        else
                        {
                            sample.WellX = hSmp.Well.Substring(1);
                            sample.WellY = pAlpha.IndexOf(hSmp.Well.Substring(0, 1)).ToString();
                        }



                        inputSamples.Add(sample);
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
            }

            return inputSamples;
        }
        private InputPlate GetHisDestPlate(ValidPlate alidPlate)
        {
            //Get the history plates as destination, and modify it
            InputPlate newDestPlate = new InputPlate();
            string sizeStart = "";
            string sizeEnd = "";
            string startPos = "";
            string endPos = "";

            if (CurDBPlate != null)
            {
                sizeStart = CurDBPlate.SizeStartWell;
                sizeEnd = CurDBPlate.SizeEndWell;
                startPos = CurDBPlate.SizeStartWell;
                endPos = CurDBPlate.SizeEndWell;

                int startY = pAlpha.IndexOf(startPos.Substring(0, 1));
                int startX = Convert.ToInt32(startPos.Substring(1));

                int endY = pAlpha.IndexOf(endPos.Substring(0, 1));
                int endX = Convert.ToInt32(endPos.Substring(1));

                Pos sPos = new Pos();
                Pos ePos = new Pos();

                sPos.X = startX.ToString();
                sPos.Y = startY.ToString();
                ePos.X = endX.ToString();
                ePos.Y = endY.ToString();

                newDestPlate.Start = sPos;
                newDestPlate.End = ePos;
                newDestPlate.Name = CurDBPlate.PlateId;
                newDestPlate.Exclude = CurDBPlate.ExcludeWells.Split('|').Distinct().Select(WellToPosition).ToList();

                newDestPlate.Direction = "0";
            }

            return newDestPlate;
        }

        private Pos WellToPosition(string input)
        {
            var Y = (int)input[0] - (int)'A';
            var X = int.Parse(input.Substring(1)) - 1;
            return new Pos() { X = X.ToString(), Y = Y.ToString() };
        }

        private void cbExportFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbExportFormat.SelectedItem.ToString()) && !string.IsNullOrEmpty(txbExportPath.Text))
            {
                btnSent.Enabled = true;
            }
            else
            {
                btnSent.Enabled = false;
            }
        }

        private void txbExportPath_TextChanged(object sender, EventArgs e)
        {
            if (cbExportFormat.SelectedIndex>=0 && !string.IsNullOrEmpty(cbExportFormat.SelectedItem.ToString()) && !string.IsNullOrEmpty(txbExportPath.Text))
            {
                btnSent.Enabled = true;
            }
            else
            {
                btnSent.Enabled = false;
            }
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
