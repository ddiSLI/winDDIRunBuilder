using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    public class BatchWorklist
    {
        public string Id { set; get; }
        public string BatchId { set; get; }
        public string PlateId { set; get; }
        public int Length { set; get; }
        public int Width { set; get; }
        public int Offset { set; get; }
    }
}
