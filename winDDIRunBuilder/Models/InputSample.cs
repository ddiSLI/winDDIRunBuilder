using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    public class InputSample
    {
        public Dictionary<string, string> Attributes { set; get; } = new Dictionary<string, string>();
    }

    public class SampleAttributes
    {
        public string SampleId { set; get; }
    }
}
