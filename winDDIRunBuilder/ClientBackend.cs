using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using winDDIRunBuilder.Models;

namespace winDDIRunBuilder
{
    public class ClientBackend
    {
        // public ClientRunBuilder CurRunBuilder { get; set; }

        public string ErrMsg { set; get; } = "";

        public List<ErrorInfo> AddSampleErrs { set; get; }
        public class DtoWorklist
        {
            public string SourcePlateId { set; get; }
            public string SourceWellId { set; get; }
            public string SourceIndex { set; get; }
            public string DestIndex { set; get; }
            public string DestPlateId { set; get; }
            public string DestWellId { set; get; }

            //Accept,siga, SampleId
            public Dictionary<string, string> Attributes { set; get; }
        }

        public class Attributes
        {
            public string Accept { set; get; }
            public string SampleId { set; get; }
            public string Alias { set; get; }
        }



        public class DtoSample
        {
            public string ShortId { get; set; }
            public string SampleId { get; set; }
            public string PlateId { get; set; }
        }

        public class DtoScannedSample
        {
            public string Position { get; set; }
            public string ShortId { get; set; }
            public string SampleId { get; set; }
            public string PlateId { get; set; }
            public string Well { get; set; }
            public string WellX { get; set; }
            public string WellY { get; set; }
        }

        public class DtoBatch
        {
            public string BatchId { set; get; }
            public string Orc { set; get; }
            public string SampleId { set; get; }
            public string UserSequence { set; get; }
            public string Version { set; get; }
            public string Well { set; get; }
            public string ShortId { set; get; }
            public string PlateSuffix { set; get; }
            public DtoModify Modify { set; get; }
        }

        public class DtoModify
        {
            public string Date { set; get; }
            public string By { set; get; }
            public string Tool { set; get; }
        }
        public class DtoProtocol
        {
            public string Department { set; get; }
            public string PlateId { set; get; }
            public string SourcePlate { set; get; }
            public string ProtocolName { set; get; }
            public string WorklistName { set; get; }
            public string HasAliasId { set; get; }
            public string CheckCancelled { set; get; }
            public string Pooling { set; get; }
            public string CherryPick { set; get; }
            public string Database { set; get; }
            public string DBTable { set; get; }
            public string DBTest { set; get; }
            public string PlateRotated { set; get; }
            public string StartPos { set; get; }
            public string EndPos { set; get; }
            public string ExcludeWells { set; get; }
            public string Samples { set; get; }
            public string Diluent { set; get; }
            public string Opt1 { set; get; }
            public string Opt2 { set; get; }
            public string Opt3 { set; get; }
            public string Opt4 { set; get; }
            public string Opt5 { set; get; }
        }
        private readonly RestClient _DDIBatchClient;
        private readonly string _endpointResourceDDIBatch;


        public ClientBackend(string processType = null)
        {
            AddSampleErrs = new List<ErrorInfo>();
            string runCondition = ConfigurationManager.AppSettings["RunCondition"];
            string endpointUrlDDIBatch = "";
            string endpointPortDDIBatch = "";

            if (runCondition == "PROD")
            {
                endpointUrlDDIBatch = ConfigurationManager.AppSettings["DDIBatchClient_endpointUrl_Dev"];
                endpointPortDDIBatch = ConfigurationManager.AppSettings["DDIBatchClient_endpointPort_Dev"];

                if (string.IsNullOrEmpty(processType))
                {
                    _endpointResourceDDIBatch = ConfigurationManager.AppSettings["DDIBatchClient_endpointResource"];
                }
            }
            else
            {
                endpointUrlDDIBatch = ConfigurationManager.AppSettings["DDIBatchClient_endpointUrl_Dev"];
                endpointPortDDIBatch = ConfigurationManager.AppSettings["DDIBatchClient_endpointPort_Dev"];

                if (string.IsNullOrEmpty(processType))
                {
                    _endpointResourceDDIBatch = ConfigurationManager.AppSettings["DDIBatchClient_endpointResource"];
                }
            }

            _DDIBatchClient = new RestClient($"{endpointUrlDDIBatch}:{endpointPortDDIBatch}");
        }

        public static DtoProtocol ReadProtocolFile(string csvLine)
        {
            string[] values = csvLine.Split(',');
            DtoProtocol inProtocol = new DtoProtocol();
            inProtocol.Department = values[0].Trim();
            inProtocol.PlateId = values[1].Trim();
            inProtocol.SourcePlate = values[2].Trim();
            inProtocol.ProtocolName = values[3].Trim();
            inProtocol.WorklistName = values[4].Trim();
            inProtocol.HasAliasId = values[5].Trim();
            //inProtocol.CheckCancelled = values[6].Trim();
            inProtocol.Pooling = values[6].Trim();
            //inProtocol.CherryPick = values[8].Trim();
            inProtocol.DBTest = values[7].Trim();
            inProtocol.PlateRotated = values[8].Trim();
            inProtocol.StartPos = values[9].Trim();
            inProtocol.EndPos = values[10].Trim();
            inProtocol.ExcludeWells = values[11].Trim();
            inProtocol.Samples = values[12].Trim();
            inProtocol.Diluent = values[13].Trim();
            inProtocol.Opt1 = values[14].Trim();
            inProtocol.Opt2 = values[15].Trim();
            inProtocol.Opt3 = values[16].Trim();
            inProtocol.Opt4 = values[17].Trim();
            inProtocol.Opt5 = values[18].Trim();
            return inProtocol;
        }
        public IEnumerable<DtoProtocol> GetProtocolPlates(string plateSettingFile = "")
        {
            List<DtoProtocol> prots = new List<DtoProtocol>();


            if (!string.IsNullOrEmpty(plateSettingFile) && File.Exists(plateSettingFile))
            {
                prots = File.ReadAllLines(plateSettingFile)
                    .Skip(1)
                    .Select(v => ReadProtocolFile(v))
                    .ToList();

                //List<DtoProtocol> rawValues = File.ReadAllLines(plateSettingFile).Skip(1).Select(v => ReadProtocolFile(v)).ToList();

            }

            return prots;
        }

        public string CreatePlate(InputPlate inputPlate)
        {
            string actionResult = "YES";
            string endPointResource = "";

            try
            {
                endPointResource = _endpointResourceDDIBatch;

                if (inputPlate != null && !string.IsNullOrEmpty(inputPlate.Name))
                {
                    var request = new RestRequest(endPointResource);
                    request.AddJsonBody(inputPlate);

                    //jsonPlate = JsonConvert.SerializeObject(inputPlate);

                    //request.AddParameter("application/json", jsonPlate, ParameterType.RequestBody);

                    var response = _DDIBatchClient.Execute(request, Method.POST);

                    if (!response.IsSuccessful)
                    {
                        ErrMsg = "APIService.CreatePlate() error: " + response.Content;
                        actionResult = "FAILED";
                        throw new Exception(response.Content);
                    }
                }

                return actionResult;
                //return JsonConvert.DeserializeObject<DtoScannedSample>(response.Content);
            }
            catch (Exception ex)
            {

                actionResult = "ERROR";
                //  GenMessaging errProcess = new GenMessaging(DateTime.Now.ToLongTimeString(), "Sapphire sync to SalesForce");
                string errMsg = "ClientBackend.CreatePlate() met an issue:";
                errMsg += Environment.NewLine;
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                //errProcess.MsgHandler(msgType: "SYS-ERROR", errMsg);
                ErrMsg = "APIService.CreatePlate() Exception: " + ex.Message;
            }

            return actionResult;
        }

        public string AddSamples(string plateName, List<InputFile> plateSamples, bool isQC = false)
        {
            string actionResult = "YES";
            //List<InputFile> processSamples = new List<InputFile>();
            ModelTransfer runFun = new ModelTransfer();
            AddSampleErrs = new List<ErrorInfo>();

            try
            {
                if (plateSamples == null || plateSamples.Count <= 0)
                {
                    return actionResult;
                }

                foreach (var sam in plateSamples)
                {
                    var inSample = new Dictionary<string, string>();
                    string inSampleId = sam.ShortId.ToUpper();
                    string indexSample = "";

                    if (string.IsNullOrWhiteSpace(inSampleId) != true)
                    {
                        if (inSampleId.IndexOf("X") > 0)
                        {
                            inSampleId = inSampleId.Substring(0, inSampleId.IndexOf("X"));
                        }

                        var match = Regex.Match(inSampleId, "([0-9]{6}-[0-9]{4}-[0-9]{1,2}).*", RegexOptions.None);
                        if (match.Success)
                        {
                            inSampleId = match.Value;
                        }
                        else
                        {
                            match = Regex.Match(inSampleId, "([0-9]{6})([0-9]{4})([0-9]{1,2})", RegexOptions.None);
                            if (match.Success)
                            {
                                inSampleId = $"{match.Groups[1].Value}-{match.Groups[2].Value}-{match.Groups[3].Value}";
                            }
                            else
                            {
                                inSample.Add(inSampleId, "a");
                            }
                        }
                    }

                    inSample.Add("SampleId", inSampleId);
                    inSample.Add("Alias", sam.ShortId);

                    //if (inSampleId == null)
                    //{
                    //    inSample.Add(sam.ShortId, "a");
                    //}

                    if (isQC && sam.SampleType == "QC")
                    {
                        inSample.Add("QCId", sam.ShortId);
                    }

                    string jsonSample = JsonConvert.SerializeObject(new
                    {
                        Attributes = inSample
                    });
                    //jsonSample=Newtonsoft.Json.JsonConvert.SerializeObject(new { SampleId = "sss" });

                    string endPointResource = $"{_endpointResourceDDIBatch}/{plateName}/{sam.WellX}/{sam.WellY}";

                    //string endPointResource = $"{_endpointResourceDDIBatch}/{plateName}/{int.Parse(sam.Position.Split(',')[0])}/{int.Parse(sam.Position.Split(',')[1])}";

                    var request = new RestRequest(endPointResource, Method.POST);
                    request.AddParameter("application/json", jsonSample, ParameterType.RequestBody);

                    var response = _DDIBatchClient.Execute(request);

                    if (!response.IsSuccessful)
                    {
                        StringBuilder sbResult = new StringBuilder();
                        ErrMsg = "APIService.AddSamples() error: " + response.Content;
                        actionResult = "FAILED";
                        sbResult.Append("APIPost_ERROR: " + inSampleId + "; " + indexSample + "; ");
                        sbResult.AppendLine();

                        //BCRSampleIssueHandle_24
                        //Collect backend issues
                        AddSampleErrs.Add(new ErrorInfo
                        {
                            SampleId = inSampleId,
                            ErrDesc = response.Content
                        });
                        //

                        throw new Exception(response.Content);
                    }
                }

                return actionResult;
                //return JsonConvert.DeserializeObject<DtoScannedSample>(response.Content);
            }
            catch (Exception ex)
            {
                actionResult = "ERROR";
                //  GenMessaging errProcess = new GenMessaging(DateTime.Now.ToLongTimeString(), "Sapphire sync to SalesForce");
                string errMsg = "ClientBackend.GetSamples() met an issue:";
                errMsg += Environment.NewLine;
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                //errProcess.MsgHandler(msgType: "SYS-ERROR", errMsg);
                ErrMsg = "APIService.AddSamples() Exception: " + ex.Message;
            }

            return actionResult;
        }
        public string AddSamples_His(string plateName, List<InputFile> plateSamples, bool isQC = false)
        {
            string actionResult = "YES";
            ModelTransfer runFun = new ModelTransfer();

            try
            {
                if (plateSamples == null || plateSamples.Count <= 0)
                {
                    return actionResult;
                }

                //Add year to dilut SampleId 
                InputFile dilutSmp = new InputFile();
                foreach (var smp in plateSamples)
                {
                    if (smp.ShortId.ToUpper().IndexOf("X") > 0)
                    {
                        dilutSmp = new InputFile();
                        dilutSmp.ShortId = runFun.GetYearSampleId(smp.ShortId);
                        dilutSmp.RackName = smp.RackName;
                        dilutSmp.Position = smp.Position;
                        dilutSmp.WellX = smp.Position;
                        dilutSmp.WellY = "0";
                        plateSamples.Add(dilutSmp);
                    }
                }


                foreach (var sam in plateSamples)
                {
                    var inSample = new Dictionary<string, string>();
                    string inSampleId = null;
                    string indexSample = "";


                    if (string.IsNullOrWhiteSpace(sam.ShortId) != true && sam.ShortId.ToUpper().IndexOf("X") < 0)
                    {
                        var match = Regex.Match(sam.ShortId, "([0-9]{6}-[0-9]{4}-[0-9]{1,2}).*", RegexOptions.None);
                        if (match.Success)
                        {
                            inSampleId = match.Value;
                        }
                        else
                        {
                            match = Regex.Match(sam.ShortId, "([0-9]{6})([0-9]{4})([0-9]{1,2})", RegexOptions.None);
                            if (match.Success)
                            {
                                inSampleId = $"{match.Groups[1].Value}-{match.Groups[2].Value}-{match.Groups[3].Value}";
                            }
                        }
                    }

                    inSample.Add("SampleId", inSampleId ?? sam.ShortId);

                    if (inSampleId == null)
                    {
                        inSample.Add(sam.ShortId, "a");
                    }



                    if (isQC && sam.SampleType == "QC")
                    {
                        inSample.Add("QCId", sam.ShortId);
                    }




                    string jsonSample = JsonConvert.SerializeObject(new
                    {
                        Attributes = inSample
                    });
                    //jsonSample=Newtonsoft.Json.JsonConvert.SerializeObject(new { SampleId = "sss" });

                    string endPointResource = $"{_endpointResourceDDIBatch}/{plateName}/{sam.WellX}/{sam.WellY}";

                    //string endPointResource = $"{_endpointResourceDDIBatch}/{plateName}/{int.Parse(sam.Position.Split(',')[0])}/{int.Parse(sam.Position.Split(',')[1])}";

                    var request = new RestRequest(endPointResource, Method.POST);
                    request.AddParameter("application/json", jsonSample, ParameterType.RequestBody);

                    var response = _DDIBatchClient.Execute(request);

                    if (!response.IsSuccessful)
                    {
                        StringBuilder sbResult = new StringBuilder();
                        ErrMsg = "APIService.AddSamples() error: " + response.Content;
                        actionResult = "FAILED";
                        sbResult.Append("APIPost_ERROR: " + inSampleId + "; " + indexSample + "; ");
                        sbResult.AppendLine();
                        throw new Exception(response.Content);
                    }
                }

                return actionResult;
                //return JsonConvert.DeserializeObject<DtoScannedSample>(response.Content);
            }
            catch (Exception ex)
            {
                actionResult = "ERROR";
                //  GenMessaging errProcess = new GenMessaging(DateTime.Now.ToLongTimeString(), "Sapphire sync to SalesForce");
                string errMsg = "ClientBackend.GetSamples() met an issue:";
                errMsg += Environment.NewLine;
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                //errProcess.MsgHandler(msgType: "SYS-ERROR", errMsg);
                ErrMsg = "APIService.AddSamples() Exception: " + ex.Message;
            }

            return actionResult;
        }

        public IEnumerable<DtoWorklist> GetWorklist(string sourcePlate, string destPlate, string options, bool isSchedCompletedSamples)
        {

            List<DtoWorklist> dtoWorklist = new List<DtoWorklist>();
            string endPointResource = "";

            try
            {
                options = "lookup_alias,in_process,cherry_pick";
                if (isSchedCompletedSamples)
                {
                    //"lookup_alias,skip_cancelled,cherry_pick", 
                    options = "lookup_alias,in_process,approved,cherry_pick";
                }

                //v1/worklist/BCR/transfer/SigA1?options=lookup_alias,skip_cancelled
                endPointResource = $"{_endpointResourceDDIBatch}/{sourcePlate}/transfer/{destPlate}?options={options}";

                var request = new RestRequest(endPointResource, Method.GET);

                //if (!string.IsNullOrEmpty(sourcePlate) && !string.IsNullOrEmpty(destPlate))
                //{
                //    request.AddQueryParameter("src", sourcePlate);
                //    request.AddQueryParameter("dst", destPlate);
                //}

                var response = _DDIBatchClient.Execute(request);
                if (!response.IsSuccessful)
                {
                    ErrMsg = "APIService.GetWorklist() error: " + response.Content;
                    throw new Exception(response.Content);
                }

                return JsonConvert.DeserializeObject<List<DtoWorklist>>(response.Content);
            }
            catch (Exception ex)
            {
                //  GenMessaging errProcess = new GenMessaging(DateTime.Now.ToLongTimeString(), "Sapphire sync to SalesForce");
                string errMsg = "ClientBackend.GetSamples() met an issue:";
                errMsg += Environment.NewLine;
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                //errProcess.MsgHandler(msgType: "SYS-ERROR", errMsg);
                ErrMsg = "APIService.GetWorklist() Exception: " + ex.Message;
            }

            return dtoWorklist;
        }

        public IEnumerable<DtoSample> GetSamples(string shortIds)
        {

            List<DtoSample> samples = new List<DtoSample>();

            try
            {
                var request = new RestRequest(_endpointResourceDDIBatch, Method.GET);


                if (shortIds.Length > 0)
                {
                    request.AddQueryParameter("shortids", shortIds);

                }

                var response = _DDIBatchClient.Execute(request);
                if (!response.IsSuccessful)
                {
                    throw new Exception(response.Content);
                }

                //   samples= JsonConvert.DeserializeObject<DtoSample[]>(response.Content).ToList();

                return JsonConvert.DeserializeObject<DtoSample[]>(response.Content);

            }
            catch (Exception ex)
            {
                //  GenMessaging errProcess = new GenMessaging(DateTime.Now.ToLongTimeString(), "Sapphire sync to SalesForce");
                string errMsg = "ClientBackend.GetSamples() met an issue:";
                errMsg += Environment.NewLine;
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                //errProcess.MsgHandler(msgType: "SYS-ERROR", errMsg);
            }

            return samples;
        }
        public IEnumerable<DtoBatch> GetBatch(string batchId, string version = null, string userSequence = null, string sampleIds = null)
        {
            List<DtoBatch> dtobatch = new List<DtoBatch>();
            StringBuilder sbResult = new StringBuilder();

            try
            {
                var url = _endpointResourceDDIBatch;

                if (!string.IsNullOrEmpty(batchId))
                {
                    url += "/" + batchId;

                    if (!string.IsNullOrEmpty(version))
                    {
                        url += "/" + version;

                        if (!string.IsNullOrEmpty(userSequence))
                        {
                            url += "/" + userSequence;
                        }
                    }
                }

                var request = new RestRequest(url, Method.GET);

                if (!string.IsNullOrEmpty(sampleIds))
                    request.AddQueryParameter("sampleids", sampleIds);

                var response = _DDIBatchClient.Execute(request);
                if (!response.IsSuccessful)
                {
                    sbResult.Append("APIGet_ERROR: " + batchId + "; ");
                    sbResult.AppendLine();
                    throw new Exception(response.Content);
                }

                return JsonConvert.DeserializeObject<List<DtoBatch>>(response.Content);

            }
            catch (Exception ex)
            {
                //  GenMessaging errProcess = new GenMessaging(DateTime.Now.ToLongTimeString(), "Sapphire sync to SalesForce");
                string errMsg = "ClientBackend.GetSamples() met an issue:";
                errMsg += Environment.NewLine;
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                //errProcess.MsgHandler(msgType: "SYS-ERROR", errMsg);
            }

            return dtobatch;
        }

        public IEnumerable<DtoBatch> GetBatchLatest(string batchId, string version = null, string plateSuffix = null, string userSequence = null, string sampleIds = null)
        {
            List<DtoBatch> dtobatch = new List<DtoBatch>();
            StringBuilder sbResult = new StringBuilder();

            try
            {
                var url = _endpointResourceDDIBatch;

                if (!string.IsNullOrEmpty(batchId))
                {
                    url += "/" + batchId + "/latest";

                    if (!string.IsNullOrEmpty(plateSuffix))
                    {
                        url += "/" + plateSuffix;

                        if (!string.IsNullOrEmpty(userSequence))
                        {
                            url += "/" + userSequence;
                        }
                    }
                }

                var request = new RestRequest(url, Method.GET);

                if (!string.IsNullOrEmpty(sampleIds))
                    request.AddQueryParameter("sampleids", sampleIds);

                var response = _DDIBatchClient.Execute(request);
                if (!response.IsSuccessful)
                {
                    sbResult.Append("APIGet_ERROR: " + batchId + "; ");
                    sbResult.AppendLine();
                    throw new Exception(response.Content);
                }

                return JsonConvert.DeserializeObject<List<DtoBatch>>(response.Content);

            }
            catch (Exception ex)
            {
                //  GenMessaging errProcess = new GenMessaging(DateTime.Now.ToLongTimeString(), "Sapphire sync to SalesForce");
                string errMsg = "ClientBackend.GetSamples() met an issue:";
                errMsg += Environment.NewLine;
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                //errProcess.MsgHandler(msgType: "SYS-ERROR", errMsg);
            }

            return dtobatch;
        }



        public string CreateBatch(List<DtoBatch> dtoBatch)
        {
            StringBuilder sbResult = new StringBuilder();
            string jsonBatch = "";

            try
            {

                if (dtoBatch.Count > 0)
                {
                    foreach (var bat in dtoBatch)
                    {
                        var request = new RestRequest(_endpointResourceDDIBatch, Method.POST);
                        jsonBatch = JsonConvert.SerializeObject(bat);
                        request.AddParameter("application/json", jsonBatch, ParameterType.RequestBody);

                        var response = _DDIBatchClient.Execute(request);

                        if (!response.IsSuccessful)
                        {
                            sbResult.Append("APIPost_ERROR: " + bat.BatchId + "; " + bat.SampleId + "; ");
                            sbResult.AppendLine();
                            throw new Exception(response.Content);
                        }
                    }

                    if (sbResult.Length <= 0) sbResult.Append("SUCCESS");
                }

            }
            catch (Exception ex)
            {
                sbResult.Append("ERROR:system exception; ");
                //  GenMessaging errProcess = new GenMessaging(DateTime.Now.ToLongTimeString(), "Sapphire sync to SalesForce");
                string errMsg = "ClientBackend.CreateBatch() met an issue:";
                errMsg += Environment.NewLine;
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                //errProcess.MsgHandler(msgType: "SYS-ERROR", errMsg);
            }

            return sbResult.ToString();
        }

        public string DeleteBatch(List<DtoBatch> dtoBatch)
        {
            StringBuilder sbResult = new StringBuilder();
            string jsonBatch = "";

            try
            {
                if (dtoBatch.Count > 0)
                {
                    foreach (var bat in dtoBatch)
                    {
                        var delBatch = new
                        {
                            BatchId = bat.BatchId,
                            Version = bat.Version,
                            UserSequence = bat.UserSequence
                        };

                        var request = new RestRequest(_endpointResourceDDIBatch, Method.DELETE);
                        jsonBatch = JsonConvert.SerializeObject(delBatch);
                        request.AddParameter("application/json", jsonBatch, ParameterType.RequestBody);
                        var response = _DDIBatchClient.Execute(request);

                        if (!response.IsSuccessful)
                        {
                            sbResult.Append("APIDelete_ERROR: " + bat.BatchId + "; " + bat.SampleId + "; ");
                            sbResult.AppendLine();
                            throw new Exception(response.Content);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                sbResult.Append("ERROR:system exception; ");
                //  GenMessaging errProcess = new GenMessaging(DateTime.Now.ToLongTimeString(), "Sapphire sync to SalesForce");
                string errMsg = "ClientBackend.DeleteBatch() met an issue:";
                errMsg += Environment.NewLine;
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                //errProcess.MsgHandler(msgType: "SYS-ERROR", errMsg);
            }

            return sbResult.ToString();
        }

        public string CreateBatch_History(List<DtoBatch> dtoBatch)
        {

            string result = "NA";

            try
            {
                string jsonBatch = "";

                if (dtoBatch.Count > 0)
                {
                    foreach (var bat in dtoBatch)
                    {
                        var uri = $"{_endpointResourceDDIBatch}";
                        var request = new RestRequest(uri, Method.POST);
                        jsonBatch = JsonConvert.SerializeObject(bat);

                        request.AddJsonBody(bat);
                        //request.AddParameter("application/json",jsonBatch,ParameterType.RequestBody);

                        var response = _DDIBatchClient.Execute(request);

                        if (!response.IsSuccessful)
                        {
                            throw new Exception(response.Content);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //  GenMessaging errProcess = new GenMessaging(DateTime.Now.ToLongTimeString(), "Sapphire sync to SalesForce");
                string errMsg = "ClientBackend.GetSamples() met an issue:";
                errMsg += Environment.NewLine;
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
                //errProcess.MsgHandler(msgType: "SYS-ERROR", errMsg);
            }

            return result;
        }
    }
}
