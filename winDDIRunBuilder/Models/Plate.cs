using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    public class Plate
    {
        public int Id { set; get; }
        public string BatchId { set; get; }
        public string ProtocolId { set; get; }
        public string ProtocolName { set; get; }
        public string PlateId { set; get; }
        public int Sequence { set; get; }
        public string ShortId { set; get; }
        public string SampleId { set; get; }
        public string WellX { set; get; }
        public string WellY { set; get; }
        public string BatchVersion { set; get; }

    }

}
