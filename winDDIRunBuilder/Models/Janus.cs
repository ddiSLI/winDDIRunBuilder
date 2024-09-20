using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    public class Janus
    {
        public string Id { set; get; }
        public string Department { set; get; }
        public string JanusName { set; get; }
        public string HostName { set; get; }
        public string BCROutput { set; get; }
        public string BCRArchive { set; get; }
        public string RunBuilderOutput { set; get; }
        public string RunBuilderOutputArchive { set; get; }
        public string RunBuilderExport { set; get; }
        public string JanusOutPut { set; get; }
        public string Description { set; get; }
        public Int16 PrtLocLeft { set; get; }
        public Int16 PrtLocTop { set; get; }

    }
}
