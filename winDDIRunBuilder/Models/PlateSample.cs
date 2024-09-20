using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    public class PlateSample
    {
        public string Id { set; get; }
        public string PlateId { set; get; }
        public int Sequence { set; get; }
        public string SampleId { set; get; }
        public string CopiaSampleId { set; get; }
        public string Well { set; get; }
        public string PlateVersion { set; get; }
        public string SourcePlateId { set; get; }
        public string SourceWell { set; get; }
        public string SourcePlateVersion { set; get; }
        public string SampleType { set; get; } = "";
        public string Status { set; get; } = "";
        public string DBTest { set; get; } = "";
        public string ModifiedDate { set; get; } = "";
        public string ModifiedBy { set; get; } = "";
        public Dictionary<string, string> Attributes { get; set; }
    }
}
