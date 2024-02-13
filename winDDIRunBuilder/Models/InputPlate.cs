using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    public class InputPlate : ICloneable
    {
        public string Name { set; get; }
        public bool Rotated { set; get; } = false;
        public Pos Start { set; get; }
        public Pos End { set; get; }
        public List<Pos> Exclude { get; set; }
        public int Offset { get; set; } 
        public string Direction { set; get; }

        public string SourcePlateId { set; get; }

        public string GroupKey { set; get; }

        public string WorklistFormat { get; set; }

        public string OrderKey { set; get; }
        public string Include { get; set; }
        public Dictionary<string, string> Attributes = new Dictionary<string, string>();

        public class Pos
        {
            public string X { set; get; }
            public string Y { set; get; }
        }

        public object Clone()
        {
            return new InputPlate()
            {
                Name = this.Name,
                Start = this.Start == null ? null : new Pos()
                {
                    X = this.Start.X,
                    Y = this.Start.Y
                },
                End = this.End == null ? null : new Pos()
                {
                    X = this.End.X,
                    Y = this.End.Y
                },
                Direction = this.Direction,
                Exclude = this.Exclude,
                Offset = this.Offset,
                Attributes = new Dictionary<string, string>(this.Attributes)
            };
        }
    }
}
