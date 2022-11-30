using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    class RunBuilderInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class OrcShortIdRef
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ShortId { get; set; }
        public string SampleId { get; set; }
    }

    public class OrcWebProfile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ReportProfile { get; set; }
    }

}
