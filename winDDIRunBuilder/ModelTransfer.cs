using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using winDDIRunBuilder.Models;

namespace winDDIRunBuilder
{
    public class ModelTransfer
    {
        public ValidPlate DBPlate2ValidPlate(DBPlate dbPlate, string plateType)
        {
            ValidPlate vPlate = new ValidPlate();

            if (plateType == "DEST")
            {
                vPlate.PlateName = dbPlate.PlateName;
                vPlate.PlateId = dbPlate.PlateId;
                vPlate.PlateType = "DEST";
                vPlate.Rotated = dbPlate.PlateRotated;
                vPlate.StartWell = dbPlate.StartPos;
                vPlate.EndWell = dbPlate.EndPos;
                vPlate.SizeStartWell = dbPlate.SizeStartWell;
                vPlate.SizeEndWell = dbPlate.SizeEndWell;
                vPlate.ExcludeWells = dbPlate.ExcludeWells;
                vPlate.Sample = dbPlate.Sample;
                vPlate.Diluent = dbPlate.Diluent;
                vPlate.Direction = dbPlate.PlateRotated == true ? "1" : "0";
                vPlate.PlateVersion = dbPlate.PlateVersion;
                vPlate.Accept = dbPlate.Accept.ToString().Replace("|", ",");
                vPlate.SourcePlateId = dbPlate.SourcePlateId;
                vPlate.SourcePlateVersion = dbPlate.SourcePlateVersion;
                vPlate.Attributes.Add("Sample", dbPlate.Sample.ToString());
                vPlate.Attributes.Add("Diluent", dbPlate.Diluent.ToString());
                vPlate.Attributes.Add("Accept", dbPlate.Accept == null ? "" : dbPlate.Accept.ToString());
                vPlate.Attributes.Add("Opt1", dbPlate.Opt1);
                vPlate.Attributes.Add("Opt2", dbPlate.Opt2);
                vPlate.Attributes.Add("Opt3", dbPlate.Opt3);
                vPlate.Attributes.Add("Opt4", dbPlate.Opt4);
                vPlate.Attributes.Add("Opt5", dbPlate.Opt5);
            }
            else
            {
                vPlate.PlateName = dbPlate.PlateName;
                vPlate.PlateId = dbPlate.PlateId;
                vPlate.Rotated = dbPlate.PlateRotated;
                vPlate.StartWell = dbPlate.StartPos;
                vPlate.EndWell = dbPlate.EndPos;
                vPlate.SizeStartWell = dbPlate.SizeStartWell;
                vPlate.SizeEndWell = dbPlate.SizeEndWell;
                vPlate.ExcludeWells = dbPlate.ExcludeWells;
                vPlate.Sample = dbPlate.Sample;
                vPlate.Diluent = dbPlate.Diluent;
                vPlate.Direction = dbPlate.PlateRotated == true ? "1" : "0";
                vPlate.PlateVersion = dbPlate.PlateVersion;
                vPlate.Accept = dbPlate.Accept.ToString().Replace("|", ",");
                vPlate.SourcePlateId = dbPlate.SourcePlateId;
                vPlate.SourcePlateVersion = dbPlate.SourcePlateVersion;
                vPlate.Attributes.Add("Sample", dbPlate.Sample.ToString());
                vPlate.Attributes.Add("Diluent", dbPlate.Diluent.ToString());
                vPlate.Attributes.Add("Accept", dbPlate.Accept == null ? "" : dbPlate.Accept.ToString());
                vPlate.Attributes.Add("Opt1", dbPlate.Opt1);
                vPlate.Attributes.Add("Opt2", dbPlate.Opt2);
                vPlate.Attributes.Add("Opt3", dbPlate.Opt3);
                vPlate.Attributes.Add("Opt4", dbPlate.Opt4);
                vPlate.Attributes.Add("Opt5", dbPlate.Opt5);
            }


            return vPlate;
        }

        public List<OutputPlateSample> DBSample2Outputs(List<PlateSample> dbSamples)
        {
            List<OutputPlateSample> outSamples = new List<OutputPlateSample>();
            OutputPlateSample outSample = new OutputPlateSample();

            if (dbSamples != null && dbSamples.Count > 0)
            {
                foreach (var dbSmp in dbSamples)
                {
                    outSample = new OutputPlateSample();
                    outSample.DestPlateId = dbSmp.PlateId;
                    outSample.DestPlateVersion = dbSmp.PlateVersion;
                    outSample.DestWellId = dbSmp.Well;
                    outSample.SourcePlateId = dbSmp.SourcePlateId;
                    outSample.SourceWellId = dbSmp.SourceWell;
                    outSample.SourcePlateVersion = dbSmp.SourcePlateVersion;
                    outSample.SampleId = dbSmp.SampleId;

                    outSamples.Add(outSample);
                }
            }

            return outSamples;
        }

        public string GetNextWell(string plateSize, string lastSampleWell, bool isRotated = false)
        {
            string nextWell = "";
            string pltSizeX = "";
            string pltSizeY = "";

            string wellX = "";
            string wellY = "";

            if (isRotated)
            {
                pltSizeX = plateSize.Substring(0, 1);
                pltSizeY = plateSize.Substring(1);

                if ((Char.Parse(lastSampleWell.Substring(0, 1)) <= Char.Parse(pltSizeX)) &&
                    (Convert.ToInt32(lastSampleWell.Substring(1)) < Convert.ToInt32(pltSizeY)))
                {
                    wellX = lastSampleWell.Substring(0, 1);
                    wellY = (Convert.ToInt32(lastSampleWell.Substring(1)) + 1).ToString();
                }
                else if (Convert.ToInt32(lastSampleWell.Substring(1)) == Convert.ToInt32(pltSizeY))
                {
                    wellX = ((char)((int)Char.Parse(lastSampleWell.Substring(0, 1)) + 1)).ToString();
                    wellY = "1";
                }

                nextWell = wellX + wellY;
            }
            else if (isRotated == false)
            {
                pltSizeX = plateSize.Substring(1);
                pltSizeY = plateSize.Substring(0, 1);

                if (Char.Parse(lastSampleWell.Substring(0, 1)) < Char.Parse(pltSizeY))
                {
                    wellX = lastSampleWell.Substring(1);
                    wellY = ((char)((int)Char.Parse(lastSampleWell.Substring(0, 1)) + 1)).ToString();
                }
                else
                {
                    if (Convert.ToInt32(lastSampleWell.Substring(1)) < Convert.ToInt32(pltSizeX))
                    {
                        wellX = (Convert.ToInt32(lastSampleWell.Substring(1)) + 1).ToString();
                        wellY = "A";
                    }
                }

                nextWell = wellY + wellX;
            }

            return nextWell;
        }
        public string GetWell(string xyString, bool isBCR)
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

        public string GetYearSampleId(string idWithYear)
        {
            string yearSampleId = "";

            //testing
            //idWithYear = "0717046601X2";
            //
            if (idWithYear.ToUpper().IndexOf("X") >= 0)
            {
                idWithYear = idWithYear.Substring(0, idWithYear.ToUpper().IndexOf('X'));
                //var str = $"{DateTime.Now.Year.ToString().Substring(2)}{idWithYear.Substring(0, 4)}";

                //for (var i = DateTime.Now.Year; ; i--)
                //{
                //    var year = i;
                //    var month = int.Parse(idWithYear.Substring(0, 2));
                //    var day = int.Parse(idWithYear.Substring(2, 2));
                //    var date = new DateTime(year, month, day);
                //    var diff = DateTime.Now.Subtract(date);

                //    if (diff.TotalMilliseconds < 0)
                //    {
                //        continue;
                //    }
                //    else
                //    {
                //        yearSampleId = year.ToString().Substring(2) + idWithYear;
                //        break;
                //    }
                //}

                yearSampleId = idWithYear;
            }
            else
            {
                yearSampleId = idWithYear;
            }


            return yearSampleId;
        }


    }
}
