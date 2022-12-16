using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using winDDIRunBuilder.Models;

namespace winDDIRunBuilder
{
    public class ClientBackend
    {
        // public ClientRunBuilder CurRunBuilder { get; set; }

        public class DtoWorklist
        {
            public string SourcePlateId { set; get; }
            public string SourceWellId { set; get; }
            public string DestPlateId { set; get; }
            public string DestWellId { set; get; }
            public Dictionary<string,string> Attributes { set; get; } 
        }

        public class Attributes
        {
            public string Accept { set; get; }
            public string SampleId { set; get; }
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
            public string Samples { set; get; }
            public string Diluent { set; get; }
            public string Opt1 { set; get; }
            public string Opt2 { set; get; }
            public string etc { set; get; }
        }
        private readonly RestClient _DDIBatchClient;
        private readonly string _endpointResourceDDIBatch;


        public ClientBackend(string processType = null)
        {
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

            inProtocol.PlateId = values[0].Trim();
            inProtocol.SourcePlate = values[1].Trim();
            inProtocol.ProtocolName = values[2].Trim();
            inProtocol.WorklistName = values[3].Trim();
            inProtocol.HasAliasId = values[4].Trim();
            inProtocol.CheckCancelled = values[5].Trim();
            inProtocol.Pooling = values[6].Trim();
            inProtocol.CherryPick = values[7].Trim();
            inProtocol.DBTest = values[8].Trim();
            inProtocol.PlateRotated = values[9].Trim();
            inProtocol.StartPos = values[10].Trim();
            inProtocol.EndPos = values[11].Trim();
            inProtocol.Samples = values[12].Trim();
            inProtocol.Diluent = values[13].Trim();
            inProtocol.Opt1 = values[14].Trim();
            inProtocol.Opt2 = values[15].Trim();
            inProtocol.etc = values[16].Trim();

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
            string jsonPlate = "";
            string endPointResource = "";

            try
            {
                endPointResource = _endpointResourceDDIBatch ;

                if (inputPlate != null && !string.IsNullOrEmpty(inputPlate.Name))
                {
                    var request = new RestRequest(endPointResource);
                    request.AddJsonBody(inputPlate);

                    //jsonPlate = JsonConvert.SerializeObject(inputPlate);

                    //request.AddParameter("application/json", jsonPlate, ParameterType.RequestBody);

                    var response = _DDIBatchClient.Execute(request, Method.POST);

                    if (!response.IsSuccessful)
                    {
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
            }

            return actionResult;
        }
        public string AddSamples(string plateName, List<InputFile> plateSamples)
        {
            string actionResult = "YES";
            StringBuilder sbResult = new StringBuilder();
            string jsonSample = "";

            string endPointResource ="";
            string pos = "";
            string indexSample = "";
            string inSampleId = "";

            InputSample inSample = new InputSample();

            try
            {
                if (plateSamples != null && plateSamples.Count > 0)
                {
                    foreach (var sam in plateSamples)
                    {
                        //221201-0001-01
                        //221201000101
                        if (sam.ShortId !=null && sam.ShortId.Length == 12)
                        {
                            inSampleId = sam.ShortId.Substring(0, 6) + "-";
                            inSampleId = inSampleId + sam.ShortId.Substring(6,4) + "-";
                            inSampleId = inSampleId + sam.ShortId.Substring(10);
                        }
                        
                        inSample.Attributes.SampleId = inSampleId;
                        //inSample.Attributes.SampleId = sam.ShortId;

                        jsonSample = JsonConvert.SerializeObject(inSample);
                        //jsonSample=Newtonsoft.Json.JsonConvert.SerializeObject(new { SampleId = "sss" });

                        endPointResource = $"{_endpointResourceDDIBatch}/{plateName}/{sam.WellX}/{sam.WellY}";

                        var request = new RestRequest(endPointResource, Method.POST);
                        request.AddParameter("application/json", jsonSample, ParameterType.RequestBody);

                        var response = _DDIBatchClient.Execute(request);

                        if ( !response.IsSuccessful)
                        {
                            actionResult = "FAILED";
                            sbResult.Append("APIPost_ERROR: " + inSampleId + "; " + indexSample + "; ");
                            sbResult.AppendLine();
                            throw new Exception(response.Content);
                        }
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
            }

            return actionResult;
        }

        public IEnumerable<DtoWorklist> GetWorklist(string sourcePlate, string destPlate, string options)
        {

            List<DtoWorklist> dtoWorklist = new List<DtoWorklist>();
            string endPointResource = "";

            try
            {
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
