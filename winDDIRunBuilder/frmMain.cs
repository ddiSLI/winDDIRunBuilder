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
using static winDDIRunBuilder.Models.InputPlate;

namespace winDDIRunBuilder
{
    public partial class frmMain : Form
    {
        public ClientRunBuilder CurRunBuilder { get; set; }
        public List<InputFile> InputFileValues { get; set; }
        public List<ValidPlate> CurValidPlates { get; set; }
        public List<PlateSample> CurPlateSamples { get; set; }          //samples from history plates
        public List<OutputPlateSample> OutPlateSamples { get; set; }    //samples from plates transfer, worklist samples
        public List<ProtocolPlate> ProtocolPlates { get; set; } = new List<ProtocolPlate>();

        private List<Protocol> pProtocol = new List<Protocol>();

        private List<string> ProcessedWorklist { get; set; }

        private string CurUniqueId { get; set; } = "";
        private string pAlpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private string pSelectedPlatePage = "";
        private string pCurMapPlateId = "";

        private string pCurSelectedPlateId = "";
        private string pCurSelectedPlateVersion = "";

        private DBPlate ScannedDBPalte { get; set; }
        private List<PlateSample> ScannedDBPlateSamples { get; set; }


        private bool pIsRotated = false;
        private bool pIsFrmLoaded = false;

        public frmMain()
        {
            InitializeComponent();
        }

        public string VersionLabel
        {
            get
            {
                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                {
                    Version ver = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                    return string.Format("Product Name: {4}, Version: {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision, Assembly.GetEntryAssembly().GetName().Name);
                }
                else
                {
                    var ver = Assembly.GetExecutingAssembly().GetName().Version;
                    return string.Format("Product Name: {4}, Version: {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision, Assembly.GetEntryAssembly().GetName().Name);
                }
            }
        }
        private void frmMain_Load(object sender, EventArgs e)
        {

            CurRunBuilder = new ClientRunBuilder();
            RepoSQL sqlService = new RepoSQL();

            try
            {
                string runBuilderVersion = "1.0.0.39";
                //var ver = Assembly.GetExecutingAssembly().GetName().Version;
                //string runBuilderVersion = System.Windows.Forms.Application.pu;
                //string runBuilderVersion = System.Windows.Forms.Application.ProductVersion;

                this.Text += String.Format("  Version {0}", runBuilderVersion);

                CurPlateSamples = new List<PlateSample>();
                CurValidPlates = new List<ValidPlate>();
                OutPlateSamples = new List<OutputPlateSample>();
                InputFileValues = new List<InputFile>();

                ScannedDBPalte = new DBPlate();
                ScannedDBPlateSamples = new List<PlateSample>();

                //setup fileWatcherPlate
                fileWatcherBCR.Path = CurRunBuilder.BCROutput;
                fileWatcherBCR.Filter = "*.CSV";
                //

                lblJanusName.Text = CurRunBuilder.JanusName;

                //get Protocol From CSV
                pProtocol = GetProtocols();

                //get Protocol From SQL
                //pProtocol =sqlService.GetProtocols(CurRunBuilder.Department);

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

                ProcessedWorklist = new List<string>();

                pIsFrmLoaded = true;

                txbPrompt.Text = "Please first select a protocol.";
            }
            catch (Exception ex)
            {
                string errMsg = "{frmMain_Load} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;

                txbPrompt.ForeColor = Color.DarkRed;
                txbPrompt.Text = errMsg;
            }
            finally
            {
                txbBarcode.Text = "";
                this.ActiveControl = txbBarcode;
            }

        }



        private List<Protocol> GetProtocols()
        {
            ClientBackend backendService = new ClientBackend();
            List<Protocol> prots = new List<Protocol>();
            List<ClientBackend.DtoProtocol> dtoProts = new List<ClientBackend.DtoProtocol>();


            dtoProts = (List<ClientBackend.DtoProtocol>)backendService.GetProtocolPlates(CurRunBuilder.PlateSettingFile);
            //dtoProts = (List<ClientBackend.DtoProtocol>)backendService.GetProtocols();
            int protId = 0;
            foreach (var dtoProt in dtoProts)
            {
                prots.Add(new Protocol
                {
                    Id = "P" + protId.ToString().Trim(),
                    Department = dtoProt.Department,
                    PlateId = dtoProt.PlateId,
                    SourcePlate = dtoProt.SourcePlate,
                    ProtocolName = dtoProt.ProtocolName,
                    WorklistName = dtoProt.WorklistName,
                    HasAliasId = dtoProt.HasAliasId.ToUpper() == "FALSE" ? true : false,
                    Pooling = dtoProt.Pooling.ToUpper() == "FALSE" ? true : false,
                    Database = dtoProt.Database,
                    DBTable = dtoProt.DBTable,
                    DBTest = dtoProt.DBTest,
                    PlateRotated = dtoProt.PlateRotated.ToUpper() == "FALSE" ? false : true,
                    StartPos = dtoProt.StartPos,
                    EndPos = dtoProt.EndPos,
                    ExcludeWells = dtoProt.ExcludeWells,
                    Sample = dtoProt.Samples,
                    Diluent = dtoProt.Diluent,
                    Opt1 = dtoProt.Opt1,
                    Opt2 = dtoProt.Opt2,
                    Opt3 = dtoProt.Opt3,
                    Opt4 = dtoProt.Opt4,
                    Opt5 = dtoProt.Opt5
                });

                protId += 1;
            }

            return prots;
        }


        private void cbProtocolCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbPlates.Items.Clear();
            cbPlates.Text = "";
            string[] platePos;
            List<string> delWorklists = new List<string>();


            try
            {
                //CurUniqueId = $"{DateTime.Now.ToString("yyMMddHHmm")}";
                //CurUniqueId = $"{DateTime.Now.ToString("yyMMddffff")}";
                //dgvPlateSet.ReadOnly = false;

                if (pIsFrmLoaded && cbProtocolCd.SelectedValue != null && cbProtocolCd.SelectedValue.ToString().Length > 0)
                {
                    //Clean properties
                    txbPrompt.Text = "";
                    CurPlateSamples = new List<PlateSample>();
                    CurValidPlates = new List<ValidPlate>();
                    OutPlateSamples = new List<OutputPlateSample>();
                    InputFileValues = new List<InputFile>();
                    ScannedDBPalte = new DBPlate();
                    ScannedDBPlateSamples = new List<PlateSample>();

                    cbPlates.Items.Clear();
                    txbManualPlateId.Text = "";
                    txbManualPlateDesc.Text = "";
                    lblMsg.Text = "";
                    lblMsg.ForeColor = Color.DarkBlue;
                    btnCreateManualPlate.Enabled = false;
                    btnPrintManuPlate.Enabled = false;

                    dgvSamplePlate.Rows.Clear();
                    dgvSamplePlate.Columns.Clear();
                    dgvSamplePlate.Refresh();

                    lblCurPlateId.Text = "";
                    btnPrintPlateBarcode.Enabled = false;

                    this.Size = new Size(1280, 390);

                    //Get current sesion serial No.
                    RepoSQL sqlService = new RepoSQL();
                    CurUniqueId = sqlService.GetSeries();

                    //Delete Previous worklists and BCR files
                    foreach (string csvFile in Directory.GetFiles(CurRunBuilder.RunBuilderOutput, "*.csv"))
                    {
                        //Delete worklist files
                        if (csvFile.IndexOf("_") < 0)
                        {
                            delWorklists.Add(csvFile);
                            File.Delete(csvFile);
                        }
                    }

                    //Rename worklist file
                    string fileNameWithoutExt = "";
                    string newWorlistName = "";
                    string justName = "";
                    string origWL = "";

                    if (delWorklists != null && delWorklists.Count > 0)
                    {
                        foreach (var delWL in delWorklists)
                        {
                            foreach (string csvFile in Directory.GetFiles(CurRunBuilder.RunBuilderOutput, "*.csv"))
                            {
                                fileNameWithoutExt = csvFile.Substring(0, csvFile.IndexOf("."));

                                if (fileNameWithoutExt.IndexOf("_") >= 0)
                                {
                                    origWL = fileNameWithoutExt.Substring(0, fileNameWithoutExt.IndexOf("_")) + ".csv";

                                    if (delWL == origWL)
                                    {
                                        newWorlistName = fileNameWithoutExt.Substring(0, fileNameWithoutExt.IndexOf("_")) + ".csv";
                                        File.Move(csvFile, newWorlistName);
                                        if (!File.Exists(csvFile))
                                        {
                                            justName = Path.GetFileName(newWorlistName);
                                            ProcessedWorklist.Add(justName);
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    string dbTests = "";
                    string curProtName = cbProtocolCd.SelectedValue.ToString();
                    var curProtPlates = pProtocol.Where(p => p.ProtocolName == curProtName).ToList();

                    ProtocolPlates = new List<ProtocolPlate>();

                    if (curProtPlates.Count > 0)
                    {
                        //get each plate info
                        foreach (var pp in curProtPlates)
                        {
                            //platePos = new string[];
                            platePos = PlateProperties(pp.StartPos, pp.EndPos);

                            var newPlate = new ProtocolPlate
                            {
                                Id = pp.Id,
                                Department = pp.Department,
                                ProtocolName = pp.ProtocolName,
                                DestPlateId = pp.PlateId + CurUniqueId,
                                DestPlateName = pp.PlateId,
                                SourcePlateId = pp.SourcePlate + CurUniqueId,
                                SourcePlateName = pp.SourcePlate,
                                WorklistName = pp.WorklistName,
                                //WorklistName = pp.PlateId + ".CSV",
                                StartPos = pp.StartPos,
                                EndPos = pp.EndPos,
                                ExcludeWells = pp.ExcludeWells,
                                PlateRotated = pp.PlateRotated,
                                StartX = platePos[0],
                                StartY = platePos[1],
                                EndX = platePos[2],
                                EndY = platePos[3],
                                Accept = pp.DBTest,
                                Sample = pp.Sample,
                                Diluent = pp.Diluent
                            };

                            if (!string.IsNullOrWhiteSpace(pp.Sample.ToString()))
                            {
                                newPlate.Attributes.Add("Sample", pp.Sample.ToString());
                            }
                            else
                            {
                                newPlate.Attributes.Add("Sample", "");
                            }

                            if (!string.IsNullOrWhiteSpace(pp.Diluent.ToString()))
                            {
                                newPlate.Attributes.Add("Diluent", pp.Diluent.ToString());
                            }
                            else
                            {
                                newPlate.Attributes.Add("Diluent", "");
                            }

                            if (!string.IsNullOrWhiteSpace(pp.DBTest.ToString()))
                            {
                                dbTests = pp.DBTest.ToString().Replace("|", ",");
                                newPlate.Attributes.Add("Accept", dbTests);
                            }
                            else
                            {
                                newPlate.Attributes.Add("Accept", "");
                            }
                            if (!string.IsNullOrWhiteSpace(pp.Opt1.ToString()))
                            {
                                newPlate.Attributes.Add("Opt1", pp.Opt1.ToString());
                            }
                            else
                            {
                                newPlate.Attributes.Add("Opt1", "");
                            }
                            if (!string.IsNullOrWhiteSpace(pp.Opt2.ToString()))
                            {
                                newPlate.Attributes.Add("Opt2", pp.Opt2.ToString());
                            }
                            else
                            {
                                newPlate.Attributes.Add("Opt2", "");
                            }

                            if (!string.IsNullOrWhiteSpace(pp.Opt3.ToString()))
                            {
                                newPlate.Attributes.Add("Opt3", pp.Opt3.ToString());
                            }
                            else
                            {
                                newPlate.Attributes.Add("Opt3", "");
                            }

                            if (!string.IsNullOrWhiteSpace(pp.Opt4.ToString()))
                            {
                                newPlate.Attributes.Add("Opt4", pp.Opt3.ToString());
                            }
                            else
                            {
                                newPlate.Attributes.Add("Opt4", "");
                            }

                            if (!string.IsNullOrWhiteSpace(pp.Opt5.ToString()))
                            {
                                newPlate.Attributes.Add("Opt5", pp.Opt3.ToString());
                            }
                            else
                            {
                                newPlate.Attributes.Add("Opt5", "");
                            }



                            ProtocolPlates.Add(newPlate);
                        }
                    }

                    SetProtocolPlates();

                    //set default plate
                    pSelectedPlatePage = (string)cbPlates.SelectedItem;

                    txbPrompt.ForeColor = Color.DarkBlue;
                    txbPrompt.Text = "The avaliable protocol selected. Please make sure the available plates";

                    txbBarcode.Text = "";
                    txbBarcode.Focus();
                }
            }
            catch (Exception ex)
            {
                string errMsg = "{cbProtocolCd_SelectedIndexChanged()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;

                txbPrompt.ForeColor = Color.Red;
                txbPrompt.Text = errMsg;
            }
            finally
            {
                txbBarcode.Focus();
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            string barcode = "";
            string worklistName = "";
            bool hasInclude = false;
            bool hasBCR = false;
            bool isNewDestPlate = false;
            try
            {

                txbPrompt.Text = "";
                PrinBarCodeZXing printPlateBarcode = new PrinBarCodeZXing();

                ////barcodePrinterName = GetPrinterName();
                foreach (DataGridViewRow rwPlate in dgvPlateSet.Rows)
                {
                    if (Convert.ToBoolean(rwPlate.Cells["Included"].Value))
                    {
                        hasInclude = true;
                        isNewDestPlate = Convert.ToBoolean(rwPlate.Cells["DestPlateIsNew"].Value);

                        if (rwPlate.Cells["SourcePlate"].Value.ToString().IndexOf("BCR") >= 0)
                        {
                            hasBCR = true;
                        }

                        barcode = rwPlate.Cells["DestPlate"].Value.ToString();

                        //txbPrompt.Text = "The plate Barcode Printed for [" + barcode + "]";
                        txbPrompt.Text += "The plate Barcode Printed for [" + barcode + "]" + Environment.NewLine;

                        //
                        if (isNewDestPlate)
                        {
                            printPlateBarcode = new PrinBarCodeZXing();
                            printPlateBarcode.Print(barcode, GetPrinterName());
                        }
                        //

                        worklistName = rwPlate.Cells["CurWorkList"].Value.ToString();
                        var completeWorklist = ProcessedWorklist.Where(w => w.ToUpper() == worklistName.ToUpper()).FirstOrDefault();
                        if (completeWorklist == null)
                        {
                            CreateEmptyWorklist(worklistName);
                        }
                        else
                        {
                            rwPlate.DefaultCellStyle.BackColor = Color.LightGreen;
                            rwPlate.Cells["PlateDesc"].Value = "Worklist created";
                            rwPlate.Cells["wlReady"].Value = true;
                            rwPlate.Cells["ProcessedDB"].Value = true;
                            rwPlate.Cells["ProcessedWL"].Value = true;
                        }
                    }
                }

                if (hasInclude && !hasBCR)
                {
                    //set waitCursor
                    Cursor.Current = Cursors.WaitCursor;
                    Application.UseWaitCursor = true;
                    txbPrompt.Text = "Processing......";
                    txbPrompt.Font = new Font("Arial", 21, FontStyle.Bold);
                    txbPrompt.ForeColor = Color.Blue;

                    //processing
                    ProcessPlates();
                    SaveCreateWorklists();
                    GetValidPlates();

                    //set default cursor
                    Cursor.Current = Cursors.Default;
                    Application.UseWaitCursor = false;
                    Application.DoEvents();

                    if (txbPrompt.Text == "Processing......")
                    {
                        txbPrompt.Text = "";
                    }
                    txbPrompt.Font = new Font("Arial", 11, FontStyle.Regular);
                    txbPrompt.ForeColor = Color.Black;
                }

                //Just for developer using
                if (Environment.UserName.ToUpper() == "SLI")
                {
                    //set waitCursor
                    Cursor.Current = Cursors.WaitCursor;
                    Application.UseWaitCursor = true;
                    txbPrompt.Text = "Processing......";
                    txbPrompt.Font = new Font("Arial", 21, FontStyle.Bold);
                    txbPrompt.ForeColor = Color.Blue;

                    //processing
                    ProcessBCRPlates();
                    ProcessPlates();
                    SaveCreateWorklists();
                    GetValidPlates();

                    //set default cursor
                    Cursor.Current = Cursors.Default;
                    Application.UseWaitCursor = false;
                    Application.DoEvents();

                    if (txbPrompt.Text == "Processing......")
                    {
                        txbPrompt.Text = "";
                    }
                    txbPrompt.Font = new Font("Arial", 11, FontStyle.Regular);
                    txbPrompt.ForeColor = Color.Black;
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


        private void SetProtocolPlates()
        {
            string included = "true";
            string destPlate = "";
            string sourcePlate = "";
            string SourcePlateIsNew = "true";
            string destPlateIsNew = "true";
            string desc = "nothing happened";
            string worklist = "";

            dgvPlateSet.Rows.Clear();

            if (ProtocolPlates != null && ProtocolPlates.Count > 0)
            {
                int plateOrder = 1;
                foreach (var pp in ProtocolPlates)
                {
                    included = "true";
                    destPlate = pp.DestPlateId;
                    sourcePlate = pp.SourcePlateId;
                    SourcePlateIsNew = "true";
                    destPlateIsNew = "true";
                    desc = "Checking SoucePlates";
                    worklist = pp.WorklistName;

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

        private string AddNewValidPlate(InputPlate inputPlate)
        {
            string actionResult = "SUCCCESS";

            if (inputPlate != null)
            {
                if (inputPlate.Name.IndexOf("BCR") >= 0)
                {
                    CurValidPlates.Add(new ValidPlate
                    {
                        Department = CurRunBuilder.Department,
                        PlateId = inputPlate.Name,
                        PlateName = "",
                        StartWell = "",
                        EndWell = "",
                        StartX = "",
                        StartY = "",
                        EndX = "",
                        EndY = "",
                        ExcludeWells = "",
                        Direction = "0",
                        SourcePlateId = "",
                        WorkList = "",
                        IsNewCreatedPlate = true
                    }); ;
                }
                else
                {
                    var findPlate = ProtocolPlates.Where(pp => string.Compare(pp.DestPlateId.ToUpper(), inputPlate.Name.ToUpper(), true) == 0).FirstOrDefault();
                    CurValidPlates.Add(new ValidPlate
                    {
                        Department = CurRunBuilder.Department,
                        PlateId = inputPlate.Name,
                        PlateName = findPlate.DestPlateName,
                        PlateType = "DEST",
                        SizeStartWell = findPlate.StartPos,      //PlateSize
                        SizeEndWell = findPlate.EndPos,          //PlateSize   
                        StartX = inputPlate.Start.X,
                        StartY = inputPlate.Start.Y,
                        EndX = inputPlate.End.X,
                        EndY = inputPlate.End.Y,
                        ExcludeWells = findPlate.ExcludeWells,
                        Exclude = string.Join(",", inputPlate.Exclude),
                        Direction = inputPlate.Direction,
                        Attributes = inputPlate.Attributes,
                        SourcePlateId = findPlate.SourcePlateId,
                        SourcePlateVersion = "1",
                        WorkList = findPlate.WorklistName,
                        Sample = findPlate.Sample,
                        Diluent = findPlate.Diluent,
                        Accept = findPlate.Accept,
                        PlateVersion = "1",
                        IsNewCreatedPlate = true
                    }); ;
                }
            }

            return actionResult;
        }

        private string AddNewSpilloverValidPlate(string basePlateId, DBPlate dbPlate, string startWell = "", string endtWell = "")
        {
            string actionResult = "SUCCCESS";

            var findPlate = CurValidPlates.Where(vp => vp.PlateId == basePlateId).FirstOrDefault();
            if (findPlate != null)
            {
                CurValidPlates.Add(new ValidPlate
                {
                    Department = CurRunBuilder.Department,
                    PlateId = dbPlate.PlateId,
                    PlateName = dbPlate.PlateName,
                    PlateType = "DEST",
                    SizeStartWell = findPlate.SizeStartWell,      //PlateSize
                    SizeEndWell = findPlate.SizeEndWell,          //PlateSize   
                    StartWell = dbPlate.StartPos,
                    EndWell = dbPlate.EndPos,
                    ExcludeWells = findPlate.ExcludeWells,
                    Exclude = string.Join(",", findPlate.Exclude),
                    Direction = findPlate.Direction,
                    Attributes = findPlate.Attributes,
                    SourcePlateId = findPlate.SourcePlateId,
                    SourcePlateVersion = findPlate.SourcePlateVersion,
                    WorkList = findPlate.WorkList,
                    Sample = findPlate.Sample,
                    Diluent = findPlate.Diluent,
                    Accept = findPlate.Accept,
                    PlateVersion = dbPlate.PlateVersion,
                    IsNewCreatedPlate = true
                });
            }

            return actionResult;
        }

        private string[] PlateProperties(string startPos, string endPos)
        {
            string[] plateProperties = { "startX", "startY", "endX", "endY" };
            int startY = 0;
            int startX = 0;
            int endY = 0;
            int endX = 0;

            //A1:A->0; 1->1
            startY = pAlpha.IndexOf(startPos.Substring(0, 1));   //0-Base
            startX = Convert.ToInt32(startPos.Substring(1)) - 1;
            endY = pAlpha.IndexOf(endPos.Substring(0, 1));  //0-Base
            endX = Convert.ToInt32(endPos.Substring(1)) - 1;


            plateProperties = new string[] { startX.ToString().Trim(),
                                            startY.ToString().Trim(),
                                            endX.ToString().Trim(),
                                            endY.ToString().Trim()};

            return plateProperties;
        }

        private string SaveToDB()
        {
            string actionResult = "SUCCESS";

            List<OutputPlateSample> outSamples = new List<OutputPlateSample>();
            DBPlate dbPlate = new DBPlate();

            string worklist = "";
            string sourcePlate = "";
            string destPlateId = "";
            string destPlateName = "";
            string curPlateTimeVersion = "";
            RepoSQL sqlService = new RepoSQL();

            string resultPlateSaveDB = "NV";
            string resultSampleSaveDB = "NV";
            string resultMakeWorklist = "NV";

            bool isIncluded = false;
            bool isProcessedDB = false;
            bool isOneWorklist = false;

            try
            {
                PrinBarCodeZXing printPlateBarcode = new PrinBarCodeZXing();

                if (dgvPlateSet.RowCount > 0)
                {
                    foreach (DataGridViewRow rwPlate in dgvPlateSet.Rows)
                    {
                        isIncluded = Convert.ToBoolean(rwPlate.Cells["Included"].Value);
                        isProcessedDB = Convert.ToBoolean(rwPlate.Cells["ProcessedDB"].Value);

                        if (isIncluded)
                        {
                            outSamples = new List<OutputPlateSample>();
                            dbPlate = new DBPlate();
                            curPlateTimeVersion = $"{DateTime.Now.ToString("yyMMddHHmmss")}";

                            sourcePlate = rwPlate.Cells["SourcePlate"].Value.ToString();
                            destPlateId = rwPlate.Cells["DestPlate"].Value.ToString();    //.ToUpper();
                            worklist = rwPlate.Cells["CurWorkList"].Value.ToString();

                            var validPlate = CurValidPlates.Where(pp => pp.PlateId.ToUpper() == destPlateId.ToUpper()).FirstOrDefault();
                            if (validPlate != null)
                            {
                                destPlateName = validPlate.PlateName;
                                dbPlate.Department = validPlate.Department;
                                dbPlate.PlateName = validPlate.PlateName;
                                //dbPlate.HasQC
                                dbPlate.SizeStartWell = validPlate.SizeStartWell;
                                dbPlate.SizeEndWell = validPlate.SizeEndWell;
                                dbPlate.SourcePlateId = sourcePlate;
                                dbPlate.SourcePlateVersion = validPlate.SourcePlateVersion;
                                dbPlate.Sample = validPlate.Sample;
                                dbPlate.Diluent = validPlate.Diluent;
                                dbPlate.PlateVersion = curPlateTimeVersion;
                                dbPlate.Accept = validPlate.Accept;
                                dbPlate.StartPos = validPlate.StartWell;
                                dbPlate.EndPos = validPlate.EndWell;
                                dbPlate.ExcludeWells = validPlate.ExcludeWells;
                            }

                            var outPlates = OutPlateSamples.Where(ps => ps.DestPlateId.ToUpper() == destPlateId.ToUpper())
                                                        .OrderBy(grp => grp.DestId)
                                                        .GroupBy(dst => dst.DestId)
                                                        .Select(smp => new { GroupName = smp.Key, GroupSize = smp.Count(), GroupItems = smp.ToList() })
                                                        .ToList();

                            if (outPlates != null && outPlates.Count() == 1)
                            {
                                isOneWorklist = true;
                                dbPlate.PlateId = destPlateId;
                                outSamples = (List<OutputPlateSample>)OutPlateSamples.Where(ps => ps.DestPlateId.ToUpper() == destPlateId.ToUpper()).ToList();
                                outSamples.ForEach(v => v.DestPlateVersion = curPlateTimeVersion);
                            }
                            else if (outPlates != null && outPlates.Count() > 1)
                            {
                                //Message box
                                DialogResult dialogResult = MessageBox.Show("Number of samples exceeds plate capacity." +
                                                                            "Do you want to create a spillover plate/worklist ?",
                                                                            "Spillover Plate/Worklist",
                                                                            MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    //Plate Spillover processing
                                    isOneWorklist = false;

                                    foreach (var pId in outPlates)
                                    {
                                        if (pId.GroupName == "0")
                                        {
                                            //Group=0,original plateId
                                            dbPlate.PlateId = destPlateId;
                                        }
                                        else
                                        {
                                            //Group>0,new plateId
                                            dbPlate.PlateId = destPlateName + sqlService.GetSeries();

                                            //Print the Plate bar code
                                            printPlateBarcode = new PrinBarCodeZXing();
                                            printPlateBarcode.Print(dbPlate.PlateId, GetPrinterName());
                                            //
                                        }

                                        //Get plateSamples
                                        outSamples = new List<OutputPlateSample>();
                                        outSamples = (List<OutputPlateSample>)OutPlateSamples
                                                        .Where(ps => ps.DestPlateId.ToUpper() == destPlateId.ToUpper() && ps.DestId == pId.GroupName)
                                                        .ToList();

                                        if (outSamples != null && outSamples.Count > 0)
                                        {
                                            //Modify StartPos and EndPos
                                            dbPlate.StartPos = GetWell(outSamples.FirstOrDefault().DestWellId.ToString(), isBCR: false);
                                            dbPlate.EndPos = GetWell(outSamples.LastOrDefault().DestWellId.ToString(), isBCR: false);

                                            if (pId.GroupName == "0")
                                            {
                                                CurValidPlates.Where(pp => pp.PlateId.ToUpper() == destPlateId.ToUpper()).ToList()
                                                                        .ForEach(p =>
                                                                        {
                                                                            p.StartWell = dbPlate.StartPos;
                                                                            p.EndWell = dbPlate.EndPos;
                                                                        });
                                            }
                                            else
                                            {
                                                //Mapping spilt Plate sample
                                                //update spilt samples DestPlateId,originalDestPateId=NewDestPlateId
                                                OutPlateSamples.Where(ps => ps.DestPlateId.ToUpper() == destPlateId.ToUpper() && ps.DestId == pId.GroupName)
                                                .ToList()
                                                .ForEach(s =>
                                                {
                                                    s.DestPlateId = dbPlate.PlateId;
                                                });

                                                //Add a new plate depends on Original Plate property
                                                AddNewSpilloverValidPlate(destPlateId, dbPlate);
                                            }

                                            //Save to Plate
                                            resultPlateSaveDB = sqlService.AddPlate(dbPlate, Environment.UserName);

                                            //Save to Sample
                                            outSamples.ForEach(s =>
                                            {
                                                s.DestPlateVersion = curPlateTimeVersion;
                                                s.DestNewPlateId = dbPlate.PlateId;
                                            });

                                            resultSampleSaveDB = sqlService.AddSamples(outSamples, Environment.UserName);

                                            //CurValidPlates.Where(p => p.PlateId.ToUpper() == dbPlate.PlateId.ToUpper()).ToList().ForEach(v => v.PlateVersion = dbPlate.PlateVersion);

                                            //Make Worklist
                                            resultMakeWorklist = MakeWorklist(worklist, outSamples, dbPlate, pId.GroupName);



                                        }
                                    }

                                    rwPlate.Cells["ProcessedDB"].Value = 1;
                                    rwPlate.Cells["MultiOutput"].Value = 1;
                                }
                                else if (dialogResult == DialogResult.No)
                                {
                                    isOneWorklist = true;
                                    dbPlate.PlateId = destPlateId;

                                    outSamples = new List<OutputPlateSample>();
                                    outSamples = (List<OutputPlateSample>)OutPlateSamples
                                                        .Where(ps => ps.DestPlateId.ToUpper() == destPlateId.ToUpper() && ps.DestId == "0")
                                                        .ToList();

                                    outSamples.ForEach(v => v.DestPlateVersion = curPlateTimeVersion);
                                }
                            }


                            if (isOneWorklist)
                            {
                                if (outSamples != null && outSamples.Count > 0)
                                {
                                    //Modify StartPos and EndPos
                                    dbPlate.StartPos = GetWell(outSamples.FirstOrDefault().DestWellId.ToString(), isBCR: false);
                                    dbPlate.EndPos = GetWell(outSamples.LastOrDefault().DestWellId.ToString(), isBCR: false);

                                    CurValidPlates.Where(w => w.PlateId == destPlateId).ToList().ForEach(s =>
                                    {
                                        s.StartWell = dbPlate.StartPos;
                                        s.EndWell = dbPlate.EndPos;
                                    });

                                    //Save to Plate
                                    resultPlateSaveDB = sqlService.AddPlate(dbPlate, Environment.UserName);

                                    //Save to Sample
                                    resultSampleSaveDB = sqlService.AddSamples(outSamples, Environment.UserName);


                                    //Make Worklist
                                    resultMakeWorklist = MakeWorklist(worklist, outSamples, dbPlate, GroupName: "0");
                                }

                                rwPlate.Cells["ProcessedDB"].Value = 1;
                                rwPlate.Cells["MultiOutput"].Value = 0;
                            }

                            //Check the result
                            if (resultPlateSaveDB == "SUCCESS" &&
                                resultSampleSaveDB == "SUCCESS" &&
                                resultMakeWorklist == "SUCCESS")
                            {
                                CurValidPlates.Where(p => p.PlateId.ToUpper() == dbPlate.PlateId.ToUpper()).ToList().ForEach(v => v.PlateVersion = dbPlate.PlateVersion);

                                actionResult = "SUCCESS";
                                rwPlate.DefaultCellStyle.BackColor = Color.LightGreen;
                                //rwPlate.Cells["CurWorkList"].Value = destPlateId.Substring(0, destPlateId.Length - CurUniqueId.Length) + ".csv";
                                rwPlate.Cells["PlateDesc"].Value = "Plate and Samples saved; Worklist created";
                            }
                            else if (resultMakeWorklist != "SUCCESS")
                            {
                                actionResult = "NOWORKLIST";
                                rwPlate.Cells["PlateDesc"].Value = "Worklist did not create";
                            }
                            else
                            {
                                actionResult = "NOTSAVED";
                                rwPlate.Cells["PlateDesc"].Value = "Plate and Samples did not save";
                            }

                        }
                    }
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

        }

        private string MakeWorklist(string worklist, List<OutputPlateSample> outSamples, DBPlate dbPlate, string GroupName = "")
        {
            string actionResult = "";
            string resultWriteWorklist = "";
            OutputPlate outPlate = new OutputPlate();

            outPlate.Name = dbPlate.PlateId;
            outPlate.SourcePlateId = dbPlate.SourcePlateId;
            outPlate.StartWell = dbPlate.StartPos;
            outPlate.EndWell = dbPlate.EndPos;

            if (!string.IsNullOrWhiteSpace(dbPlate.Sample))
            {
                outPlate.Attributes.Add("Sample", dbPlate.Sample.ToString());
            }
            else
            {
                outPlate.Attributes.Add("Sample", "");
            }
            if (!string.IsNullOrWhiteSpace(dbPlate.Diluent.ToString()))
            {
                outPlate.Attributes.Add("Diluent", dbPlate.Diluent.ToString());
            }
            else
            {
                outPlate.Attributes.Add("Diluent", "");
            }

            if (!string.IsNullOrWhiteSpace(dbPlate.Accept))
            {
                outPlate.Attributes.Add("Accept", dbPlate.Accept.ToString());
            }
            else
            {
                outPlate.Attributes.Add("Accept", "");
            }
            if (!string.IsNullOrWhiteSpace(dbPlate.Opt1))
            {
                outPlate.Attributes.Add("Opt1", dbPlate.Opt1.ToString());
            }
            else
            {
                outPlate.Attributes.Add("Opt1", "");
            }
            if (!string.IsNullOrWhiteSpace(dbPlate.Opt2))
            {
                outPlate.Attributes.Add("Opt2", dbPlate.Opt2.ToString());
            }
            else
            {
                outPlate.Attributes.Add("Opt2", "");
            }

            if (!string.IsNullOrWhiteSpace(dbPlate.Opt3))
            {
                outPlate.Attributes.Add("Opt3", dbPlate.Opt3.ToString());
            }
            else
            {
                outPlate.Attributes.Add("Opt3", "");
            }

            if (!string.IsNullOrWhiteSpace(dbPlate.Opt4))
            {
                outPlate.Attributes.Add("Opt4", dbPlate.Opt3.ToString());
            }
            else
            {
                outPlate.Attributes.Add("Opt4", "");
            }

            if (!string.IsNullOrWhiteSpace(dbPlate.Opt5))
            {
                outPlate.Attributes.Add("Opt5", dbPlate.Opt3.ToString());
            }
            else
            {
                outPlate.Attributes.Add("Opt5", "");
            }

            if (GroupName == "0")
            {
                outPlate.WorkList = worklist;
            }
            else
            {
                outPlate.WorkList = worklist.Substring(0, worklist.IndexOf(".")) + "_" + (Convert.ToInt32(GroupName) + 1).ToString() + ".csv";
            }

            resultWriteWorklist = WriteWorklist(outSamples, outPlate);

            if (resultWriteWorklist == "SUCCESS")
            {
                actionResult = "SUCCESS";
            }
            else
            {
                actionResult = resultWriteWorklist + "; ";
            }

            return actionResult;
        }

        public string CreateEmptyWorklist(string worklistName)
        {
            string actionResult = "NA";
            string exportPath = "";
            string plateFileItems = "SampleID,SourceRack,SourcePosition,DestRack,DestPosition, Sample, Diluent";

            try
            {
                exportPath = CurRunBuilder.RunBuilderOutput + worklistName;

                using (var writer = new StreamWriter(exportPath))
                {
                    writer.WriteLine(plateFileItems);
                }

                actionResult = "SUCCESS";
            }
            catch (Exception ex)
            {
                string errMsg = "{CreateEmptyWorklist()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                actionResult = "ERROR:" + errMsg;

                txbPrompt.ForeColor = Color.DarkRed;
                txbPrompt.Text = errMsg;
            }

            return actionResult;
        }

        public string WriteWorklist(List<OutputPlateSample> outSamples, OutputPlate outPlate)
        {
            string actionResult = "NA";
            string exportPath = "";
            string exportArchivePath = "";

            string itemValue = "";
            string sourcePlate = "";
            string extValue = "";

            //var columns = new HashSet<string>(outSamples.SelectMany(x => x.Attributes.Keys).Distinct().ToList());
            // var columnMap = outSamples.SelectMany(x => x.Attributes.Keys).Distinct().ToDictionary(x => x, y => y);

            string plateFileItems = "SampleID,SourceRack,SourcePosition,DestRack,DestPosition, Sample, Diluent";
            //string plateFileItems = "SourceRack,SourcePosition,DestRack,DestPosition," + string.Join(",", columns);

            try
            {
                exportPath = CurRunBuilder.RunBuilderOutput + outPlate.WorkList;
                exportArchivePath = CurRunBuilder.RunBuilderOutputArchive + outPlate.Name + "__" + outPlate.WorkList;

                if (outPlate.SourcePlateId.IndexOf("BCR") >= 0)
                {
                    sourcePlate = "BCR";
                }
                else
                {
                    sourcePlate = outPlate.SourcePlateId;
                }

                if (!string.IsNullOrEmpty(outPlate.Attributes["Opt1"]))
                {
                    extValue = "," + outPlate.Attributes["Opt1"];
                }
                if (!string.IsNullOrEmpty(outPlate.Attributes["Opt2"]))
                {
                    extValue += "," + outPlate.Attributes["Opt2"];
                }
                if (!string.IsNullOrEmpty(outPlate.Attributes["Opt3"]))
                {
                    extValue += "," + outPlate.Attributes["Opt3"];
                }
                if (!string.IsNullOrEmpty(outPlate.Attributes["Opt4"]))
                {
                    extValue += "," + outPlate.Attributes["Opt4"];
                }
                if (!string.IsNullOrEmpty(outPlate.Attributes["Opt5"]))
                {
                    extValue += "," + outPlate.Attributes["Opt5"];
                }

                using (var writer = new StreamWriter(exportPath))
                {
                    writer.WriteLine(plateFileItems);


                    foreach (var item in outSamples)
                    {
                        //itemValue += item.SourcePlateId + ",";

                        itemValue = item.SampleId + ",";
                        //itemValue = item.SampleId.Replace("-", "") + ",";

                        itemValue += sourcePlate + ",";
                        itemValue += item.SourceWellId + ",";
                        //itemValue += GetWell(item.SourceWellId, item.SourcePlateId.IndexOf("BCR") >= 0) + ",";
                        itemValue += item.DestPlateId + ",";
                        itemValue += item.DestWellId + ",";
                        //itemValue += GetWell(item.DestWellId, item.DestPlateId.IndexOf("BCR") >= 0) + ",";

                        //foreach(var column in columns)
                        //{
                        //    if (item.Attributes.ContainsKey(column))
                        //    {
                        //        itemValue += $"{item.Attributes[column]},";
                        //    }
                        //    else
                        //    {
                        //        itemValue += ",";
                        //    }
                        //}

                        itemValue += outPlate.Attributes["Sample"] + ",";
                        itemValue += outPlate.Attributes["Diluent"];   // outPlate.Diluent.ToString();
                        itemValue += extValue;

                        writer.WriteLine(itemValue);
                        itemValue = "";
                    }
                }

                File.Copy(exportPath, exportArchivePath, true);

                actionResult = "SUCCESS";
            }
            catch (Exception ex)
            {
                string errMsg = "{WriteWorklist()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                actionResult = "ERROR:" + errMsg;

                txbPrompt.ForeColor = Color.DarkRed;
                txbPrompt.Text = errMsg;
            }

            return actionResult;
        }

        private string GetWell(string xyString, bool isBCR)
        {
            if (xyString.IndexOf(",") <= 0)
            {
                return xyString;
            }

            if (isBCR)
            {
                return int.Parse(xyString.Substring(0, xyString.IndexOf(','))).ToString();

            }
            else
            {
                return (char)('A' + int.Parse(xyString.Substring(xyString.IndexOf(',') + 1)))
                    + (int.Parse(xyString.Substring(0, xyString.IndexOf(','))) + 1).ToString();
            }
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
            List<OutputPlateSample> plateSamples = new List<OutputPlateSample>();

            try
            {
                pCurSelectedPlateId = "";
                pCurSelectedPlateVersion = "";
                ScannedDBPalte = new DBPlate();

                if (pIsFrmLoaded && cbProtocolCd.SelectedValue != null && cbProtocolCd.SelectedValue.ToString().Length > 0 &&
                    cbPlates.SelectedItem != null)
                {

                    this.Size = new Size(1280, 848);

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

                        var curOutPlate = CurValidPlates.Where(pl => pl.PlateId.ToUpper() == pSelectedPlatePage.ToUpper()).FirstOrDefault();
                        plateSamples = (List<OutputPlateSample>)OutPlateSamples.Where(p => p.DestPlateId.ToUpper() == pSelectedPlatePage.ToUpper()).ToList();

                        if (plateSamples.Count > 0)
                        {
                            pCurSelectedPlateId = curOutPlate.PlateId;
                            pCurSelectedPlateVersion = curOutPlate.PlateVersion;

                            pCurMapPlateId = pSelectedPlatePage;
                            lblMsg.ForeColor = Color.Navy;
                            lblMsg.Text = "The plate, " + pSelectedPlatePage + " , has following sample(s).";

                            if (curOutPlate.Direction == "1")
                            {
                                //Rotated
                                pIsRotated = true;
                                MappingPlateSamplesRotated(curOutPlate, plateSamples);
                            }
                            else
                            {
                                pIsRotated = false;
                                MappingPlateSamples(curOutPlate, plateSamples);
                            }
                        }
                        else
                        {
                            lblMsg.ForeColor = Color.Red;
                            lblMsg.Text = "The plate, " + pSelectedPlatePage + " , does not have sample(s).";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;

            }

            txbBarcode.Text = "";
            txbBarcode.Focus();

        }


        //private void dgvSamplePlate_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        //{
        //    if (e.RowIndex < 0)
        //        return;

        //    //if (e.ColumnIndex > 0 && string.IsNullOrEmpty(e.Value.ToString()) == false)
        //    if (string.IsNullOrEmpty(e.Value.ToString()) == false)
        //    {
        //        //if ((pIsRotated == false && e.ColumnIndex > 0) || (pIsRotated && e.ColumnIndex < dgvSamplePlate.ColumnCount - 1))
        //        if (pIsRotated == false && e.ColumnIndex > 0)
        //        {
        //            e.Paint(e.CellBounds, DataGridViewPaintParts.All);

        //            var w = Properties.Resources.tubeBlue64.Width / 3;  //Properties.Resources.tubeBlue64.Width;
        //            var h = Properties.Resources.tube64.Height / 3;     // Properties.Resources.tube64.Height;
        //            var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 30; // e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
        //            var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 5; // e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

        //            if (e.Value.ToString().IndexOf("QC") >= 0)
        //            {
        //                e.Graphics.DrawImage(Properties.Resources.tubePurple64, new Rectangle(x, y, w, h));
        //            }
        //            else
        //            {
        //                e.Graphics.DrawImage(Properties.Resources.tubeBlue64, new Rectangle(x, y, w, h));
        //            }

        //            if (e.Value.ToString().IndexOf("REJ") >= 0)
        //            {
        //                e.CellStyle.BackColor = Color.Orange;
        //            }

        //            //e.Graphics.DrawImage(Properties.Resources.tubeBlue64, new Rectangle(x, y, w, h));
        //            e.Handled = true;
        //        }
        //        else if (pIsRotated && e.ColumnIndex < dgvSamplePlate.ColumnCount - 1)
        //        {
        //            e.Paint(e.CellBounds, DataGridViewPaintParts.All);

        //            var w = Properties.Resources.tubeBlue64.Width / 3;  //Properties.Resources.tubeBlue64.Width;
        //            var h = Properties.Resources.tube64.Height / 3;     // Properties.Resources.tube64.Height;
        //            var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 30; // e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
        //            var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 5; // e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

        //            e.Graphics.DrawImage(Properties.Resources.tubeBlue64, new Rectangle(x, y, w, h));
        //            e.Handled = true;
        //        }

        //    }
        //}

        private void dgvSamplePlate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            //string sourcePlate = "";
            //string destPlate = "";

            string curCellValue = "";

            try
            {
                if (e.RowIndex >= 0)
                {
                    //remove newline
                    curCellValue = senderGrid.CurrentCell.Value.ToString();
                    curCellValue = curCellValue.Replace("\n", "").Replace("\r", "");

                    //bool included = Convert.ToBoolean(senderGrid.CurrentRow.Cells["Included"].Value);
                    //bool isReadySourcePlate = Convert.ToBoolean(senderGrid.CurrentRow.Cells["SourcePlateIsNew"].Value);

                    //sourcePlate = (string)senderGrid.CurrentRow.Cells["SourcePlate"].Value;
                    //destPlate = (string)senderGrid.CurrentRow.Cells["DestPlate"].Value;

                    if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                    {
                        senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        lblMsg.ForeColor = Color.DarkBlue;
                        lblMsg.Text = "Tube Sample: " + curCellValue + "; [ " + senderGrid.CurrentCell.Tag + " ]";
                        //lblMsg.Text = "Tube Sample: " + senderGrid.CurrentCell.Value + "; [ " + senderGrid.CurrentCell.Tag + " ]";
                        //lblMsg.Text = "Tube -" + senderGrid.CurrentCell.Value + " sample is : [ " + senderGrid.CurrentCell.Tag + " ]";
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

        private void dgvSamplePlate_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            string curCellValue = "";
            string resultUpdate = "";

            try
            {
                if (e.RowIndex >= 0)
                {
                    RepoSQL sqlService = new RepoSQL();
                    List<SampleStatus> rejectSamples = new List<SampleStatus>();
                    SampleStatus smpStatus = new SampleStatus();

                    //remove newline
                    curCellValue = senderGrid.CurrentCell.Value.ToString();
                    curCellValue = curCellValue.Replace("\n", "").Replace("\r", "");

                    smpStatus.SampleId = curCellValue;
                    smpStatus.User = Environment.UserName;

                    if (string.IsNullOrEmpty(pCurSelectedPlateId) && ScannedDBPalte != null)
                    {
                        smpStatus.PlateId = ScannedDBPalte.PlateId;
                        smpStatus.PlateVersion = ScannedDBPalte.PlateVersion;
                    }
                    else if (!string.IsNullOrEmpty(pCurSelectedPlateId))
                    {
                        smpStatus.PlateId = pCurSelectedPlateId;
                        smpStatus.PlateVersion = pCurSelectedPlateVersion;
                    }


                    if (curCellValue.IndexOf("REJ_") >= 0)
                    {
                        smpStatus.Status = "";
                    }
                    else
                    {
                        smpStatus.Status = "REJECT";
                    }

                    curCellValue = curCellValue.Replace("REJ_", "");
                    smpStatus.SampleId = curCellValue;

                    rejectSamples.Add(smpStatus);
                    resultUpdate = sqlService.UpdateSampleStatus(rejectSamples);
                    if (resultUpdate == "SUCCESS")
                    {
                        if (smpStatus.Status == "REJECT")
                        {
                            senderGrid.CurrentCell.Value = "REJ_" + "\n" + curCellValue.Substring(0, 6) + "\n" + curCellValue.Substring(6);
                            senderGrid.CurrentCell.Style.BackColor = Color.HotPink;
                        }
                        else
                        {
                            senderGrid.CurrentCell.Value = curCellValue.Substring(0, 6) + "\n" + curCellValue.Substring(6);
                            senderGrid.CurrentCell.Style.BackColor = Color.LightGray;
                        }

                    }

                    //////bool included = Convert.ToBoolean(senderGrid.CurrentRow.Cells["Included"].Value);
                    //////bool isReadySourcePlate = Convert.ToBoolean(senderGrid.CurrentRow.Cells["SourcePlateIsNew"].Value);

                    //////sourcePlate = (string)senderGrid.CurrentRow.Cells["SourcePlate"].Value;
                    //////destPlate = (string)senderGrid.CurrentRow.Cells["DestPlate"].Value;

                    ////if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                    ////{
                    ////    senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    ////    lblMsg.ForeColor = Color.DarkBlue;
                    ////    lblMsg.Text = "Tube Sample: " + curCellValue + "; [ " + senderGrid.CurrentCell.Tag + " ]";
                    ////    //lblMsg.Text = "Tube Sample: " + senderGrid.CurrentCell.Value + "; [ " + senderGrid.CurrentCell.Tag + " ]";
                    ////    //lblMsg.Text = "Tube -" + senderGrid.CurrentCell.Value + " sample is : [ " + senderGrid.CurrentCell.Tag + " ]";
                    ////}
                    ///
                }

            }
            catch (Exception ex)
            {
                string errMsg = "dgvSamplePlate_CellContentDoubleClick) met some issues:";
                errMsg += ex.Message;

                lblMsg.ForeColor = Color.Red;
                lblMsg.Text = errMsg;
            }

        }

        private void SetSameSourcePlate(string oriPlate, string curPlate, bool isHisPlate = true)
        {
            string destPlate = "";
            string sourcePlate = "";
            bool included = true;

            foreach (DataGridViewRow rwPlate in dgvPlateSet.Rows)
            {
                destPlate = rwPlate.Cells["DestPlate"].Value.ToString();
                sourcePlate = rwPlate.Cells["SourcePlate"].Value.ToString();
                included = Convert.ToBoolean(rwPlate.Cells["Included"].Value);
                if (included && sourcePlate == oriPlate)
                {
                    rwPlate.Cells["SourcePlate"].Value = curPlate;
                    rwPlate.Cells["SourcePlateIsNew"].Value = !isHisPlate;
                }
            }
        }

        private void GetValidPlates()
        {
            cbPlates.Items.Clear();
            cbPlates.SelectedItem = null;
            cbPlates.SelectedText = "-Select a plate-";

            foreach (var plate in CurValidPlates)
            {
                if (plate.PlateType == "DEST")
                {
                    cbPlates.Items.Add(plate.PlateId.ToString());
                }
            }
        }

        private void MappingPlateSamples(ValidPlate outPlate, List<OutputPlateSample> plateSamples)
        {
            string issues = "";

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
                    destWellId = GetWell(smp.DestWellId, isBCR: false);
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

                MapPlates(dtPlateSamples);

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
                    destWellId = GetWell(smp.DestWellId, isBCR: false);
                    wellX = Convert.ToInt32(destWellId.Substring(1));
                    wellY = pAlpha.IndexOf(destWellId.Substring(0, 1));

                    if (wellX <= RotateSizeY && wellY <= RotateSizeX)
                    {
                        dtPlateSamples.Rows[wellX - 1][wellColSatrtId - wellY] = smp.SampleId;
                        //dtPlateSamples.Rows[RotateSizeX - wellY][wellX] = smp.SampleId;
                    }
                    else
                    {
                        issues += "Well is Over PlateSize";
                        issues += Environment.NewLine;
                    }
                }

                MapPlates(dtPlateSamples);

            }
            catch (Exception ex)
            {
                string errMsg = "{MappingPlateSamplesRotated()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }
        }

        private void MapPlates(DataTable dtPlateSamples)
        {
            string curWell = "";
            string smpItem = "";

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
                            smpItem = tRw[tCol].ToString().Trim();
                            if (smpItem.Length > 6)
                            {
                                smpItem = smpItem.Substring(0, 6) + "\n" + smpItem.Substring(6);
                                //smpItem = smpItem.Substring(0, 10) + Environment.NewLine + smpItem.Substring(10);
                            }

                            if (pIsRotated)
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
                string errMsg = "MapPlates() met errors: ";
                errMsg += ex.Message;

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (lblCurPlateId.Text.Trim().Length > 0)
            {
                PrinBarCodeZXing printPlateBarcode = new PrinBarCodeZXing();
                printPlateBarcode.Print(lblCurPlateId.Text.Trim(), GetPrinterName());
            }

            //string barCode = "";
            //if (dgvPlateSet.CurrentRow.Cells["DestPlate"].Value != null)
            //{
            //    barCode = dgvPlateSet.CurrentRow.Cells["DestPlate"].Value.ToString();
            //    PrinBarCodeZXing printPlateBarcode = new PrinBarCodeZXing();
            //    //PrintBarCode printPlateBarcode = new PrintBarCode();
            //    printPlateBarcode.Print(barCode, GetPrinterName());
            //}

            txbBarcode.Text = "";
            txbBarcode.Focus();
        }

        private void txbBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string mapPlateResult = "NA";
                string plateNewSerialNo = "";
                try
                {
                    pCurSelectedPlateId = "";
                    pCurSelectedPlateVersion = "";

                    ScannedDBPalte = new DBPlate();
                    ScannedDBPlateSamples = new List<PlateSample>();
                    btnCreateManualPlate.Enabled = false;
                    btnPrintManuPlate.Enabled = false;

                    RepoSQL sqlService = new RepoSQL();

                    if (!string.IsNullOrEmpty(txbBarcode.Text.Trim()))
                    {

                        mapPlateResult = GetMapAnyPlateSamples(txbBarcode.Text.Trim());

                        if (mapPlateResult == "SUCCESS")
                        {
                            plateNewSerialNo = sqlService.GetSeries();
                            txbManualPlateId.Text = ScannedDBPalte.PlateName + plateNewSerialNo;
                            txbManualPlateDesc.Text = "From " + txbBarcode.Text.Trim();
                            btnCreateManualPlate.Enabled = true;

                            pCurMapPlateId = txbBarcode.Text.Trim();

                            lblCurPlateId.Text = txbBarcode.Text.Trim();
                            btnPrintPlateBarcode.Enabled = true;
                        }
                        else
                        {
                            txbPrompt.Text = mapPlateResult;
                        }
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

        private string GetMapAnyPlateSamples(string plateId)
        {
            string mapResult = "NO-SAMPLES";
            RepoSQL sqlService = new RepoSQL();
            List<DBPlate> anyDBPlates = new List<DBPlate>();
            List<OutputPlateSample> outSamples = new List<OutputPlateSample>();
            string plateIdVersion = "";
            ValidPlate validPlate = new ValidPlate();
            List<OutputPlateSample> samples = new List<OutputPlateSample>();

            ModelTransfer tranService = new ModelTransfer();
            ScannedDBPalte = new DBPlate();
            ScannedDBPlateSamples = new List<PlateSample>();
            string smpWell = "";

            try
            {
                anyDBPlates = sqlService.GetPlates(plateId);
                if (anyDBPlates != null && anyDBPlates.Count > 0)
                {
                    ScannedDBPalte = anyDBPlates.LastOrDefault();
                    //ScannedDBPalte = anyDBPlates.FirstOrDefault();
                    plateIdVersion = ScannedDBPalte.PlateVersion;
                    validPlate = tranService.DBPlate2ValidPlate(ScannedDBPalte, "DEST");

                    ScannedDBPlateSamples = sqlService.GetPlateSamples(plateId, plateIdVersion);
                    if (ScannedDBPlateSamples != null && ScannedDBPlateSamples.Count > 0)
                    {
                        this.Size = new Size(1280, 848);

                        foreach (var smp in ScannedDBPlateSamples)
                        {
                            if (!string.IsNullOrEmpty(smp.SampleType) && smp.SampleType == "QCEND")
                            {
                                smpWell = tranService.GetNextWell(ScannedDBPalte.SizeEndWell
                                                                , ScannedDBPalte.EndPos
                                                                , ScannedDBPalte.PlateRotated);
                            }
                            else
                            {
                                smpWell = smp.Well;
                            }

                            outSamples.Add(new OutputPlateSample
                            {
                                DestPlateId = smp.PlateId,
                                DestWellId = smpWell,
                                //DestWellId = smp.Well,
                                SampleId = smp.SampleId,
                                Status = smp.Status,
                                SampleType = smp.SampleType
                            });
                        }

                        lblMsg.ForeColor = Color.Navy;
                        lblMsg.Text = "The history plate, " + plateId + " , has following sample(s).";

                        //testing use
                        //validPlate.Direction = "1";
                        //end


                        if (validPlate.Direction == "1")
                        {
                            //Rotated
                            pIsRotated = true;
                            MappingPlateSamplesRotated(validPlate, outSamples);
                        }
                        else
                        {
                            pIsRotated = false;
                            MappingPlateSamples(validPlate, outSamples);
                        }
                    }
                    else
                    {
                        lblMsg.ForeColor = Color.Red;
                        lblMsg.Text = "The histrory plate, " + plateId + " , does not have sample(s).";
                    }

                    mapResult = "SUCCESS";
                }
            }
            catch (Exception ex)
            {
                string errMsg = "GetMapAnyPlateSamples() met some issues:";
                errMsg += Environment.NewLine + ex.Message;
                mapResult = errMsg;
            }

            return mapResult;
        }


        private void fileWatcherBCR_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                //set waitCursor
                Cursor.Current = Cursors.WaitCursor;
                Application.UseWaitCursor = true;
                txbPrompt.Text = "Processing......";
                txbPrompt.Font = new Font("Arial", 21, FontStyle.Bold);
                txbPrompt.ForeColor = Color.Blue;

                //processing
                ProcessBCRPlates();
                ProcessPlates();
                SaveCreateWorklists();
                GetValidPlates();

                //set default cursor
                Cursor.Current = Cursors.Default;
                Application.UseWaitCursor = false;
                Application.DoEvents();

                if (txbPrompt.Text == "Processing......")
                {
                    txbPrompt.Text = "";
                }
                txbPrompt.Font = new Font("Arial", 11, FontStyle.Regular);
                txbPrompt.ForeColor = Color.Black;

            }
            catch (Exception ex)
            {
                string errMsg = "fileWatcherBCR_Created() met some issues:";
                errMsg += Environment.NewLine + ex.Message;
                txbPrompt.Text = errMsg;
            }

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
            string sourcePlateId = "";
            string destPlateId = "";

            try
            {
                if (e.RowIndex >= 0)
                {
                    bool included = Convert.ToBoolean(senderGrid.CurrentRow.Cells["Included"].Value);
                    bool isNewSourcePlate = Convert.ToBoolean(senderGrid.CurrentRow.Cells["SourcePlateIsNew"].Value);
                    bool isNewDestPlate = Convert.ToBoolean(senderGrid.CurrentRow.Cells["DestPlateIsNew"].Value);

                    sourcePlateId = (string)senderGrid.CurrentRow.Cells["SourcePlate"].Value;
                    destPlateId = (string)senderGrid.CurrentRow.Cells["DestPlate"].Value;

                    if (included)
                    {
                        lblCurPlateId.Text = destPlateId;
                        btnPrintPlateBarcode.Enabled = true;
                    }


                    if (senderGrid.Columns[e.ColumnIndex].Name == "SourcePlateIsNew" && senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                    {
                        senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);

                        isNewSourcePlate = Convert.ToBoolean(senderGrid.CurrentRow.Cells["SourcePlateIsNew"].Value);
                        if (isNewSourcePlate == false && sourcePlateId.IndexOf("BCR") < 0)
                        {
                            if (SourcePlateIsFromBCR(sourcePlateId))
                            {
                                txbPrompt.BackColor = txbPrompt.BackColor;
                                txbPrompt.ForeColor = Color.DarkRed;
                                txbPrompt.Text = "Source Plate is from BCR. You cannot change the source Plate.";
                                senderGrid.CurrentRow.Cells["SourcePlateIsNew"].Value = true;
                                senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                            }
                            else
                            {
                                frmImportFromDB importDB = new frmImportFromDB();
                                importDB.IsSourcePlate = true;
                                importDB.ShowDialog();
                                if (importDB.DialogResult == DialogResult.Yes)
                                {
                                    CurValidPlates.RemoveAll(bp => bp.PlateId == importDB.SelectedDBPlate.PlateId);
                                    CurValidPlates.Add(importDB.SelectedDBPlate);
                                    CurPlateSamples.AddRange(importDB.SelectedPlateSamples);

                                    //senderGrid.CurrentRow.Cells["WorkList"].Value = destPlate + ".CSV";
                                    senderGrid.CurrentRow.Cells["PlateDesc"].Value = "Source Plate is ready to go.";
                                    senderGrid.CurrentRow.Cells["SourcePlate"].Value = importDB.SelectedDBPlate.PlateId;
                                    //senderGrid.CurrentRow.DefaultCellStyle.BackColor = Color.LightGreen;
                                }
                            }

                        }
                        else if (sourcePlateId.IndexOf("BCR") >= 0)
                        {
                            senderGrid.CurrentRow.Cells["SourcePlateIsNew"].Value = true;
                            senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        }

                    }
                    else if (senderGrid.Columns[e.ColumnIndex].Name == "DestPlateIsNew" && senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                    {
                        senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);

                        isNewDestPlate = Convert.ToBoolean(senderGrid.CurrentRow.Cells["DestPlateIsNew"].Value);
                        bool isBRCSource = senderGrid.CurrentRow.Cells["SourcePlate"].Value.ToString().IndexOf("BCR") >= 0 ? true : false;

                        if (isNewDestPlate == false)
                        {
                            frmImportFromDB importDB = new frmImportFromDB();
                            importDB.IsSourceBCR = isBRCSource;
                            importDB.ShowDialog();
                            if (importDB.DialogResult == DialogResult.Yes)
                            {
                                CurValidPlates.RemoveAll(bp => bp.PlateId == importDB.SelectedDBPlate.PlateId);
                                importDB.SelectedDBPlate.Offset = new int[] { importDB.SelectedPlateSamples.Count };
                                CurValidPlates.Add(importDB.SelectedDBPlate);

                                CurPlateSamples.AddRange(importDB.SelectedPlateSamples);

                                //senderGrid.CurrentRow.Cells["WorkList"].Value = destPlate + ".CSV";
                                senderGrid.CurrentRow.Cells["PlateDesc"].Value = "Source Plate ready to go.";
                                senderGrid.CurrentRow.Cells["DestPlate"].Value = importDB.SelectedDBPlate.PlateId;

                                SetSameSourcePlate(destPlateId, importDB.SelectedDBPlate.PlateId, isHisPlate: true);
                                //senderGrid.CurrentRow.DefaultCellStyle.BackColor = Color.LightGreen;

                            }
                        }

                    }
                    else if (senderGrid.Columns[e.ColumnIndex].Name == "Included" && senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                    {
                        senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        included = Convert.ToBoolean(senderGrid.CurrentRow.Cells["Included"].Value);
                    }
                }

            }
            catch (Exception ex)
            {
                string errMsg = "dgvPlateSet_CellContentClick() met some issues:";
                errMsg = Environment.NewLine + ex.Message;
            }
        }

        private bool SourcePlateIsFromBCR(string sourcePlate)
        {
            bool isFromBCR = false;
            bool included = true;
            string curSourcePlate = "";
            string curDestPlate = "";

            //bool isNewSourcePlate = Convert.ToBoolean(senderGrid.CurrentRow.Cells["SourcePlateIsNew"].Value);
            //bool isNewDestPlate = Convert.ToBoolean(senderGrid.CurrentRow.Cells["DestPlateIsNew"].Value);

            if (dgvPlateSet != null && dgvPlateSet.RowCount > 0)
            {
                foreach (DataGridViewRow rwPlt in dgvPlateSet.Rows)
                {
                    included = Convert.ToBoolean(rwPlt.Cells["Included"].Value);
                    curSourcePlate = (string)rwPlt.Cells["SourcePlate"].Value;
                    curDestPlate = (string)rwPlt.Cells["DestPlate"].Value;

                    if (curDestPlate == sourcePlate)
                    {
                        if (curSourcePlate.IndexOf("BCR") >= 0 && included == true)
                        {
                            isFromBCR = true;
                        }
                    }
                }
            }


            return isFromBCR;
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

        private void ProcessBCRPlates()
        {
            ClientBackend backService = new ClientBackend();
            InputPlate bcrPlate = new InputPlate();
            List<DtoWorklist> dtoWorklist = new List<DtoWorklist>();

            bool needCreatBCR = true;
            string hasSourcePlate = "YES";
            string hasSamples = "YES";

            string csvName = "";
            string csvFullName = "";
            string destPlateId = "";
            string sourcePlateId = "";

            bool included = true;
            bool isNewSourcePlate = true;
            bool isNewDestPlate = true;

            try
            {
                if (Directory.GetFiles(CurRunBuilder.BCROutput, "*.CSV").Length > 0)
                {
                    var csvFiles = Directory.GetFiles(CurRunBuilder.BCROutput, "*.CSV");

                    csvFullName = Path.GetFileName(csvFiles.FirstOrDefault());
                    csvName = Path.GetFileNameWithoutExtension(csvFiles.FirstOrDefault()).ToUpper();

                    foreach (DataGridViewRow rwPlate in dgvPlateSet.Rows)
                    {
                        destPlateId = rwPlate.Cells["DestPlate"].Value.ToString();
                        sourcePlateId = rwPlate.Cells["SourcePlate"].Value.ToString();
                        included = Convert.ToBoolean(rwPlate.Cells["Included"].Value);
                        isNewSourcePlate = Convert.ToBoolean(rwPlate.Cells["SourcePlateIsNew"].Value);
                        isNewDestPlate = Convert.ToBoolean(rwPlate.Cells["DestPlateIsNew"].Value);
                        //scannedPlateBar = rwPlate.Cells["ScannedPlate"].Value == null ? "" : rwPlate.Cells["ScannedPlate"].Value.ToString();

                        if (included && isNewSourcePlate)
                        {
                            if (sourcePlateId == "BCR" + CurUniqueId)
                            {
                                rwPlate.Cells["PlateDesc"].Value = "BCR file is ready to go";

                                if (needCreatBCR)
                                {
                                    bcrPlate = new InputPlate();

                                    //Get the BCR Plate propertoes and load samples
                                    bcrPlate = LoadBCRSamples(csvFullName);

                                    //Create BCR Plate
                                    hasSourcePlate = backService.CreatePlate(bcrPlate);
                                    if (!string.IsNullOrEmpty(backService.ErrMsg))
                                    {
                                        txbPrompt.ForeColor = Color.DarkRed;
                                        txbPrompt.Text = backService.ErrMsg;
                                    }

                                    //Add samples to BCR Plate
                                    if (hasSourcePlate == "YES")
                                    {
                                        AddNewValidPlate(bcrPlate);
                                        hasSamples = backService.AddSamples(sourcePlateId, InputFileValues);

                                        if (!string.IsNullOrEmpty(backService.ErrMsg))
                                        {
                                            txbPrompt.ForeColor = Color.DarkRed;
                                            txbPrompt.Text = backService.ErrMsg;
                                        }

                                        //Only One BCR Sample
                                        needCreatBCR = false;
                                    }
                                }


                                if (isNewDestPlate)
                                {
                                    //Destination is New Plate
                                    dtoWorklist = ProcessNewDestPlate(sourcePlateId, destPlateId);
                                }
                                else
                                {
                                    //Destination is History Plate
                                    dtoWorklist = ProcessHisDestPlate(sourcePlateId, destPlateId);
                                }

                                //Worklist
                                if (dtoWorklist != null && dtoWorklist.Count > 0)
                                {
                                    if (AddPlateNewSamples(dtoWorklist, destPlateId) == "SUCCESS")
                                    {
                                        rwPlate.DefaultCellStyle.BackColor = Color.LightBlue;              //Color.LightGreen;
                                        rwPlate.Cells["CurWorkList"].Value = destPlateId.Substring(0, destPlateId.Length - CurUniqueId.Length) + ".csv";
                                        rwPlate.Cells["PlateDesc"].Value = "Worklist is ready to create";
                                        rwPlate.Cells["wlReady"].Value = true;
                                    }
                                }
                                else
                                {
                                    rwPlate.DefaultCellStyle.BackColor = Color.Orange;              //Color.LightGreen;
                                    rwPlate.Cells["CurWorkList"].Value = "";
                                    rwPlate.Cells["PlateDesc"].Value = "There is no Worklist-samples";
                                    rwPlate.Cells["wlReady"].Value = false;
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string errMsg = "ProcessBCRPlates() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                txbPrompt.ForeColor = Color.DarkRed;
                txbPrompt.Text = errMsg;
            }
        }

        private List<DtoWorklist> ProcessNewDestPlate(string sourcePlateId, string destPlateId)
        {
            List<DtoWorklist> dtoWorklist = new List<DtoWorklist>();
            ClientBackend backService = new ClientBackend();
            InputPlate destPlate = new InputPlate();
            string hasDestPlate = "NA";

            //Get DestPlate Properties
            destPlate = GetDestPlateProperties(destPlateId);

            //Create DestPlate
            hasDestPlate = backService.CreatePlate(destPlate);
            if (!string.IsNullOrEmpty(backService.ErrMsg))
            {
                txbPrompt.ForeColor = Color.DarkRed;
                txbPrompt.Text = backService.ErrMsg;
            }
            if (hasDestPlate == "YES")
            {
                AddNewValidPlate(destPlate);

                //Move to New Destination Plate
                dtoWorklist = (List<DtoWorklist>)backService.GetWorklist(sourcePlateId, destPlate.Name, "lookup_alias,skip_cancelled,cherry_pick");
                if (!string.IsNullOrEmpty(backService.ErrMsg))
                {
                    txbPrompt.ForeColor = Color.DarkRed;
                    txbPrompt.Text = backService.ErrMsg;
                }
            }

            return dtoWorklist;
        }

        private List<DtoWorklist> ProcessHisDestPlate(string sourcePlateId, string destPlateId)
        {
            List<DtoWorklist> dtoWorklist = new List<DtoWorklist>();
            ClientBackend backService = new ClientBackend();
            InputPlate destPlate = new InputPlate();
            string hasDestPlate = "NA";

            var findDestPlate = CurValidPlates.Where(pl => pl.PlateId.ToUpper() == destPlateId.ToUpper()).FirstOrDefault();

            //Get History DestPlate
            destPlate = GetHisDestPlate(findDestPlate);
            if (destPlate == null)
            {
                txbPrompt.ForeColor = Color.DarkRed;
                txbPrompt.Text = "Cannot get history plate.";
            }
            else
            {
                //Create DestPlate
                hasDestPlate = backService.CreatePlate(destPlate);
                List<InputFile> inSamples = new List<InputFile>();
                if (hasDestPlate == "YES")
                {
                    //add hisPlateSamples
                    var desPlate = CurValidPlates.Where(s => s.PlateId.ToUpper() == destPlateId.ToUpper()).FirstOrDefault();
                    inSamples = GetPlateInSamples("DEST", destPlateId, desPlate.Rotated);
                    backService.AddSamples(destPlateId, inSamples);

                    //Create worklist
                    dtoWorklist = (List<DtoWorklist>)backService.GetWorklist(sourcePlateId, destPlate.Name, "lookup_alias,skip_cancelled,cherry_pick");
                    //dtoWorklist = (List<DtoWorklist>)backService.GetWorklist(sourcePlateId.ToUpper(), destPlate.Name.ToUpper(), "lookup_alias,skip_cancelled,cherry_pick");
                }

                if (!string.IsNullOrEmpty(backService.ErrMsg))
                {
                    txbPrompt.ForeColor = Color.DarkRed;
                    txbPrompt.Text = backService.ErrMsg;
                }
            }

            return dtoWorklist;
        }

        private void ProcessPlates()
        {
            string destPlateId = "";
            string sourcePlateId = "";

            bool included = true;
            bool isNewSourcePlate = true;
            bool isNewDestPlate = true;

            string plateProcessResult = "NA";
            string processType = "";

            try
            {
                foreach (DataGridViewRow rwPlate in dgvPlateSet.Rows)
                {
                    destPlateId = rwPlate.Cells["DestPlate"].Value.ToString();
                    sourcePlateId = rwPlate.Cells["SourcePlate"].Value.ToString();
                    included = Convert.ToBoolean(rwPlate.Cells["Included"].Value);
                    isNewSourcePlate = Convert.ToBoolean(rwPlate.Cells["SourcePlateIsNew"].Value);
                    isNewDestPlate = Convert.ToBoolean(rwPlate.Cells["DestPlateIsNew"].Value);
                    if (rwPlate.Cells["ProcessType"].Value is null || string.IsNullOrEmpty(rwPlate.Cells["ProcessType"].Value.ToString()))
                    {
                        processType = "TRANS";
                    }
                    else
                    {
                        processType = rwPlate.Cells["ProcessType"].Value.ToString().ToUpper();
                    }

                    if (included && sourcePlateId != "BCR" + CurUniqueId)
                    {
                        var findPlate = CurValidPlates.Where(pl => pl.PlateId == sourcePlateId).FirstOrDefault();

                        if (isNewSourcePlate && findPlate.IsNewCreatedPlate)
                        {
                            //NewSource => NewDestination
                            plateProcessResult = ProcessNewSourceNewDest(sourcePlateId, destPlateId);

                        }
                        else if (isNewSourcePlate && isNewDestPlate == false)
                        {
                            //NewSource => HistoryDestination
                            plateProcessResult = ProcessNewSourceHisDest(sourcePlateId, destPlateId);

                        }
                        else if (isNewSourcePlate == false && isNewDestPlate)
                        {
                            //HistorySource => NewDestination
                            plateProcessResult = ProcessHisSourceNewDest(sourcePlateId, destPlateId);

                        }
                        else if (isNewSourcePlate == false && isNewDestPlate == false)
                        {
                            //HistorySource => HistoryDestination
                            plateProcessResult = ProcessHisSourceHisDest(sourcePlateId, destPlateId);

                        }

                        if (plateProcessResult == "SUCCESS")
                        {
                            rwPlate.Cells["CurWorkList"].Value = destPlateId.Substring(0, destPlateId.Length - CurUniqueId.Length) + ".csv";
                            rwPlate.Cells["PlateDesc"].Value = "Worklist is ready to create";
                            rwPlate.Cells["wlReady"].Value = true;
                            rwPlate.DefaultCellStyle.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            rwPlate.DefaultCellStyle.BackColor = Color.Orange;
                            rwPlate.Cells["CurWorkList"].Value = "";
                            rwPlate.Cells["PlateDesc"].Value = "There is no Worklist-samples";
                            rwPlate.Cells["wlReady"].Value = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = "ProcessPlates() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                txbPrompt.ForeColor = Color.DarkRed;
                txbPrompt.Text = errMsg;
            }
        }

        private string ProcessNewSourceNewDest(string sourcePlateId, string destPlateId)
        {
            //NewSource => NewDestination
            //New SourcePlate is from other new destination created
            string processResult = "NA";

            ClientBackend backService = new ClientBackend();
            InputPlate destPlate = new InputPlate();
            List<DtoWorklist> dtoWorklist = new List<DtoWorklist>();


            //To Find New Source Plate
            var findPlate = CurValidPlates.Where(pl => pl.PlateId == sourcePlateId).FirstOrDefault();

            //Check New Source Plate
            //Process New Dest Plate
            if (findPlate.IsNewCreatedPlate)
            {
                dtoWorklist = ProcessNewDestPlate(sourcePlateId, destPlateId);
                if (dtoWorklist != null && dtoWorklist.Count > 0)
                {
                    if (AddPlateNewSamples(dtoWorklist, destPlateId) == "SUCCESS")
                    {
                        ////Modify StartPos and EndPos
                        //destPlateStartPos = dtoWorklist.FirstOrDefault().DestWellId.ToString();
                        //destPlateEndPos = dtoWorklist.LastOrDefault().DestWellId.ToString();
                        //CurValidPlates.Where(w => w.PlateId == destPlateId).ToList().ForEach(s =>
                        //{
                        //    s.StartWell = destPlateStartPos;
                        //    s.EndWell = destPlateEndPos;
                        //});

                        processResult = "SUCCESS";
                    }
                }
            }

            return processResult;
        }

        private string ProcessNewSourceHisDest(string sourcePlateId, string destPlateId)
        {
            //NewSource => HistoryDestination
            string processResult = "NA";

            ClientBackend backService = new ClientBackend();
            InputPlate destPlate = new InputPlate();
            List<DtoWorklist> dtoWorklist = new List<DtoWorklist>();

            //To Find New Source Plate
            var findPlate = CurValidPlates.Where(pl => pl.PlateId == sourcePlateId).FirstOrDefault();

            //Check New Source Plate
            //Process History Dest Plate
            if (findPlate.IsNewCreatedPlate)
            {
                dtoWorklist = ProcessHisDestPlate(sourcePlateId, destPlateId);
                if (dtoWorklist != null && dtoWorklist.Count > 0)
                {
                    if (AddPlateNewSamples(dtoWorklist, destPlateId) == "SUCCESS")
                    {
                        processResult = "SUCCESS";
                    }
                }
            }

            return processResult;
        }

        private string ProcessHisSourceNewDest(string sourcePlateId, string destPlateId)
        {
            //HistorySource => NewDestination
            string processResult = "NA";

            ClientBackend backService = new ClientBackend();
            InputPlate sourcePlate = new InputPlate();
            InputPlate destPlate = new InputPlate();
            List<InputFile> plateSamples = new List<InputFile>();

            List<DtoWorklist> dtoWorklist = new List<DtoWorklist>();

            string hasSourcePlate = "NA";
            string hasSamples = "NA";

            //Process History Source plate
            var findSourcePlate = CurValidPlates.Where(pl => pl.PlateId == sourcePlateId).FirstOrDefault();
            if (findSourcePlate != null)
            {
                //Get History Source Plate
                sourcePlate = GetHisDestPlate(findSourcePlate);

                //Create Source Plate
                hasSourcePlate = backService.CreatePlate(sourcePlate);

                //Add SourceHistory Plate samples and new plate samples
                if (hasSourcePlate == "YES")
                {
                    plateSamples = GetPlateInSamples("SOURCE", sourcePlateId, sourcePlate.Rotated);
                    //plateSamples = GetSourcePlateSamples(sourcePlateId);

                    hasSamples = backService.AddSamples(sourcePlateId, plateSamples);
                    if (!string.IsNullOrEmpty(backService.ErrMsg))
                    {
                        txbPrompt.ForeColor = Color.DarkRed;
                        txbPrompt.Text = backService.ErrMsg;
                    }
                }
            }

            //Process new destination Plate
            dtoWorklist = ProcessNewDestPlate(sourcePlateId, destPlateId);
            if (dtoWorklist != null && dtoWorklist.Count > 0)
            {
                if (AddPlateNewSamples(dtoWorklist, destPlateId) == "SUCCESS")
                {
                    processResult = "SUCCESS";
                }
            }

            return processResult;
        }

        private string ProcessHisSourceHisDest(string sourcePlateId, string destPlateId)
        {
            //History Source => History Destintion
            string processResult = "NA";

            ClientBackend backService = new ClientBackend();
            InputPlate sourcePlate = new InputPlate();
            InputPlate destPlate = new InputPlate();
            List<InputFile> plateSamples = new List<InputFile>();

            List<DtoWorklist> dtoWorklist = new List<DtoWorklist>();

            string hasSourcePlate = "NA";
            string hasSamples = "NA";

            //Process History Source Plate
            var findSourcePlate = CurValidPlates.Where(pl => pl.PlateId == sourcePlateId).FirstOrDefault();
            if (findSourcePlate != null)
            {
                //Get History Source Plate
                sourcePlate = GetHisSourcePlate(findSourcePlate);
                //sourcePlate = GetHisDestPlate(findSourcePlate);

                //Create Source Plate
                hasSourcePlate = backService.CreatePlate(sourcePlate);

                //Add SourceHistory Plate samples and new plate samples

                if (hasSourcePlate == "YES")
                {
                    plateSamples = GetPlateInSamples("SOURCE", sourcePlateId, sourcePlate.Rotated);
                    //plateSamples = GetSourcePlateSamples(sourcePlateId);

                    hasSamples = backService.AddSamples(sourcePlateId, plateSamples);
                    if (!string.IsNullOrEmpty(backService.ErrMsg))
                    {
                        txbPrompt.ForeColor = Color.DarkRed;
                        txbPrompt.Text = backService.ErrMsg;
                    }
                }
            }

            //Process History Destination Plate
            dtoWorklist = ProcessHisDestPlate(sourcePlateId, destPlateId);
            if (dtoWorklist != null && dtoWorklist.Count > 0)
            {
                if (AddPlateNewSamples(dtoWorklist, destPlateId) == "SUCCESS")
                {
                    processResult = "SUCCESS";
                }
            }

            return processResult;
        }

        private List<InputFile> GetPlateInSamples(string SourceDest, string plateId, bool isRotated = false)
        {
            List<InputFile> inSamples = new List<InputFile>();
            InputFile inSmp = new InputFile();
            List<PlateSample> samples = new List<PlateSample>();

            string wx = "";
            string wy = "";

            try
            {
                samples = CurPlateSamples.Where(p => p.PlateId.ToUpper() == plateId.ToUpper()).ToList();
                if (samples != null && samples.Count > 0)
                {
                    foreach (var smp in samples)
                    {
                        if ((SourceDest == "DEST" && smp.SampleType != "QCEND") || (SourceDest == "SOURCE" && smp.SampleType.IndexOf("QC") < 0))
                        {
                            inSmp = new InputFile();
                            inSmp.ShortId = smp.SampleId;    // scanner read sample label
                            inSmp.RackName = smp.PlateId;

                            if (isRotated)
                            {
                                wx = pAlpha.IndexOf(smp.Well.Substring(0, 1)).ToString();
                                wy = smp.Well.Substring(1);
                            }
                            else
                            {
                                wx = $"{int.Parse(smp.Well.Substring(1)) - 1}";
                                wy = pAlpha.IndexOf(smp.Well.Substring(0, 1)).ToString();
                            }
                            inSmp.Well = wx + "," + wy;
                            inSmp.WellX = wx;
                            inSmp.WellY = wy;

                            //inSmp.Position = smp.Well;

                            inSamples.Add(inSmp);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
            }

            return inSamples;
        }

        private List<InputFile> GetSourcePlateSamples(string sourcePlateId)
        {
            //Get source history plate samples and new plates samples
            List<InputFile> inputSamples = new List<InputFile>();
            InputFile sample = new InputFile();
            try
            {
                //Get samples from the history plate
                var hisPlateSamples = CurPlateSamples.Where(sm => sm.PlateId == sourcePlateId).ToList();
                if (hisPlateSamples != null && hisPlateSamples.Count > 0)
                {
                    foreach (var hSmp in hisPlateSamples)
                    {
                        sample = new InputFile();
                        sample.ShortId = hSmp.SampleId;    // scanner read sample label
                        sample.RackName = hSmp.PlateId;
                        sample.Position = hSmp.Well;
                        inputSamples.Add(sample);
                    }
                }

                //Get samples from the new plate created
                var newPlateSamples = OutPlateSamples.Where(sm => sm.DestPlateId == sourcePlateId).ToList();
                if (newPlateSamples != null && newPlateSamples.Count > 0)
                {
                    foreach (var nSmp in newPlateSamples)
                    {
                        sample = new InputFile();
                        sample.ShortId = nSmp.SampleId;    // scanner read sample label
                        sample.RackName = nSmp.DestPlateId;
                        sample.Position = nSmp.DestWellId;
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

        private string AddPlateNewSamples(List<DtoWorklist> outWorklist, string destPlateId)
        {
            //Add Plate new samples
            string actionResult = "SUCCESS";
            OutputPlateSample newSample = new OutputPlateSample();

            try
            {
                OutPlateSamples.RemoveAll(osp => osp.DestPlateId == destPlateId);

                foreach (var smp in outWorklist)
                {
                    newSample = new OutputPlateSample();
                    newSample.SourcePlateId = smp.SourcePlateId;

                    if (smp.SourcePlateId.IndexOf("BCR") >= 0 && smp.SourceWellId.IndexOf(",") > 0)
                    {
                        newSample.SourceWellId = GetWell(smp.SourceWellId, isBCR: true);
                    }
                    else if (smp.SourceWellId.IndexOf(",") > 0)
                    {
                        newSample.SourceWellId = GetWell(smp.SourceWellId, isBCR: false);
                    }
                    else
                    {
                        newSample.SourceWellId = smp.SourceWellId;
                    }

                    newSample.SourcePlateId = smp.SourcePlateId == null ? "" : smp.SourcePlateId;

                    newSample.SourceId = smp.SourceIndex;
                    newSample.DestId = smp.DestIndex;

                    newSample.DestPlateId = smp.DestPlateId;
                    newSample.DestWellId = smp.DestWellId.IndexOf(",") > 0 ? GetWell(smp.DestWellId, isBCR: false) : smp.DestWellId;
                    newSample.Attributes = smp.Attributes;
                    newSample.Accept = smp.Attributes["Accept"];

                    //if (string.IsNullOrEmpty(smp.Attributes["SampleId"]))
                    //{
                    //    newSample.SampleId = "blank";
                    //}
                    //else
                    //{
                    //    newSample.SampleId = smp.Attributes["SampleId"];
                    //}
                    newSample.SampleId = smp.Attributes["SampleId"];

                    OutPlateSamples.Add(newSample);
                }

            }
            catch (Exception ex)
            {
                actionResult = "ERROR";
                string errMsg = ex.Message;
            }

            return actionResult;
        }

        private string GetExcludeWells(string plateSizeStart, string plateSizeEnd, string startPos, string endPos)
        {
            List<string> plateWells = new List<string>();
            string excludeWells = "";

            try
            {
                Char sizeStartY = char.Parse(plateSizeStart[0].ToString());
                int sizeStartX = Convert.ToInt32(plateSizeStart.Substring(1));
                Char sizeEndY = char.Parse(plateSizeEnd[0].ToString());
                int sizeEndX = Convert.ToInt32(plateSizeEnd.Substring(1));

                Char posStartY = char.Parse(startPos[0].ToString());
                int posStartX = Convert.ToInt32(startPos.Substring(1));
                char posEndY = char.Parse(endPos[0].ToString());
                int posEndX = Convert.ToInt32(endPos.Substring(1));

                if (posStartY >= sizeStartY && posEndX <= sizeEndX && posEndY <= sizeEndY)
                {
                    for (int x9 = sizeStartX; x9 <= sizeEndX; x9++)
                    {
                        for (Char yA = sizeStartY; yA <= sizeEndY; yA++)
                        {
                            plateWells.Add(yA + x9.ToString());
                        }
                    }

                    foreach (var well in plateWells)
                    {
                        excludeWells = excludeWells + well.ToString();
                        if (well == endPos)
                        {
                            break;
                        }
                        else
                        {
                            excludeWells = excludeWells + "|";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = "GetExcludeWells() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }

            return excludeWells;
        }

        private InputPlate GetHisSourcePlate(ValidPlate validPlate)
        {
            //Get the history plates as SourcePlate, and modify it

            InputPlate newDestPlate = new InputPlate();
            string sizeStart = "";
            string sizeEnd = "";
            string startPos = "";
            string endPos = "";
            string excludeWells = "";

            if (validPlate != null)
            {
                sizeStart = validPlate.SizeStartWell;
                sizeEnd = validPlate.SizeEndWell;
                startPos = validPlate.StartWell;
                endPos = validPlate.EndWell;

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
                newDestPlate.Name = validPlate.PlateId;
                newDestPlate.Rotated = validPlate.Rotated;

                excludeWells = validPlate.ExcludeWells;
                //excludeWells = GetExcludeWells(sizeStart, sizeEnd, startPos, endPos);
                newDestPlate.Exclude = excludeWells.Split('|').Distinct().Select(WellToPosition).ToList();

                newDestPlate.Direction = "0";
                newDestPlate.Attributes = validPlate.Attributes;
            }

            return newDestPlate;
        }

        private InputPlate GetHisDestPlate(ValidPlate validPlate)
        {
            //Get the history plates as destination, and modify it

            InputPlate newDestPlate = new InputPlate();
            string sizeStart = "";
            string sizeEnd = "";
            string startPos = "";
            string endPos = "";
            string excludeWells = "";

            if (validPlate != null)
            {
                sizeStart = validPlate.SizeStartWell;
                sizeEnd = validPlate.SizeEndWell;
                startPos = validPlate.SizeStartWell;
                endPos = validPlate.SizeEndWell;
                //startPos = validPlate.StartWell;
                //endPos = validPlate.EndWell;

                int startY = pAlpha.IndexOf(startPos.Substring(0, 1));
                int startX = Convert.ToInt32(startPos.Substring(1)) - 1;
                //int startX = Convert.ToInt32(startPos.Substring(1));

                int endY = pAlpha.IndexOf(endPos.Substring(0, 1));
                int endX = Convert.ToInt32(endPos.Substring(1)) - 1;
                //int endX = Convert.ToInt32(endPos.Substring(1));

                Pos sPos = new Pos();
                Pos ePos = new Pos();

                sPos.X = startX.ToString();
                sPos.Y = startY.ToString();
                ePos.X = endX.ToString();
                ePos.Y = endY.ToString();

                newDestPlate.Rotated = validPlate.Rotated;
                newDestPlate.Start = sPos;
                newDestPlate.End = ePos;
                newDestPlate.Name = validPlate.PlateId;
                newDestPlate.Exclude = validPlate.ExcludeWells.Split('|').Distinct().Select(WellToPosition).ToList();
                //newDestPlate.Offset = validPlate.Offset;
                //excludeWells = GetExcludeWells(sizeStart, sizeEnd, startPos, endPos);
                //newDestPlate.Exclude = excludeWells.Split('|').Distinct().Select(WellToPosition).ToList();

                newDestPlate.Direction = "0";

                if (!string.IsNullOrEmpty(validPlate.Attributes["Accept"]))
                {
                    validPlate.Attributes["Accept"] = validPlate.Attributes["Accept"].Replace("|", ",");
                }

                newDestPlate.Attributes = new Dictionary<string, string>(validPlate.Attributes);
            }

            return newDestPlate;
        }

        private InputPlate GetDestPlateProperties(string plateId)
        {
            InputPlate getPlate = new InputPlate();
            string startPos = "";
            string endPos = "";

            if (ProtocolPlates.Count > 0)
            {
                var findPlate = ProtocolPlates.Where(pp => string.Compare(pp.DestPlateId.ToUpper(), plateId.ToUpper(), true) == 0).FirstOrDefault();
                startPos = findPlate.StartPos;
                endPos = findPlate.EndPos;

                int startY = pAlpha.IndexOf(startPos.Substring(0, 1));   //0-Base
                int startX = Convert.ToInt32(startPos.Substring(1)) - 1;
                //int startY = pAlpha.IndexOf(startPos.Substring(0, 1)) + 1;    //1-Base
                //int startX = Convert.ToInt32(startPos.Substring(1));

                int endY = pAlpha.IndexOf(endPos.Substring(0, 1));  //0-Base
                int endX = Convert.ToInt32(endPos.Substring(1)) - 1;
                //int endY = pAlpha.IndexOf(endPos.Substring(0, 1)) + 1;    //1-Base
                //int endX = Convert.ToInt32(endPos.Substring(1));


                //offset = (startY) + ((startX - 1) * PlateSizeY);    //0-Base


                Pos sPos = new Pos();
                Pos ePos = new Pos();

                sPos.X = startX.ToString();
                sPos.Y = startY.ToString();
                ePos.X = endX.ToString();
                ePos.Y = endY.ToString();

                getPlate.Start = sPos;
                getPlate.End = ePos;
                getPlate.Name = findPlate.DestPlateId;
                getPlate.Exclude = findPlate.ExcludeWells.Split('|').Distinct().Select(WellToPosition).ToList();

                getPlate.Direction = "0";
                getPlate.Attributes = findPlate.Attributes;

            }

            return getPlate;
        }


        private InputPlate LoadBCRSamples(string importFile)
        {
            InputPlate bcrPlate = new InputPlate();

            try
            {
                importFile = CurRunBuilder.BCROutput + importFile;

                List<InputFile> rawValues = File.ReadAllLines(importFile)
                    .Skip(1)
                    .Select(v => InputFile.ReadInputFile(v))
                    .ToList();

                if (rawValues != null && rawValues.Count > 0)
                {
                    foreach (var smp in rawValues)
                    {
                        InputFileValues.Add(new InputFile
                        {
                            FullSampleId = smp.FullSampleId,
                            ShortId = smp.ShortId,
                            RackName = "BCR",
                            Position = smp.Position,
                            WellX = smp.Position,       //wX.ToString(),
                            WellY = "0",                // wY.ToString(),
                            PlateId = ""
                        });
                    }

                    var startSample = InputFileValues.FirstOrDefault();
                    var endSample = InputFileValues.LastOrDefault();

                    Pos startPos = new Pos();
                    Pos endPos = new Pos();

                    startPos.X = startSample.WellX;
                    startPos.Y = startSample.WellY;
                    endPos.X = endSample.WellX;
                    endPos.Y = endSample.WellY;

                    bcrPlate.Start = startPos;
                    bcrPlate.End = endPos;
                    bcrPlate.Name = "BCR" + CurUniqueId;
                    bcrPlate.Direction = "0";
                }
            }
            catch (Exception ex)
            {
                string errMsg = "ImportBCRPlate() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }

            return bcrPlate;
        }

        private string SaveCreateWorklists()
        {
            string actionResult = "NA";
            string actionSaveToDB = "SUCCESS";

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
                        //Save Plate to DB
                        actionSaveToDB = SaveToDB();

                        if (actionSaveToDB == "SUCCESS")
                        {
                            lblMsg.ForeColor = Color.ForestGreen;
                            lblMsg.Text = "The Worklist(s) created.";
                        }
                        else
                        {
                            lblMsg.ForeColor = Color.Red;
                            lblMsg.Text = "To Save Plate and samples, create Worklist(s) met some issues.";
                        }

                        actionResult = "SUCCESS";
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

            return actionResult;
        }

        private Pos WellToPosition(string input)
        {
            var Y = (int)input[0] - (int)'A';
            var X = int.Parse(input.Substring(1)) - 1;
            return new Pos() { X = X.ToString(), Y = Y.ToString() };
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //rwPlate.Cells["wlReady"].Value = false;
            //rwPlate.Cells["ProcessedDB"].Value = 1;

            Application.Exit();
        }


        public string WriteWorklist_2(List<OutputPlateSample> outSamples, OutputPlate outPlate)
        {
            string actionResult = "NA";
            string exportPath = "";


            var columnMap = outSamples.SelectMany(x => x.Attributes.Keys)
                .Concat(outPlate.Attributes.Keys)
                .Distinct()
                .ToDictionary(x => x, y => y);

            columnMap.Add("SourcePlateId", "SourcePlateId");
            columnMap.Add("SourceWellId", "SourceWellId");
            columnMap.Add("SourceIndex", "SourceIndex");
            columnMap.Add("DestPlateId", "DestPlateId");
            columnMap.Add("DestWellId", "DestWellId");
            columnMap.Add("DestIndex", "DestIndex");

            try
            {
                exportPath = CurRunBuilder.RunBuilderOutput + outPlate.WorkList;

                var output = new List<Dictionary<string, string>>();
                using (var writer = new StreamWriter(exportPath))
                {
                    foreach (var sample in outSamples)
                    {
                        var plateValues = new Dictionary<string, string>(columnMap.Keys.ToDictionary(x => x, y => string.Empty));

                        plateValues["SourcePlateId"] = sample.SourcePlateId;
                        plateValues["SourceWellId"] = sample.SourceWellId;
                        plateValues["SourceIndex"] = sample.SourceId;
                        plateValues["DestPlateId"] = sample.DestPlateId;
                        plateValues["DestWellId"] = sample.DestWellId;
                        plateValues["DestIndex"] = sample.DestId;

                        foreach (var kvp in outPlate.Attributes)
                        {
                            plateValues[kvp.Key] = kvp.Value;
                        }

                        foreach (var kvp in sample.Attributes)
                        {
                            // todo: should overwrite?
                            plateValues[kvp.Key] = string.IsNullOrWhiteSpace(plateValues[kvp.Key]) ? kvp.Value : plateValues[kvp.Key];
                        }

                        output.Add(plateValues);
                    }

                    var outputColumns = columnMap.Keys
                        .Where(column => output.Select(record => record[column])
                            .Any(value => string.IsNullOrWhiteSpace(value) == false))
                        .ToList();

                    writer.WriteLine(string.Join(",", outputColumns));

                    foreach (var record in output)
                    {
                        writer.WriteLine(string.Join(",", outputColumns.Select(x => record[x])));
                    }
                }

                actionResult = "SUCCESS";
            }
            catch (Exception ex)
            {
                string errMsg = "{WriteWorklist()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                actionResult = "ERROR:" + errMsg;

                txbPrompt.ForeColor = Color.DarkRed;
                txbPrompt.Text = errMsg;
            }

            return actionResult;
        }
        private void btnCreateManualPlate_Click(object sender, EventArgs e)
        {
            string resultAddPlate = "";
            string resultAddSamples = "";
            string plateNewSerialNo = "";
            string curPlateTimeVersion = "";
            string mapPlateResult = "";
            List<DBPlate> dbPlates = new List<DBPlate>();

            DBPlate newDestPlate = new DBPlate();
            RepoSQL sqlService = new RepoSQL();
            List<OutputPlateSample> outSamples = new List<OutputPlateSample>();
            ModelTransfer transModel = new ModelTransfer();
            try
            {
                if (ScannedDBPalte != null && !string.IsNullOrEmpty(txbManualPlateId.Text.Trim()))
                {
                    //Check ManualPlateId
                    dbPlates = sqlService.GetPlates(txbManualPlateId.Text.Trim());
                    if (dbPlates == null || dbPlates.Count == 0)
                    {
                        curPlateTimeVersion = $"{DateTime.Now.ToString("yyMMddHHmmss")}";
                        plateNewSerialNo = sqlService.GetSeries();

                        ScannedDBPalte.SourcePlateId = ScannedDBPalte.PlateId;
                        ScannedDBPalte.SourcePlateVersion = ScannedDBPalte.PlateVersion;

                        //ScannedDBPalte.PlateId = ScannedDBPalte.PlateName + plateNewSerialNo;
                        ScannedDBPalte.PlateId = txbManualPlateId.Text.Trim();
                        ScannedDBPalte.PlateVersion = curPlateTimeVersion;
                        ScannedDBPalte.PlateDesc = string.IsNullOrEmpty(txbManualPlateDesc.Text.Trim()) ? "" : txbManualPlateDesc.Text.Trim();

                        ScannedDBPlateSamples.ForEach(s =>
                            {
                                s.SourcePlateId = s.PlateId;
                                s.SourceWell = s.Well;
                                s.SourcePlateVersion = s.PlateVersion;
                                s.PlateId = ScannedDBPalte.PlateId;
                                s.PlateVersion = curPlateTimeVersion;
                            });

                        outSamples = transModel.DBSample2Outputs(ScannedDBPlateSamples);

                        resultAddPlate = sqlService.AddPlate(ScannedDBPalte, Environment.UserName);
                        if (resultAddPlate == "SUCCESS")
                        {
                            pCurMapPlateId = ScannedDBPalte.PlateId;
                            resultAddSamples = sqlService.AddSamples(outSamples, Environment.UserName);
                        }
                        else
                        {
                            lblMsg.ForeColor = Color.DarkRed;
                            lblMsg.Text = resultAddSamples;
                            btnPrintManuPlate.Enabled = false;
                        }

                        if (resultAddSamples == "SUCCESS")
                        {
                            mapPlateResult = GetMapAnyPlateSamples(ScannedDBPalte.PlateId);

                            if (mapPlateResult == "SUCCESS")
                            {
                                lblMsg.ForeColor = Color.DarkBlue;
                                lblMsg.Text = "Manual Plate, " + ScannedDBPalte.PlateId + ", Created";
                                btnPrintManuPlate.Enabled = true;
                            }
                            else
                            {
                                txbPrompt.Text = mapPlateResult;
                                btnPrintManuPlate.Enabled = false;
                            }
                        }
                        else
                        {
                            lblMsg.ForeColor = Color.DarkRed;
                            lblMsg.Text = resultAddSamples;
                            btnPrintManuPlate.Enabled = false;
                        }
                    }
                    else
                    {
                        //ManualPlateId is exiting
                        btnPrintManuPlate.Enabled = false;

                        MessageBox.Show("The PlateId," + txbManualPlateId.Text + ", already exists." +
                                        Environment.NewLine +
                                        "Please enter a new PlateId.",
                                        "Manual Plate Id",
                                        MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = "CreatingManualPlate() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                lblMsg.ForeColor = Color.DarkRed;
                lblMsg.Text = errMsg;
            }
            finally
            {
                txbBarcode.Text = "";
                txbBarcode.Focus();

            }
        }
        private void btnPrintManuPlate_Click(object sender, EventArgs e)
        {
            string barCode = "";

            if (!string.IsNullOrEmpty(txbManualPlateId.Text.Trim()))
            {
                barCode = txbManualPlateId.Text.Trim();
                PrinBarCodeZXing printPlateBarcode = new PrinBarCodeZXing();
                printPlateBarcode.Print(barCode, GetPrinterName());
            }

            txbBarcode.Text = "";
            txbBarcode.Focus();
        }
        private void btnCloseMapping_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            lblMsg.ForeColor = Color.DarkBlue;

            btnCreateManualPlate.Enabled = false;
            btnPrintManuPlate.Enabled = false;

            dgvSamplePlate.Rows.Clear();
            dgvSamplePlate.Columns.Clear();
            dgvSamplePlate.Refresh();

            this.Size = new Size(1280, 390);

            //Default Size, 1280,960

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool hasWorklist = true;
            bool didSavePlate = true;

            foreach (DataGridViewRow rwPlate in dgvPlateSet.Rows)
            {
                if (Convert.ToBoolean(rwPlate.Cells["Included"].Value))
                {

                    hasWorklist = Convert.ToBoolean(rwPlate.Cells["wlReady"].Value);
                    didSavePlate = Convert.ToBoolean(rwPlate.Cells["ProcessedDB"].Value);

                    if (!hasWorklist || !didSavePlate)
                    {
                        break;
                    }
                }
            }

            if (!hasWorklist || !didSavePlate)
            {
                DialogResult dialogResult = MessageBox.Show("Current session did not create correct worklist(s)." +
                                                            Environment.NewLine +
                                                            Environment.NewLine +
                                                            "DDIRunBuilder must be running to create Janus worklists." +
                                                            Environment.NewLine +
                                                            Environment.NewLine +
                                                            "Do you want to close current session ?",
                                                            "Close application?",
                                                            MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }



        }

        private void btnQC_Click(object sender, EventArgs e)
        {

            try
            {
                frmQC addQC = new frmQC();
                if (string.IsNullOrEmpty(pCurMapPlateId))
                {
                    lblMsg.Text = "There is no an available plate.";
                }
                else
                {
                    addQC.PlateId = pCurMapPlateId;
                    addQC.CurExportPath = CurRunBuilder.RunBuilderExport;
                    addQC.ShowDialog();
                }


            }
            catch (Exception ex)
            {
                string errMsg = "btnQC_Click() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                lblMsg.ForeColor = Color.DarkRed;
                lblMsg.Text = errMsg;
            }

        }

        private void btnSampleStatus_Click(object sender, EventArgs e)
        {
            try
            {
                frmSampleStatus sampleStatus = new frmSampleStatus();
                sampleStatus.AllProtocols = pProtocol;
                sampleStatus.ShowDialog();

            }
            catch (Exception ex)
            {
                string errMsg = "btnSampleStatus_Click() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                lblMsg.ForeColor = Color.DarkRed;
                lblMsg.Text = errMsg;
            }
        }

       
    }
}
