using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zen.Barcode;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace winDDIRunBuilder
{
    public class PrintBarCode
    {
        private string pBarcode;
        private bool pShowReadable;
        private static Font pBarcodeFont = new Font("BarCode", 16);
        private static Font pPrintFont = new Font("Times New Roman", 10);
        //public PrintBarCode(string barCode, bool showReadable=false)
        //{
        //    pBarcode = barCode;
        //    pShowReadable = showReadable;
        //}

        public void Print(string barCode, string barPrinterName, bool showReadable = false)
        {
            pBarcode = barCode;
            pShowReadable = showReadable;

            PrintDocument prtDoc = new PrintDocument();
            prtDoc.PrintPage += new PrintPageEventHandler(ToPrinter128);

            // For debugging purposes, only print to the PrintPreview 
            //#if DEBUG
            //            PrintPreviewDialog prtPreview = new PrintPreviewDialog();
            //            prtPreview.Document = prtDoc;
            //            prtPreview.Show();
            //#else
            //            prtDoc.Print();
            //#endif

            prtDoc.PrinterSettings.PrinterName = barPrinterName;
            prtDoc.Print();

        }

        private void ToPrinter128(Object sender, PrintPageEventArgs e)
        {
            //System.Drawing.Image img = System.Drawing.Image.FromFile("D:\\Foto.jpg");
            int maxheight = 40;
            Code128BarcodeDraw barcode128 = BarcodeDrawFactory.Code128WithChecksum;

            Image img = barcode128.Draw(pBarcode, maxheight);
            //Image img = barcode128.Draw("TESTBARCODE", maxheight);

            //Point loc = new Point(100, 100);
            Point loc = new Point(60, 20);
            e.Graphics.DrawImage(img, loc);

        }

        private void ToPrinter(Object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush br = new SolidBrush(Color.Black);

            // the Asterisk (*) is used to delimit the barcode, and is required as the start and stop charachters by the 3of9 Symbology
            g.DrawString("*" + pBarcode + "*", pBarcodeFont, br, 60, 20);

            if (pShowReadable)
            {
                g.DrawString(pBarcode, pPrintFont, br, 50, 65);
            }
        }

        private string PrintBarcode(string barCode)
        {
            string printResult = "";

            // string barCode = txbBarcode.Text;
            Bitmap bitMap = new Bitmap(barCode.Length * 40, 80);

            using (Graphics graphics = Graphics.FromImage(bitMap))
            {
                Font oFont = new Font("BarCode", 16);
                PointF point = new PointF(2f, 2f);
                SolidBrush blackBrush = new SolidBrush(Color.Black);
                SolidBrush whiteBrush = new SolidBrush(Color.White);
                graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
            }

            //using (MemoryStream ms = new MemoryStream())
            //{
            //    bitMap.Save(ms, ImageFormat.Png);
            //    pictureBox1.Image = bitMap;
            //    pictureBox1.Height = bitMap.Height;
            //    pictureBox1.Width = bitMap.Width;
            //}

            return printResult;
        }

    }
}
