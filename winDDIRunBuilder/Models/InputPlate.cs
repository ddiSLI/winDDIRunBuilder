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
        public List<Pos> Exclude { get; set; }
        public int[] Offset { get; set; } = new int[0];
        public string Direction { set; get; }
        public Dictionary<string, string> Attributes = new Dictionary<string, string>();

        public class Pos
        {
            public string X { set; get; }
            public string Y { set; get; }
        }
    }
}
