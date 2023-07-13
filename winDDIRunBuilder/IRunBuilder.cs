using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using winDDIRunBuilder.Models;

namespace winDDIRunBuilder
{
    interface ISQLService
    {
        string GetSeries(string Dept);
        List<Janus> GetJanus(string hostName);
        List<Protocol> GetProtocols(string dept);

        List<DBPlate> GetPlates(string plateId, string plateVersion=null);
        string AddPlate(DBPlate dbPlate,string user);
        List<PlateSample> GetPlateSamples(string plateId, string plateVersion = null);
        string AddSamples(List<OutputPlateSample> plateSamples, string user);

        List<QCSample> GetQCSamples(string plateName, string dept, string qcType);

        string AddPlateQCSamples(List<PlateSample> plateSamples,string user);

        string UpdatePlateQC(string plateId, string plateVersion, bool hasQC, string user);

        string UpdateSampleStatus(List<SampleStatus> sampleStatus);

        List<ExportFile> GetExportFiles(string range);

    }

}
