using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    public class Protocol
    {
        public string Id { set; get; }
        public string Department { set; get; }
        public string Name { set; get; }
        public string PlateId { set; get; }
        public string SourcePlate { set; get; }
        public string ProtocolName { set; get; }
        public string WorklistName { set; get; }
        public bool HasAliasId { set; get; } = false;
        public bool Pooling { set; get; } = false;
        public string Database { set; get; }
        public string DBTable { set; get; }
        public string DBTest { set; get; }
        public bool PlateRotated { set; get; } = false;
        public string StartPos { set; get; }
        public string EndPos { set; get; }
        public string ExcludeWells { set; get; }
        public string Sample { set; get; }
        public string Diluent { set; get; }
        public string Opt1 { set; get; }
        public string Opt2 { set; get; }
        public string Opt3 { set; get; }
        public string Opt4 { set; get; }
        public string Opt5 { set; get; }
    }
}
