using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winDDIRunBuilder.Models
{
    public class Plate
    {
        public int Id { set; get; }
        public string BatchId { set; get; }
        public string ProtocolId { set; get; }
        public string ProtocolName { set; get; }
        public string PlateId { set; get; }
        public int Sequence { set; get; }
        public string ShortId { set; get; }
        public string SampleId { set; get; }
        public string WellX { set; get; }
        public string WellY { set; get; }
        public string BatchVersion { set; get; }

    }

    public class Sample
    {
        public string ShortId { set; get; }
        public string SampleId { set; get; }
        public string Sequence { set; get; }
    }

    public class RealPlate
    {
        public int Id { set; get; }
        public Sample Smp01 { set; get; }
        public Sample Smp02 { set; get; }
        public Sample Smp03 { set; get; }
        public Sample Smp04 { set; get; }
        public Sample Smp05 { set; get; }
        public Sample Smp06 { set; get; }
        public Sample Smp07 { set; get; }
        public Sample Smp08 { set; get; }
        public Sample Smp09 { set; get; }
        public Sample Smp10 { set; get; }
        public Sample Smp11 { set; get; }
        public Sample Smp12 { set; get; }
        public Sample Smp13 { set; get; }
        public Sample Smp14 { set; get; }
        public Sample Smp15 { set; get; }
        public Sample Smp16 { set; get; }
    }
}
