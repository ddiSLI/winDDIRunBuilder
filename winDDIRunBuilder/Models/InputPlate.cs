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
        public Dictionary<string, string> Attributes = new Dictionary<string, string>();
        //public PlateAttributes Attributes { set; get; } = new PlateAttributes();

        public class Pos
        {
            public string X { set; get; }
            public string Y { set; get; }
        }
        
        //public class PlateAttributes
        //{
        //    public string Accept { set; get; }
        //}
    }
}
