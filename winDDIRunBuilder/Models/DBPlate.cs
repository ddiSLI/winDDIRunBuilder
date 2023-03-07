using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    public class DBPlate
    {
        public string PlateName { set; get; }
        public string PlateId { set; get; }
        public string PlateDesc { set; get; } = "";
        public string StartPos { set; get; }
        public string EndPos { set; get; }
        public string SizeStartWell { set; get; }
        public string SizeEndWell { set; get; }
        public string Diluent { set; get; }
        public string ExcludeWells { set; get; }
        public string Sample { set; get; }
        public bool PlateRotated { set; get; }
        public string PlateVersion { set; get; }
        public string Accept { set; get; }
        public string Opt1 { set; get; }
        public string Opt2 { set; get; }
        public string Opt3 { set; get; }
        public string Opt4 { set; get; }
        public string Opt5 { set; get; }
        public string SourcePlateId { set; get; }
        public string SourcePlateVersion { set; get; }
        public string ModifiedDate { set; get; }
        public string ModifiedBy { set; get; }


    }
}
