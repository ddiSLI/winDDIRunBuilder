using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using winDDIRunBuilder.Models;
using static winDDIRunBuilder.Models.InputPlate;

namespace winDDIRunBuilder
{
    public class BCRFile
    {
        public string CurUniqueId { get; set; } = "";
        public InputPlate BCRPlate { get; set; }

        public static BCRSample ReadInputFile(string csvLine)
        {
            string[] values = csvLine.Split(',');
            BCRSample bcrSample = new BCRSample();

            if (IsDigitsOnly(values[2].Trim()))
            {
                bcrSample.ShortId = values[0].Trim();    // scanner read sample label
                bcrSample.RackName = values[1].Trim();
                bcrSample.Position = values[2].Trim();

                bcrSample.FullSampleId = "";
            }
            else
            {
                return bcrSample;
            }

            return bcrSample;
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

        public List<BCRSample> BuildBCRPlateSamples(string bcrFile)
        {
            List<BCRSample> bcrSamples = new List<BCRSample>();
            BCRPlate = new InputPlate();

            try
            {
                //bcrFile = CurRunBuilder.BCROutput + bcrFile;

                List<InputFile> rawValues = File.ReadAllLines(bcrFile)
                    .Skip(1)
                    .Select(v => InputFile.ReadInputFile(v))
                    .ToList();

                if (rawValues != null && rawValues.Count > 0)
                {
                    foreach (var smp in rawValues)
                    {
                        bcrSamples.Add(new BCRSample
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

                    var startSample = bcrSamples.FirstOrDefault();
                    var endSample = bcrSamples.LastOrDefault();

                    Pos startPos = new Pos();
                    Pos endPos = new Pos();

                    startPos.X = startSample.WellX;
                    startPos.Y = startSample.WellY;
                    endPos.X = endSample.WellX;
                    endPos.Y = endSample.WellY;

                    BCRPlate.Start = startPos;
                    BCRPlate.End = endPos;
                    BCRPlate.Name = "BCR" + CurUniqueId;
                    BCRPlate.Direction = "0";
                }
            }
            catch (Exception ex)
            {
                string errMsg = "BCRFile.BuildBCRPlateSamples() met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }

            return bcrSamples;
        }

    }
}
