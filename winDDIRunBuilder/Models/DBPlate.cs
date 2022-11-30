using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    public class DBPlate
    {
        public string PlateName { set; get; }
        public string PlateId { set; get; }
        public string StartPos { set; get; }
        public string EndPos { set; get; }
        public string Diluent { set; get; }
        public string Samples { set; get; }
        public bool PlateRotated { set; get; }
        public string ModifiedDate { set; get; }
        public string PlateVersion { set; get; }


    }
}
