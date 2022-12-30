using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    public class ValidPlate
    {
        public string PlateName { set; get; }
        public string PlateId { set; get; }
        public bool IsNewCreatedPlate { set; get; }
        public string StartWell { set; get; }
        public string EndWell { set; get; }
        public string StartX { set; get; }
        public string StartY { set; get; }
        public string EndX { set; get; }
        public string EndY { set; get; }
        public string Offset { set; get; }
        public string Direction { set; get; }
        public string Accept { set; get; }
        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
        public string Sample { set; get; }
        public string Diluent { set; get; }
        public string PlateVersion { set; get; }
        public string SourcePlateId { set; get; }
        public string SourcePlateVersion { set; get; }
        public string WorkList { set; get; }
    }
}
