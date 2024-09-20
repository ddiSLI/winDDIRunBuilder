using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using winDDIRunBuilder.Models;

namespace winDDIRunBuilder
{
    public partial class frmPlateSample : Form
    {
        private string CurPlateId { get; set; }

        public Int16 PrtLblLocLeft { get; set; }
        public Int16 PrtLblLocTop { get; set; }

        public frmPlateSample()
        {
            InitializeComponent();
        }

        private void frmPlateSample_Load(object sender, EventArgs e)
        {
            ActiveControl = txbEnterSampleId;
            //txbEnterSampleId.Focus();
        }

        private void txbBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lblMsg.ForeColor =SystemColors.Control;
                lblMsg.Text = "";

                LoadPlateSamples(txbBarcode.Text.Trim());

                txbEnterSampleId.Focus();
            }
        }

        private void txbEnterSampleId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int iRw = 0;
                string smpId = "";

                lblMsg.ForeColor = SystemColors.Control;
                lblMsg.Text = "";

                if (!string.IsNullOrEmpty(txbEnterSampleId.Text.Trim()))
                {
                    foreach(DataGridViewRow smpRW in dgvSamples.Rows)
                    {
                        smpId = smpRW.Cells["SampleId"].Value.ToString().ToUpper();
                        if (smpId == txbEnterSampleId.Text.Trim().ToUpper())
                        {
                            smpRW.Cells["Checked"].Value = true;
                            smpRW.DefaultCellStyle.BackColor = Color.LightGreen;
                        }

                    }

                    //dgvSamples.Rows.Add();
                    //iRw = dgvSamples.Rows.Count - 1;
                    //dgvSamples.Rows[iRw].Cells["SampleId"].Value = txbEnterSampleId.Text.Trim();
                    //dgvSamples.Rows[iRw].Cells["Well"].Value = "";
                    //dgvSamples.Rows[iRw].Cells["Status"].Value = "Scanned";
                    //dgvSamples.Rows[iRw].Cells["Type"].Value = "";
                    //dgvSamples.Rows[iRw].Cells["ToPrint"].Value = false;
                    //dgvSamples.Rows[iRw].Cells["Checked"].Value = false;
                }

                txbEnterSampleId.Text = "";
                txbEnterSampleId.Focus();
            }

        }

        private void LoadPlateSamples(string loadPlateId)
        {
             ProductPlate prodPlate = new ProductPlate();
            List<OutputPlateSample> outSamples = new List<OutputPlateSample>();
            ValidPlate validPlate = new ValidPlate();
           // List<OutputPlateSample> samples = new List<OutputPlateSample>();
            int iRw = 0;

            try
            {
                lblMsg.ForeColor = SystemColors.Control;
                lblMsg.Text = "";

                dgvSamples.Rows.Clear();

                if (!string.IsNullOrEmpty(loadPlateId.Trim()))
                {
                    validPlate = prodPlate.GetProductPlate(loadPlateId, plateIdVersion: "");
                    if (validPlate != null)
                    {
                        outSamples = prodPlate.GetProductPlateSamples(validPlate.PlateId, validPlate.PlateVersion);
                        if (outSamples != null && outSamples.Count > 0)
                        {
                            CurPlateId = loadPlateId;
                            lblPlate.Text = " Current Plate; " + Environment.NewLine + CurPlateId;

                            foreach(var smp in outSamples)
                            {
                                dgvSamples.Rows.Add();
                                iRw = dgvSamples.Rows.Count - 1;
                                dgvSamples.Rows[iRw].Cells["SampleId"].Value = smp.SampleId;
                                dgvSamples.Rows[iRw].Cells["Well"].Value = smp.DestWellId;
                                dgvSamples.Rows[iRw].Cells["Status"].Value = smp.Status;
                                dgvSamples.Rows[iRw].Cells["Type"].Value = smp.SampleType;
                                dgvSamples.Rows[iRw].Cells["ToPrint"].Value = false;
                                dgvSamples.Rows[iRw].Cells["Checked"].Value = false;
                            }
                        }
                        else
                        {
                            lblMsg.ForeColor = Color.Red;
                            lblMsg.Text = "The histrory plate, " + loadPlateId + " , does not have sample(s).";
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                string errMsg = "LoadPlateSamples() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }
            finally
            {
                txbBarcode.Text = "";
                txbEnterSampleId.Focus();
            }
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            if (dgvSamples.Rows.Count > 0)
            {
                foreach (DataGridViewRow smpRw in dgvSamples.Rows)
                {
                    smpRw.Cells["ToPrint"].Value= true;
                }

            }

            txbEnterSampleId.Focus();
        }

        private void btnUncheck_Click(object sender, EventArgs e)
        {
            if (dgvSamples.Rows.Count > 0)
            {
                foreach (DataGridViewRow smpRw in dgvSamples.Rows)
                {
                    smpRw.Cells["ToPrint"].Value = false;
                }

            }

            txbEnterSampleId.Focus();
        }

       
        private void btnPrintLbl_Click(object sender, EventArgs e)
        {
            string barcode = "";
            
            PrinBarCodeZXing printSampleBarcode = new PrinBarCodeZXing();

            try
            {
                if (dgvSamples.Rows.Count > 0)
                {
                    foreach (DataGridViewRow smpRw in dgvSamples.Rows)
                    {
                        if ((bool)smpRw.Cells["ToPrint"].Value) 
                        {
                            barcode = smpRw.Cells["SampleId"].Value.ToString();

                            printSampleBarcode = new PrinBarCodeZXing();
                            printSampleBarcode.PrtLblLocLeft = PrtLblLocLeft;
                            printSampleBarcode.PrtLblLocTop = PrtLblLocTop;
                            printSampleBarcode.Print(barcode, GetPrinterName());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string errMsg = "LoadPlateSamples() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }
            finally
            {
                txbEnterSampleId.Focus();
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

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddQC_Click(object sender, EventArgs e)
        {

        }
    }
}
