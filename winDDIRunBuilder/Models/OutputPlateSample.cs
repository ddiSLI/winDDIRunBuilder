using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    public class OutputPlateSample
    {
        public string Id { set; get; }
        public int Sequence { set; get; }
        public string SourcePlateId { set; get; }
        public string SourceWellId { set; get; }
        public string DestPlateId { set; get; }
        public string DestWellId { set; get; }
        public string Accept { set; get; }
        public string SampleId { set; get; }
        public Dictionary<string,string> Attributes { get; set; }

    }
}
