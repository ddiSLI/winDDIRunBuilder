using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using winDDIRunBuilder.Models;
using static winDDIRunBuilder.ClientBackend;

namespace winDDIRunBuilder
{
    public partial class frmSampleStatus : Form
    {
        private bool IsFrmLoaded = false;
        public List<Protocol> AllProtocols { get; set; } = new List<Protocol>();
        private List<string> DBTests { get; set; } = new List<string>();
        private List<PlateSample> CopiaPlateSamples = new List<PlateSample>();
        private List<PlateSample> RunbuilderSamples = new List<PlateSample>();
        private Int16 _currentPageNo = 0;
        private Int16 _totalPages = 0;
        private bool _isCmbLoadedData = false;
        private Color _infoColor = Color.LightGoldenrodYellow;
        private Color _selectedColor = Color.LightBlue;

        public string CurUser { get; set; } = "";
        public frmSampleStatus()
        {
            InitializeComponent();
        }

        private void frmSampleStatus_Load(object sender, EventArgs e)
        {
            ClientBackend backService = new ClientBackend();
            RepoSQL sqlService = new RepoSQL();
            List<DBtestItem> copiaDBTests = new List<DBtestItem>();
            List<string> allDBtests = new List<string>();
            string dbTestSettings = "";
            string[] dbTestSets;
            string rbStatus = "";

            try
            {
                //Load Status
                cmbStatus.Items.Clear();
                cmbStatus.Items.Add("JanusToDo");
                cmbStatus.Items.Add("JanusCompleted");
                cmbStatus.Items.Add("REJECT");
                cmbStatus.SelectedIndex = 0;

                cmbModifiedBy.Items.Add("ALL");
                cmbModifiedBy.SelectedIndex = 0;

                // Load profile from JSON
                dbTestSettings = GetCopiaDBTestSettings();
                dbTestSets = dbTestSettings.Split('^');
                if (dbTestSets.Count() > 0 && !string.IsNullOrEmpty(dbTestSets.First()))
                {
                    lblMsg.Text = "Tests displayed: ";
                    foreach (var ts in dbTestSets)
                    {

                        lblMsg.Text += "[" + ts + "]  ";
                    }
                }
                else
                {
                    lblMsg.Text = "Tests displayed: All tests ";
                }


                //Load DBTests
                dgvDBTests.Rows.Clear();

                copiaDBTests = backService.GetCopiaDBTests().Distinct().ToList();
                if (copiaDBTests != null && copiaDBTests.Count > 0)
                {
                    dgvDBTests.Rows.Add();
                    dgvDBTests.Rows[dgvDBTests.RowCount - 1].Cells["DBTestCd"].Value = "REJECT";
                    dgvDBTests.Rows[dgvDBTests.RowCount - 1].Cells["DBTestName"].Value = "Rejected in RunBuilder";
                    foreach (var test in copiaDBTests)
                    {
                        dgvDBTests.Rows.Add();
                        if (dbTestSets.Contains(test.abbreviation.Trim()))
                        {
                            dgvDBTests.Rows[dgvDBTests.RowCount - 1].Cells["Sel"].Value = true;
                            dgvDBTests.Rows[dgvDBTests.RowCount - 1].DefaultCellStyle.BackColor = _selectedColor;
                        }
                        else
                        {
                            dgvDBTests.Rows[dgvDBTests.RowCount - 1].Cells["Sel"].Value = false;
                        }

                        dgvDBTests.Rows[dgvDBTests.RowCount - 1].Cells["DBTestCd"].Value = test.abbreviation.Trim();
                        dgvDBTests.Rows[dgvDBTests.RowCount - 1].Cells["DBTestName"].Value = test.name;
                    }
                }

                if (dbTestSettings.IndexOf("REJECT") >= 0)
                {
                    dbTestSettings= dbTestSettings.Replace("^REJECT","").Replace("REJECT^","").Replace("REJECT", "");
                    rbStatus = "REJECT";
                }
                GetCopiaPlateSamples(dbTestSettings, rbStatus);

                IsFrmLoaded = true;
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

        private void btnGo_Click(object sender, EventArgs e)
        {
            string dbTestUser = "";
            List<string> dbTestContents = new List<string>();

            string rbStatus = "";
            string dbTestSettings = "";
            
            try
            {
                dbTestUser = Environment.MachineName;

                //Get dbTestContents
                if (dgvDBTests != null && dgvDBTests.RowCount > 0)
                {
                    foreach (DataGridViewRow rw in dgvDBTests.Rows)
                    {
                        if (Convert.ToBoolean(rw.Cells["Sel"].Value))
                        {
                            dbTestContents.Add((string)rw.Cells["DBTestCd"].Value);
                            rw.DefaultCellStyle.BackColor = _selectedColor;
                        }
                        else
                        {
                            rw.DefaultCellStyle.BackColor =_infoColor;
                        }
                    }
                }

                // Save DBTestProfile to JSON
                AppProfile profile = new AppProfile { Name = dbTestUser, Settings = dbTestContents };
                string json = JsonConvert.SerializeObject(profile);
                File.WriteAllText("rbDBTestProfile.json", json);
                //string existingContent = File.ReadAllText("runBuilderProfile.json");
                //string newContent = $"{existingContent}{Environment.NewLine}{json}";
                //

                dbTestSettings = GetCopiaDBTestSettings();
                if (!string.IsNullOrEmpty(dbTestSettings))
                {
                    lblMsg.Text = "Tests Displayed: ";
                    foreach (var ts in dbTestSettings.Split('^'))
                    {

                        lblMsg.Text += "[" + ts + "]  ";
                    }
                }
                else
                {
                    lblMsg.Text = "Tests Displayed: All tests";
                }

                if (dbTestSettings.IndexOf("REJECT") >= 0)
                {
                    dbTestSettings = dbTestSettings.Replace("^REJECT", "").Replace("REJECT^", "").Replace("REJECT", "");
                    rbStatus = "REJECT";
                }
                GetCopiaPlateSamples(dbTestSettings, rbStatus);

            }
            catch (Exception ex)
            {
                string errMsg = "frmSampleStatus.btnGo_Click() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                lblMsg.ForeColor = Color.DarkRed;
                lblMsg.Text = errMsg;
            }
        }

        private string GetCopiaDBTestSettings()
        {
            string jsonDBTestContent = "";
            string dbTestSettings = "";

            try
            {
                if (File.Exists("rbDBTestProfile.json"))
                {
                    jsonDBTestContent = File.ReadAllText("rbDBTestProfile.json");
                    AppProfile loadedProfile = JsonConvert.DeserializeObject<AppProfile>(jsonDBTestContent);

                    if (loadedProfile != null && loadedProfile.Settings.Count > 0)
                    {
                        bool is1stTest = true;
                        foreach (var test in loadedProfile.Settings)
                        {
                            if (is1stTest)
                            {
                                dbTestSettings = test.Trim();
                                is1stTest = false;
                            }
                            else
                            {
                                dbTestSettings += $"^{test.Trim()}";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = "GetCopiaDBTestSettings()() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                lblMsg.ForeColor = Color.DarkRed;
                lblMsg.Text = errMsg;
            }

            return dbTestSettings;
        }

        private void GetCopiaPlateSamples(string dbTestSettings, string rbStatus = "")
        {
            List<string> dbTests = new List<string>();
            List<string> status = new List<string>();
            List<string> modifiedBys = new List<string>();

            RepoSQL sqlService = new RepoSQL();
            ClientBackend backService = new ClientBackend();
            CopiaPlateSamples = new List<PlateSample>();

            DeptSample dptSample = new DeptSample();
            List<Item> copiaSamples = new List<Item>();
            List<PlateSample> rejectSamples = new List<PlateSample>();
            List<PlateSample> copiaRunSamples = new List<PlateSample>();

            int copiaSmpCount = 0;
            string copiaSmpIds = "";

            try
            {
                RunbuilderSamples = new List<PlateSample>();

                //get RunBuilder reject samples
                RunbuilderSamples = sqlService.GetHistorySamples(copiaSampleIds: "", status: "REJECT");

                //get Copia samples
                //status = 1(accept) or 5(InProcessing); 
                copiaSamples = backService.GetCopiaSamples(dbTestSettings);

                //runBuilder Reject samples
                if (!string.IsNullOrEmpty(rbStatus) && RunbuilderSamples != null && RunbuilderSamples.Count > 0)
                {
                    rejectSamples = RunbuilderSamples.ToList();
                    rejectSamples.ForEach(stu => stu.Status = "REJECT");
                    CopiaPlateSamples.AddRange(rejectSamples);

                    if (rejectSamples != null && rejectSamples.Count > 0)
                    {
                        btnUnReject.Enabled = true;
                    }
                    else
                    {
                        btnUnReject.Enabled = false;
                    }
                }

                //Copia samples
                if (copiaSamples != null && copiaSamples.Count > 0)
                {
                    foreach (var cpSmp in copiaSamples)
                    {
                        copiaSmpCount += 1;
                        
                        if (copiaSmpCount <= 100)
                        {
                            if (string.IsNullOrEmpty(copiaSmpIds))
                            {
                                copiaSmpIds = cpSmp.labFillerOrderNumber.Trim();
                            }
                            else
                            {
                                copiaSmpIds += "," + cpSmp.labFillerOrderNumber.Trim();
                            }
                        }
                        else
                        {
                            //send to sqlRepo
                            copiaRunSamples.AddRange(sqlService.GetHistorySamples(copiaSmpIds));
                            copiaSmpIds = cpSmp.labFillerOrderNumber.Trim();
                            copiaSmpCount = 0;
                        }
                    }

                    //copiaSamples.count < 100
                    if (!string.IsNullOrEmpty(copiaSmpIds))
                    {
                        copiaRunSamples.AddRange(sqlService.GetHistorySamples(copiaSmpIds));
                        copiaSmpIds = "";
                    }


                    //CopiaSamples => plateSamples
                    var janusTodoSamples = copiaSamples.Where(cp => copiaRunSamples.Any(rb => string.IsNullOrEmpty(rb.SampleId))).ToList();
                    var janusCompletedSamples = copiaSamples.Where(cp => !copiaRunSamples.Any(rb => string.IsNullOrEmpty(rb.SampleId))).ToList();

                    //var janusTodoSamples = copiaSamples.Where(cp => !RunbuilderSamples.Any(hs => hs.SampleId == cp.labFillerOrderNumber)).ToList();
                    //var janusCompletedSamples = copiaSamples.Where(cp => RunbuilderSamples.Any(hs => hs.SampleId == cp.labFillerOrderNumber)).ToList();

                    CopiaPlateSamples.AddRange(BuildCopiaPlateSamples(janusTodoSamples, "JanusToDo"));
                    CopiaPlateSamples.AddRange(BuildCopiaPlateSamples(janusCompletedSamples, "JanusCompleted"));
                }

                if (CopiaPlateSamples != null && CopiaPlateSamples.Count > 0)
                {
                    _isCmbLoadedData = false;
                    CopiaPlateSamples = CopiaPlateSamples.OrderBy(ob => ob.DBTest).ToList();

                    //Load modified By
                    cmbModifiedBy.Items.Clear();
                    modifiedBys = CopiaPlateSamples.Where(cs => !string.IsNullOrEmpty(cs.ModifiedBy)).Select(smp => smp.ModifiedBy).Distinct().ToList();
                    cmbModifiedBy.Items.AddRange(modifiedBys.ToArray());

                    LoadingCopiaPlateSamples(CopiaPlateSamples);

                    lblStatus.Text = $"The current Total Sample(s): " + CopiaPlateSamples.Count.ToString("#,###") + " ; ";

                    _isCmbLoadedData = true;
                }
            }
            catch (Exception ex)
            {
                string errMsg = "GetCopiaPlateSamples() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                lblMsg.ForeColor = Color.DarkRed;
                lblMsg.Text = errMsg;
            }
        }

        private void FilterSamples()
        {
            List<string> dbTests = new List<string>();
            List<PlateSample> copiaPlateSamples = new List<PlateSample>();

            //filter By Status
            if (cmbStatus.SelectedItem.ToString() == "ALL")
            {
                copiaPlateSamples = CopiaPlateSamples;
            }
            else
            {
                copiaPlateSamples = CopiaPlateSamples.Where(s => s.Status == (string)cmbStatus.SelectedItem).ToList();
            }

            //filter ModifiedBy
            if (cmbModifiedBy.Text != "ALL")
            {
                copiaPlateSamples = copiaPlateSamples.Where(s => s.ModifiedBy == (string)cmbModifiedBy.SelectedItem).ToList();
            }

            LoadingCopiaPlateSamples(copiaPlateSamples);
        }

        private void LoadingCopiaPlateSamples(List<PlateSample> copiaPlateSamples)
        {
            dgvSamples.Rows.Clear();
            if (copiaPlateSamples != null && copiaPlateSamples.Count > 0)
            {
                foreach (var smp in copiaPlateSamples)
                {
                    dgvSamples.Rows.Add();
                    dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["UnReject"].Value = false;
                    dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["Status"].Value = string.IsNullOrEmpty(smp.Status) ? "" : smp.Status;
                    dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["SampleId"].Value = string.IsNullOrEmpty(smp.SampleId) ? "" : smp.SampleId;
                    dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["DBTest"].Value = string.IsNullOrEmpty(smp.DBTest) ? "" : smp.DBTest;
                    dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["ModifiedDate"].Value = smp.ModifiedDate;
                    dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["PlateId"].Value = string.IsNullOrEmpty(smp.PlateId) ? "" : smp.PlateId;
                    dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["PlateVersion"].Value = string.IsNullOrEmpty(smp.PlateVersion) ? "" : smp.PlateVersion;
                    dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["Id"].Value = string.IsNullOrEmpty(smp.Id) ? "" : smp.Id;
                    dgvSamples.Rows[dgvSamples.RowCount - 1].Cells["ModifiedBy"].Value = string.IsNullOrEmpty(smp.ModifiedBy) ? "" : smp.ModifiedBy;
                }
            }
        }

        private List<PlateSample> BuildCopiaPlateSamples(List<Item> copiaSamples, string statusDesc)
        {
            List<PlateSample> copiaPlateSamples = new List<PlateSample>();
            if (copiaSamples != null && copiaSamples.Count > 0)
            {
                foreach (var cpSmp in copiaSamples)
                {
                    copiaPlateSamples.Add(new PlateSample
                    {
                        SampleId = cpSmp.labFillerOrderNumber,
                        Status = statusDesc,
                        DBTest = cpSmp.labPanelCode,
                        ModifiedDate = cpSmp.createStamp.ToString()
                    });
                }
            }

            return copiaPlateSamples;
        }

        private void btnUnReject_Click(object sender, EventArgs e)
        {
            string unrejectResult = "SUCCESS";
            RepoSQL sqlService = new RepoSQL();
            List<SampleStatus> rejectSamples = new List<SampleStatus>();
            SampleStatus smpStatus = new SampleStatus();
            bool isUnreject = false;

            try
            {
                if (dgvSamples != null && dgvSamples.RowCount > 0)
                {
                    foreach (DataGridViewRow srw in dgvSamples.Rows)
                    {
                        isUnreject = Convert.ToBoolean(srw.Cells["UnReject"].Value);
                        if (isUnreject)
                        {
                            smpStatus = new SampleStatus();
                            smpStatus.Status = "";
                            smpStatus.User = CurUser;
                            smpStatus.Id = srw.Cells["Id"].Value.ToString();
                            smpStatus.SampleId = srw.Cells["SampleId"].Value.ToString();
                            smpStatus.PlateId = srw.Cells["PlateId"].Value.ToString();
                            smpStatus.PlateVersion = srw.Cells["PlateVersion"].Value.ToString();

                            rejectSamples.Add(smpStatus);
                        }
                    }

                    if (rejectSamples != null && rejectSamples.Count > 0)
                    {
                        unrejectResult = sqlService.UpdateSampleStatus(rejectSamples);
                        if (unrejectResult == "SUCCESS")
                        {
                            lblMsg.Text = "All Un-Reject processed";
                            //GetRejectSamples();
                        }
                        else
                        {
                            lblMsg.Text = "Processing Un-Reject met issues: " + unrejectResult;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = "btnUnReject_Click() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                lblMsg.ForeColor = Color.DarkRed;
                lblMsg.Text = errMsg;
            }
        }

        private void dgvSamples_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            string smpStatus = "";

            try
            {
                if (e.RowIndex >= 0)
                {
                    if (senderGrid.Columns[e.ColumnIndex].Name == "UnReject" && senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                    {
                        senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);

                        smpStatus = senderGrid.CurrentRow.Cells["Status"].Value.ToString();
                        if (smpStatus == "REJECT")
                        {
                            if ((bool)senderGrid.CurrentRow.Cells["UnReject"].Value == false)
                            {
                                senderGrid.CurrentRow.Cells["UnReject"].Value = true;
                            }
                            else
                            {
                                senderGrid.CurrentRow.Cells["UnReject"].Value = false;
                            }
                            senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        }
                        else
                        {
                            senderGrid.CurrentRow.Cells["UnReject"].Value = false;
                            senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        }

                        //senderGrid.Invalidate();
                        //senderGrid.Refresh();
                        //senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }

                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "dgvsamples_cellcontentclick() met some issues:" + ex.Message;
            }
        }

        private void cmbModifiedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsFrmLoaded && _isCmbLoadedData)
            {
                FilterSamples();
            }
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsFrmLoaded && _isCmbLoadedData)
            {
                FilterSamples();
            }
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="copiaPageNo"></param>
        /// 
        private void GetCopiaPlateSamples_His(Int16 copiaPageNo = 0)
        {
            bool dateRangeChanged = false;
            DateTime dtiStartDate;
            DateTime dtiEndDate;
            Int32 copiaSampleCount = 0;

            List<string> dbTests = new List<string>();
            List<string> status = new List<string>();
            List<string> modifiedBys = new List<string>();

            RepoSQL sqlService = new RepoSQL();
            ClientBackend backService = new ClientBackend();
            CopiaPlateSamples = new List<PlateSample>();

            DeptSample dptSample = new DeptSample();
            List<PlateSample> hisSamples = new List<PlateSample>();
            List<Item> copiaSamples = new List<Item>();
            List<Item> unapprovaledSamples = new List<Item>();

            List<PlateSample> rejectSamples = new List<PlateSample>();

            try
            {
                //Int16 weeks = (Int16)selWeeks.Value;
                //weeks = (Int16)(weeks > 5 ? 5 : weeks);

                //dtiStartDate = DateTime.Now.AddDays(-(weeks * 7));
                dtiEndDate = DateTime.Now;

                //testing ("7/1/2024", "8/2/2024");
                dtiStartDate = new DateTime(2024, 7, 1);
                dtiEndDate = new DateTime(2024, 8, 2);
                //

                //if (_startDate.Date != dtiStartDate.Date)
                //{
                //    copiaPageNo = 0;
                //    dateRangeChanged = true;
                //}

                //_startDate = dtiStartDate;
                //_endDate = dtiEndDate;

                if (dateRangeChanged)
                {
                    _currentPageNo = 0;
                    dptSample.Status = "REJECT";
                    dptSample.StartDate = dtiStartDate;
                    //dptSample.DBTest = dbtests;
                    //dptSample.Dept = dept;

                    //get RunBuilder samples
                   // hisSamples = sqlService.GetHistorySamples(dptSample);

                    //get copia sample count
                    copiaSampleCount = backService.GetCopiaSampleCount(dtiStartDate.ToString("M/d/yyyy"), dtiEndDate.ToString("M/d/yyyy"));

                    if (copiaSampleCount <= 1000)
                    {
                      //  lblCureentPage.Text = "0 / 1";
                    }
                    else
                    {
                        if (copiaSampleCount % 1000 == 0)
                        {
                            _totalPages = (Int16)(copiaSampleCount / 1000);
                        }
                        else
                        {
                            _totalPages = (Int16)(copiaSampleCount / 1000 + 1);
                        }

                      //  lblCureentPage.Text = "0 / " + _totalPages.ToString("#,###");
                    }



                }

                //get Copia samples
                //           copiaSamples = backService.GetCopiaSamples(dtiStartDate.ToString("M/d/yyyy"), dtiEndDate.ToString("M/d/yyyy"), copiaPageNo);


                //runBuilder Reject samples
                if (hisSamples != null && hisSamples.Count > 0)
                {
                    rejectSamples = hisSamples.Where(s => s.Status == "REJECT").ToList();
                    rejectSamples.ForEach(stu => stu.Status = "REJECT");
                    CopiaPlateSamples.AddRange(rejectSamples);
                }

                //Copia samples
                if (copiaSamples != null && copiaSamples.Count > 0)
                {
                    unapprovaledSamples = copiaSamples.Where(cs => cs.status != 7).ToList();

                    //CopiaSamples => plateSamples
                    var janusTodoSamples = copiaSamples.Where(cp => !hisSamples.Any(hs => hs.SampleId == cp.labFillerOrderNumber)).ToList();
                    var janusCompletedSamples = unapprovaledSamples.Where(cp => hisSamples.Any(hs => hs.SampleId == cp.labFillerOrderNumber)).ToList();

                    CopiaPlateSamples.AddRange(BuildCopiaPlateSamples(janusTodoSamples, "JanusToDo"));
                    CopiaPlateSamples.AddRange(BuildCopiaPlateSamples(janusCompletedSamples, "JanusCompleted"));

                    //copiaSamples = copiaOrderedPanels.SelectMany(op => op.copiaSamples).ToList();
                    //unapprovaledOrderedPanels = copiaOrderedPanels.Where(cp => cp.status != 7).ToList();
                    //unapprovaledSamples = unapprovaledOrderedPanels.SelectMany(op => op.Results).ToList();
                    //SendingHarvest.ForEach(stu => stu.Status = "InProcessing");
                }

                if (CopiaPlateSamples != null && CopiaPlateSamples.Count > 0)
                {
                    //Load Status
                    status = CopiaPlateSamples.Where(cs => !string.IsNullOrEmpty(cs.Status)).Select(smp => smp.Status).Distinct().ToList();
                    cmbStatus.Items.Add("ALL");
                    cmbStatus.Items.AddRange(status.ToArray());
                    cmbStatus.SelectedIndex = 0;

                    //Load modified By
                    modifiedBys = CopiaPlateSamples.Where(cs => !string.IsNullOrEmpty(cs.ModifiedBy)).Select(smp => smp.ModifiedBy).Distinct().ToList();
                    cmbModifiedBy.Items.Add("ALL");
                    cmbModifiedBy.Items.AddRange(modifiedBys.ToArray());
                    cmbModifiedBy.SelectedIndex = 0;
                    if (modifiedBys != null && modifiedBys.Count > 0)
                    {
                        btnUnReject.Enabled = true;
                    }
                    else
                    {
                        btnUnReject.Enabled = false;
                    }

                    //Load DBTests
                    dbTests = CopiaPlateSamples.Where(dbt => !string.IsNullOrEmpty(dbt.DBTest)).Select(smp => smp.DBTest).Distinct().ToList();
                    //clbDBTests.Items.Add("AllDBTests");
                    //clbDBTests.Items.AddRange(dbTests.ToArray());

                    LoadingCopiaPlateSamples(CopiaPlateSamples);

                    //lblStatus.Text = "Total Sample(s): " + copiaSampleCount.ToString("#,###");
                }
            }
            catch (Exception ex)
            {
                string errMsg = "GetCopiaPlateSamples() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                lblMsg.ForeColor = Color.DarkRed;
                lblMsg.Text = errMsg;
            }
        }

        private void GetCopiaPlateSamples_SameDate(Int16 copiaPageNo = 0)
        {
            RepoSQL sqlService = new RepoSQL();
            ClientBackend backService = new ClientBackend();
            //List<PlateSample> hisSamples = new List<PlateSample>();
            List<Item> copiaSamples = new List<Item>();
            List<Item> unapprovaledSamples = new List<Item>();

            List<string> dbTests = new List<string>();
            List<string> status = new List<string>();
            List<string> modifiedBys = new List<string>();

            try
            {
                CopiaPlateSamples = new List<PlateSample>();

                //testing ("7/1/2024", "8/2/2024");
                //_startDate = new DateTime(2024, 7, 1);
                //_endDate = new DateTime(2024, 8, 2);
                //

                //get Copia samples
                //         copiaSamples = backService.GetCopiaSamples(_startDate.ToString("M/d/yyyy"), _endDate.ToString("M/d/yyyy"), copiaPageNo);

                //Copia samples
                if (copiaSamples != null && copiaSamples.Count > 0)
                {
                    unapprovaledSamples = copiaSamples.Where(cs => cs.status != 7).ToList();

                    //CopiaSamples => plateSamples
                    var janusTodoSamples = copiaSamples.Where(cp => !RunbuilderSamples.Any(hs => hs.SampleId == cp.labFillerOrderNumber)).ToList();
                    var janusCompletedSamples = unapprovaledSamples.Where(cp => RunbuilderSamples.Any(hs => hs.SampleId == cp.labFillerOrderNumber)).ToList();

                    CopiaPlateSamples.AddRange(BuildCopiaPlateSamples(janusTodoSamples, "JanusToDo"));
                    CopiaPlateSamples.AddRange(BuildCopiaPlateSamples(janusCompletedSamples, "JanusCompleted"));
                }

                if (CopiaPlateSamples != null && CopiaPlateSamples.Count > 0)
                {
                    _isCmbLoadedData = false;

                    //Load Status
                    cmbStatus.Items.Clear();
                    status = CopiaPlateSamples.Where(cs => !string.IsNullOrEmpty(cs.Status)).Select(smp => smp.Status).Distinct().ToList();
                    cmbStatus.Items.Add("ALL");
                    cmbStatus.Items.AddRange(status.ToArray());
                    cmbStatus.SelectedIndex = 0;

                    //Load modified By
                    cmbModifiedBy.Items.Clear();
                    modifiedBys = CopiaPlateSamples.Where(cs => !string.IsNullOrEmpty(cs.ModifiedBy)).Select(smp => smp.ModifiedBy).Distinct().ToList();
                    //cmbModifiedBy.Items.Add("ALL");
                    cmbModifiedBy.Items.AddRange(modifiedBys.ToArray());
                    cmbModifiedBy.SelectedIndex = 0;

                    if (modifiedBys != null && modifiedBys.Count > 0)
                    {
                        btnUnReject.Enabled = true;
                    }
                    else
                    {
                        btnUnReject.Enabled = false;
                    }

                    //Load DBTests
                    //clbDBTests.Items.Clear();
                    dbTests = CopiaPlateSamples.Where(dbt => !string.IsNullOrEmpty(dbt.DBTest)).Select(smp => smp.DBTest).Distinct().ToList();
                    //clbDBTests.Items.Add("AllDBTests");
                    //clbDBTests.Items.AddRange(dbTests.ToArray());

                    //lblCureentPage.Text = "0 / " + _totalPages.ToString("#,###");

                    _isCmbLoadedData = true;
                }
            }
            catch (Exception ex)
            {
                string errMsg = "GetCopiaPlateSamples() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                lblMsg.ForeColor = Color.DarkRed;
                lblMsg.Text = errMsg;
            }
        }

    }
}
