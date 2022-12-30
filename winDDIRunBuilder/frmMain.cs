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
        public int PlateSizeX { get; set; } = 12;
        public int PlateSizeY { get; set; } = 8;
        public int BCRSizeX { get; set; } = 6;
        public int BCRSizeY { get; set; } = 16;
        public ClientRunBuilder CurRunBuilder { get; set; }
        public List<InputFile> InputFileValues { get; set; }
        public List<ValidPlate> CurValidPlates { get; set; }
        public List<OutputPlateSample> OutPlateSamples { get; set; }
        public List<ProtocolPlate> ProtocolPlates { get; set; } = new List<ProtocolPlate>();

        private List<Protocol> pProtocol = new List<Protocol>();
        private List<Plate> PlateInputs { get; set; }
        private List<Plate> PostPlates { get; set; }

        private string CurUniqueId { get; set; } = "";
        private string pAlpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
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
                CurValidPlates = new List<ValidPlate>();
                OutPlateSamples = new List<OutputPlateSample>();
                InputFileValues = new List<InputFile>();
                PlateInputs = new List<Plate>();
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
            }
            catch (Exception ex)
            {
                string errMsg = "{frmMain_Load} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;

                lblPrompt.ForeColor = Color.DarkRed;
                lblPrompt.Text = errMsg;
            }
        }

        private void cbProtocolCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbPlates.Items.Clear();
            cbPlates.Text = "";
            string[] platePos;
            

            try
            {
                //CurUniqueId = $"{DateTime.Now.ToString("yyMMddHHmm")}";
                //CurUniqueId = $"{DateTime.Now.ToString("yyMMddffff")}";
                //dgvPlateSet.ReadOnly = false;

                if (pIsFrmLoaded && cbProtocolCd.SelectedValue != null && cbProtocolCd.SelectedValue.ToString().Length > 0)
                {
                    //Get current sesion serial No.
                    RepoSQL sqlService = new RepoSQL();
                    CurUniqueId= sqlService.GetSeries();

                    //Delete Previous worklists and BCR files
                    foreach (string csvFile in Directory.GetFiles(CurRunBuilder.ExportFilePath, "*.csv"))
                    {
                        //Delete worklist files
                        File.Delete(csvFile);
                    }

                    //foreach (string csvFile in Directory.GetFiles(CurRunBuilder.ReadFilePath, "*.csv"))
                    //{
                    //    //Delete BCR files
                    //    File.Delete(csvFile);
                    //}


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
                            cbPlates.Items.Add(pp.PlateId + CurUniqueId);
                            //platePos = new string[];
                            platePos = PlateProperties(pp.StartPos, pp.EndPos);

                            var newPlate = new ProtocolPlate
                            {
                                Id = pp.Id,
                                ProtocolName = pp.ProtocolName,
                                DestPlateId = pp.PlateId + CurUniqueId,
                                DestPlateName = pp.PlateId,
                                SourcePlateId = pp.SourcePlate + CurUniqueId,
                                SourcePlateName = pp.SourcePlate,
                                WorklistName = pp.WorklistName,
                                //WorklistName = pp.PlateId + ".CSV",
                                StartPos = pp.StartPos,
                                EndPos = pp.EndPos,
                                PlateRotated = pp.PlateRotated,
                                Offset = platePos[0],
                                StartX = platePos[1],
                                StartY = platePos[2],
                                EndX = platePos[3],
                                EndY = platePos[4],
                                Accept=pp.DBTest,
                                Sample=pp.Sample,
                                Diluent=pp.Diluent
                            };

                            if (!string.IsNullOrWhiteSpace(pp.Sample.ToString()))
                            {
                                newPlate.Attributes.Add("Sample", pp.Sample.ToString());
                            }

                            if (!string.IsNullOrWhiteSpace(pp.Diluent.ToString()))
                            {
                                newPlate.Attributes.Add("Diluent", pp.Diluent.ToString());
                            }

                            if (!string.IsNullOrWhiteSpace(pp.DBTest.ToString()))
                            {
                                newPlate.Attributes.Add("Accept", pp.DBTest.ToString());
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

                            if (!string.IsNullOrWhiteSpace(pp.etc.ToString()))
                            {
                                newPlate.Attributes.Add("etc", pp.etc.ToString());
                            }
                            else
                            {
                                newPlate.Attributes.Add("etc", "");
                            }

                            ProtocolPlates.Add(newPlate);
                        }
                    }

                    SetProtocolPlates();

                    //set default plate
                    pSelectedPlatePage = (string)cbPlates.SelectedItem;

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

        private string AddVaildPlate(bool isNewPlate, InputPlate inputPlate = null, ValidPlate hisPlate = null)
        {
            string actionResult = "SUCCCESS";
            bool isBCRPlate = true;

            if (inputPlate.Name.IndexOf("BCR") < 0)
                isBCRPlate = false;
            
            if (isNewPlate && inputPlate != null && isBCRPlate)
            {
                CurValidPlates.Add(new ValidPlate
                {
                    PlateId = inputPlate.Name,
                    PlateName = "",
                    StartWell = "",
                    EndWell = "",
                    StartX = "",
                    StartY = "",
                    EndX = "",
                    EndY = "",
                    Offset = "0",
                    Direction = "0",
                    SourcePlateId = "",
                    WorkList = "",
                    IsNewCreatedPlate = isNewPlate
                });
            }
            else if(isNewPlate && inputPlate != null)
            {
                var findPlate = ProtocolPlates.Where(pp => string.Compare(pp.DestPlateId.ToUpper(), inputPlate.Name.ToUpper(), true) == 0).FirstOrDefault();

                CurValidPlates.Add(new ValidPlate
                {
                    PlateId = inputPlate.Name,
                    PlateName = findPlate.DestPlateName,
                    StartWell = findPlate.StartPos,
                    EndWell = findPlate.EndPos,
                    StartX = inputPlate.Start.X,
                    StartY = inputPlate.Start.Y,
                    EndX = inputPlate.End.X,
                    EndY = inputPlate.End.Y,
                    Offset = inputPlate.Offset,
                    Direction = inputPlate.Direction,
                    Attributes = inputPlate.Attributes,
                    SourcePlateId = findPlate.SourcePlateId,
                    SourcePlateVersion = "1",
                    WorkList = findPlate.WorklistName,
                    Sample=findPlate.Sample,
                    Diluent=findPlate.Diluent,
                    Accept=findPlate.Accept,
                    PlateVersion = "1",
                    IsNewCreatedPlate = isNewPlate
                });
            }
            
            return actionResult;
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
                var plateSet = ProtocolPlates.Where(p => p.DestPlateId == pSelectedPlatePage).FirstOrDefault();
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
            int endY = 0;
            int endX = 0;
            int offset = 0;

            startY = pAlpha.IndexOf(startPos.Substring(0, 1));   //0-Base
            startX = Convert.ToInt32(startPos.Substring(1)) - 1;
            endY = pAlpha.IndexOf(endPos.Substring(0, 1));  //0-Base
            endX = Convert.ToInt32(endPos.Substring(1)) - 1;
            offset = (startY) + ((startX - 1) * PlateSizeY);    //0-Base

            //int startY = pAlpha.IndexOf(startPos.Substring(0, 1)) + 1;    //1-Base
            //int startX = Convert.ToInt32(startPos.Substring(1));
            //int endY = pAlpha.IndexOf(endPos.Substring(0, 1)) + 1;    //1-Base
            //int endX = Convert.ToInt32(endPos.Substring(1));
            //offset = (startY-1) + ((startX - 1) * PlateSizeY);    //1-Base

            plateProperties = new string[] { offset.ToString().Trim(),
                                            startX.ToString().Trim(),
                                            startY.ToString().Trim(),
                                            endX.ToString().Trim(),
                                            endY.ToString().Trim()};

            return plateProperties;
        }

        private string SavePlatesToDB()
        {
            string actionResult = "";

            List<OutputPlateSample> outSamples = new List<OutputPlateSample>();
            DBPlate dbPlate = new DBPlate();

            string worklist = "";
            string sourcePlate = "";
            string destPlate = "";

            RepoSQL sqlService = new RepoSQL();
            string resultSaveDB = "NV";

            try
            {
                if (dgvPlateSet.RowCount > 0)
                {
                    foreach (DataGridViewRow rwPlate in dgvPlateSet.Rows)
                    {
                        if (Convert.ToBoolean(rwPlate.Cells["Included"].Value) == true)
                        {
                            //outSamples = new List<OutputPlateSample>();
                            dbPlate = new DBPlate(); 

                            sourcePlate = rwPlate.Cells["SourcePlate"].Value.ToString();
                            destPlate = rwPlate.Cells["DestPlate"].Value.ToString().ToUpper();
                            worklist = rwPlate.Cells["CurWorkList"].Value.ToString();

                            var validPlate = CurValidPlates.Where(pp => pp.PlateId.ToUpper() == destPlate).FirstOrDefault();
                            //outSamples = (List<OutputPlateSample>)OutPlateSamples.Where(ps => ps.DestPlateId.ToUpper() == destPlate).ToList();

                            dbPlate.PlateName = validPlate.PlateName;
                            dbPlate.PlateId = destPlate;
                            dbPlate.SourcePlateId = sourcePlate;
                            dbPlate.SourcePlateVersion = validPlate.SourcePlateVersion;
                            dbPlate.StartPos= validPlate.StartWell;
                            dbPlate.EndPos = validPlate.EndWell;
                            dbPlate.Sample = validPlate.Sample;
                            dbPlate.Diluent = validPlate.Diluent;
                            dbPlate.PlateVersion = validPlate.PlateVersion;
                            dbPlate.OffSet = validPlate.Offset;
                            dbPlate.Accept = validPlate.Accept;
                            resultSaveDB =sqlService.AddPlate(dbPlate);

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

            //var people = new List<Person>(); 
            //people.Add(new Person { Surname = "Matt", Forename = "Abbott" });
            //people.Add(new Person { Surname = "John", Forename = "Smith" });
            //WriteCSV(people, @"C:\Users\sli\Documents\Logs\people.csv");
        }

        private string CreateWorklist()
        {
            string actionResult = "";

            List<OutputPlateSample> outSamples = new List<OutputPlateSample>();
            OutputPlate outPlate = new OutputPlate();

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
                //todatDate = DateTime.Today.Date.ToString("yyMMddss");

                if (dgvPlateSet.RowCount > 0)
                {
                    foreach (DataGridViewRow rwPlate in dgvPlateSet.Rows)
                    {
                        if (Convert.ToBoolean(rwPlate.Cells["Included"].Value) == true)
                        {
                            outSamples = new List<OutputPlateSample>();
                            outPlate = new OutputPlate();

                            sourcePlate = rwPlate.Cells["SourcePlate"].Value.ToString();
                            destPlate = rwPlate.Cells["DestPlate"].Value.ToString().ToUpper();
                            worklist = rwPlate.Cells["CurWorkList"].Value.ToString();

                            var pplate = ProtocolPlates.Where(pp => pp.DestPlateId.ToUpper() == destPlate.ToUpper()).FirstOrDefault();
                            outSamples = (List<OutputPlateSample>)OutPlateSamples.Where(ps => ps.DestPlateId.ToUpper() == destPlate).ToList();

                            outPlate.Name = destPlate;
                            outPlate.SourcePlateId = sourcePlate;
                            outPlate.WorkList = worklist;
                            outPlate.StartWell = pplate.StartPos;
                            outPlate.EndWell = pplate.EndPos;

                            outPlate.Attributes = pplate.Attributes;

                            exportResult = WriteWorklist(outSamples, outPlate);
                            if (exportResult != "SUCCESS")
                            {
                                exportIssues += exportResult + "; ";
                            }




                            //newSourcePlate = new InputPlate();
                            //newDestPlate = new InputPlate();
                            //dtoWorklsit = new List<DtoWorklist>();

                            ////create sourcePlate
                            //newSourcePlate = new InputPlate();
                            ////var plateSet = ProtocolPlates.Where(p => p.PlateId == sourcePlate).FirstOrDefault();
                            //var plateSet = ProtocolPlates.Where(p => p.DestPlateId == destPlate).FirstOrDefault();
                            //if (plateSet != null)
                            //{
                            //    newSourcePlate.Name = sourcePlate + "-" + todatDate;
                            //    newSourcePlate.Offset = plateSet.Offset;
                            //    newSourcePlate.Start.X = plateSet.StartX;
                            //    newSourcePlate.Start.Y = plateSet.StartY;
                            //    newSourcePlate.End.X = plateSet.EndX;
                            //    newSourcePlate.End.Y = plateSet.EndY;
                            //    newSourcePlate.Direction = plateSet.PlateRotated == false ? "0" : "1";
                            //}
                            //if (sourcePlate != "BCR")
                            //{
                            //    newSourcePlate.Attributes.Accept = plateSet.Accept;
                            //}
                            //hasSourcePlate = backService.CreatePlate(newSourcePlate);

                            ////create destPlate
                            //newDestPlate = new InputPlate();
                            //newDestPlate.Name = sourcePlate + "-" + todatDate;
                            //newDestPlate.Offset = plateSet.Offset;
                            //newDestPlate.Start.X = plateSet.StartX;
                            //newDestPlate.Start.Y = plateSet.StartY;
                            //newDestPlate.End.X = plateSet.EndX;
                            //newDestPlate.End.Y = plateSet.EndY;
                            //newDestPlate.Direction = plateSet.PlateRotated == false ? "0" : "1";
                            //newDestPlate.Attributes.Accept = plateSet.Accept;
                            //hasDestPlate = backService.CreatePlate(newDestPlate);


                            ////Add samples to source plate
                            //if (hasSourcePlate == "YES" && hasDestPlate == "YES" && sourcePlate == "BCR")
                            //{
                            //    var smps = InputFileValues.Where(inp => inp.PlateId == destPlate).ToList();
                            //    if (smps != null && smps.Count > 0)
                            //    {
                            //        foreach (var smp in smps)
                            //        {
                            //            newSmpAttr = new SampleAttributes();
                            //            newSmpAttr.SampleId = smp.ShortId;
                            //            pltSmps.Add(new InputSample
                            //            {
                            //                Attributes = newSmpAttr
                            //            });
                            //        }
                            //    }

                            //    List<Plate> pltSamples = PostPlates.Where(smp => smp.PlateId == destPlate).ToList();

                            //    //
                            //    //hasSamples = backService.AddSamples(pltSamples);
                            //    //

                            //    if (hasSamples == "YES")
                            //    {
                            //        dtoWorklsit = (List<DtoWorklist>)backService.GetWorklist(sourcePlate, destPlate, "lookup_alias,skip_cancelled");
                            //        if (dtoWorklsit != null && dtoWorklsit.Count > 0)
                            //        {
                            //            plateSamples = new List<SamplePlate>();
                            //            foreach (var wl in dtoWorklsit)
                            //            {
                            //                plateSamples.Add(new SamplePlate
                            //                {
                            //                    SampleId = destPlate
                            //                    //SourceRack = smp.WellX + smp.WellY,
                            //                    //SourcePosition = smp.WellX + smp.WellY,
                            //                    //DestRack = smp.WellX + smp.WellY,
                            //                    //DestPosition = smp.WellX + smp.WellY,
                            //                    //Samples = samples.Count,
                            //                    //Diluent = wl.Diluent
                            //                });
                            //            }

                            //            exportResult = WriteSamplePlate(plateSamples, worklist);
                            //            if (exportResult != "SUCCESS")
                            //            {
                            //                exportIssues += exportResult + "; ";
                            //            }
                            //        }
                            //    }
                            //}

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
                lblPrompt.ForeColor = Color.DarkRed;
                lblPrompt.Text = errMsg;
            }

            return actionResult;

            //var people = new List<Person>(); 
            //people.Add(new Person { Surname = "Matt", Forename = "Abbott" });
            //people.Add(new Person { Surname = "John", Forename = "Smith" });
            //WriteCSV(people, @"C:\Users\sli\Documents\Logs\people.csv");
        }

        public string CreateEmptyWorklist(string worklistName)
        {
            string actionResult = "NA";
            string exportPath = "";
            string plateFileItems = "SampleID,SourceRack,SourcePosition,DestRack,DestPosition, Sample, Diluent";

            try
            {
                exportPath = CurRunBuilder.ExportFilePath + worklistName;
                     
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

                lblPrompt.ForeColor = Color.DarkRed;
                lblPrompt.Text = errMsg;
            }

            return actionResult;
        }

        public string WriteWorklist(List<OutputPlateSample> outSamples, OutputPlate outPlate)
        {
            string actionResult = "NA";
            string exportPath = "";
            string itemValue = "";
            string sourcePlate = "";
            string extValue = "";

            var columns = new HashSet<string>(outSamples.SelectMany(x => x.Attributes.Keys).Distinct().ToList());
            string plateFileItems = "SampleID,SourceRack,SourcePosition,DestRack,DestPosition, Sample, Diluent";
            //string plateFileItems = "SourceRack,SourcePosition,DestRack,DestPosition," + string.Join(",", columns);


            try
            {
                exportPath = CurRunBuilder.ExportFilePath + outPlate.WorkList;

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
                if (!string.IsNullOrEmpty(outPlate.Attributes["etc"]))
                {
                    extValue += "," + outPlate.Attributes["etc"];
                }

                using (var writer = new StreamWriter(exportPath))
                {
                    writer.WriteLine(plateFileItems);


                    foreach (var item in outSamples)
                    {
                        //itemValue += item.SourcePlateId + ",";
                        itemValue = item.SampleId + ",";
                        itemValue += sourcePlate + ",";
                        itemValue += GetWell(item.SourceWellId, item.SourcePlateId.IndexOf("BCR") >= 0) + ",";
                        itemValue += item.DestPlateId + ",";
                        itemValue += GetWell(item.DestWellId, item.DestPlateId.IndexOf("BCR") >= 0) + ",";

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
                        itemValue += extValue ;

                        writer.WriteLine(itemValue);
                        itemValue = "";
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

                lblPrompt.ForeColor = Color.DarkRed;
                lblPrompt.Text = errMsg;
            }

            return actionResult;
        }

        private string GetWell(string xyString, bool isBCR)
        {
            if (isBCR)
            {
                return int.Parse(xyString.Substring(0, xyString.IndexOf(','))).ToString();
                    
            }
            else
            {
                return (char)('A' + int.Parse(xyString.Substring(xyString.IndexOf(',')+1)))
                    + (int.Parse(xyString.Substring(0,xyString.IndexOf(','))) + 1).ToString();
            }
        }

        //private string GetWell(string xyString, bool isBCR = false)
        //{
        //    string well = "";
        //    string wellX = "";
        //    string wellY = "";
        //    int ind = 0;
        //    int indR = 0;

        //    ind = xyString.Trim().IndexOf(",");
        //    indR = xyString.Trim().IndexOf(")");

        //    wellX = xyString.Substring(1, ind - 1);
        //    wellY = xyString.Substring(ind + 1, indR - ind - 1);

        //    if (isBCR)
        //    {
        //        well = wellX;
        //    }
        //    else
        //    {
        //        well = wellX + "," + wellY;
        //    }

        //    return well;
        //}

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
            List<OutputPlateSample> plateSamples = new List<OutputPlateSample>();
            try
            {
                if (pIsFrmLoaded && cbProtocolCd.SelectedValue != null && cbProtocolCd.SelectedValue.ToString().Length > 0 &&
                    cbPlates.SelectedItem != null)
                {

                    this.Size = new Size(1111, 848);

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

                        var curOutPlate = CurValidPlates.Where(pl => pl.PlateId.ToUpper() == pSelectedPlatePage.ToUpper());
                        plateSamples = (List<OutputPlateSample>)OutPlateSamples.Where(p => p.DestPlateId.ToUpper() == pSelectedPlatePage.ToUpper()).ToList();

                        if (plateSamples.Count > 0)
                        {
                            lblMsg.ForeColor = Color.Navy;
                            lblMsg.Text = "The plate, " + pSelectedPlatePage + " , has following sample(s).";

                            MappingPlateSamples(plateSamples);
                            //MappingPlateSamplesRotated(plateSamples);

                            //if (MapPlateSamples() == "SUCCESS")
                            //{
                            //    foreach (DataGridViewRow plt in dgvPlateSet.Rows)
                            //    {
                            //        if (plt.Cells["DestPlate"].Value.ToString() == pSelectedPlatePage)
                            //        {
                            //            plt.DefaultCellStyle.BackColor = Color.LightGreen;
                            //            plt.Cells["CurWorkList"].Value = pSelectedPlatePage + ".csv";
                            //        }
                            //    }
                            //}
                        }
                        else
                        {
                            lblMsg.ForeColor = Color.Red;
                            lblMsg.Text = "The plate, " + pSelectedPlatePage + " , does not have sample(s).";
                        }

                    }
                }

                //bool ready2CreateNew = true;
                //if (dgvPlateSet != null && dgvPlateSet.Rows.Count > 0)
                //{
                //    foreach (DataGridViewRow wlRw in dgvPlateSet.Rows)
                //    {
                //        if (wlRw.Cells["CurWorkList"].Value != null && wlRw.Cells["CurWorkList"].Value.ToString().Length > 0)
                //        {
                //            ready2CreateNew = false;
                //        }
                //    }

                //    if (ready2CreateNew)
                //        btnCreateWorklist.Enabled = true;
                //}

            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;

            }

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
                        lblMsg.Text = "Tube Sample: " + senderGrid.CurrentCell.Value + "; [ " + senderGrid.CurrentCell.Tag + " ]";
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



        private void btnGo_Click(object sender, EventArgs e)
        {
            string barcodePrinterName = "";
            string barcode = "";
            string worklistName = "";
            try
            {

                lblPrompt.Text = "";

            //    ProcessBCRPlates();
            //    ProcessRegularPlates();

                ////barcodePrinterName = GetPrinterName();
                foreach (DataGridViewRow rwPlate in dgvPlateSet.Rows)
                {
                    if (Convert.ToBoolean(rwPlate.Cells["Included"].Value))
                    {
                        barcode = rwPlate.Cells["DestPlate"].Value.ToString();
                        
                        lblPrompt.Text = "The plate Barcode Printed for [" + barcode + "]";

                        ////PrintBarCode printPlateBarcode = new PrintBarCode();
                        ////printPlateBarcode.Print(barcode, GetPrinterName());
                        ///

                        worklistName= rwPlate.Cells["CurWorkList"].Value.ToString();
                        CreateEmptyWorklist(worklistName);

                    }
                }

                //dgvPlateSet.ReadOnly = true;
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

        private void MappingPlateSamples(List<OutputPlateSample> plateSamples)
        {
            string issues = "";
            int plateSizeX = 12;    //1-12
            int plateSizeY = 8;     //A-H
            int RotateSizeX = plateSizeY;   //H-A
            int RotateSizeY = plateSizeX;   //1-12

            string destWellId = "";
            int wellX = 0;
            int wellY = 0;

            DataTable dtPlateSamples = new DataTable();

            string curWell = "";

            try
            {
                //adding columns, plateSizeX  
                for (int i = 0; i <= plateSizeX; i++)
                {
                    dtPlateSamples.Columns.Add(i.ToString());
                }

                //adding rows, plateSizeY
                DataRow drPlate = dtPlateSamples.NewRow();
                for (int j = 0; j <= plateSizeY - 1; j++)
                {
                    drPlate = dtPlateSamples.NewRow();
                    drPlate[0] = pAlpha.Substring(j, 1);
                    dtPlateSamples.Rows.Add(drPlate);
                }

                foreach (var smp in plateSamples)
                {
                    destWellId = GetWell(smp.DestWellId, isBCR: false);
                    wellX = Convert.ToInt32(destWellId.Substring(1));
                    wellY = pAlpha.IndexOf(destWellId.Substring(0,1));

                    if (wellX <= plateSizeX && wellY < plateSizeY)
                    {
                        dtPlateSamples.Rows[wellY][wellX] = smp.SampleId;
                    }
                    else
                    {
                        issues += "Well is Over PlateSize";
                        issues += Environment.NewLine;
                    }

                }

                MapPlates(dtPlateSamples);

                //Rotate90
                //dtPlateSamples = Rotated90(dtPlateSamples);

                ////foreach (DataColumn col in dtPlateSamples.Columns)
                ////{
                ////    if (col.ColumnName == "0")
                ////    {
                ////        DataGridViewTextBoxColumn dgvCol = new DataGridViewTextBoxColumn();
                ////        dgvCol.Name = col.ColumnName;
                ////        dgvCol.Width = 28;
                ////        dgvSamplePlate.Columns.Add(dgvCol);
                ////    }
                ////    else
                ////    {
                ////        DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                ////        dgvBtn.Name = col.ColumnName;
                ////        dgvBtn.DefaultCellStyle.WrapMode=DataGridViewTriState.True;
                ////        dgvBtn.Width = 70;
                ////        dgvSamplePlate.Columns.Add(dgvBtn);
                ////    }
                ////}

                ////dgvSamplePlate.RowTemplate.Height = 50;
                ////foreach (DataRow tRw in dtPlateSamples.Rows)
                ////{
                ////    dgvSamplePlate.Rows.Add();
                ////    for (int tCol = 0; tCol < dtPlateSamples.Columns.Count; tCol++)
                ////    {
                ////        if (tCol == 0)
                ////        {
                ////            dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Value = tRw[tCol];
                ////        }
                ////        else
                ////        {
                ////            dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Value = tRw[tCol];

                ////            //Put info to Tag
                ////            if (tRw[tCol] ==null)
                ////            {
                ////                dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Tag = "Empty Sample";
                ////            }
                ////            else
                ////            {
                ////                curWell = dgvSamplePlate[0, dgvSamplePlate.RowCount - 1].Value.ToString();
                ////                curWell += dgvSamplePlate.Columns[tCol].Name;
                ////                dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Tag = "Well is "+ curWell;
                ////            }

                ////            //var sampleName = InputFileValues.Where(smp => smp.PlateId.ToUpper() == pSelectedPlatePage.ToUpper() &&
                ////            //                                         smp.ShortId == tRw[tCol].ToString()).Select(smp => smp.FullSampleId).FirstOrDefault();
                ////            //if (sampleName == null || string.IsNullOrEmpty(sampleName.ToString()))
                ////            //{
                ////            //    dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Tag = "empty sample";
                ////            //}
                ////            //else
                ////            //{
                ////            //    dgvSamplePlate[tCol, dgvSamplePlate.RowCount - 1].Tag = sampleName.ToString();
                ////            //}

                ////        }
                ////    }
                ////}



            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
            }



            //OutPlateSamples.Add(new OutputPlateSample
            //{
            //    SourcePlateId = smp.SourcePlateId,
            //    SourceWellId = smp.SourceWellId,
            //    DestPlateId = smp.DestPlateId,
            //    DestWellId = smp.DestWellId,
            //    Accept = smp.Attributes["Accept"],
            //    SampleId = smp.Attributes["SampleId"]
            //});

        }

        private void MappingPlateSamplesRotated(List<OutputPlateSample> plateSamples)
        {
            string issues = "";
            int RotateSizeX = PlateSizeY;   //H-A
            int RotateSizeY = PlateSizeX;   //1-12

            string destWellId = "";
            int wellX = 0;
            int wellY = 0;

            DataTable dtPlateSamples = new DataTable();

            try
            {
                //adding columns, plateSizeX  
                for (int i = RotateSizeX-1; i <= 0; i--)
                {
                    dtPlateSamples.Columns.Add(pAlpha.Substring(i, 1));
                }
                dtPlateSamples.Columns.Add("0");

                //adding rows, plateSizeY
                DataRow drPlate = dtPlateSamples.NewRow();
                for (int j = 1; j <= RotateSizeY; j++)
                {
                    drPlate = dtPlateSamples.NewRow();
                    drPlate[RotateSizeX] = j.ToString();
                    dtPlateSamples.Rows.Add(drPlate);
                }

                foreach (var smp in plateSamples)
                {
                    destWellId = GetWell(smp.DestWellId, isBCR: false);
                    wellX = Convert.ToInt32(destWellId.Substring(1));
                    wellY = pAlpha.IndexOf(destWellId.Substring(0, 1));

                    if (wellX <= RotateSizeY && wellY < RotateSizeX)
                    {
                        dtPlateSamples.Rows[RotateSizeX - wellY][wellX] = smp.SampleId;
                    }
                    else
                    {
                        issues += "Well is Over PlateSize";
                        issues += Environment.NewLine;
                    }
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
                        dgvBtn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        dgvBtn.Width = 70;
                        dgvSamplePlate.Columns.Add(dgvBtn);
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

            dgvSamplePlate.Rows.Clear();
            dgvSamplePlate.Columns.Clear();

            if (dtPlateSamples !=null && dtPlateSamples.Rows.Count > 0)
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string barCode = "";
            if (dgvPlateSet.CurrentRow.Cells["DestPlate"].Value != null)
            {
                barCode = dgvPlateSet.CurrentRow.Cells["DestPlate"].Value.ToString();
                PrintBarCode printPlateBarcode = new PrintBarCode();
                printPlateBarcode.Print(barCode, GetPrinterName());
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
            ProcessBCRPlates();
            ProcessRegularPlates();

            SaveCreateWorklists();

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
                                CurValidPlates.RemoveAll(bp => bp.PlateId.ToUpper() == importDB.SelectedDBPlate.PlateId);
                                CurValidPlates.Add(importDB.SelectedDBPlate);

                                //senderGrid.CurrentRow.Cells["WorkList"].Value = destPlate + ".CSV";
                                senderGrid.CurrentRow.Cells["PlateDesc"].Value = "Source Plate reday to go.";
                                senderGrid.CurrentRow.Cells["SourcePlate"].Value = importDB.SelectedDBPlate.PlateId;
                                //senderGrid.CurrentRow.DefaultCellStyle.BackColor = Color.LightGreen;
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
                                CurValidPlates.Add(importDB.SelectedDBPlate);

                                //senderGrid.CurrentRow.Cells["WorkList"].Value = destPlate + ".CSV";
                                senderGrid.CurrentRow.Cells["PlateDesc"].Value = "Source Plate reday to go.";
                                senderGrid.CurrentRow.Cells["DestPlate"].Value = importDB.SelectedDBPlate.PlateId;
                                //senderGrid.CurrentRow.DefaultCellStyle.BackColor = Color.LightGreen;
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




        private void ProcessBCRPlates()
        {
            ClientBackend backService = new ClientBackend();
            InputPlate bcrPlate = new InputPlate();
            InputPlate destPlate = new InputPlate();
            List<DtoWorklist> dtoWorklist = new List<DtoWorklist>();

            string hasSourcePlate = "YES";
            string hasDestPlate = "YES";
            string hasSamples = "YES";
            bool needCreatBCR = true;
            string csvName = "";
            string csvFullName = "";
            string destPlateId = "";
            string sourcePlate = "";

            bool included = true;
            bool isReadyPlate = true;
            //string scannedPlateBar = "";

            try
            {
                if (Directory.GetFiles(CurRunBuilder.ReadFilePath, "*.CSV").Length > 0)
                {
                    var csvFiles = Directory.GetFiles(CurRunBuilder.ReadFilePath, "*.CSV");

                    csvFullName = Path.GetFileName(csvFiles.FirstOrDefault());
                    csvName = Path.GetFileNameWithoutExtension(csvFiles.FirstOrDefault()).ToUpper();

                    foreach (DataGridViewRow rwPlate in dgvPlateSet.Rows)
                    {
                        destPlateId = rwPlate.Cells["DestPlate"].Value.ToString().ToUpper();
                        sourcePlate = rwPlate.Cells["SourcePlate"].Value.ToString().ToUpper();
                        included = Convert.ToBoolean(rwPlate.Cells["Included"].Value);
                        isReadyPlate = Convert.ToBoolean(rwPlate.Cells["SourcePlateIsNew"].Value);
                        //scannedPlateBar = rwPlate.Cells["ScannedPlate"].Value == null ? "" : rwPlate.Cells["ScannedPlate"].Value.ToString();

                        if (included && isReadyPlate)
                        {
                            destPlate = new InputPlate();

                            if (sourcePlate == "BCR" + CurUniqueId)
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
                                        lblPrompt.ForeColor = Color.DarkRed;
                                        lblPrompt.Text = backService.ErrMsg;
                                    }


                                    //Add samples to BCR-Plate
                                    if (hasSourcePlate == "YES")
                                    {
                                        AddVaildPlate(isNewPlate: true, bcrPlate);
                                        hasSamples = backService.AddSamples(sourcePlate, InputFileValues);
                                        if (!string.IsNullOrEmpty(backService.ErrMsg))
                                        {
                                            lblPrompt.ForeColor = Color.DarkRed;
                                            lblPrompt.Text = backService.ErrMsg;
                                        }

                                        //One BCR Sample
                                        needCreatBCR = false;
                                    }
                                }

                                //Create DestPlate
                                destPlate = GetDestPlateProperties(destPlateId.ToUpper());
                                hasDestPlate = backService.CreatePlate(destPlate);
                                if (!string.IsNullOrEmpty(backService.ErrMsg))
                                {
                                    lblPrompt.ForeColor = Color.DarkRed;
                                    lblPrompt.Text = backService.ErrMsg;
                                }
                                if (hasDestPlate == "YES")
                                {
                                    AddVaildPlate(isNewPlate: true, destPlate);

                                    //Move BCR To Dest
                                    dtoWorklist = (List<DtoWorklist>)backService.GetWorklist(bcrPlate.Name.ToUpper(), destPlate.Name.ToUpper(), "lookup_alias,skip_cancelled,cherry_pick");
                                    if (!string.IsNullOrEmpty(backService.ErrMsg))
                                    {
                                        lblPrompt.ForeColor = Color.DarkRed;
                                        lblPrompt.Text = backService.ErrMsg;
                                    }

                                }


                                //Worklist
                                if (dtoWorklist != null && dtoWorklist.Count > 0)
                                {
                                    if (AddPlateSamples(dtoWorklist, destPlateId) == "SUCCESS")
                                    {
                                        rwPlate.DefaultCellStyle.BackColor = Color.LightGreen;
                                        rwPlate.Cells["CurWorkList"].Value = destPlateId.Substring(0, destPlateId.Length - CurUniqueId.Length) + ".csv";
                                        rwPlate.Cells["PlateDesc"].Value = "Worklist is ready to create";
                                        rwPlate.Cells["wlReady"].Value = true;
                                    }
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
            }
        }

        private void ProcessRegularPlates()
        {
            ClientBackend backService = new ClientBackend();
            InputPlate regularPlate = new InputPlate();
            InputPlate destPlate = new InputPlate();
            List<DtoWorklist> dtoWorklist = new List<DtoWorklist>();

            string hasSourcePlate = "YES";
            string hasDestPlate = "YES";
            string hasSamples = "YES";
            bool needCreatBCR = true;

            string destPlateId = "";
            string sourcePlate = "";

            bool included = true;
            bool isNewSourcePlate = true;
            bool isNewDestPlate = true;
            //string scannedPlateBar = "";


            try
            {
                foreach (DataGridViewRow rwPlate in dgvPlateSet.Rows)
                {
                    destPlateId = rwPlate.Cells["DestPlate"].Value.ToString().ToUpper();
                    sourcePlate = rwPlate.Cells["SourcePlate"].Value.ToString().ToUpper();
                    included = Convert.ToBoolean(rwPlate.Cells["Included"].Value);
                    isNewSourcePlate = Convert.ToBoolean(rwPlate.Cells["SourcePlateIsNew"].Value);
                    isNewDestPlate = Convert.ToBoolean(rwPlate.Cells["DestPlateIsNew"].Value);
                    //scannedPlateBar = rwPlate.Cells["ScannedPlate"].Value == null ? "" : rwPlate.Cells["ScannedPlate"].Value.ToString();

                    if (included && sourcePlate != "BCR" + CurUniqueId)
                    {
                        var findPlate = CurValidPlates.Where(pl => pl.PlateId.ToUpper() == sourcePlate).FirstOrDefault();

                        if (findPlate.IsNewCreatedPlate && isNewSourcePlate)
                        {
                            hasSourcePlate = "YES";
                            hasSamples = "YES";
                            rwPlate.Cells["PlateDesc"].Value = "SourcePlate is ready to go";

                            //Create DestPlate
                            destPlate = new InputPlate();
                            destPlate = GetDestPlateProperties(destPlateId);
                            hasDestPlate = backService.CreatePlate(destPlate);
                            if (hasDestPlate == "YES")
                                AddVaildPlate(isNewDestPlate, destPlate);

                            //Move BCR To Dest
                            dtoWorklist = (List<DtoWorklist>)backService.GetWorklist(sourcePlate, destPlate.Name, "lookup_alias,skip_cancelled,cherry_pick");

                            //Worklist
                            if (dtoWorklist != null && dtoWorklist.Count > 0)
                            {
                                if (AddPlateSamples(dtoWorklist, destPlateId) == "SUCCESS")
                                {
                                    rwPlate.DefaultCellStyle.BackColor = Color.LightGreen;
                                    rwPlate.Cells["CurWorkList"].Value = destPlateId.Substring(0, destPlateId.Length - CurUniqueId.Length) + ".csv";
                                    rwPlate.Cells["PlateDesc"].Value = "Worklist is ready to create";
                                    rwPlate.Cells["wlReady"].Value = true;
                                }
                            }

                        }
                        else if (findPlate.IsNewCreatedPlate && isNewSourcePlate == false)
                        {


                        }

                    }

                }

            }
            catch (Exception ex)
            {
                string errMsg = "ProcessBCRPlates() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }
        }

        private string AddPlateSamples(List<DtoWorklist> outWorklist, string destPlateId)
        {
            string actionResult = "SUCCESS";

            try
            {
                OutPlateSamples.RemoveAll(osp => osp.DestPlateId == destPlateId);

                foreach (var smp in outWorklist)
                {
                    OutPlateSamples.Add(new OutputPlateSample
                    {
                        SourcePlateId = smp.SourcePlateId,
                        SourceWellId = smp.SourceWellId,
                        DestPlateId = smp.DestPlateId,
                        DestWellId = smp.DestWellId,
                        Attributes = smp.Attributes,
                        Accept = smp.Attributes["Accept"],
                        SampleId = smp.Attributes["SampleId"]
                    });
                }

            }
            catch (Exception ex)
            {
                actionResult = "ERROR";
                string errMsg = ex.Message;
            }


            return actionResult;
        }

        private InputPlate GetDestPlateProperties(string plateId)
        {
            InputPlate getPlate = new InputPlate();
            string startPos = "";
            string endPos = "";
            int offset = 0;

            if (ProtocolPlates.Count > 0)
            {
                var findPlate = ProtocolPlates.Where(pp => string.Compare(pp.DestPlateId.ToUpper(), plateId.ToUpper(), true) == 0).FirstOrDefault();
                startPos = findPlate.StartPos;
                endPos = findPlate.EndPos;

                int startY = pAlpha.IndexOf(startPos.Substring(0, 1));   //0-Base
                int startX = Convert.ToInt32(startPos.Substring(1));   
                //int startY = pAlpha.IndexOf(startPos.Substring(0, 1)) + 1;    //1-Base
                //int startX = Convert.ToInt32(startPos.Substring(1));

                int endY = pAlpha.IndexOf(endPos.Substring(0, 1));  //0-Base
                int endX = Convert.ToInt32(endPos.Substring(1))-1;
                //int endY = pAlpha.IndexOf(endPos.Substring(0, 1)) + 1;    //1-Base
                //int endX = Convert.ToInt32(endPos.Substring(1));

                
                offset = (startY) + ((startX - 1) * PlateSizeY);    //0-Base
                //offset = (startY-1) + ((startX - 1) * PlateSizeY);    //1-Base

                Pos sPos = new Pos();
                Pos ePos = new Pos();

                sPos.X = startX.ToString();
                sPos.Y = startY.ToString();
                ePos.X = endX.ToString();
                ePos.Y = endY.ToString();

                getPlate.Start = sPos;
                getPlate.End = ePos;
                getPlate.Name = findPlate.DestPlateId.ToUpper();
                getPlate.Offset = offset.ToString();
                getPlate.Direction = "0";
                getPlate.Attributes = findPlate.Attributes;

            }

            return getPlate;
        }

        private InputPlate LoadBCRSamples(string importFile)
        {
            RepoSQLOracle repoService = new RepoSQLOracle();
            InputPlate bcrPlate = new InputPlate();

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
                        bcrPlate.Offset = "0";
                        bcrPlate.Direction = "0";
                    }
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
            string actionCreateWorklist = "";

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
                        actionSaveToDB = SavePlatesToDB();

                        //create worklist
                        //if(actionSaveToDB == "SUCCESS")
                        //{
                        //    actionCreateWorklist = CreateWorklist();
                        //}

                        actionCreateWorklist = CreateWorklist();

                        //if (actionSaveToDB == "SUCCESS" && actionCreateWorklist == "SUCCESS")
                        if (actionCreateWorklist == "SUCCESS")
                        {

                            lblMsg.ForeColor = Color.ForestGreen;
                            lblMsg.Text = "The Worklist(s) created.";
                        }
                        else
                        {
                            lblMsg.ForeColor = Color.Red;
                            lblMsg.Text = "The Worklist(s) did not create.";
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            SaveCreateWorklists();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetMsg();
            cbProtocolCd.SelectedIndex = -1;
            cbProtocolCd.Text = "-Select-";
            dgvPlateSet.Rows.Clear();
            dgvPlateSet.ReadOnly = false;

            CurValidPlates = new List<ValidPlate>();
            OutPlateSamples = new List<OutputPlateSample>();

            //PlateInputs = new List<Plate>();
            //PostPlates = new List<Plate>();

            cbPlates.Items.Clear();
            cbPlates.Text = "";

            dgvSamplePlate.Columns.Clear();
            dgvSamplePlate.Rows.Clear();

            btnPrintPlateBarcode.Enabled = false;
            btnCreateWorklist.Enabled = false;

            txbBarcode.Text = "";
            txbBarcode.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
                    //string scannedPlateBar = "";

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

        private void cbPlates_SelectedIndexChanged_His()
        {
            try
            {
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
                                        plt.Cells["CurWorkList"].Value = pSelectedPlatePage + ".csv";
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

    }
}
