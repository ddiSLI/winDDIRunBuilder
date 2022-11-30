using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    public class SamplePlate
    {
        public string Id { set; get; }    //sys
        public string Name { set; get; }  //sys
        public string PlateId { set; get; }  //sysw
        public string SampleId { set; get; }
        public string SourceRack { set; get; }
        public string SourcePosition { set; get; }
        public string DestRack { set; get; }
        public string DestPosition { set; get; }
        public int Samples { set; get; }
        public string Diluent { set; get; }

        public string Aseprite { set; get; }   //?
    }
}
