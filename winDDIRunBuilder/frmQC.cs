﻿using System;
using System.Reflection;
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
using OfficeOpenXml;


namespace winDDIRunBuilder
{
    public partial class frmQC : Form
    {
        public string CurUser { get; set; } = "";
        public string DeptName { get; set; } = "";
        public ClientRunBuilder CurRunBuilder { get; set; }
        public bool IsSchedCompletedSamples { get; set; } = false;
        public string PlateId { get; set; } = "";
        public DBPlate CurDBPlate { get; set; } = new DBPlate();
        private List<PlateSample> PlateSamples = new List<PlateSample>();
        private List<OutputPlateSample> HisQCSamples { get; set; }
        public string CurExportPath { get; set; }
        private List<ExportFile> Exports { get; set; }

        private string pPlateSizeX = "";
        private string pPlateSizeY = "";
        public bool IsRotated { get; set; } = false;
        private string pAlpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private List<string> WellXs { get; set; }
        private List<string> WellYs { get; set; }

        private bool pHasQC = false;

        private bool pFrmLoaded = false;

        public frmQC()
        {
            InitializeComponent();
        }

        private void frmQC_Load(object sender, EventArgs e)
        {
            string assay = "";
            List<QCSample> DeptQCPlates = new List<QCSample>();
            List<string> assays = new List<string>();
            RepoSQL sqlService = new RepoSQL();
            List<DBPlate> dbPlates = new List<DBPlate>();
            HisQCSamples = new List<OutputPlateSample>();

            try
            {
                //Testing
                //PlateId = "eBeta230228AH";
                //

                //Gen5
                //Luminex GA - MAP
                //Luminex GPP
                //Aus
                //ABI750
                //Kashi.CSV
                //Manual


                Exports = new List<ExportFile>();
                if (!string.IsNullOrEmpty(PlateId))
                {
                    dbPlates = sqlService.GetPlates(PlateId).OrderByDescending(p => p.PlateVersion).ToList();
                    CurDBPlate = dbPlates.FirstOrDefault();
                    txbPlateId.Text = PlateId;
                    txbExportPath.Text = CurExportPath;

                    //Set expoprts
                    cbExportFormat.Items.Add("");
                    Exports = sqlService.GetExportFiles(range: "ACTIVE");
                    cbExportFormat.Items.AddRange(Exports.Select(s => s.Name).Cast<string>().ToArray());

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

                IsRotated = CurDBPlate.PlateRotated;

                SetWellXY(sizeX: "A1", sizeY: CurDBPlate.SizeEndWell, isRotated: CurDBPlate.PlateRotated);

                //Get DBTests
                DeptQCPlates = sqlService.GetQCSamples(plateName: "", dept: DeptName, qcType: "DEPT");
                if (DeptQCPlates != null && DeptQCPlates.Count > 0)
                {
                    assays = DeptQCPlates.Select(p => p.Plate).Distinct().ToList();
                }
                //assay = ConfigurationManager.AppSettings["Assay"];
                //assays = assay.Split(',').ToList();

                //Add DBTest to Control
                cmbAssay.Items.AddRange(assays.ToArray());

                cmbAssay.SelectedItem = CurDBPlate.PlateName;

                cbExportFormat.SelectedIndex = 0;

                pFrmLoaded = true;
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
                string qcPlate = "";
                List<QCSample> qcSamples = new List<QCSample>();
                MapPlateSamples mapPlateSamples = new MapPlateSamples();
                DataTable dtPlateSamples = new DataTable();

                if(string.IsNullOrEmpty(txbPlateId.Text.Trim()))
                {
                    PlateId = "";
                }
                else if (txbPlateId.Text.Trim() != PlateId)
                {
                    PlateId = txbPlateId.Text.Trim();
                }

                if(cmbAssay.SelectedItem != null)
                {
                    qcPlate = cmbAssay.SelectedItem.ToString();
                }

                // (07/05/2024)-directly buid report
                if (!string.IsNullOrEmpty(PlateId) )
                {
                    mapPlateSamples.GetMapAnyPlateSamples(PlateId);
                    dtPlateSamples = mapPlateSamples.PlateSampleMapTable;
                    PlateSamples = mapPlateSamples.ScannedDBPlateSamples;

                    MapSamples(dtPlateSamples);
                    lastSampleWell = mapPlateSamples.LastSampleWell;
                }

                // (07/05/2024)-directly buid report
                if (!string.IsNullOrEmpty(PlateId) && !string.IsNullOrEmpty(qcPlate))
                {
                    qcSamples = sqlService.GetQCSamples(qcPlate);
                    LoadQCSamples(qcSamples, lastSampleWell);
                }

                // (07/05/2024)
                ////if (!string.IsNullOrEmpty(PlateId) && !string.IsNullOrEmpty(qcPlate))
                ////{
                ////    mapPlateSamples.GetMapAnyPlateSamples(PlateId);
                ////    dtPlateSamples = mapPlateSamples.PlateSampleMapTable;
                ////    PlateSamples = mapPlateSamples.ScannedDBPlateSamples;

                ////    MapSamples(dtPlateSamples);
                ////    lastSampleWell = mapPlateSamples.LastSampleWell;

                ////    qcSamples = sqlService.GetQCSamples(qcPlate);
                ////    LoadQCSamples(qcSamples, lastSampleWell);
                ////}

                if (pHasQC)
                {
                    btnAddQC.Enabled = false;
                }
                else
                {
                    btnAddQC.Enabled = true;
                }

                //Testing
                //
                //btnAddQC.Enabled = true;
                //
                //

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
                        dgvQCSamples.Rows[iRw].Cells["QCType"].Value = "SYS";

                        if (qs.Well.ToUpper() == "END")
                        {
                            if (CurDBPlate.PlateRotated)
                            {
                                if (pHasQC)
                                {
                                    wellX = lastSampleWell.Substring(0, 1);
                                    wellY = lastSampleWell.Substring(1);
                                    //wellY = ((char)((int)Char.Parse(lastSampleWell.Substring(1)))).ToString();
                                }
                                else if (!pHasQC)
                                {
                                    if ((Char.Parse(lastSampleWell.Substring(0, 1)) <= Char.Parse(pPlateSizeY)) &&
                                    (Convert.ToInt32(lastSampleWell.Substring(1)) < Convert.ToInt32(pPlateSizeX)))
                                    {
                                        wellX = lastSampleWell.Substring(0, 1);
                                        wellY = (Convert.ToInt32(lastSampleWell.Substring(1)) + 1).ToString();
                                    }
                                    else
                                    {
                                        if (Convert.ToInt32(lastSampleWell.Substring(1)) == Convert.ToInt32(pPlateSizeY))
                                        {
                                            wellX = (Char.Parse(lastSampleWell.Substring(0, 1)) + 1).ToString();
                                            wellY = "1";
                                        }
                                    }

                                    ////if ((Char.Parse(lastSampleWell.Substring(0, 1)) <= Char.Parse(pPlateSizeX)) &&
                                    ////(Convert.ToInt32(lastSampleWell.Substring(1)) < Convert.ToInt32(pPlateSizeY)))
                                    ////{
                                    ////    wellX = lastSampleWell.Substring(0, 1);
                                    ////    //wellY = (Convert.ToInt32(lastSampleWell.Substring(1)) + 1).ToString();
                                    ////    wellY = ((char)((int)Char.Parse(lastSampleWell.Substring(1)) + 1)).ToString();
                                    ////}
                                    ////else
                                    ////{
                                    ////    if (Convert.ToInt32(lastSampleWell.Substring(1)) == Convert.ToInt32(pPlateSizeY))
                                    ////    {
                                    ////        wellX = (Char.Parse(lastSampleWell.Substring(0, 1)) + 1).ToString();
                                    ////        wellY = "1";
                                    ////    }
                                    ////}
                                }

                            }
                            else if (CurDBPlate.PlateRotated == false)
                            {
                                if (pHasQC)
                                {
                                    wellX = lastSampleWell.Substring(1);
                                    wellY = ((char)((int)Char.Parse(lastSampleWell.Substring(0, 1)))).ToString();
                                }
                                else if (!pHasQC)
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
                        }
                        else
                        {
                            if (CurDBPlate.PlateRotated)
                            {
                                wellY = qs.Well.Substring(1);
                                wellX = qs.Well.Substring(0, 1);
                            }
                            else
                            {
                                wellX = qs.Well.Substring(1);
                                wellY = qs.Well.Substring(0, 1);
                            }
                        }

                        //WellX setting
                        cbCell = new DataGridViewComboBoxCell();
                        cbCell = (DataGridViewComboBoxCell)dgvQCSamples.Rows[iRw].Cells["WellX"];
                        cbCell.Items.AddRange(WellXs.ToArray());
                        dgvQCSamples.Rows[iRw].Cells["WellX"].Value = wellX;

                        //WellY setting
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
            string qcType = "";
            int iRw = 0;

            try
            {
                foreach (DataGridViewRow dr in dgvQCSamples.Rows)
                {
                    if (Convert.ToBoolean(dr.Cells["Include"].Value))
                    {
                        qcType = dr.Cells["QCType"].Value.ToString();

                        // sample = dr.Cells["Sample"].Value.ToString() + "_QC";
                        //sample = dr.Cells["Prefix"].Value.ToString() + "_QC";

                        if (qcType == "SYS")
                        {
                            sample = "QC_" + dr.Cells["HarvestId"].Value.ToString();
                            sample = sample.Substring(0, 6) + "\n" + sample.Substring(6);
                        }
                        else
                        {
                            sample = "QC_" + dr.Cells["Sample"].Value.ToString();
                            sample = sample.Substring(0, 6) + "\n" + sample.Substring(6);
                        }

                        wellX = dr.Cells["WellX"].Value.ToString();
                        wellY = dr.Cells["WellY"].Value.ToString();

                        if (!string.IsNullOrEmpty(wellX) && !string.IsNullOrEmpty(wellY))
                        {
                            if (IsRotated)
                            {
                                iRw = Convert.ToInt32(wellY) - 1;
                                dgvSamplePlate.Rows[iRw].Cells[wellX].Value = sample;
                                //iRw = pAlpha.IndexOf(wellX);
                                //dgvSamplePlate.Rows[iRw].Cells[wellY].Value = sample;
                            }
                            else
                            {
                                iRw = pAlpha.IndexOf(wellY);
                                dgvSamplePlate.Rows[iRw].Cells[wellX].Value = sample;
                            }
                            dgvSamplePlate.Rows[iRw].Cells[wellX].Style.BackColor = Color.Yellow;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = "btnAddQC_Click() met some issues:";
                errMsg += ex.Message;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = errMsg;
            }
        }

        private void MapSamples(DataTable dtPlateSamples)
        {
            string curWell = "";
            string smpItem = "";

            HisQCSamples = new List<OutputPlateSample>();

            try
            {
                dgvSamplePlate.Rows.Clear();
                dgvSamplePlate.Columns.Clear();
                dgvSamplePlate.RowsDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 7);

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

                            smpItem = tRw[tCol].ToString().Trim();

                            if (smpItem.IndexOf("QC") >= 0)
                            {
                                HisQCSamples.Add(new OutputPlateSample
                                {
                                    SampleId = smpItem.Replace("QC_", ""),
                                    Status = "HISQC"
                                });
                            }

                            if (smpItem.Length > 6)
                            {
                                smpItem = smpItem.Substring(0, 6) + "\n" + smpItem.Substring(6);
                                //smpItem = smpItem.Substring(0, 10) + Environment.NewLine + smpItem.Substring(10);
                            }

                            if (IsRotated)
                            {
                                //Rotated
                                if (tCol == dgvSamplePlate.ColumnCount - 1)
                                {
                                    dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Value = smpItem;
                                    //dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Value = tRw[tCol];
                                }
                                else
                                {
                                    dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Value = smpItem;
                                    //dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Value = tRw[tCol];

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
                                    dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Value = smpItem;
                                    //dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Value = tRw[tCol];
                                }
                                else
                                {
                                    dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Value = smpItem;
                                    //dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Value = tRw[tCol];

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

                            //Set sample color
                            if (dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Value.ToString().Length > 2)
                            {
                                if (dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Value.ToString().IndexOf("QC") >= 0)
                                {
                                    dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Style.BackColor = Color.Yellow;
                                }
                                else if (dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Value.ToString().IndexOf("REJ") >= 0)
                                {
                                    dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Style.BackColor = Color.HotPink;
                                }
                                else
                                {
                                    dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Style.BackColor = Color.LightGray;
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = "MapSamples() met errors: ";
                errMsg += ex.Message;
                lblMsg.Text = errMsg;
            }

        }

        private void dgvSamplePlate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            string curCellValue = "";
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
                        //remove newline
                        curCellValue = senderGrid.CurrentCell.Value.ToString();
                        curCellValue = curCellValue.Replace("\n", "").Replace("\r", "");

                        senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        lblMsg.ForeColor = Color.DarkBlue;
                        lblMsg.Text = "Tube Sample: " + curCellValue + "; [ " + senderGrid.CurrentCell.Tag + " ]";
                        //lblMsg.Text = "Tube Sample: " + senderGrid.CurrentCell.Value + "; [ " + senderGrid.CurrentCell.Tag + " ]";
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

            dgvQCSamples.Rows[dgvQCSamples.Rows.Count - 1].Cells["QCType"].Value = "TEMP";
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
                    WellYs.Add(intY.ToString());
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
                curTime = curDateTime.Hour.ToString("0#") + curDateTime.Minute.ToString("0#") + curDateTime.Second.ToString("##");

                if (!string.IsNullOrEmpty(cbExportFormat.SelectedItem.ToString()) && !string.IsNullOrEmpty(txbExportPath.Text))
                {
                    if (txbExportPath.Text.Trim().LastIndexOf("\\") == txbExportPath.Text.Trim().Length - 1)
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

                    //Collect plateSamples for export
                    rawQCSamples = MakePlateQCSamples();

                    //Collect QCSamples for upinsert
                    qcSamples = MakeQCSamples();

                    if (HisQCSamples != null && HisQCSamples.Count > 0)
                    {
                        foreach (var hisQC in HisQCSamples)
                        {
                            qcSamples.RemoveAll(qc => qc.SampleId == hisQC.SampleId);
                        }
                    }


                    //Add QC samples
                    if (qcSamples != null && qcSamples.Count > 0)
                        resultSaveSample = sqlService.AddSamples(qcSamples, Environment.UserName);
                    //resultSaveSample = sqlService.AddPlateQCSamples(rawQCSamples, Environment.UserName);


                    if (pHasQC == false)
                    {
                        //Update Plate Status
                        if (resultSaveSample == "SUCCESS")
                        {
                            resultUpdPlate = sqlService.UpdatePlateQC(CurDBPlate.PlateId, CurDBPlate.PlateVersion, hasQC: true, Environment.UserName);
                        }
                    }

                    //Sent QC Samples
                    //if (WriteExport(CurDBPlate.PlateId, rawQCSamples, exportFile) == "SUCCESS")
                    if (CreateExport(CurDBPlate.PlateId, rawQCSamples, exportFile) == "SUCCESS")
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
                                    sample = sample.Replace("\n", "").Replace("\r", "");

                                    if (sample.IndexOf("QC") >= 0)
                                    {
                                        pltSmp.SampleId = sample.Replace("QC_", "");
                                        //pltSmp.SampleId = sample.Remove(sample.IndexOf("_QC"));
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
                                    sample = sample.Replace("\n", "").Replace("\r", "");

                                    if (sample.IndexOf("QC") >= 0)
                                    {
                                        pltSmp.SampleId = sample.Replace("QC_", "");
                                        //pltSmp.SampleId = sample.Remove(sample.IndexOf("_QC"));
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
                var inSample = new Dictionary<string, string>();
                inSample.Add("Sample", CurDBPlate.Sample);
                inSample.Add("Diluent", CurDBPlate.Diluent);
                inSample.Add("Opt1", CurDBPlate.Opt1);
                inSample.Add("Opt2", CurDBPlate.Opt2);
                inSample.Add("Opt3", CurDBPlate.Opt3);
                inSample.Add("Opt4", CurDBPlate.Opt4);
                inSample.Add("Opt5", CurDBPlate.Opt5);

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
                                    sample = sample.Replace("\n", "").Replace("\r", "");

                                    if (sample.IndexOf("QC") >= 0)
                                    {
                                        pltSmp.SampleId = sample.Replace("QC_", "");
                                        //pltSmp.SampleId = sample.Remove(sample.IndexOf("QC_"));
                                        pltSmp.SampleType = "QC";
                                    }
                                    else
                                    {
                                        pltSmp.SampleId = sample;
                                        pltSmp.SampleType = "";
                                    }
                                    pltSmp.Well = well;

                                    pltSmp.SourcePlateId = "";
                                    pltSmp.SourceWell = "";
                                    var sourceSmp = PlateSamples.Where(s => s.SampleId == sample).FirstOrDefault();
                                    if (sourceSmp != null)
                                    {
                                        pltSmp.SourcePlateId = sourceSmp.SourcePlateId;
                                        pltSmp.SourceWell = sourceSmp.SourceWell;
                                    }

                                    pltSmp.Attributes = inSample;
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
                                    sample = sample.Replace("\n", "").Replace("\r", "");

                                    if (sample.IndexOf("QC") >= 0)
                                    {
                                        pltSmp.SampleId = sample.Replace("QC_", "");
                                        //pltSmp.SampleId = sample.Remove(sample.IndexOf("QC_"));
                                        pltSmp.SampleType = "QC";
                                    }
                                    else
                                    {
                                        pltSmp.SampleId = sample;
                                        pltSmp.SampleType = "";
                                    }
                                    pltSmp.Well = well;

                                    pltSmp.SourcePlateId = "";
                                    pltSmp.SourceWell = "";
                                    var sourceSmp = PlateSamples.Where(s => s.SampleId == sample).FirstOrDefault();
                                    if (sourceSmp != null)
                                    {
                                        pltSmp.SourcePlateId = sourceSmp.SourcePlateId;
                                        pltSmp.SourceWell = sourceSmp.SourceWell;
                                    }

                                    pltSmp.Attributes = inSample;
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
            if (cbExportFormat.SelectedIndex >= 0 && !string.IsNullOrEmpty(cbExportFormat.SelectedItem.ToString()) && !string.IsNullOrEmpty(txbExportPath.Text))
            {
                btnSent.Enabled = true;
            }
            else
            {
                btnSent.Enabled = false;
            }
        }

        public string CreateExport(string plateId, List<PlateSample> plateSamples, string exportPath)
        {
            string actionResult = "NA";
            string exportArchivePath = "";

            string itemValue = "";
            string curValue = "";

            string fileHeader = "SampleID, Well, SampleType ";
            List<string> fileItems = new List<string>();
            ExportFile export = new ExportFile();
            bool isFirstItem;
            bool isSampleField = true;
            bool addPlateId = false;

            try
            {
                export = Exports.FirstOrDefault(exp => exp.Name == cbExportFormat.SelectedItem.ToString());
                if (!string.IsNullOrEmpty(export.Field0))
                    fileItems.Add(export.Field0);
                if (!string.IsNullOrEmpty(export.Field1))
                    fileItems.Add(export.Field1);
                if (!string.IsNullOrEmpty(export.Field2))
                    fileItems.Add(export.Field2);
                if (!string.IsNullOrEmpty(export.Field3))
                    fileItems.Add(export.Field3);
                if (!string.IsNullOrEmpty(export.Field4))
                    fileItems.Add(export.Field4);
                if (!string.IsNullOrEmpty(export.Field5))
                    fileItems.Add(export.Field5);
                if (!string.IsNullOrEmpty(export.Field6))
                    fileItems.Add(export.Field6);
                if (!string.IsNullOrEmpty(export.Field7))
                    fileItems.Add(export.Field7);
                if (!string.IsNullOrEmpty(export.Field8))
                    fileItems.Add(export.Field8);
                if (!string.IsNullOrEmpty(export.Field9))
                    fileItems.Add(export.Field9);

                if (export.WithHeader)
                {
                    isFirstItem = true;
                    foreach (var itm in fileItems)
                    {
                        if (isFirstItem)
                        {
                            fileHeader = itm;
                            isFirstItem = false;
                        }
                        else
                        {
                            fileHeader += "," + itm;
                        }
                    }
                }

                //Current plate info
                Type tP = CurDBPlate.GetType();
                PropertyInfo[] propsP = tP.GetProperties();

                using (var writer = new StreamWriter(exportPath))
                {
                    if (export.WithHeader)
                        writer.WriteLine(fileHeader);


                    foreach (var item in plateSamples)
                    {
                        isFirstItem = true;

                        if (string.IsNullOrEmpty(item.SampleType) == false && item.SampleType.IndexOf("QC") >= 0)
                        {
                            addPlateId = true;
                        }
                        else
                        {
                            addPlateId = false;
                        }

                        foreach (var head in fileItems)
                        {
                            isSampleField = false;
                            Type t = item.GetType();
                            PropertyInfo[] props = t.GetProperties();

                            //Sample Field(s) processing
                            foreach (var prop in props)
                            {
                                if (prop.Name.ToUpper() == head.ToUpper())
                                {
                                    isSampleField = true;
                                    curValue = prop.GetValue(item) == null ? "" : prop.GetValue(item).ToString();

                                    if (addPlateId)
                                        curValue = curValue + "_" + plateId;

                                    if (isFirstItem)
                                    {
                                        itemValue += curValue;
                                        isFirstItem = false;
                                    }
                                    else
                                    {
                                        itemValue += "," + curValue;
                                    }
                                    break;
                                }
                            }

                            //Plate Field(s) processing
                            if (isSampleField == false)
                            {
                                foreach (var propP in propsP)
                                {
                                    if (propP.Name.ToUpper() == head.ToUpper())
                                    {
                                        curValue = propP.GetValue(CurDBPlate) == null ? "" : propP.GetValue(CurDBPlate).ToString();

                                        if (isFirstItem)
                                        {
                                            itemValue += curValue;
                                            isFirstItem = false;
                                        }
                                        else
                                        {
                                            itemValue += "," + curValue;
                                        }
                                        break;
                                    }
                                }

                            }
                        }

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
                string errMsg = "{CreateExport()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                actionResult = "ERROR:" + errMsg;
            }

            return actionResult;
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
                        itemValue = item.SampleId;
                        //itemValue = item.SampleId.Replace("-", "") + ",";
                        //itemValue += item.Well + ",";
                        //itemValue += item.SampleType + "";

                        if (!string.IsNullOrEmpty(extValue))
                            itemValue += "," + extValue;

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


        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnBuildReports_Click(object sender, EventArgs e)
        {
            string resultSaveSample = "";
            string resultUpdPlate = "";

            List<PlateSample> rawQCSamples = new List<PlateSample>();
            List<OutputPlateSample> qcSamples = new List<OutputPlateSample>();
            RepoSQL sqlService = new RepoSQL();

            bool isDirectReport = false;

            try
            {
                //(07/05/2024) dirctly biuld report
                if (dgvSamplePlate !=null && dgvSamplePlate.Rows.Count > 2)
                {
                    //Collect QCSamples for upinsert
                    qcSamples = MakeQCSamples();

                    if (qcSamples == null || qcSamples.Count == 0)
                    {
                        isDirectReport = true;
                    }
                    else
                    {
                        isDirectReport = false;
                    }
                }
                else
                {
                    lblMsg.ForeColor = Color.Red;
                    lblMsg.Text = "There is no sample.";

                    return;
                }


                //(07/05/2024) dirctly biuld report
                //if(txbPlateId.Text.Trim().Length <5 || cmbAssay.SelectedItem.ToString().Length < 2)
                //{
                //    lblMsg.ForeColor = Color.Red;
                //    lblMsg.Text = "There is no plate or Assay.";

                //    return;
                //}
                //if (dgvSamplePlate ==null || dgvSamplePlate.Rows.Count <= 2)
                //{
                //    lblMsg.ForeColor = Color.Red;
                //    lblMsg.Text = "There is no sample.";

                //    return;
                //}


                //Collect plateSamples for export       //
                //rawQCSamples = MakePlateQCSamples();  //


                //(07/05/2024) dirctly biuld report
                ////Collect QCSamples for upinsert
                //qcSamples = MakeQCSamples();

                if (!isDirectReport)
                {
                    if (HisQCSamples != null && HisQCSamples.Count > 0)
                    {
                        foreach (var hisQC in HisQCSamples)
                        {
                            qcSamples.RemoveAll(qc => qc.SampleId == hisQC.SampleId);
                        }
                    }

                    //Add QC samples
                    if (qcSamples != null && qcSamples.Count > 0)
                        resultSaveSample = sqlService.AddSamples(qcSamples, Environment.UserName);
                    //resultSaveSample = sqlService.AddPlateQCSamples(rawQCSamples, Environment.UserName);


                    if (pHasQC == false)
                    {
                        //Update Plate Status
                        if (resultSaveSample == "SUCCESS")
                        {
                            resultUpdPlate = sqlService.UpdatePlateQC(CurDBPlate.PlateId, CurDBPlate.PlateVersion, hasQC: true, Environment.UserName);
                        }
                    }
                }   //End-if(!isDirectReport)


                //Build report
                if (BuildReport() == "SUCCESS")
                {
                    lblMsg.Text = "The Following samples report created.";
                    lblMsg.ForeColor = Color.DarkBlue;
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

        private string BuildReport()
        {
            string resutlt = "NA";

            List<PlateSample> rawQCSamples = new List<PlateSample>();
            int sheetRow = 1;
            int smpModels = 0;
            var sourceFilePath = string.Empty;
            var destFilePath = string.Empty;

            List<string[]> headerRow = new List<string[]>();
            List<string[]> sampleRow = new List<string[]>();

            bool gotoBuildRpt = false;

            try
            {
                smpModels = typeof(PlateSample).GetProperties().Count();

                //Collect plateSamples for export
                rawQCSamples = MakePlateQCSamples();
                if (CurDBPlate is null || rawQCSamples is null || rawQCSamples.Count <= 0)
                {
                    lblMsg.ForeColor = Color.Red;
                    lblMsg.Text = "Cannot get samples.";
                    return "No Sample";
                }

                var worklistformat = CurDBPlate.WorklistFormat
                                         .Replace("DestPlateId", "PlateId").Replace("DestWellId", "Well").Replace("SourceWellId", "SourceWell")
                                         .Split(',').Select(x => x.Split(':')).ToDictionary(x => x[0], y => y.Length > 1 ? y[1] : y[0]);
 
                headerRow.Add(worklistformat.Keys.ToArray());

                //Get dest file
                destFilePath = CurRunBuilder.BuilReportPlace;
                if (Environment.UserName.ToUpper() == "SLI")
                {
                    destFilePath = "C:/Users/sli/Documents/DDIRunBuilder/HisFile/";
                }

                destFilePath += CurDBPlate.PlateName + "_" + CurDBPlate.PlateId + ".xlsm";
               

                var fileContent = string.Empty;

                //Get source file
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = CurRunBuilder.BuilReportTemplate;
                    openFileDialog.Filter = "xlsm files (*.xlsm)|*.xlsm|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        sourceFilePath = openFileDialog.FileName;
                        File.Copy(sourceFilePath, destFilePath, true);
                        gotoBuildRpt = true;
                    }
                }

                if (gotoBuildRpt)
                {
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    using (ExcelPackage package = new ExcelPackage(new FileInfo(destFilePath)))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets["Dump"];

                        worksheet.Cells[sheetRow, 1].LoadFromArrays(headerRow);

                        sheetRow += 1;
                        foreach (var smp in rawQCSamples)
                        {
                            sampleRow = new List<string[]>();

                            var sampleAttrs = worklistformat.Select(x =>
                            {
                                var sampleAttributes = new Dictionary<string, string>(smp.Attributes, StringComparer.OrdinalIgnoreCase);

                                var prop = smp.GetType().GetProperty(x.Value, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                                if (prop != null)
                                {
                                    return prop.GetValue(smp).ToString();
                                }
                                else if (sampleAttributes.TryGetValue(x.Key, out var attr2))
                                {
                                    return attr2;
                                }
                                else
                                {
                                    return "";
                                }
                            }).ToArray();

                            sampleRow.Add(sampleAttrs);

                            worksheet.Cells[sheetRow, 1].LoadFromArrays(sampleRow);
                            sheetRow += 1;
                        }

                        package.Save();

                        //open the file
                        FileInfo fi = new FileInfo(destFilePath);
                        if (fi.Exists)
                        {
                            System.Diagnostics.Process.Start(destFilePath);
                        }
                        else
                        {
                            lblMsg.ForeColor = Color.Red;
                            lblMsg.Text = "There is not this file: " + destFilePath;

                            return "Cannot find the file.";
                        }

                        resutlt = "SUCCESS";
                    }
                }   //End-if(gotoBuildRpt)
            }
            catch (Exception ex)
            {
                string errMsg = "{BuildReports()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = errMsg;

                resutlt = "ERROR";
            }

            return resutlt;
        }
        
    }


}

