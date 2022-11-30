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
        public string Well { set; get; }
        public int PosX { set; get; }
        public int PosY { set; get; }
        public int Offset { set; get; }
        public string PlateVersion { set; get; }
    }
}
