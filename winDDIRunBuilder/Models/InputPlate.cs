using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    public class InputPlate
    {
        public string Name { set; get; }
        public Pos Start { set; get; }
        public Pos End { set; get; }
        public string Offset { set; get; }
        public string Direction { set; get; }
        public PlateAttributes Attributes { set; get; }

        public class Pos
        {
            public string X { set; get; }
            public string Y { set; get; }
        }
        
        public class PlateAttributes
        {
            public string Accept { set; get; }
        }
    }
}
