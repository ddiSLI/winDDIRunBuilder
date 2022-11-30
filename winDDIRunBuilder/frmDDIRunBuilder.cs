using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using winDDIRunBuilder.Models;


namespace winDDIRunBuilder
{
    public partial class frmDDIRunBuilder : Form
    {
        private ClientRunBuilder pRunBuilder;
        public List<InputFile> InputFileValues { get; set; }
        public ClientRunBuilder CurRunBuilder = new ClientRunBuilder();

        private List<Batch> BatchSamples = new List<Batch>();

        public List<ProtocolPlate> ProtocolPlates { get; set; } = new List<ProtocolPlate>();

        private List<Protocol> pProtocol = new List<Protocol>();
        private List<Plate> PlateInputs { get; set; }
        private List<Plate> GetBatchPlates { get; set; }
        private List<Plate> PostPlates { get; set; }

        private string pAlpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private List<string> PlatePages = new List<string>();
        private string pSelectedPlatePage = "";

        private bool pIsRotated = false;
        private bool pIsFrmLoaded = false;
        
        public frmDDIRunBuilder()
        {
            InitializeComponent();
        }

        private void frmDDIRunBuilder_Load(object sender, EventArgs e)
        {
            pRunBuilder = new ClientRunBuilder();
            RepoSQLOracle repoService = new RepoSQLOracle();

            try
            {
                CurRunBuilder = new ClientRunBuilder();
                InputFileValues = new List<InputFile>();
                PlateInputs = new List<Plate>();
                GetBatchPlates = new List<Plate>();
                PostPlates = new List<Plate>();

                //setup fileWatcherPlate
                fileWatcherBCR.Path = pRunBuilder.ReadFilePath;
                fileWatcherBCR.Filter = "*.CSV";
                //

                pProtocol = repoService.GetProtocols();
                if (pProtocol.Count > 0)
                {
                    //var newObj = prots.Select(s => new { Guid = s.Name.PadRight(10), Value = s.PlateWidth.Trim() + "," + s.PlateLength.Trim() + "," + s.Well.Trim() + "," + s.Categoty.Trim() });
                    var newObj = pProtocol.Select(s => new { Guid = s.Name, Value = s.Name }).Distinct();

                    cbProtocolCd.DataSource = newObj.ToList();
                    cbProtocolCd.DisplayMember = "Guid";
                    cbProtocolCd.ValueMember = "Value";

                    cbProtocolCd.SelectedItem = null;
                    cbProtocolCd.SelectedText = "-Select a protocol-";
                }
                else
                {
                    cbProtocolCd.Text = "There is NO Proto Code";
                }

               // btnPrint.Enabled = false;
                pIsFrmLoaded = true;
               
            }
            catch (Exception ex)
            {
                string errMsg = "{frmMain_Load} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }
            finally
            {
                txbBarcode.Focus();
            }
        }

        private void LoadProtocolPlates()
        {
            string[] rwPlate;
            string included = "true";
            string destPlate = "";
            string sourcePlate = "";
            string SourcePlateIsNew = "true";
            string desc = "nothing happened";
            string worklist = "";

            try
            {
                dgvPlateSet.Rows.Clear();
                InputFileValues = new List<InputFile>();

                if (ProtocolPlates.Count > 0)
                {
                    int plateOrder = 1;
                    foreach (var pp in ProtocolPlates)
                    {
                        included = "true";
                        destPlate = pp.PlateId;
                        sourcePlate = pp.SourcePlateId;
                        SourcePlateIsNew = "true";
                        desc = "Checking SoucePlates";
                        worklist = "";
                        rwPlate = new string[] { included, destPlate, sourcePlate, SourcePlateIsNew, desc, worklist };
                        dgvPlateSet.Rows.Add(rwPlate);

                        plateOrder += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = "frmPlateSetting_Load() met some issues:";
                errMsg = Environment.NewLine + ex.Message;
            }
        }

        private void cbProtocolCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrintBarCode prtPlatecode = new PrintBarCode();
            cbPlates.Items.Clear();
            cbPlates.Text = "";

            try
            {
                if (pIsFrmLoaded && cbProtocolCd.SelectedValue != null && cbProtocolCd.SelectedValue.ToString().Length > 0)
                {
                    PlateInputs = new List<Plate>();

                    string curProtName = cbProtocolCd.SelectedValue.ToString();
                    var curProtPlates = pProtocol.Where(p => p.Name == curProtName).ToList();

                    ProtocolPlates = new List<ProtocolPlate>();
                    PlateInputs = new List<Plate>();

                    if (curProtPlates.Count > 0)
                    {
                        //set plate selection
                        cbPlates.SelectedItem = null;
                        cbPlates.SelectedText = "-Select a plate-";

                        //get each plate info
                        foreach (var pp in curProtPlates)
                        {
                            cbPlates.Items.Add(pp.PlateId);

                            ProtocolPlates.Add(new ProtocolPlate
                            {
                                Id = pp.Id,
                                ProtocolName = pp.Name,
                                PlateId = pp.PlateId,
                                SourcePlateId = pp.SourcePlate,
                                WorklistName = pp.PlateId + ".CSV",
                                StartPos = pp.StartPos,
                                EndPos = pp.EndPos
                            }); ;
                        }
                    }

                    LoadProtocolPlates();

                    if(dgvPlateSet !=null && dgvPlateSet.RowCount > 0)
                    {
                        foreach(DataGridViewRow grw in dgvPlateSet.Rows)
                        {
                          //  prtPlatecode.Print(grw.Cells[1].Value.ToString());
                        }
                    }


                    lblPrompt.Text = "Please scan the samples";

                    //set default plate
                    pSelectedPlatePage = (string)cbPlates.SelectedItem;
                }
            }
            catch (Exception ex)
            {
                string errMsg = "{cbProtocolCd_SelectedIndexChanged()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;

                lblPrompt.ForeColor = Color.Red;
                lblPrompt.Text = errMsg;
            }
            finally
            {
                txbBarcode.Focus();
            }
        }

        private void dgvPlateSet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                string errMsg = "{dgvPlateSet_CellContentClick} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }
            finally
            {
                txbBarcode.Focus();
            }
        }

        private void cbPlates_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (pIsFrmLoaded && cbProtocolCd.SelectedValue != null && cbProtocolCd.SelectedValue.ToString().Length > 0 &&
            //    cbPlates.SelectedItem != null)
            //    {
            //        pSelectedPlatePage = cbPlates.SelectedItem.ToString();

            //        dgvSamplePlate.Rows.Clear();
            //        dgvSamplePlate.Columns.Clear();

            //        if (InputFileValues.Count > 0)
            //        {
            //            int findPlate = InputFileValues.Count(p => p.PlateId.ToUpper() == pSelectedPlatePage.ToUpper());
            //            if (findPlate > 0)
            //            {
            //                lblMsg.ForeColor = Color.Navy;
            //                lblMsg.Text = "The plate, " + pSelectedPlatePage + " , has following sample(s).";
            //                SetupPlate();
            //            }
            //            else
            //            {
            //                lblMsg.ForeColor = Color.Red;
            //                lblMsg.Text = "The plate, " + pSelectedPlatePage + " , does not have sample(s).";
            //            }
            //        }
            //        else
            //        {
            //            lblMsg.ForeColor = Color.Red;
            //            lblMsg.Text = "Worklist is not ready.";
            //        }


            //        if (dgvInputSource.RowCount <= 0 || string.IsNullOrEmpty((string)cbProtocolCd.SelectedValue))
            //        {
            //            lblMsg.ForeColor = Color.Red;
            //            lblMsg.Text = "Please first Import File and select a Protocol !";
            //        }
            //        else
            //        {
            //            if (dgvSamplePlate != null)
            //            {
            //                dgvSamplePlate.Rows.Clear();
            //                dgvSamplePlate.Columns.Clear();
            //            }

            //            if (InputFileValues.Count > 0)
            //            {
            //                int findPlate = InputFileValues.Count(p => p.PlateId.ToUpper() == pSelectedPlatePage.ToUpper());
            //                if (findPlate > 0)
            //                {
            //                    lblMsg.ForeColor = Color.Navy;
            //                    lblMsg.Text = "The plate, " + pSelectedPlatePage + " , has following sample(s).";
            //                    SetupPlate();
            //                }
            //                else
            //                {
            //                    lblMsg.ForeColor = Color.Red;
            //                    lblMsg.Text = "The plate, " + pSelectedPlatePage + " , does not have sample(s).";
            //                }

            //            }
            //        }
            //    }

            //    bool ready2CreateNew = true;
            //    if (dgvWorklist != null && dgvWorklist.Rows.Count > 0)
            //    {
            //        foreach (DataGridViewRow wlRw in dgvWorklist.Rows)
            //        {
            //            if (wlRw.Cells["wlReady2Go"].Value != null && wlRw.Cells["wlReady2Go"].Value.ToString() != "YES")
            //            {
            //                ready2CreateNew = false;
            //            }
            //        }

            //        if (ready2CreateNew)
            //            btnNew.Enabled = true;
            //    }

            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
            //    txbBarcode.Focus();
            //}
        }

        private void SetupPlate()
        {
            string protocolId = "";
            string protocolName = "";
            string startPos = "";
            string endPos = "";
            bool isRotated = false;

            if (ProtocolPlates != null && ProtocolPlates.Count > 0)
            {
                var plateSet = ProtocolPlates.Where(p => p.PlateId == pSelectedPlatePage).FirstOrDefault();
                if (plateSet != null)
                {
                    protocolId = plateSet.Id;
                    protocolName = plateSet.ProtocolName;
                    isRotated = plateSet.PlateRotated;
                    startPos = plateSet.StartPos;
                    endPos = plateSet.EndPos;
                }

                SetupInputPlates(protocolId, protocolName, startPos, endPos);

                if (LoadPlateSamples(startPos, endPos, isRotated) == "SUCCESS")
                {
                    lblMsg.ForeColor = Color.DarkBlue;
                    lblMsg.Text = "The plate," + pSelectedPlatePage + ", is ready to go.";
                }
            }
        }

        private void SetupInputPlates(string protocolId, string protocolName, string startPos, string endPos)
        {
            int startY = pAlpha.IndexOf(startPos.Substring(0, 1)) + 1;
            int startX = Convert.ToInt32(startPos.Substring(1));
            int sizeY = pAlpha.IndexOf(endPos.Substring(0, 1)) + 1;
            int sizeX = Convert.ToInt32(endPos.Substring(1));

            //Clean current plate loaded samples
            if (PlateInputs != null && PlateInputs.Count > 0)
                PlateInputs.RemoveAll(pl => pl.PlateId == pSelectedPlatePage);

            for (int eRw = 1; eRw <= startY; eRw++)
            {
                //empty Whole rows
                if (eRw < startY)
                {
                    for (int x = 1; x <= sizeX; x++)
                    {
                        PlateInputs.Add(new Plate
                        {
                            Sequence = 0,
                            SampleId = "",
                            ShortId = "",
                            ProtocolId = protocolId,
                            ProtocolName = protocolName,
                            PlateId = pSelectedPlatePage
                        });
                    }
                }
                else if (eRw == startY)
                {
                    //empty Cells
                    for (int x = 1; x < startX; x++)
                    {
                        PlateInputs.Add(new Plate
                        {
                            Sequence = 0,
                            SampleId = "",
                            ShortId = "",
                            ProtocolId = protocolId,
                            ProtocolName = protocolName,
                            PlateId = pSelectedPlatePage
                        });
                    }
                }
            }

            if (InputFileValues.Count > 0)
            {
                foreach (var inp in InputFileValues)
                {
                    if (inp.PlateId.ToUpper() == pSelectedPlatePage.ToUpper())
                    {
                        PlateInputs.Add(new Plate
                        {
                            Sequence = Convert.ToInt32(inp.Position),
                            SampleId = inp.FullSampleId,
                            ShortId = inp.ShortId,
                            ProtocolId = protocolId,
                            ProtocolName = protocolName,
                            PlateId = pSelectedPlatePage
                        });
                    }

                }
            }
        }

        private string LoadPlateSamples(string startPos, string endPos, bool isRotated = false)
        {
            string actionResult = "NA";
            //DataTable dtPlateSamples = new DataTable();

            //int startY = pAlpha.IndexOf(startPos.Substring(0, 1)) + 1;
            //int startX = Convert.ToInt32(startPos.Substring(1));
            //int plateSizeY = pAlpha.IndexOf(endPos.Substring(0, 1)) + 1;
            //int plateSizeX = Convert.ToInt32(endPos.Substring(1));

            //try
            //{

            //    if (dgvSamplePlate != null)
            //    {
            //        dgvSamplePlate.Rows.Clear();
            //        dgvSamplePlate.Columns.Clear();
            //    }


            //    if (PlateInputs != null && PlateInputs.Count > 0)
            //    {
            //        dgvSamplePlate.RowTemplate.Height = 40;

            //        //test    
            //        isRotated = true;
            //        //

            //        pIsRotated = isRotated;
            //        if (isRotated)
            //        {
            //            //dtPlateSamples = Transpose(PlateSamples(plateSizeX, plateSizeY));
            //            dtPlateSamples = Rotated90(PlateSamples(plateSizeX, plateSizeY));

            //        }
            //        else
            //        {
            //            dtPlateSamples = PlateSamples(plateSizeX, plateSizeY);
            //        }

            //        foreach (DataColumn col in dtPlateSamples.Columns)
            //        {
            //            if (col.ColumnName == "0")
            //            {
            //                DataGridViewTextBoxColumn dgvCol = new DataGridViewTextBoxColumn();
            //                dgvCol.Name = col.ColumnName;
            //                dgvCol.Width = 28;
            //                dgvSamplePlate.Columns.Add(dgvCol);
            //            }
            //            else
            //            {
            //                DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
            //                dgvBtn.Name = col.ColumnName;
            //                dgvBtn.Width = 60;
            //                dgvSamplePlate.Columns.Add(dgvBtn);
            //            }
            //        }

            //        foreach (DataRow tRw in dtPlateSamples.Rows)
            //        {
            //            dgvSamplePlate.Rows.Add();
            //            for (int tCol = 0; tCol < dtPlateSamples.Columns.Count; tCol++)
            //            {
            //                if (tCol == 0)
            //                {
            //                    dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Value = tRw[tCol];
            //                }
            //                else
            //                {
            //                    dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Value = tRw[tCol];


            //                    var sampleName = InputFileValues.Where(smp => smp.PlateId.ToUpper() == pSelectedPlatePage.ToUpper() &&
            //                                                             smp.ShortId == tRw[tCol].ToString()).Select(smp => smp.FullSampleId).FirstOrDefault();
            //                    if (sampleName == null || string.IsNullOrEmpty(sampleName.ToString()))
            //                    {
            //                        dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Tag = "empty sample";
            //                    }
            //                    else
            //                    {
            //                        dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Tag = sampleName.ToString();
            //                    }

            //                }
            //            }
            //        }

            //        if (LoadPostPlates(dtPlateSamples, isRotated) == "DONE")
            //        {
            //            foreach (DataGridViewRow grw in dgvWorklist.Rows)
            //            {
            //                if (grw.Cells["wlPlateId"].Value.ToString().ToUpper() == pSelectedPlatePage.ToUpper())
            //                {
            //                    grw.Cells["wlReady2Go"].Value = "YES";
            //                    grw.DefaultCellStyle.BackColor = Color.LightGreen;
            //                }
            //            }
            //            actionResult = "SUCCESS";
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    string err = ex.Message;
            //    actionResult = "ERROR";
            //}

            return actionResult;
        }

        private DataTable Transpose(DataTable dtOriginal)
        {
            DataTable dtNew = new DataTable();

            try
            {
                dtNew.Columns.Add("OriField");

                for (int i = 0; i < dtOriginal.Rows.Count; i++)
                {
                    dtNew.Columns.Add();
                }

                for (int i = 0; i < dtOriginal.Columns.Count; i++)
                {
                    DataRow rwNew = dtNew.NewRow();
                    rwNew[0] = dtOriginal.Columns[i].Caption;

                    for (int j = 0; j < dtOriginal.Rows.Count; j++)
                    {
                        rwNew[j + 1] = dtOriginal.Rows[j][i];
                    }

                    dtNew.Rows.Add(rwNew);
                }

                //Orgnize
                for (int i = 0; i < dtNew.Columns.Count; i++)
                {
                    if (i == 0)
                    {
                        dtNew.Columns[i].ColumnName = "0";
                    }
                    else
                    {
                        dtNew.Columns[i].ColumnName = pAlpha.Substring(i - 1, 1);
                    }
                }
                if (dtNew.Rows.Count > 0)
                {
                    dtNew.Rows.RemoveAt(0);
                }
                //

            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;

            }
            return dtNew;
        }

        private DataTable Rotated90(DataTable dtOriginal)
        {
            DataTable dtNew = new DataTable();

            try
            {
                for (int i = 0; i < dtOriginal.Rows.Count; i++)
                {
                    dtNew.Columns.Add();
                }

                dtNew.Columns.Add("OriField");

                for (int i = 0; i < dtOriginal.Columns.Count; i++)
                {
                    DataRow rwNew = dtNew.NewRow();
                    rwNew[dtNew.Columns.Count - 1] = dtOriginal.Columns[i].Caption;

                    int op = dtOriginal.Rows.Count;
                    for (int j = 0; j < dtOriginal.Rows.Count; j++)
                    {
                        rwNew[op - 1] = dtOriginal.Rows[j][i];
                        op = op - 1;
                    }

                    dtNew.Rows.Add(rwNew);
                }

                //Orgnize
                for (int i = dtNew.Columns.Count - 1; i >= 0; i--)
                {
                    //if (i == 0)
                    if (i == dtNew.Columns.Count - 1)
                    {
                        dtNew.Columns[i].ColumnName = "0";
                    }
                    else
                    {
                        dtNew.Columns[i].ColumnName = pAlpha.Substring(dtNew.Columns.Count - i - 2, 1);
                    }
                }

                if (dtNew.Rows.Count > 0)
                {
                    dtNew.Rows.RemoveAt(0);
                }
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;

            }
            return dtNew;
        }

        private void dgvSamplePlate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
            finally
            {
                txbBarcode.Focus();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintBarCode printPlateBarcode = new PrintBarCode();
               // printPlateBarcode.Print("Siga111012022");
                
            }
            catch (Exception ex)
            {

            }
            finally
            {
                txbBarcode.Focus();
            }
        }

        

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
            finally
            {
                txbBarcode.Focus();
            }
        }

        private void fileWatcherBCR_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                string filePath = e.FullPath;
                string fileName = e.Name;

                if (Directory.GetFiles(CurRunBuilder.ReadFilePath, "*.CSV").Length > 0)
                {
                    lblMsg.Text = "The BCR SampleFile.csv is created. The program is processing the file....";
                }
            }
            catch (Exception ex)
            {
                string errMsg = "{fileWatcherBCR_Created} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }
            finally
            {
                txbBarcode.Focus();
            }


        }

        private void btnExit_Click(object sender, EventArgs e)
        {

        }


    }
}
