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

namespace winDDIRunBuilder
{
    public partial class frmPlateSetting : Form
    {
        public string ProtocolName { get; set; }
        public List<Worklist> Worklists { get; set; }
        public List<ProtocolPlate> ProtocolPlates { get; set; }
        public ClientRunBuilder CurRunBuilder { get; set; }
        public List<InputFile> InputFileValues { get; set; }
        private List<Plate> GetBatchPlates { get; set; }



        public frmPlateSetting()
        {
            InitializeComponent();
        }

        private void frmPlateSetting_Load(object sender, EventArgs e)
        {
            this.Text = ProtocolName + " Setting - DDI Run Builder";
            lblProtocol.Text = ProtocolName;
            string[] rwPlate;

            string included = "true";
            string destPlate = "";
            string sourcePlate = "";
            string SourcePlateIsNew = "true";
            string desc = "nothing happened";
            string worklist = "";

            try
            {
                Worklists = new List<Worklist>();

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

                    //BatchPlateSetting();

                    BCRFilesChecking();

                    PlatesChecking();
                }
            }
            catch (Exception ex)
            {
                string errMsg = "frmPlateSetting_Load() met some issues:";
                errMsg = Environment.NewLine + ex.Message;
            }

        }

        private void fileWatcherBCR_Created(object sender, FileSystemEventArgs e)
        {
            string filePath = e.FullPath;
            string fileName = e.Name;

            if (Directory.GetFiles(CurRunBuilder.ReadFilePath, "*.CSV").Length > 0)
            {
                lblMsg.Text = "The BCR SampleFile.csv is created. The program is processing the file....";

                BCRFilesChecking();
                lblMsg.Text = "The BCR SampleFile.csv is processed";
            }
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
                    bool isReadySourcePlate = Convert.ToBoolean(senderGrid.CurrentRow.Cells["SourcePlateIsNew"].Value);

                    sourcePlate = (string)senderGrid.CurrentRow.Cells["SourcePlate"].Value;
                    destPlate = (string)senderGrid.CurrentRow.Cells["DestPlate"].Value;

                    if (senderGrid.Columns[e.ColumnIndex].Name == "SourcePlateIsNew" && senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                    {
                        senderGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);

                        isReadySourcePlate = Convert.ToBoolean(senderGrid.CurrentRow.Cells["SourcePlateIsNew"].Value);
                        if (isReadySourcePlate == false && sourcePlate != "BCR")
                        {
                            frmImportFromDB importDB = new frmImportFromDB();
                            importDB.ShowDialog();
                            if (importDB.DialogResult == DialogResult.Yes)
                            {
                                GetBatchPlates.RemoveAll(bp => bp.PlateId.ToUpper() == sourcePlate.ToUpper());
                                //GetBatchPlates.AddRange(importDB.GetBatchPlates);
                                TransforDBPlateToInputValue(importDB.DBPlateId);

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

        private void TransforDBPlateToInputValue(string dbPlate)
        {
            if (GetBatchPlates.Count > 0)
            {
                InputFileValues.RemoveAll(inp => inp.PlateId.ToUpper() == dbPlate.ToUpper());
                foreach (var dbSmp in GetBatchPlates)
                {
                    InputFileValues.Add(new InputFile
                    {
                        Position = dbSmp.Sequence.ToString(),
                        FullSampleId = dbSmp.SampleId,
                        ShortId = dbSmp.ShortId,
                        BatchId = dbSmp.BatchId,
                        PlateId = dbPlate,
                        RackName = dbPlate
                    }); ;
                }
            }
        }

        private string ImportBCRPlateSamples()
        {
            string actionResult = "";
            RepoSQLOracle repoService = new RepoSQLOracle();
            List<InputFile> sampleValues = new List<InputFile>();
            string importFile = "";
            string csvName = "";
            string csvFullName = "";

            try
            {
                var csvFiles = Directory.GetFiles(CurRunBuilder.ReadFilePath, "*.CSV");
                if (csvFiles.Length > 0)
                {
                    foreach (var csvFile in csvFiles)
                    {
                        csvFullName = Path.GetFileName(csvFile);
                        csvName = Path.GetFileNameWithoutExtension(csvFile).ToUpper();

                        importFile = CurRunBuilder.ReadFilePath + csvFullName;
                        List<InputFile> rawValues = File.ReadAllLines(importFile)
                            .Skip(1)
                            .Select(v => InputFile.ReadInputFile(v))
                            .ToList();

                        if (rawValues.Count > 0)
                        {
                            sampleValues = repoService.GetShortSamples(rawValues);
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
                                        PlateId = smp.PlateId
                                    });
                                }
                            }
                        }
                    }

                    actionResult = "SUCCESS";
                }

            }
            catch (Exception ex)
            {
                string errMsg = "ImportBCRPlateSamples() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                actionResult = "ERROR:" + errMsg;
            }

            return actionResult;
        }

        private void ProcessBCRFile()
        {
            string worklistName = "";
            string destPlate = "";
            string sourcePlate = "";
            bool included = true;
            bool isReadyPlate = true;

            try
            {
                if (InputFileValues != null && InputFileValues.Count > 0)
                {
                    foreach (DataGridViewRow rwPlate in dgvPlateSet.Rows)
                    {
                        destPlate = rwPlate.Cells["DestPlate"].Value.ToString().ToUpper();
                        worklistName = destPlate + ".CSV";
                        sourcePlate = rwPlate.Cells["SourcePlate"].Value.ToString();
                        included = Convert.ToBoolean(rwPlate.Cells["Included"].Value);
                        isReadyPlate = Convert.ToBoolean(rwPlate.Cells["SourcePlateIsNew"].Value);

                        if (sourcePlate == "BCR")
                        {
                            var samples = InputFileValues.Where(inp => inp.PlateId.ToUpper() == destPlate).ToList();
                            if (samples.Count > 0)
                            {
                                rwPlate.Cells["PlateDesc"].Value = "BCR file is ready to go";
                                rwPlate.DefaultCellStyle.BackColor = Color.LightGreen;
                                rwPlate.Cells["WorkList"].Value = worklistName;
                            }
                            else
                            {
                                rwPlate.Cells["PlateDesc"].Value = "BCR file is NOT ready";
                                rwPlate.DefaultCellStyle.BackColor = Color.LightGray;
                                rwPlate.Cells["WorkList"].Value = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = "ProcessBCRFile() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;

            }
        }

        private void BCRFilesChecking()
        {
            if (Directory.GetFiles(CurRunBuilder.ReadFilePath, "*.CSV").Length > 0)
            {
                string csvName = "";
                string csvFullName = "";
                string destPlate = "";
                string sourcePlate = "";
                bool included = true;
                bool isReadyPlate = true;

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
                        if (sourcePlate == "BCR" && destPlate == csvName)
                        {
                            rwPlate.Cells["PlateDesc"].Value = "BCR file is ready to go";
                            if (included && isReadyPlate)
                            {
                                ImportBCRPlate(csvFullName, destPlate);

                                rwPlate.DefaultCellStyle.BackColor = Color.LightGreen;
                                rwPlate.Cells["WorkList"].Value = csvFullName;
                            }

                        }
                        //else if (sourcePlate == "BCR" && destPlate != csvName)
                        //{
                        //    rwPlate.Cells["PlateDesc"].Value = "BCR file is UN-Available";
                        //    rwPlate.DefaultCellStyle.BackColor = Color.LightYellow;
                        //    rwPlate.Cells["WorkList"].Value = "";
                        //}
                    }
                }

            }
        }



        private void ImportBCRPlate(string importFile, string plateId)
        {
            RepoSQLOracle repoService = new RepoSQLOracle();
            //importFile = "InputFile.csv";

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
                    sampleValues = repoService.GetShortSamples(rawValues);
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
                                PlateId = plateId
                                //PlateId = smp.PlateId
                            });
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = "ImportBCRPlate() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }
        }

        private string BatchPlateSetting(string batchId = "", string plateId = "")
        {
            string getResult = "NA";
            string sourcePlate = "";

            RepoSQLOracle repoService = new RepoSQLOracle();
            List<Batch> getBatch = new List<Batch>();
            //string batchId = "";
            string plateSuffix = "";
            //string plateId = "";

            try
            {
                GetBatchPlates = new List<Plate>();
                foreach (DataGridViewRow rwPlate in dgvPlateSet.Rows)
                {

                    sourcePlate = rwPlate.Cells["SourcePlate"].Value.ToString();
                    if (sourcePlate != "BCR")
                    {

                        //getBatch = repoService.GetBatch(batchId);
                        //getBatchPlates = repoService.GetBatch(PlateId);
                        if (getBatch.Count > 0)
                        {

                            foreach (var bat in getBatch)
                            {
                                plateSuffix = bat.PlateSuffix;
                                plateId = plateSuffix.Substring(plateSuffix.IndexOf("_") + 1);

                                GetBatchPlates.Add(new Plate
                                {
                                    Sequence = Convert.ToInt32(bat.Sequence),
                                    SampleId = bat.SampleId,
                                    ShortId = bat.ShortId,
                                    BatchId = bat.BatchId,
                                    PlateId = plateId,
                                    WellX = bat.Well == null ? "" : bat.Well.Substring(0, 1),
                                    WellY = bat.Well == null ? "" : bat.Well.Substring(1),
                                    BatchVersion = bat.Version
                                });
                            }

                            rwPlate.Cells["PlateDesc"].Value = "Plate file is ready to go";
                            rwPlate.Cells["WorkList"].Value = sourcePlate + ".CSV";
                            rwPlate.DefaultCellStyle.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            rwPlate.Cells["PlateDesc"].Value = "Plate file is UN-Available";
                            rwPlate.DefaultCellStyle.BackColor = Color.LightYellow;
                            rwPlate.Cells["WorkList"].Value = "";
                        }

                    }

                    getResult = "DONE";
                }

            }
            catch (Exception ex)
            {
                string errMsg = "BatchPlateSetting() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }

            return getResult;
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
                worklist = rwPlate.Cells["WorkList"].Value.ToString();
                if (included && sourcePlate == "BCR" && worklist.Length <= 0)
                {
                    isReadyBCR = false;
                }
                else if (included && sourcePlate != "BCR" && worklist.Length <= 0)
                {
                    isReadyDBPlates = false;
                }
            }

            if (isReadyBCR && isReadyDBPlates)
            {
                btnGo.Enabled = true;
            }
            else
            {
                btnGo.Enabled = false;
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow grPlt in dgvPlateSet.Rows)
            {

                if (Convert.ToBoolean(grPlt.Cells["Included"].Value) == true)
                {
                    Worklists.Add(new Worklist
                    {
                        WorklistName = grPlt.Cells["WorkList"].Value.ToString(),
                        PlateId = grPlt.Cells["DestPlate"].Value.ToString()
                    });
                }
            }

            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lblMsg_Click(object sender, EventArgs e)
        {

        }

        private void fileWatcherBCR_Changed(object sender, FileSystemEventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintBarCode printPlateBarcode = new PrintBarCode();
          //  printPlateBarcode.Print("Siga111012022");
        }
    }
}
