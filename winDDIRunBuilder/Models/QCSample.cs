using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    public class QCSample
    {
        public string Id { set; get; } = "";
        public string PlateId { set; get; }
        public string Plate { set; get; }
        public string Sample { set; get; }
        public string Prefix { set; get; }
        public string HarvestId { set; get; }
        public string PlateDesc { set; get; } = "";
        public string Well { set; get; }
        public string WellX { set; get; }
        public string WellY { set; get; }
    }
}
