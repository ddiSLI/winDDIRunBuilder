using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    public class ErrorInfo
    {
        public string Id { set; get; }
        public string ErrType { set; get; }
        public string PlateId { set; get; }
        public string SampleId { set; get; }
        public string ErrDesc { set; get; }

    }
}
