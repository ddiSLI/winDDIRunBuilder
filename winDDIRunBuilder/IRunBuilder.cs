using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using winDDIRunBuilder.Models;

namespace winDDIRunBuilder
{
    interface IRunBuilder
    {
        List<OrcShortIdRef> GetSampleIds(List<InputFile> inputFileValues);
    }

    interface ISQLOracleService
    {
        List<InputFile> GetPlateSamples(List<InputFile> inputs);
        List<InputFile> GetShortSamples(List<InputFile> inputs);
        List<Protocol> GetProtocols();
    }

    interface IOracleService
    {
        List<OrcShortIdRef> GetShortIdRef(List<string> inputShortIdGrp);

        List<OrcWebProfile> GetWebProfile(string webProfileDesc);

        //string[] GetLastSyncInfo(string category);
        //string SetSyncTransPlan(string category, string syncDoneDate, string syncStatus, string syncDesc);
        //string UpdateSQL(List<SFObjectValue> objValueUpd);
    }

    interface ISQLService
    {
        string GetSeries(string Dept);
        List<DBPlate> GetPlates(string plateId, string plateVersion=null);
        string AddPlate(DBPlate dbPlate);
        List<PlateSample> GetPlateSamples(string plateId, string plateVersion = null);
        DBPlate AddSamples(List<PlateSample> plateSamples);
        //string CreateBatch(Batch newBatch);

        //List<AccountOwner> AccountOwnerMap();
    //    List<AccountStatus> AccountStatusMap();
    //    string[] GetLastSyncInfo(string category);
    //    string SetSyncTransPlan(string category, string syncDoneDate, string syncStatus, string syncDesc);
    //    string UpdateSQL(List<SFObjectValue> objValueUpd);
    }

}
