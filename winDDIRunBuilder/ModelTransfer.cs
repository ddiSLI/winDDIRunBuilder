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

            if (dbSamples != null && dbSamples.Count>0)
            {
                foreach(var dbSmp in dbSamples)
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

    }
}
