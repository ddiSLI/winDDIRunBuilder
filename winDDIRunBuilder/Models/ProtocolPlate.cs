using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    public class ProtocolPlate
    {
        public string Id { set; get; }
        public string ProtocolName { set; get; }
        public string PlateId { set; get; }
        public string SourcePlateId { set; get; }
        public string WorklistName { set; get; }
        public string StartPos { set; get; }
        public string FlowDirection { set; get; } = "RightToLeft";
        public bool PlateRotated { set; get; } = false;
        public string EndPos { set; get; }
        public string StartX { set; get; }
        public string StartY { set; get; }
        public string EndX { set; get; }
        public string EndY { set; get; }
        public string Offset { set; get; }
        public string Accept { set; get; }
    }
}
