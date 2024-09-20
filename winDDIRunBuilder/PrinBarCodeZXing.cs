using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace winDDIRunBuilder
{
    public class PrinBarCodeZXing
    {
        public Int16 PrtLblLocLeft { get; set; } = 50;
        public Int16 PrtLblLocTop { get; set; } = 20;

        private string pBarcode;
        private bool pShowReadable;
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
            
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.CODE_128;
            writer.Options = new ZXing.Common.EncodingOptions
            {
                Height=50,
                Width=200,
                PureBarcode=false,
                Margin=20
            };

            Image img = writer.Write(pBarcode);

            /////Point loc = new Point(50, 20);
            Point loc = new Point(PrtLblLocLeft, PrtLblLocTop);

            //Testing 
            //Point loc = new Point(-20, 20);

            e.Graphics.DrawImage(img, loc);
            
        }
 
    }
}
