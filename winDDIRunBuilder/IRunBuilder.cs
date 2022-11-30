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
        List<Batch> GetBatch(string plateId, string version = null);
        string CreateBarch(List<Batch> newBatch);
    }

    interface IOracleService
    {
        List<OrcShortIdRef> GetShortIdRef(List<string> inputShortIdGrp);

        string CreateBatch(Batch newBatch); 
        List<OrcWebProfile> GetWebProfile(string webProfileDesc);

        //string[] GetLastSyncInfo(string category);
        //string SetSyncTransPlan(string category, string syncDoneDate, string syncStatus, string syncDesc);
        //string UpdateSQL(List<SFObjectValue> objValueUpd);
    }

    interface ISQLService
    {
        List<DBPlate> GetPlates(string plateId, string plateVersion=null);
        DBPlate AddPlate(DBPlate dbPlate);
        List<PlateSample> GetPlateSamples(string plateId, string plateVersion = null);
        DBPlate AddSamples(List<PlateSample> plateSamples);
        string UpdateBatch(Batch newBatch);
        //string CreateBatch(Batch newBatch);

        //List<AccountOwner> AccountOwnerMap();
    //    List<AccountStatus> AccountStatusMap();
    //    string[] GetLastSyncInfo(string category);
    //    string SetSyncTransPlan(string category, string syncDoneDate, string syncStatus, string syncDesc);
    //    string UpdateSQL(List<SFObjectValue> objValueUpd);
    }

}
