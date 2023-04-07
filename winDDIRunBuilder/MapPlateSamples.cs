using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using winDDIRunBuilder.Models;

namespace winDDIRunBuilder
{
    public class MapPlateSamples
    {
        public DataTable PlateSampleMapTable { get; set; } = new DataTable();
        public string PromoMsg { get; set; } = "";
        public string PromoMsgType { get; set; } = "";
        private DBPlate ScannedDBPalte { get; set; } = new DBPlate();
        private List<PlateSample> ScannedDBPlateSamples { get; set; } = new List<PlateSample>();

        public string FirstSampleWell { get; set; } = "";
        public string LastSampleWell { get; set; } = "";

        private string pAlpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public string GetMapAnyPlateSamples(string plateId)
        {
            string mapResult = "NO-SAMPLES";
            RepoSQL sqlService = new RepoSQL();
            List<DBPlate> anyDBPlates = new List<DBPlate>();
            List<OutputPlateSample> outSamples = new List<OutputPlateSample>();
            string plateIdVersion = "";
            ValidPlate validPlate = new ValidPlate();
            List<OutputPlateSample> samples = new List<OutputPlateSample>();

            ModelTransfer tranService = new ModelTransfer();
            ScannedDBPalte = new DBPlate();
            ScannedDBPlateSamples = new List<PlateSample>();

            string sample = "";

            try
            {
                anyDBPlates = sqlService.GetPlates(plateId);
                if (anyDBPlates != null && anyDBPlates.Count > 0)
                {
                    ScannedDBPalte = anyDBPlates.FirstOrDefault();
                    plateIdVersion = ScannedDBPalte.PlateVersion;
                    validPlate = tranService.DBPlate2ValidPlate(ScannedDBPalte, "DEST");

                    ScannedDBPlateSamples = sqlService.GetPlateSamples(plateId, plateIdVersion);
                    if (ScannedDBPlateSamples != null && ScannedDBPlateSamples.Count > 0)
                    {
                        //Reset windows size
                        //this.Size = new Size(1111, 848);

                        foreach (var smp in ScannedDBPlateSamples)
                        {

                            if (!string.IsNullOrEmpty(smp.SampleType) && (smp.SampleType.IndexOf("QC") >= 0))
                            {
                                sample = "QC_" + smp.SampleId;

                            }
                            else
                            {
                                sample = smp.SampleId;
                            }

                            outSamples.Add(new OutputPlateSample
                            {
                                DestPlateId = smp.PlateId,
                                DestWellId = smp.Well,
                                SampleId = sample   //smp.SampleId
                            });
                        }

                        FirstSampleWell = outSamples.FirstOrDefault().DestWellId;
                        LastSampleWell = outSamples.LastOrDefault().DestWellId;

                        if (validPlate.Direction == "1")
                        {
                            //Rotated
                            MappingPlateSamplesRotated(validPlate, outSamples);
                        }
                        else
                        {
                            MappingPlateSamples(validPlate, outSamples);
                        }
                    }
                    else
                    {
                        PromoMsg = "The histrory plate, " + plateId + " , does not have sample(s).";
                        PromoMsgType = "WARNING";
                    }

                    mapResult = "SUCCESS";
                }
            }
            catch (Exception ex)
            {
                string errMsg = "MapPlateSamples.GetMapAnyPlateSamples() met some issues:";
                errMsg = Environment.NewLine + ex.Message;
                mapResult = errMsg;
                PromoMsg = errMsg;
                PromoMsgType = "SYS-ERROR";

            }

            return mapResult;
        }

        private void MappingPlateSamples(ValidPlate outPlate, List<OutputPlateSample> plateSamples)
        {
            string issues = "";

            try
            {
                Char sizeStartY = char.Parse(outPlate.SizeStartWell[0].ToString());
                int sizeStartX = Convert.ToInt32(outPlate.SizeStartWell.Substring(1));
                Char sizeEndY = char.Parse(outPlate.SizeEndWell[0].ToString());
                int sizeEndX = Convert.ToInt32(outPlate.SizeEndWell.Substring(1));

                Char posStartY = char.Parse(outPlate.StartWell[0].ToString());
                int posStartX = Convert.ToInt32(outPlate.StartWell.Substring(1));
                char posEndY = char.Parse(outPlate.EndWell[0].ToString());
                int posEndX = Convert.ToInt32(outPlate.EndWell.Substring(1));

                DataTable dtPlateSamples = new DataTable();
                string destWellId = "";
                int wellX = 0;
                int wellY = 0;

                //adding columns, plateSizeX  
                for (int i = 0; i <= sizeEndX; i++)
                {
                    dtPlateSamples.Columns.Add(i.ToString());
                }

                //adding rows, plateSizeY
                DataRow drPlate = dtPlateSamples.NewRow();
                for (Char chr = 'A'; chr <= sizeEndY; chr++)
                {
                    drPlate = dtPlateSamples.NewRow();
                    drPlate[0] = chr;
                    dtPlateSamples.Rows.Add(drPlate);
                }

                foreach (var smp in plateSamples)
                {
                    destWellId = GetWell(smp.DestWellId, isBCR: false);
                    wellX = Convert.ToInt32(destWellId.Substring(1));
                    wellY = pAlpha.IndexOf(destWellId.Substring(0, 1));

                    if (wellX <= sizeEndX && wellY < sizeEndY)
                    {
                        dtPlateSamples.Rows[wellY][wellX] = smp.SampleId;
                    }
                    else
                    {
                        issues += "Well is Over PlateSize";
                        issues += Environment.NewLine;
                    }
                }

                //MapPlates(dtPlateSamples);
                PlateSampleMapTable = new DataTable();
                PlateSampleMapTable = dtPlateSamples;

            }
            catch (Exception ex)
            {
                string errMsg = "MapPlateSamples.MappingPlateSamples() has some issue: " +ex.Message;
                PromoMsg = errMsg;
                PromoMsgType = "SYS-ERROR";
            }
        }

        private void MappingPlateSamplesRotated(ValidPlate outPlate, List<OutputPlateSample> plateSamples)
        {
            //Plate rotated
            string issues = "";
            Char sizeStartY = char.Parse(outPlate.SizeStartWell[0].ToString());
            int sizeStartX = Convert.ToInt32(outPlate.SizeStartWell.Substring(1));
            Char sizeEndY = char.Parse(outPlate.SizeEndWell[0].ToString());
            int sizeEndX = Convert.ToInt32(outPlate.SizeEndWell.Substring(1));

            int RotateSizeX = pAlpha.IndexOf(sizeEndY);   //H-A
            int RotateSizeY = sizeEndX;                   //1-12

            string destWellId = "";
            int wellX = 0;
            int wellY = 0;
            int maxColId = 0;
            int wellColSatrtId = 0;
            DataTable dtPlateSamples = new DataTable();

            try
            {
                //adding columns, plateSizeX  
                for (int i = RotateSizeX; i >= 0; i--)
                {
                    dtPlateSamples.Columns.Add(pAlpha.Substring(i, 1));
                }
                dtPlateSamples.Columns.Add("0");
                wellColSatrtId = dtPlateSamples.Columns.Count - 2;
                maxColId = dtPlateSamples.Columns.Count - 1;

                //adding rows, plateSizeY
                DataRow drPlate = dtPlateSamples.NewRow();
                for (int j = 1; j <= RotateSizeY; j++)
                {
                    drPlate = dtPlateSamples.NewRow();
                    drPlate[maxColId] = j.ToString();
                    dtPlateSamples.Rows.Add(drPlate);
                }

                //Mapping samples
                foreach (var smp in plateSamples)
                {
                    destWellId = GetWell(smp.DestWellId, isBCR: false);
                    wellX = Convert.ToInt32(destWellId.Substring(1));
                    wellY = pAlpha.IndexOf(destWellId.Substring(0, 1));

                    if (wellX <= RotateSizeY && wellY <= RotateSizeX)
                    {
                        dtPlateSamples.Rows[wellX - 1][wellColSatrtId - wellY] = smp.SampleId;
                        //dtPlateSamples.Rows[RotateSizeX - wellY][wellX] = smp.SampleId;
                    }
                    else
                    {
                        issues += "Well is Over PlateSize";
                        issues += Environment.NewLine;
                    }
                }

                //MapPlates(dtPlateSamples);
                PlateSampleMapTable = new DataTable();
                PlateSampleMapTable = dtPlateSamples;
            }
            catch (Exception ex)
            {
                string errMsg = "{MapPlateSamples.MappingPlateSamplesRotated()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                PromoMsg = errMsg;
                PromoMsgType = "SYS-ERROR";
            }
        }

        private string GetWell(string xyString, bool isBCR)
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
