using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    public class DeptSample
    {
        public string Id { set; get; }
        public string Dept { set; get; }
        public string PlateId { set; get; }
        public string PlateVersion { set; get; }
        public string DBTest { set; get; }
        public string SampleId { set; get; }
        public string Status { set; get; }
        public int DateRangeMonths { set; get; } = 1;
        public DateTime StartDate { set; get; }
        public string User { set; get; }
    }
}
