using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    public class Batch
    {
        public string Id { set; get; }
        public string Name { set; get; }
        public string BatchId { set; get; }
        public string PlateId { set; get; }
        public string Sequence { set; get; }
        public string ShortId { set; get; }
        public string SampleId { set; get; }
        public string PlateSuffix { set; get;}
        public string ModifiedBy { set; get; }
        public string ModifiedDate { set; get; }
        public string ModifiedTool { set; get; }
        public string Well { set; get; }
        public string ORC { set; get; }
        public string Version { set; get; }
    }
}
