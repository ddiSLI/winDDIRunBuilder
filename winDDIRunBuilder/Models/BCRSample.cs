using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    public class BCRSample
    {
        public int Id { get; set; }
        public string FullSampleId { get; set; }
        public string ShortId { get; set; }   // scanner read sample label
        public string RackName { get; set; }
        public string Position { get; set; }
        public string SourcePlate { get; set; }
        public string PlateId { get; set; }     //destPlate
        public string Well { get; set; }
        public string BatchId { get; set; }
        public string WellX { get; set; }
        public string WellY { get; set; }
    }
}
