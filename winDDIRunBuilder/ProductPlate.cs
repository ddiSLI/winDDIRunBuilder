using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using winDDIRunBuilder.Models;

namespace winDDIRunBuilder
{
    public class ProductPlate
    {
        public DBPlate ProdDBPlate { get; set; }
        public List<PlateSample> ProdDBPlateSamples { get; set; }
        public string ErrorMsg { get; set; }

        public ValidPlate GetProductPlate(string plateId, string plateIdVersion = "")
        {
            ValidPlate validPlate = new ValidPlate();
            RepoSQL sqlService = new RepoSQL();
            List<DBPlate> anyDBPlates = new List<DBPlate>();

            ModelTransfer tranService = new ModelTransfer();
            ProdDBPlate = new DBPlate();

            try
            {
                anyDBPlates = sqlService.GetPlates(plateId);
                if (anyDBPlates != null && anyDBPlates.Count > 0)
                {
                    ProdDBPlate = anyDBPlates.LastOrDefault();
                    //ScannedDBPalte = anyDBPlates.FirstOrDefault();
                    plateIdVersion = ProdDBPlate.PlateVersion;
                    validPlate = tranService.DBPlate2ValidPlate(ProdDBPlate, "DEST");
                }
            }
            catch (Exception ex)
            {
                string errMsg = "GetProductPlate() met some issues:";
                errMsg += Environment.NewLine + ex.Message;
                ErrorMsg = errMsg;
            }

            return validPlate;
        }

        public List<OutputPlateSample> GetProductPlateSamples(string plateId, string plateIdVersion = "")
        {
            List<OutputPlateSample> outSamples = new List<OutputPlateSample>();
            RepoSQL sqlService = new RepoSQL();
            ProdDBPlateSamples = new List<PlateSample>();
            string smpWell = "";

            try
            {
                ProdDBPlateSamples = sqlService.GetPlateSamples(plateId, plateIdVersion);
                if (ProdDBPlateSamples != null && ProdDBPlateSamples.Count > 0)
                {
                    foreach (var smp in ProdDBPlateSamples)
                    {
                        smpWell = smp.Well;
                        outSamples.Add(new OutputPlateSample
                        {
                            DestPlateId = smp.PlateId,
                            DestWellId = smpWell,
                            //DestWellId = smp.Well,
                            SampleId = smp.SampleId,
                            Status = smp.Status,
                            SampleType = smp.SampleType
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                string errMsg = "GetPlateSamples() met some issues:";
                errMsg += ex.Message;
                ErrorMsg = errMsg;
            }

            return outSamples;
        }

    }
}
