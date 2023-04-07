using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder
{
    public class InputFile
    {
        public int Id { get; set; }
        public string FullSampleId { get; set; }
        public string ShortId { get; set; }   // scanner read sample label
        public string RackName { get; set; }
        public string Position { get; set; }
        public string SourcePlate { get; set; }
        public string PlateId { get; set; }     //destPlate
        public string Well { get; set; }
        public string SampleType { get; set; } = "";
        public string WellX { get; set; }
        public string WellY { get; set; }

        public static InputFile ReadInputFile(string csvLine)
        {
            string[] values = csvLine.Split(',');
            InputFile inputFile = new InputFile();

            if (IsDigitsOnly(values[2].Trim()))
            {
                inputFile.ShortId = values[0].Trim();    // scanner read sample label
                inputFile.RackName = values[1].Trim();
                inputFile.Position = values[2].Trim();
                
                inputFile.FullSampleId = "";
            }
            else
            {
                return inputFile;
            }            
           
            return inputFile;
        }

        static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
               
    }
}
