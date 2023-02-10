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
                vPlate.Accept = dbPlate.Accept;
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
                vPlate.Accept = dbPlate.Accept;
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
     

    }
}
