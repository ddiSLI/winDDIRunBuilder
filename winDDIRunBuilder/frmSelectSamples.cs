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
    public partial class frmSelectSamples : Form
    {
        public ValidPlate CurPlate { get; set; }
        public List<PlateSample> OriginalPlateSamples { get; set; }
        private string pAlpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public frmSelectSamples()
        {
            InitializeComponent();
        }

        private void frmSelectSamples_Load(object sender, EventArgs e)
        {
            InitialPlateSamples();
        }

        private void InitialPlateSamples()
        {
            List<OutputPlateSample> outSamples = new List<OutputPlateSample>();

            try
            {
                txbBarcode.Text = CurPlate.PlateId;

                if (OriginalPlateSamples != null && OriginalPlateSamples.Count > 0)
                {
                    //format translate
                    foreach (var smp in OriginalPlateSamples)
                    {
                        outSamples.Add(new OutputPlateSample
                        {
                            DestPlateId = smp.PlateId,
                            DestWellId = smp.Well,
                            SampleId = smp.SampleId,
                            Status = smp.Status,
                            SampleType = smp.SampleType
                        });
                    }

                    //map sampes
                    if (CurPlate.Rotated)
                    {
                        //Rotated
                        MappingPlateSamplesRotated(CurPlate, outSamples);
                    }
                    else
                    {
                        MappingPlateSamples(CurPlate, outSamples);
                    }


                }
            }
            catch (Exception ex)
            {
                string errMsg = "frmSelectSamples.InitialPlateSamples() met some issues:";
                errMsg += Environment.NewLine + ex.Message;
                lblMsg.Text = errMsg;
            }

        }

        private void txbBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadPlateSamples(txbBarcode.Text.Trim());
                InitialPlateSamples();
            }

        }

        private void LoadPlateSamples(string plateId)
        {
            //Directly get plate and samples from DB
            CurPlate = new ValidPlate();
            OriginalPlateSamples = new List<PlateSample>();

            RepoSQL sqlService = new RepoSQL();
            ModelTransfer tranService = new ModelTransfer();

            try
            {
                
                if (! string.IsNullOrEmpty(plateId))
                {
                    List<DBPlate> sourcePlates = new List<DBPlate>();
                    DBPlate origialPlate = new DBPlate();
                    DBPlate lastPlate = new DBPlate();

                    sourcePlates = (List<DBPlate>)sqlService.GetPlates(plateId).OrderBy(se => se.PlateVersion).ToList();
                    if (sourcePlates != null && sourcePlates.Count > 0)
                    {
                        origialPlate = sourcePlates.FirstOrDefault();
                        lastPlate = sourcePlates.LastOrDefault();

                        //Get Plate Samples
                       
                        OriginalPlateSamples = sqlService.GetPlateSamples(plateId);

                        //remove X2
                        OriginalPlateSamples.Where(s => s.SampleId.IndexOf("X") > 0)
                                            .ToList()
                                            .ForEach(s => s.SampleId = s.SampleId.Substring(0, s.SampleId.IndexOf("X")));
                        
                        CurPlate = tranService.DBPlate2ValidPlate(origialPlate, "SOURCE");
                        
                        txbBarcode.Text = CurPlate.PlateId;

                    }
                    else
                    {
                        lblMsg.Text = "The Source Plate must be existing. You entered an UN-Existing plate.";
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = "frmSelectSamples.LoadPlateSamples() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }
        }



        private void MappingPlateSamples(ValidPlate outPlate, List<OutputPlateSample> plateSamples)
        {
            string issues = "";
            ModelTransfer trans = new ModelTransfer();

            try
            {
                Char sizeStartY = char.Parse(outPlate.SizeStartWell[0].ToString());
                int sizeStartX = Convert.ToInt32(outPlate.SizeStartWell.Substring(1));
                Char sizeEndY = char.Parse(outPlate.SizeEndWell[0].ToString());
                int sizeEndX = Convert.ToInt32(outPlate.SizeEndWell.Substring(1));

                Char posStartY = char.Parse(outPlate.StartWell[0].ToString());
                int posStartX = Convert.ToInt32(outPlate.StartWell.Substring(1));
                char posEndY = char.Parse(outPlate.EndWell[0].ToString());
                int posEndX = Convert.ToInt32(outPlate.EndWell.Substring(1));

                DataTable dtPlateSamples = new DataTable();
                string destWellId = "";
                int wellX = 0;
                int wellY = 0;

                //adding columns, plateSizeX  
                for (int i = 0; i <= sizeEndX; i++)
                {
                    dtPlateSamples.Columns.Add(i.ToString());
                }

                //adding rows, plateSizeY
                DataRow drPlate = dtPlateSamples.NewRow();
                for (Char chr = 'A'; chr <= sizeEndY; chr++)
                {
                    drPlate = dtPlateSamples.NewRow();
                    drPlate[0] = chr;
                    dtPlateSamples.Rows.Add(drPlate);
                }

                foreach (var smp in plateSamples)
                {
                    destWellId = trans.GetWell(smp.DestWellId, isBCR: false);
                    wellX = Convert.ToInt32(destWellId.Substring(1));
                    wellY = pAlpha.IndexOf(destWellId.Substring(0, 1));

                    if (wellX <= sizeEndX && wellY < sizeEndY)
                    {
                        if (string.IsNullOrEmpty(smp.Status) && string.IsNullOrEmpty(smp.SampleType))
                        {
                            dtPlateSamples.Rows[wellY][wellX] = smp.SampleId;
                        }
                        else if (!string.IsNullOrEmpty(smp.Status) && smp.Status == "REJECT")
                        {
                            dtPlateSamples.Rows[wellY][wellX] = "REJ_" + smp.SampleId;
                        }
                        else if (smp.SampleType.IndexOf("QC") >= 0)
                        {
                            dtPlateSamples.Rows[wellY][wellX] = "QC_" + smp.SampleId;
                        }
                    }
                    else
                    {
                        issues += "Well is Over PlateSize";
                        issues += Environment.NewLine;
                    }

                }

                MapSamples(dtPlateSamples);

            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
            }
        }

        private void MappingPlateSamplesRotated(ValidPlate outPlate, List<OutputPlateSample> plateSamples)
        {
            //Plate rotated
            string issues = "";
            Char sizeStartY = char.Parse(outPlate.SizeStartWell[0].ToString());
            int sizeStartX = Convert.ToInt32(outPlate.SizeStartWell.Substring(1));
            Char sizeEndY = char.Parse(outPlate.SizeEndWell[0].ToString());
            int sizeEndX = Convert.ToInt32(outPlate.SizeEndWell.Substring(1));

            int RotateSizeX = pAlpha.IndexOf(sizeEndY);   //H-A
            int RotateSizeY = sizeEndX;                   //1-12

            string destWellId = "";
            int wellX = 0;
            int wellY = 0;
            int maxColId = 0;
            int wellColSatrtId = 0;
            DataTable dtPlateSamples = new DataTable();
            ModelTransfer trans = new ModelTransfer();

            try
            {
                //adding columns, plateSizeX  
                for (int i = RotateSizeX; i >= 0; i--)
                {
                    dtPlateSamples.Columns.Add(pAlpha.Substring(i, 1));
                }
                dtPlateSamples.Columns.Add("0");
                wellColSatrtId = dtPlateSamples.Columns.Count - 2;
                maxColId = dtPlateSamples.Columns.Count - 1;

                //adding rows, plateSizeY
                DataRow drPlate = dtPlateSamples.NewRow();
                for (int j = 1; j <= RotateSizeY; j++)
                {
                    drPlate = dtPlateSamples.NewRow();
                    drPlate[maxColId] = j.ToString();
                    dtPlateSamples.Rows.Add(drPlate);
                }

                //Mapping samples
                foreach (var smp in plateSamples)
                {
                    destWellId = trans.GetWell(smp.DestWellId, isBCR: false);
                    wellX = Convert.ToInt32(destWellId.Substring(1));
                    wellY = pAlpha.IndexOf(destWellId.Substring(0, 1));

                    if (wellX <= RotateSizeY && wellY <= RotateSizeX)
                    {
                        if (string.IsNullOrEmpty(smp.Status) && string.IsNullOrEmpty(smp.SampleType))
                        {
                            dtPlateSamples.Rows[wellX - 1][wellColSatrtId - wellY] = smp.SampleId;
                        }
                        else if (!string.IsNullOrEmpty(smp.Status) && smp.Status == "REJECT")
                        {
                            dtPlateSamples.Rows[wellX - 1][wellColSatrtId - wellY] = "REJ_" + smp.SampleId;
                        }
                        else if (smp.SampleType.IndexOf("QC") >= 0)
                        {
                            dtPlateSamples.Rows[wellX - 1][wellColSatrtId - wellY] = "QC_" + smp.SampleId;
                        }

                        //
                        //dtPlateSamples.Rows[wellX - 1][wellColSatrtId - wellY] = smp.SampleId;
                        //dtPlateSamples.Rows[RotateSizeX - wellY][wellX] = smp.SampleId;
                    }
                    else
                    {
                        issues += "Well is Over PlateSize";
                        issues += Environment.NewLine;
                    }
                }

                MapSamples(dtPlateSamples);

            }
            catch (Exception ex)
            {
                string errMsg = "{MappingPlateSamplesRotated()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }
        }

        private void MapSamples(DataTable dtPlateSamples, bool isRotated = false)
        {
            string curWell = "";
            string smpItem = "";

            try
            {
                dgvSamplePlate.Rows.Clear();
                dgvSamplePlate.Columns.Clear();
                dgvSamplePlate.Refresh();

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
                            smpItem = tRw[tCol].ToString().Trim();
                            if (smpItem.Length > 6)
                            {
                                smpItem = smpItem.Substring(0, 6) + "\n" + smpItem.Substring(6);
                                //smpItem = smpItem.Substring(0, 10) + Environment.NewLine + smpItem.Substring(10);
                            }

                            if (isRotated)
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

        private void dgvPlateSamples_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            //string sourcePlate = "";
            //string destPlate = "";
            string curCellValue = "";
            string newCellValue = "";

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

                        curCellValue = senderGrid.CurrentCell.Value.ToString();
                        curCellValue = curCellValue.Replace("\n", "").Replace("\r", "");
                        curCellValue = curCellValue.Replace("REJ_", "");
                        //curCellValue = curCellValue.Replace("*", "");

                        if (curCellValue.IndexOf("*") > 0)
                        {
                            senderGrid.CurrentCell.Style.BackColor = Color.LightGray;
                            newCellValue = curCellValue.Replace("*", "");
                            senderGrid.CurrentCell.Value = curCellValue.Replace("*", "");
                        }
                        else
                        {
                            senderGrid.CurrentCell.Style.BackColor = Color.DarkGreen;
                            newCellValue = "*" + curCellValue;
                            senderGrid.CurrentCell.Value = "*" + senderGrid.CurrentCell.Value;
                        }

                        OriginalPlateSamples.Where(s => s.SampleId == curCellValue).ToList().ForEach(smp => smp.SampleId = newCellValue);
                        //OriginalPlateSamples.Where(s => s.PlateId == curCellValue).ToList().ForEach(smp => smp.PlateId = newCellValue);



                    }
                }

            }
            catch (Exception ex)
            {
                string errMsg = "dgvPlateSamples_CellContentClick met errors: ";
                errMsg += ex.Message;
                lblMsg.Text = errMsg;

            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {

            string cellValue = "";

            try
            {
                if (dgvSamplePlate != null && dgvSamplePlate.Rows.Count > 0)
                {
                    OriginalPlateSamples.ForEach(s => s.SampleId = "*" + s.SampleId.Replace("*", ""));

                    foreach (DataGridViewRow dr in dgvSamplePlate.Rows)
                    {
                        foreach (DataGridViewColumn dc in dgvSamplePlate.Columns)
                        {
                            if (dr.Cells[dc.Index].Value != null && dr.Cells[dc.Index].Value.ToString().Length > 2)
                            {
                                cellValue = dr.Cells[dc.Index].Value.ToString().Replace("*", "");
                                dr.Cells[dc.Index].Value = "*" + cellValue;
                                dr.Cells[dc.Index].Style.BackColor = Color.DarkGreen;
                            }
                        }
                    }

                    dgvSamplePlate.Refresh();
                }
            }
            catch (Exception ex)
            {
                string errMsg = "btnSelectAll_Click() met errors: ";
                errMsg += ex.Message;
                lblMsg.Text = errMsg;
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSamplePlate != null && dgvSamplePlate.Rows.Count > 0)
                {
                    OriginalPlateSamples.ForEach(s => s.SampleId = s.SampleId.Replace("*", ""));

                    foreach (DataGridViewRow dr in dgvSamplePlate.Rows)
                    {
                        foreach (DataGridViewColumn dc in dgvSamplePlate.Columns)
                        {
                            if (dr.Cells[dc.Index].Value != null && dr.Cells[dc.Index].Value.ToString().Length > 2)
                            {
                                dr.Cells[dc.Index].Value = dr.Cells[dc.Index].Value.ToString().Replace("*", "");
                                dr.Cells[dc.Index].Style.BackColor = Color.LightGray;
                            }
                        }
                    }

                    dgvSamplePlate.Refresh();
                }
            }
            catch (Exception ex)
            {
                string errMsg = "btnClearAll_Click() met errors: ";
                errMsg += ex.Message;
                lblMsg.Text = errMsg;
            }

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            bool closeThisForm = true;

            try
            {
                frmMsg msgBox = new frmMsg();

                int selectedSamples = OriginalPlateSamples.Count(s => s.SampleId.IndexOf("*") >= 0);

                if (selectedSamples <= 0)
                {
                    msgBox.MyMsg = "Warning: There are no samples selected.";
                    msgBox.MyMsg += Environment.NewLine;
                    msgBox.MyMsg += Environment.NewLine;
                    msgBox.MyMsg += "Do you want to process it?";
                    msgBox.ShowDialog();
                    if (msgBox.DialogResult == DialogResult.No)
                    {
                        closeThisForm = false;
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = "SelectSamples.Go() met errors: ";
                errMsg += ex.Message;
                lblMsg.Text = errMsg;

            }
            finally
            {
                if (closeThisForm == true)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }


    }
}
