using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    public class PlateDTO
    {
        public Dictionary<string,object> Attributes { get; set; }
        public Well End { get; set; }
        public List<Well> Exclude { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public PlateDTO Next { get; set; }
        public List<SampleDTO> Samples { get; set; }
        public Well Start { get; set; }
    }

    public class SampleDTO
    {
        public Dictionary<string,object> Attributes { get; set; }
        public Well Well { get; set; }
        public string SampleId
        {
            get
            {
                if (Attributes.TryGetValue("SampleId", out var id))
                {
                    return id as string;
                }
                return null;
            }
        }
    }

    public class Well
    {
        public int X { get; set; }
        public int Y { get; set; }
    }


}
