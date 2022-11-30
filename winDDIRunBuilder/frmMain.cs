using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using winDDIRunBuilder.Models;
using static winDDIRunBuilder.ClientBackend;

namespace winDDIRunBuilder
{
    public partial class frmMain : Form
    {
        public ClientRunBuilder CurRunBuilder { get; set; }
        public List<InputFile> InputFileValues { get; set; }
        private List<Batch> BatchSamples = new List<Batch>();
        public List<ProtocolPlate> ProtocolPlates { get; set; } = new List<ProtocolPlate>();

        private List<Protocol> pProtocol = new List<Protocol>();
        private List<Plate> PlateInputs { get; set; }
        private List<Plate> GetBatchPlates { get; set; }
        private List<Plate> PostPlates { get; set; }

        private string pAlpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private List<string> PlatePages = new List<string>();
        private string pSelectedPlatePage = "";

        private String CurScannedPlateBarcode { get; set; } = "";
        private bool pIsRotated = false;

        private bool pIsFrmLoaded = false;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            CurRunBuilder = new ClientRunBuilder();
            RepoSQLOracle repoService = new RepoSQLOracle();
            repoService.CurRunBuilder = CurRunBuilder;

            try
            {
                InputFileValues = new List<InputFile>();
                PlateInputs = new List<Plate>();
                GetBatchPlates = new List<Plate>();
                PostPlates = new List<Plate>();

                //setup fileWatcherPlate
                fileWatcherBCR.Path = CurRunBuilder.ReadFilePath;
                fileWatcherBCR.Filter = "*.CSV";
                //

                pProtocol = repoService.GetProtocols();
                if (pProtocol.Count > 0)
                {
                    //var newObj = prots.Select(s => new { Guid = s.Name.PadRight(10), Value = s.PlateWidth.Trim() + "," + s.PlateLength.Trim() + "," + s.Well.Trim() + "," + s.Categoty.Trim() });
                    var newObj = pProtocol.Select(s => new { Guid = s.ProtocolName, Value = s.ProtocolName }).Distinct();

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

                pIsFrmLoaded = true;

                lblPrompt.Text = "Please first select a protocol.";
                //CheckSourceFile();
            }
            catch (Exception ex)
            {
                string errMsg = "{frmMain_Load} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }
        }

        private void cbProtocolCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            frmPlateSetting newPlates = new frmPlateSetting();
            cbPlates.Items.Clear();
            cbPlates.Text = "";
            string[] platePos;

            try
            {
                if (pIsFrmLoaded && cbProtocolCd.SelectedValue != null && cbProtocolCd.SelectedValue.ToString().Length > 0)
                {
                    PlateInputs = new List<Plate>();

                    string curProtName = cbProtocolCd.SelectedValue.ToString();
                    var curProtPlates = pProtocol.Where(p => p.ProtocolName == curProtName).ToList();

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
                            //platePos = new string[];
                            platePos = PlateProperties(pp.StartPos, pp.EndPos);
                            ProtocolPlates.Add(new ProtocolPlate
                            {
                                Id = pp.Id,
                                ProtocolName = pp.ProtocolName,
                                PlateId = pp.PlateId,
                                SourcePlateId = pp.SourcePlate,
                                WorklistName = pp.PlateId + ".CSV",
                                StartPos = pp.StartPos,
                                EndPos = pp.EndPos,
                                Accept = pp.DBTest,
                                PlateRotated = pp.PlateRotated,
                                Offset = platePos[0],
                                StartX = platePos[1],
                                StartY = platePos[2],
                                EndX = platePos[3],
                                EndY = platePos[4],

                            });
                        }
                    }

                    SetProtocolPlates();

                    //set default plate
                    pSelectedPlatePage = (string)cbPlates.SelectedItem;

                    //BCRFilesChecking();
                    //PlatesChecking();

                    lblPrompt.ForeColor = Color.DarkBlue;
                    lblPrompt.Text = "The avaliable protocol selected. Please make sure the available plates";
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

        private void SetProtocolPlates()
        {
            string included = "true";
            string destPlate = "";
            string sourcePlate = "";
            string SourcePlateIsNew = "true";
            string destPlateIsNew = "true";
            string desc = "nothing happened";
            string worklist = "";
            string[] rwPlate;

            dgvPlateSet.Rows.Clear();

            if (ProtocolPlates != null && ProtocolPlates.Count > 0)
            {
                int plateOrder = 1;
                foreach (var pp in ProtocolPlates)
                {
                    included = "true";
                    destPlate = pp.PlateId;
                    sourcePlate = pp.SourcePlateId;
                    SourcePlateIsNew = "true";
                    destPlateIsNew = "true";
                    desc = "Checking SoucePlates";
                    worklist = "";

                    // rwPlate = new string[] { included,desc, worklist, SourcePlateIsNew, sourcePlate, destPlate };

                    dgvPlateSet.Rows.Add();
                    dgvPlateSet.Rows[dgvPlateSet.RowCount - 1].Cells[0].Value = included;
                    dgvPlateSet.Rows[dgvPlateSet.RowCount - 1].Cells[1].Value = desc;
                    dgvPlateSet.Rows[dgvPlateSet.RowCount - 1].Cells[2].Value = worklist;
                    dgvPlateSet.Rows[dgvPlateSet.RowCount - 1].Cells[3].Value = SourcePlateIsNew;
                    dgvPlateSet.Rows[dgvPlateSet.RowCount - 1].Cells[4].Value = sourcePlate;
                    dgvPlateSet.Rows[dgvPlateSet.RowCount - 1].Cells[5].Value = destPlateIsNew;
                    dgvPlateSet.Rows[dgvPlateSet.RowCount - 1].Cells[6].Value = destPlate;
                    dgvPlateSet.Rows[dgvPlateSet.RowCount - 1].Cells[6].Style.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);


                    ////    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    ////    // edit: to change the background color:
                    ////    //e.CellStyle.SelectionBackColor = Color.Coral;

                    //dgvPlateSet.Rows.Add(rwPlate);

                    plateOrder += 1;
                }

            }

            dgvPlateSet.Refresh();

        }

        private void SetupInputPlates(string protocolId, string protocolName, string startPos, string endPos)
        {
            int countId = 0;

            try
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
                    //Horizontal: if (eRw < startY)
                    if (eRw < startX)
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
                    else if (eRw == startX)     //Horizontal: if (eRw == startY)
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
                        //test
                        //if (countId == 185)
                        //{
                        //    string xxx = "";
                        //}


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

                            countId++;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
            }
        }

        private string LoadPlateSamples(string startPos, string endPos, bool isRotated = false)
        {
            string actionResult = "NA";
            DataTable dtPlateSamples = new DataTable();

            int startY = pAlpha.IndexOf(startPos.Substring(0, 1)) + 1;
            int startX = Convert.ToInt32(startPos.Substring(1));

            int plateSizeY = pAlpha.IndexOf(endPos.Substring(0, 1)) + 1;
            int plateSizeX = Convert.ToInt32(endPos.Substring(1));

            try
            {

                if (dgvSamplePlate != null)
                {
                    dgvSamplePlate.Rows.Clear();
                    dgvSamplePlate.Columns.Clear();
                }


                if (PlateInputs != null && PlateInputs.Count > 0)
                {
                    dgvSamplePlate.RowTemplate.Height = 40;

                    //test    
                    //isRotated = true;
                    //

                    pIsRotated = isRotated;
                    if (isRotated)
                    {
                        //dtPlateSamples = Transpose(PlateSamples(plateSizeX, plateSizeY));
                        dtPlateSamples = Rotated90(PlateSamples(plateSizeX, plateSizeY));

                    }
                    else
                    {
                        //dtPlateSamples = PlateSamples(plateSizeX, plateSizeY);
                        dtPlateSamples = PlateSamplesVertical(plateSizeX, plateSizeY);
                    }

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
                            dgvBtn.Width = 60;
                            dgvSamplePlate.Columns.Add(dgvBtn);
                        }
                    }

                    foreach (DataRow tRw in dtPlateSamples.Rows)
                    {
                        dgvSamplePlate.Rows.Add();
                        for (int tCol = 0; tCol < dtPlateSamples.Columns.Count; tCol++)
                        {
                            if (tCol == 0)
                            {
                                dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Value = tRw[tCol];
                            }
                            else
                            {
                                dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Value = tRw[tCol];


                                var sampleName = InputFileValues.Where(smp => smp.PlateId.ToUpper() == pSelectedPlatePage.ToUpper() &&
                                                                         smp.ShortId == tRw[tCol].ToString()).Select(smp => smp.FullSampleId).FirstOrDefault();
                                if (sampleName == null || string.IsNullOrEmpty(sampleName.ToString()))
                                {
                                    dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Tag = "empty sample";
                                }
                                else
                                {
                                    dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Tag = sampleName.ToString();
                                }

                            }
                        }
                    }

                    //setup PostPlates 
                    if (LoadPostPlates(dtPlateSamples, isRotated) == "DONE")
                    {
                        actionResult = "SUCCESS";
                    }

                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                actionResult = "ERROR";
            }

            return actionResult;
        }

        private string LoadPostPlates(DataTable dtPlateSamples, bool isRotated = false)
        {
            string actionResult = "NA";
            string inpSeq = "";
            string inpSampleId = "";
            string inpPlateId = "";
            string wellX = "";
            string wellY = "";

            if (dgvSamplePlate != null && dgvSamplePlate.Rows.Count > 0)
            {
                inpPlateId = pSelectedPlatePage.ToUpper();

                PostPlates.RemoveAll(pp => pp.PlateId.ToUpper() == inpPlateId);
                foreach (DataRow drw in dtPlateSamples.Rows)
                {
                    for (int iCol = 1; iCol < dtPlateSamples.Columns.Count; iCol++)
                    {
                        var inpSample = InputFileValues.Where(p => p.PlateId.ToUpper() == inpPlateId && p.ShortId == drw[iCol].ToString()).FirstOrDefault();
                        if (inpSample != null)
                        {
                            inpSeq = inpSample.Position;
                            inpSampleId = inpSample.FullSampleId;

                            if (isRotated)
                            {
                                wellY = dtPlateSamples.Columns[iCol].ColumnName;
                                wellX = drw[0].ToString();
                            }
                            else
                            {
                                wellX = dtPlateSamples.Columns[iCol].ColumnName;
                                wellY = drw[0].ToString();
                            }

                            PostPlates.Add(new Plate
                            {
                                Sequence = Convert.ToInt32(inpSeq),
                                BatchId = "",
                                SampleId = inpSampleId,
                                ShortId = drw[iCol].ToString(),
                                ProtocolId = "",
                                ProtocolName = "",
                                PlateId = inpPlateId,
                                WellX = wellX,
                                WellY = wellY
                            });

                        }
                    }
                }

                actionResult = "DONE";
            }

            return actionResult;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                bool ready2Go = true;
                ResetMsg();

                if (dgvPlateSet != null && dgvPlateSet.Rows.Count > 0)
                {
                    foreach (DataGridViewRow grw in dgvPlateSet.Rows)
                    {

                        if (Convert.ToBoolean(grw.Cells["Included"].Value) && string.IsNullOrEmpty(grw.Cells["CurWorkList"].Value.ToString()))
                            ready2Go = false;
                    }

                    if (ready2Go)
                    {
                        if (CreateWorklist() == "SUCCESS")
                        {
                            lblMsg.ForeColor = Color.ForestGreen;
                            lblMsg.Text = "The Worklist(s) created.";
                        }
                        else
                        {
                            lblMsg.ForeColor = Color.Red;
                            lblMsg.Text = "The Worklist(s) did not create.";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = "{btnNew_Click()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = errMsg;
            }
            finally
            {
                txbBarcode.Focus();
            }
        }


        private string MapPlateSamples()
        {
            string actionResult = "SUCCESS";
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
                else
                {
                    actionResult = "ISSUE";
                }
            }

            return actionResult;
        }

        private string[] PlateProperties(string startPos, string endPos)
        {
            string[] plateProperties = { "offset", "startX", "startY", "endX", "endY" };
            int startY = 0;
            int startX = 0;
            int sizeY = 0;
            int sizeX = 0;
            int Offset = 0;

            startY = pAlpha.IndexOf(startPos.Substring(0, 1)) + 1;
            startX = Convert.ToInt32(startPos.Substring(1));
            sizeY = pAlpha.IndexOf(endPos.Substring(0, 1)) + 1;  //length
            sizeX = Convert.ToInt32(endPos.Substring(1));        //width 
            Offset = (sizeY - startY) * sizeX + (startX - 1);

            plateProperties = new string[] { Offset.ToString().Trim(),
                                            startX.ToString().Trim(),
                                            startY.ToString().Trim(),
                                            sizeX.ToString().Trim(),
                                            sizeY.ToString().Trim()};

            return plateProperties;
        }

        private string SaveBatch()
        {
            string actionResult = "";
            RepoSQLOracle repoService = new RepoSQLOracle();
            List<Batch> newBatch = new List<Batch>();

            try
            {
                string modifiedBy = Environment.UserName;
                string modifiedDate = DateTime.Now.ToString();
                string modifiedTool = "DDIRunBuildervX";
                // string version = "1";

                if (PostPlates != null && PostPlates.Count > 0)
                {
                    foreach (var pt in PlateInputs)
                    {
                        if (!string.IsNullOrEmpty(pt.ShortId))
                        {
                            newBatch.Add(new Batch
                            {
                                ShortId = pt.ShortId,
                                SampleId = pt.SampleId,
                                //PlateSuffix = batchId + "_" + pt.PlateId,
                                Sequence = pt.Sequence.ToString(),
                                Well = pt.WellX + pt.WellY,
                                //Version = version,
                                ModifiedBy = modifiedBy,
                                ModifiedDate = modifiedDate,
                                ModifiedTool = modifiedTool

                            });
                        }
                    }

                    actionResult = repoService.CreateBarch(newBatch);
                    //if (actionResult != "SUCCESS")
                    // actionResult = actionResult;
                }
                else
                {
                    actionResult = "Warning! Current plate does not have avaialble data to save!";
                }
            }
            catch (Exception ex)
            {
                string errMsg = "{SaveBatch()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                actionResult = "ERROR:" + errMsg;
            }

            return actionResult;
        }

        private string CreateWorklist()
        {
            string actionResult = "";
            string worklist = "";
            string todatDate = "";
            string sourcePlate = "";
            string destPlate = "";

            ClientBackend backService = new ClientBackend();
            string hasSourcePlate = "YES";
            string hasDestPlate = "YES";
            string hasSamples = "YES";

            string exportIssues = "";
            string exportResult = "";
            List<SamplePlate> plateSamples = new List<SamplePlate>();

            InputPlate newSourcePlate = new InputPlate();
            InputPlate newDestPlate = new InputPlate();
            List<InputSample> pltSmps = new List<InputSample>();
            SampleAttributes newSmpAttr = new SampleAttributes();
            List<DtoWorklist> dtoWorklsit = new List<DtoWorklist>();

            try
            {
                todatDate = DateTime.Today.Date.ToString("yyMMdd");

                if (dgvPlateSet.RowCount > 0)
                {
                    foreach (DataGridViewRow rwPlate in dgvPlateSet.Rows)
                    {
                        if (Convert.ToBoolean(rwPlate.Cells["Included"].Value) == true)
                        {
                            sourcePlate = rwPlate.Cells["SourcePlate"].Value.ToString();
                            destPlate = rwPlate.Cells["DestPlate"].Value.ToString();
                            worklist = rwPlate.Cells["CurWorkList"].Value.ToString();

                            newSourcePlate = new InputPlate();
                            newDestPlate = new InputPlate();
                            dtoWorklsit = new List<DtoWorklist>();

                            //create sourcePlate
                            newSourcePlate = new InputPlate();
                            //var plateSet = ProtocolPlates.Where(p => p.PlateId == sourcePlate).FirstOrDefault();
                            var plateSet = ProtocolPlates.Where(p => p.PlateId == destPlate).FirstOrDefault();
                            if (plateSet != null)
                            {
                                newSourcePlate.Name = sourcePlate + "-" + todatDate;
                                newSourcePlate.Offset = plateSet.Offset;
                                newSourcePlate.Start.X = plateSet.StartX;
                                newSourcePlate.Start.Y = plateSet.StartY;
                                newSourcePlate.End.X = plateSet.EndX;
                                newSourcePlate.End.Y = plateSet.EndY;
                                newSourcePlate.Direction = plateSet.PlateRotated == false ? "0" : "1";
                            }
                            if (sourcePlate != "BCR")
                            {
                                newSourcePlate.Attributes.Accept = plateSet.Accept;
                            }
                            hasSourcePlate = backService.CreatePlate(newSourcePlate);

                            //create destPlate
                            newDestPlate = new InputPlate();
                            newDestPlate.Name = sourcePlate + "-" + todatDate;
                            newDestPlate.Offset = plateSet.Offset;
                            newDestPlate.Start.X = plateSet.StartX;
                            newDestPlate.Start.Y = plateSet.StartY;
                            newDestPlate.End.X = plateSet.EndX;
                            newDestPlate.End.Y = plateSet.EndY;
                            newDestPlate.Direction = plateSet.PlateRotated == false ? "0" : "1";
                            newDestPlate.Attributes.Accept = plateSet.Accept;
                            hasDestPlate = backService.CreatePlate(newDestPlate);


                            //Add samples to source plate
                            if (hasSourcePlate == "YES" && hasDestPlate == "YES" && sourcePlate == "BCR")
                            {
                                var smps = InputFileValues.Where(inp => inp.PlateId == destPlate).ToList();
                                if (smps != null && smps.Count > 0)
                                {
                                    foreach (var smp in smps)
                                    {
                                        newSmpAttr = new SampleAttributes();
                                        newSmpAttr.SampleId = smp.ShortId;
                                        pltSmps.Add(new InputSample
                                        {
                                            Attributes = newSmpAttr
                                        });
                                    }
                                }

                                List<Plate> pltSamples = PostPlates.Where(smp => smp.PlateId == destPlate).ToList();
                                hasSamples = backService.AddSamples(pltSamples);

                                if (hasSamples == "YES")
                                {
                                    dtoWorklsit = (List<DtoWorklist>)backService.GetWorklist(sourcePlate, destPlate);
                                    if (dtoWorklsit != null && dtoWorklsit.Count > 0)
                                    {
                                        plateSamples = new List<SamplePlate>();
                                        foreach (var wl in dtoWorklsit)
                                        {
                                            plateSamples.Add(new SamplePlate
                                            {
                                                SampleId = destPlate,
                                                //SourceRack = smp.WellX + smp.WellY,
                                                //SourcePosition = smp.WellX + smp.WellY,
                                                //DestRack = smp.WellX + smp.WellY,
                                                //DestPosition = smp.WellX + smp.WellY,
                                                //Samples = samples.Count,
                                                Diluent = wl.Diluent
                                            });
                                        }

                                        exportResult = WriteSamplePlate(plateSamples, worklist);
                                        if (exportResult != "SUCCESS")
                                        {
                                            exportIssues += exportResult + "; ";
                                        }
                                    }
                                }
                            }

                        }
                    }
                }


                if (string.IsNullOrEmpty(exportIssues))
                {
                    actionResult = "SUCCESS";
                }
                else
                {
                    actionResult = exportIssues;
                }
            }
            catch (Exception ex)
            {
                string errMsg = "{CreateWorklist()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                actionResult = "ERROR:" + errMsg;
            }

            return actionResult;

            //var people = new List<Person>(); 
            //people.Add(new Person { Surname = "Matt", Forename = "Abbott" });
            //people.Add(new Person { Surname = "John", Forename = "Smith" });
            //WriteCSV(people, @"C:\Users\sli\Documents\Logs\people.csv");
        }

        public string WriteSamplePlate(List<SamplePlate> samplePlate, string worklistName)
        {
            string actionResult = "NA";
            string exportPath = "";
            string plateFileItems = "SampleId,SourceRack,SourcePosition,DestRack,DestPosition,Samples,Diluent";
            string itemValue = "";

            try
            {
                exportPath = CurRunBuilder.ExportFilePath + worklistName;

                using (var writer = new StreamWriter(exportPath))
                {
                    writer.WriteLine(plateFileItems);

                    foreach (var item in samplePlate)
                    {
                        itemValue += item.SampleId + ",";
                        itemValue += item.SourceRack + ",";
                        itemValue += item.SourcePosition + ",";
                        itemValue += item.DestRack + ",";
                        itemValue += item.DestPosition + ",";
                        itemValue += item.Samples + ",";
                        itemValue += item.Diluent;

                        writer.WriteLine(itemValue);
                        itemValue = "";
                    }
                }

                actionResult = "SUCCESS";
            }
            catch (Exception ex)
            {
                string errMsg = "{WriteSamplePlate()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                actionResult = "ERROR:" + errMsg;
            }

            return actionResult;
        }

        private void ResetMsg()
        {
            lblMsg.ForeColor = Color.Black;
            lblMsg.Text = "[Message]";
        }

        private void panPlate_Paint(object sender, PaintEventArgs e)
        {
            //ControlPaint.DrawBorder(e.Graphics, this.panPlate.ClientRectangle, Color.AliceBlue, ButtonBorderStyle.Solid);
        }

        private void cbPlates_SelectedIndexChanged(object sender, EventArgs e)
        {
            string todatDate = "";
            try
            {
                todatDate = DateTime.Today.Date.ToString("yyMMdd");

                if (pIsFrmLoaded && cbProtocolCd.SelectedValue != null && cbProtocolCd.SelectedValue.ToString().Length > 0 &&
                    cbPlates.SelectedItem != null)
                {
                    pSelectedPlatePage = cbPlates.SelectedItem.ToString();

                    if (InputFileValues.Count <= 0 || string.IsNullOrEmpty((string)cbProtocolCd.SelectedValue))
                    {
                        lblMsg.ForeColor = Color.Red;
                        lblMsg.Text = "Please first Import File and select a Protocol !";
                    }
                    else
                    {
                        if (dgvSamplePlate != null)
                        {
                            dgvSamplePlate.Rows.Clear();
                            dgvSamplePlate.Columns.Clear();
                        }

                        int findPlate = InputFileValues.Count(p => p.PlateId.ToUpper() == pSelectedPlatePage.ToUpper());
                        if (findPlate > 0)
                        {
                            lblMsg.ForeColor = Color.Navy;
                            lblMsg.Text = "The plate, " + pSelectedPlatePage + " , has following sample(s).";

                            if (MapPlateSamples() == "SUCCESS")
                            {
                                foreach (DataGridViewRow plt in dgvPlateSet.Rows)
                                {
                                    if (plt.Cells["DestPlate"].Value.ToString() == pSelectedPlatePage)
                                    {
                                        plt.DefaultCellStyle.BackColor = Color.LightGreen;
                                        plt.Cells["CurWorkList"].Value = pSelectedPlatePage + todatDate + ".csv";
                                    }
                                }
                            }
                        }
                        else
                        {
                            lblMsg.ForeColor = Color.Red;
                            lblMsg.Text = "The plate, " + pSelectedPlatePage + " , does not have sample(s).";
                        }

                    }
                }

                bool ready2CreateNew = true;
                if (dgvPlateSet != null && dgvPlateSet.Rows.Count > 0)
                {
                    foreach (DataGridViewRow wlRw in dgvPlateSet.Rows)
                    {
                        if (wlRw.Cells["CurWorkList"].Value != null && wlRw.Cells["CurWorkList"].Value.ToString().Length > 0)
                        {
                            ready2CreateNew = false;
                        }
                    }

                    if (ready2CreateNew)
                        btnCreateWorklist.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;

            }

        }

        private DataTable PlateSamplesVertical(int plateSizeX, int plateSizeY)
        {
            DataTable dtPlate = new DataTable();

            try
            {
                //adding columns, plateSizeX  
                for (int i = 0; i <= plateSizeX; i++)
                {
                    dtPlate.Columns.Add(i.ToString());
                }

                //adding rows, plateSizeY
                DataRow drPlate = dtPlate.NewRow();
                for (int j = 0; j <= plateSizeY - 1; j++)
                {
                    drPlate = dtPlate.NewRow();
                    drPlate[0] = pAlpha.Substring(j, 1);
                    dtPlate.Rows.Add(drPlate);
                }

                int cols = dtPlate.Columns.Count;
                int rows = dtPlate.Rows.Count;

                int col = 1;
                int row = 0;
                //DataRow drPlate = dtPlate.NewRow();
                foreach (var smp in PlateInputs)
                {
                    if (col <= cols - 1)
                    {
                        if (row <= rows - 1)
                        {
                            dtPlate.Rows[row][col] = smp.ShortId;
                            row++;
                        }
                        else
                        {
                            row = 0;
                            if (col < cols - 1)
                            {
                                col++;
                                dtPlate.Rows[row][col] = smp.ShortId;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }

                    ////for (int col =0; col<= cols-1; col++)
                    ////{
                    ////    for (int row = 0; row <= rows; row++)
                    ////    {

                    ////        if(col == 0)
                    ////        {
                    ////            dtPlate.Rows[row][col]= pAlpha.Substring(row, 1);
                    ////        }
                    ////        else
                    ////        {
                    ////            dtPlate.Rows[row][col] = smp.ShortId;
                    ////        }
                    ////    }
                    ////}

                    //////////////////////////
                    ////if (colInd <= cols - 1)
                    ////{
                    ////    drPlate[0] = pAlpha.Substring(dtPlate.Rows.Count, 1);
                    ////    drPlate[colInd] = smp.ShortId;

                    ////    if (colInd == cols - 1)
                    ////    {
                    ////        dtPlate.Rows.Add(drPlate);
                    ////        drPlate = dtPlate.NewRow();
                    ////    }
                    ////    colInd += 1;
                    ////}
                    ////else
                    ////{
                    ////    colInd = 1;
                    ////    drPlate[colInd] = smp.ShortId;
                    ////}
                }
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
            }

            //DataTable dttotate = Transpose(dtPlate);
            return dtPlate;
        }
        private DataTable PlateSamples(int plateSizeX, int plateSizeY)
        {
            DataTable dtPlate = new DataTable();

            try
            {
                //adding columns, plateSizeX  
                for (int i = 0; i <= plateSizeX; i++)
                {
                    dtPlate.Columns.Add(i.ToString());
                }

                int cols = dtPlate.Columns.Count;
                int colInd = 1;

                DataRow drPlate = dtPlate.NewRow();
                foreach (var smp in PlateInputs)
                {
                    if (smp.PlateId.ToUpper() == pSelectedPlatePage.ToUpper())
                    {
                        if (colInd <= cols - 1)
                        {
                            drPlate[0] = pAlpha.Substring(dtPlate.Rows.Count, 1);
                            drPlate[colInd] = smp.ShortId;

                            if (colInd == cols - 1)
                            {
                                dtPlate.Rows.Add(drPlate);
                                drPlate = dtPlate.NewRow();
                            }
                            colInd += 1;
                        }
                        else
                        {
                            colInd = 1;
                            drPlate[colInd] = smp.ShortId;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
            }

            //DataTable dttotate = Transpose(dtPlate);
            return dtPlate;
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

        private void dgvSamplePlate_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            //I supposed your button column is at index 0
            //if (e.ColumnIndex > 0 && string.IsNullOrEmpty(e.Value.ToString()) == false)
            if (string.IsNullOrEmpty(e.Value.ToString()) == false)
            {
                if ((pIsRotated == false && e.ColumnIndex > 0) || (pIsRotated && e.ColumnIndex < dgvSamplePlate.ColumnCount - 1))
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
                        lblMsg.Text = "Tube -" + senderGrid.CurrentCell.Value + " sample is : [ " + senderGrid.CurrentCell.Tag + " ]";

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

        private string ImportBCRFile(string importFile, string plateId)
        {
            string actionResult = "SUCCESS";
            RepoSQLOracle repoService = new RepoSQLOracle();

            try
            {
                List<InputFile> sampleValues = new List<InputFile>();
                importFile = CurRunBuilder.ReadFilePath + importFile;

                List<InputFile> rawValues = File.ReadAllLines(importFile)
                    .Skip(1)
                    .Select(v => InputFile.ReadInputFile(v))
                    .ToList();

                if (rawValues.Count > 0)
                {
                    sampleValues = repoService.GetPlateSamples(rawValues);
                    if (sampleValues.Count > 0)
                    {
                        foreach (var smp in sampleValues)
                        {
                            InputFileValues.Add(new InputFile
                            {
                                FullSampleId = smp.FullSampleId,
                                ShortId = smp.ShortId,
                                RackName = "BCR",
                                Position = smp.Position,
                                //Well = smp.Well,
                                PlateId = plateId
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                actionResult = "SYS-ERROR";
                string errMsg = "ImportBCRPlate() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }

            return actionResult;
        }

        private void PlatesChecking()
        {
            bool isReadyDBPlates = true;
            bool isReadyBCR = true;
            string sourcePlate = "";
            bool included = true;
            string worklist = "";

            foreach (DataGridViewRow rwPlate in dgvPlateSet.Rows)
            {
                sourcePlate = rwPlate.Cells["SourcePlate"].Value.ToString();
                included = Convert.ToBoolean(rwPlate.Cells["Included"].Value);
                worklist = rwPlate.Cells["CurWorkList"].Value.ToString();
                if (included && sourcePlate == "BCR" && worklist.Length <= 0)
                {
                    isReadyBCR = false;
                }
                else if (included && sourcePlate != "BCR" && worklist.Length <= 0)
                {
                    isReadyDBPlates = false;
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetMsg();
            cbProtocolCd.SelectedIndex = -1;
            cbProtocolCd.Text = "-Select-";
            dgvPlateSet.Rows.Clear();

            GetBatchPlates = new List<Plate>();
            PlateInputs = new List<Plate>();
            PostPlates = new List<Plate>();

            cbPlates.Items.Clear();
            cbPlates.Text = "";

            dgvSamplePlate.Columns.Clear();
            dgvSamplePlate.Rows.Clear();

            btnPrintPlateBarcode.Enabled = false;
            btnCreateWorklist.Enabled = false;

            txbBarcode.Focus();
        }


        private void btnGo_Click(object sender, EventArgs e)
        {
            string barcodePrinterName = "";
            string barcode = "";
            try
            {
                barcodePrinterName = GetPrinterName();
                foreach (DataGridViewRow rwPlate in dgvPlateSet.Rows)
                {
                    if (Convert.ToBoolean(rwPlate.Cells["Included"].Value))
                    {
                        barcode = rwPlate.Cells["DestPlate"].Value.ToString();
                        PrintBarCode printPlateBarcode = new PrintBarCode();
                        printPlateBarcode.Print(barcode, GetPrinterName());
                    }
                }

            }
            catch (Exception ex)
            {
                string errMsg = "btnGo_Click() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }
            finally
            {
                txbBarcode.Text = "";
                txbBarcode.Focus();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string barCode = "";
            if (dgvPlateSet.CurrentRow.Cells["DestPlate"].Value != null)
            {
                barCode = dgvPlateSet.CurrentRow.Cells["DestPlate"].Value.ToString();
                PrintBarCode printPlateBarcode = new PrintBarCode();
                printPlateBarcode.Print("Siga111012022", GetPrinterName());
            }

            txbBarcode.Text = "";
            txbBarcode.Focus();
        }

        private void txbBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (!string.IsNullOrEmpty(txbBarcode.Text.Trim()))
                    {
                        CurScannedPlateBarcode = txbBarcode.Text.Trim().ToUpper();

                        lblPrompt.ForeColor = Color.DarkBlue;
                        lblPrompt.Text = "The plate, " + CurScannedPlateBarcode + " , scanned";

                        ////foreach (DataGridViewRow bcrPlt in dgvPlateSet.Rows)
                        ////{
                        ////    if (bcrPlt.Cells["SourcePlate"].Value.ToString() == "BCR")
                        ////    {
                        ////        if (bcrPlt.Cells["DestPlate"].Value.ToString().ToUpper() == scannedBar)
                        ////            bcrPlt.Cells["ScannedPlate"].Value = scannedBar;
                        ////    }
                        ////}

                        ////BCRFilesChecking();
                        ////PlatesChecking();
                    }

                }
                catch (Exception ex)
                {
                    string errMsg = "{txbBarcode_KeyDown} met the following error: ";
                    errMsg += Environment.NewLine;
                    errMsg += ex.Message;
                }
                finally
                {
                    txbBarcode.Text = "";
                    txbBarcode.Focus();
                }

            }
        }

        private void fileWatcherBCR_Created(object sender, FileSystemEventArgs e)
        {
            BCRFilesChecking();

            txbBarcode.Text = "";
            txbBarcode.Focus();
        }

        private void fileWatcherBCR_Changed(object sender, FileSystemEventArgs e)
        {
            txbBarcode.Text = "";
            txbBarcode.Focus();
        }


        private void dgvPlateSet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            string sourcePlate = "";
            string destPlate = "";

            try
            {
                if (e.RowIndex >= 0)
                {
                    bool included = Convert.ToBoolean(senderGrid.CurrentRow.Cells["Included"].Value);
                    bool isNewSourcePlate = Convert.ToBoolean(senderGrid.CurrentRow.Cells["SourcePlateIsNew"].Value);
                    bool isNewDestPlate = Convert.ToBoolean(senderGrid.CurrentRow.Cells["DestPlateIsNew"].Value);

                    sourcePlate = (string)senderGrid.CurrentRow.Cells["SourcePlate"].Value;
                    destPlate = (string)senderGrid.CurrentRow.Cells["DestPlate"].Value;

                    if (senderGrid.Columns[e.ColumnIndex].Name == "SourcePlateIsNew" && senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                    {
                        senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);

                        isNewSourcePlate = Convert.ToBoolean(senderGrid.CurrentRow.Cells["SourcePlateIsNew"].Value);
                        if (isNewSourcePlate == false && sourcePlate != "BCR")
                        {
                            frmImportFromDB importDB = new frmImportFromDB();
                            importDB.ShowDialog();
                            if (importDB.DialogResult == DialogResult.Yes)
                            {
                                GetBatchPlates.RemoveAll(bp => bp.PlateId.ToUpper() == sourcePlate.ToUpper());
                             //   GetBatchPlates.AddRange(importDB.GetBatchPlates);

                                senderGrid.CurrentRow.Cells["WorkList"].Value = destPlate + ".CSV";
                                senderGrid.CurrentRow.Cells["PlateDesc"].Value = "Source Plate reday to go.";
                                senderGrid.CurrentRow.DefaultCellStyle.BackColor = Color.LightGreen;
                            }
                        }
                        else if (sourcePlate == "BCR")
                        {
                            senderGrid.CurrentRow.Cells["SourcePlateIsNew"].Value = true;
                            senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        }

                    }
                    else if (senderGrid.Columns[e.ColumnIndex].Name == "DestPlateIsNew" && senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                    {
                        senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);

                        isNewDestPlate = Convert.ToBoolean(senderGrid.CurrentRow.Cells["DestPlateIsNew"].Value);
                        if (isNewDestPlate == false)
                        {
                            frmImportFromDB importDB = new frmImportFromDB();
                            importDB.ShowDialog();
                            if (importDB.DialogResult == DialogResult.Yes)
                            {
                                GetBatchPlates.RemoveAll(bp => bp.PlateId.ToUpper() == sourcePlate.ToUpper());
                             //   GetBatchPlates.AddRange(importDB.GetBatchPlates);

                                senderGrid.CurrentRow.Cells["WorkList"].Value = destPlate + ".CSV";
                                senderGrid.CurrentRow.Cells["PlateDesc"].Value = "Source Plate reday to go.";
                                senderGrid.CurrentRow.DefaultCellStyle.BackColor = Color.LightGreen;
                            }
                        }

                    }
                    else if (senderGrid.Columns[e.ColumnIndex].Name == "Included" && senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                    {
                        senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        included = Convert.ToBoolean(senderGrid.CurrentRow.Cells["Included"].Value);
                    }

                    PlatesChecking();
                }

            }
            catch (Exception ex)
            {
                string errMsg = "dgvPlateSet_CellContentClick() met some issues:";
                errMsg = Environment.NewLine + ex.Message;
            }
        }
        private string GetPrinterName()
        {
            PrinterSettings prtSettings = new PrinterSettings();

            string defaultPrinterName = prtSettings.PrinterName;
            string barcodePrinterName = "ZDesigner";
            List<string> printerNames = new List<string>();

            foreach (string printerName in PrinterSettings.InstalledPrinters)
            {
                if (printerName.IndexOf(barcodePrinterName) >= 0)
                {
                    printerNames.Add(printerName);
                }
            }

            var defaultBarPrinter = printerNames.Where(pn => pn == defaultPrinterName).FirstOrDefault();
            if (defaultBarPrinter != null && !string.IsNullOrEmpty(defaultBarPrinter))
            {
                barcodePrinterName = defaultBarPrinter;
            }
            else
            {
                barcodePrinterName = printerNames.Where(pn => pn != defaultPrinterName).FirstOrDefault().ToString();
            }

            return barcodePrinterName;
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BCRFilesChecking()
        {
            try
            {

                if (Directory.GetFiles(CurRunBuilder.ReadFilePath, "*.CSV").Length > 0)
                {
                    string csvName = "";
                    string csvFullName = "";
                    string destPlate = "";
                    string sourcePlate = "";
                    bool included = true;
                    bool isReadyPlate = true;
                    string scannedPlateBar = "";

                    var csvFiles = Directory.GetFiles(CurRunBuilder.ReadFilePath, "*.CSV");
                    foreach (var csvFile in csvFiles)
                    {
                        csvFullName = Path.GetFileName(csvFile);
                        csvName = Path.GetFileNameWithoutExtension(csvFile).ToUpper();

                        foreach (DataGridViewRow rwPlate in dgvPlateSet.Rows)
                        {
                            destPlate = rwPlate.Cells["DestPlate"].Value.ToString().ToUpper();
                            sourcePlate = rwPlate.Cells["SourcePlate"].Value.ToString();
                            included = Convert.ToBoolean(rwPlate.Cells["Included"].Value);
                            isReadyPlate = Convert.ToBoolean(rwPlate.Cells["SourcePlateIsNew"].Value);
                            //scannedPlateBar = rwPlate.Cells["ScannedPlate"].Value == null ? "" : rwPlate.Cells["ScannedPlate"].Value.ToString();

                            if (sourcePlate == "BCR" && !string.IsNullOrEmpty(CurScannedPlateBarcode) && CurScannedPlateBarcode.ToUpper() == destPlate)
                            {
                                rwPlate.Cells["PlateDesc"].Value = "BCR file is ready to go";
                                if (included && isReadyPlate)
                                {
                                    ImportBCRPlate(csvFullName, destPlate);

                                    rwPlate.DefaultCellStyle.BackColor = Color.LightGreen;
                                    rwPlate.Cells["CurWorkList"].Value = csvFullName;
                                }
                            }
                            ////else if (sourcePlate == "BCR" && string.IsNullOrEmpty(scannedPlateBar))
                            ////{
                            ////    rwPlate.Cells["PlateDesc"].Value = "Plase scan Plate Barcode to go";
                            ////    rwPlate.DefaultCellStyle.BackColor = Color.LightYellow;
                            ////}


                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string errMsg = "BCRFilesChecking() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }
        }

        private void ImportBCRPlate(string importFile, string plateId)
        {
            RepoSQLOracle repoService = new RepoSQLOracle();

            try
            {
                List<InputFile> sampleValues = new List<InputFile>();
                importFile = CurRunBuilder.ReadFilePath + importFile;

                List<InputFile> rawValues = File.ReadAllLines(importFile)
                    .Skip(1)
                    .Select(v => InputFile.ReadInputFile(v))
                    .ToList();

                if (rawValues.Count > 0)
                {
                    sampleValues = repoService.GetPlateSamples(rawValues);
                    if (sampleValues.Count > 0)
                    {
                        foreach (var smp in sampleValues)
                        {
                            InputFileValues.Add(new InputFile
                            {
                                FullSampleId = smp.FullSampleId,
                                ShortId = smp.ShortId,
                                RackName = "BCR",
                                Position = smp.Position,
                                //Well = smp.Well,
                                PlateId = plateId
                            });
                        }
                    }




                    ////sampleValues = repoService.GetShortSamples(rawValues);
                    ////if (sampleValues.Count > 0)
                    ////{
                    ////    foreach (var smp in sampleValues)
                    ////    {
                    ////        InputFileValues.Add(new InputFile
                    ////        {
                    ////            FullSampleId = smp.FullSampleId,
                    ////            ShortId = smp.ShortId,
                    ////            RackName = "BCR",
                    ////            Position = smp.Position,
                    ////            PlateId = plateId
                    ////            //PlateId = smp.PlateId
                    ////        });
                    ////    }
                    ////}
                }
            }
            catch (Exception ex)
            {
                string errMsg = "ImportBCRPlate() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }
        }

        public void WriteCSV<T>(IEnumerable<T> items, string path)
        {
            try
            {
                Type itemType = typeof(T);
                var props = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance).OrderBy(p => p.Name);

                using (var writer = new StreamWriter(path))
                {
                    writer.WriteLine(string.Join(", ", props.Select(p => p.Name)));

                    foreach (var item in items)
                    {
                        writer.WriteLine(string.Join(", ", props.Select(p => p.GetValue(item, null))));
                    }
                }
            }
            catch (Exception ex)
            {
                string ee = ex.Message;

            }
        }
    }
}
