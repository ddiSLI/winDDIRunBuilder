using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using winDDIRunBuilder.Models;


namespace winDDIRunBuilder
{
    public class RepoSQLOracle : ISQLOracleService
    {
        public ClientRunBuilder CurRunBuilder { get; set; }

        public RepoSQLOracle()
        {

        }

        public List<Batch> GetBatch(string batchId, string version = null)
        {
            ClientBackend backendService = new ClientBackend();
            List<Batch> getBatch = new List<Batch>();
            List<ClientBackend.DtoBatch> dtoBatch = new List<ClientBackend.DtoBatch>();

            try
            {
                if (batchId.Length > 0)
                {
                    dtoBatch = (List<ClientBackend.DtoBatch>)backendService.GetBatchLatest(batchId);

                    if (dtoBatch != null && dtoBatch.Count > 0)
                    {
                        foreach (var dto in dtoBatch)
                        {
                            getBatch.Add(new Batch
                            {
                                BatchId = dto.BatchId,
                                Sequence = dto.UserSequence,
                                ShortId = dto.ShortId,
                                PlateSuffix = dto.PlateSuffix,
                                SampleId = dto.SampleId,
                                ModifiedBy = dto.Modify.By,
                                ModifiedDate = dto.Modify.Date,
                                ModifiedTool = dto.Modify.Tool,
                                ORC = dto.Orc,
                                Well = dto.Well,
                                Version = dto.Version
                            });
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string errMsg = "{RepoSQLOracle.GetBatch()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }

            return getBatch;

        }

        public string CreateBatchAndWorkList(List<BatchWorklist> newBatchWorklist)
        {
            string result = "NA";

            ClientBackend backendService = new ClientBackend();

            try
            {
                if (newBatchWorklist.Count > 0)
                {
                    foreach (var bat in newBatchWorklist)
                    {

                    }

                    result = backendService.CreateBatchWorklist(newBatchWorklist);

                }
            }
            catch (Exception ex)
            {
                string errMsg = "{RepoSQLOracle.CreateBarch()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }

            return result;
        }

        public string CreateBarch(List<Batch> newBatch)
        {
            string result = "NA";

            ClientBackend backendService = new ClientBackend();
            List<ClientBackend.DtoBatch> dtoBatch = new List<ClientBackend.DtoBatch>();

            try
            {
                if (newBatch.Count > 0)
                {
                    foreach (var bat in newBatch)
                    {
                        dtoBatch.Add(new ClientBackend.DtoBatch
                        {
                            BatchId = bat.BatchId,
                            UserSequence = bat.Sequence,
                            SampleId = bat.SampleId,
                            Well = bat.Well,
                            ShortId = bat.ShortId,
                            PlateSuffix = bat.PlateSuffix,
                            Modify = new ClientBackend.DtoModify
                            {
                                Date = bat.ModifiedDate,
                                By = bat.ModifiedBy,
                                Tool = bat.ModifiedTool
                            },
                            Orc = bat.ORC,
                            Version = bat.Version
                        });
                    }

                    result = backendService.CreateBatch(dtoBatch);

                }
            }
            catch (Exception ex)
            {
                string errMsg = "{RepoSQLOracle.CreateBarch()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }

            return result;
        }

        public string DeleteBarch(List<Batch> newBatch)
        {
            string result = "NA";

            ClientBackend backendService = new ClientBackend();
            List<ClientBackend.DtoBatch> dtoBatch = new List<ClientBackend.DtoBatch>();

            try
            {
                if (newBatch.Count > 0)
                {
                    foreach (var bat in newBatch)
                    {
                        dtoBatch.Add(new ClientBackend.DtoBatch
                        {
                            BatchId = bat.BatchId,
                            UserSequence = bat.Sequence,
                            Version = bat.Version
                        });
                    }

                    result = backendService.DeleteBatch(dtoBatch);

                }
            }
            catch (Exception ex)
            {
                string errMsg = "{RepoSQLOracle.DeleteBarch()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }

            return result;
        }



        public List<Protocol> GetProtocols()
        {
            ClientBackend backendService = new ClientBackend();
            List<Protocol> prots = new List<Protocol>();
            List<ClientBackend.DtoProtocol> dtoProts = new List<ClientBackend.DtoProtocol>();


            dtoProts = (List<ClientBackend.DtoProtocol>)backendService.GetProtocolPlates(CurRunBuilder.PlateSettingFile);
            //dtoProts = (List<ClientBackend.DtoProtocol>)backendService.GetProtocols();
            int protId = 0;
            foreach (var dtoProt in dtoProts)
            {
                prots.Add(new Protocol
                {
                    Id = "P" + protId.ToString().Trim(),
                    PlateId = dtoProt.PlateId,
                    SourcePlate = dtoProt.SourcePlate,
                    ProtocolName = dtoProt.ProtocolName,
                    WorklistName = dtoProt.WorklistName,
                    HasAliasId = dtoProt.HasAliasId.ToUpper() == "FALSE" ? true : false,
                    CheckCancelled = dtoProt.CheckCancelled.ToUpper() == "FALSE" ? true : false,
                    Pooling = dtoProt.Pooling.ToUpper() == "FALSE" ? true : false,
                    CherryPick = dtoProt.CherryPick.ToUpper() == "FALSE" ? true : false,
                    Database = dtoProt.Database,
                    DBTable = dtoProt.DBTable,
                    DBTest = dtoProt.DBTest,
                    PlateRotated = dtoProt.PlateRotated.ToUpper() == "FALSE" ? false : true,
                    StartPos = dtoProt.StartPos,
                    EndPos = dtoProt.EndPos,
                    Samples = Convert.ToInt32(dtoProt.Samples),
                    Diluent = Convert.ToInt32(dtoProt.Diluent),
                    Opt1 = dtoProt.Opt1,
                    Opt2 = dtoProt.Opt2,
                    etc = dtoProt.etc
                });

                protId += 1;
            }

            return prots;
        }

        public List<InputFile> GetPlateSamples(List<InputFile> inputs)
        {
            ClientBackend backendService = new ClientBackend();
            List<InputSample> samples = new List<InputSample>();

            ClientBackend.DtoScannedSample dtoSampleInfo = new ClientBackend.DtoScannedSample();
            List<ClientBackend.DtoSample> dtoSamples = new List<ClientBackend.DtoSample>();
            StringBuilder sbShortIds = new StringBuilder();

            try
            {
               foreach(var inp in inputs)
                {
               //     samples.Add(new InputSample { Attributes.SampleId = inp.ShortId });
                }
                
  //              inputs= backendService.AddSamples(samples);

                //foreach (var input in inputs)
                //{
                //    dtoSampleInfo = backendService.GetSampleInfo(input.ShortId);
                //    if (dtoSampleInfo !=null && !string.IsNullOrEmpty(dtoSampleInfo.SampleId))
                //    {
                //        if (!string.IsNullOrEmpty(dtoSampleInfo.PlateId))
                //        {
                //            input.FullSampleId = dtoSampleInfo.SampleId;
                //            input.PlateId = dtoSampleInfo.PlateId;
                //            input.Well= dtoSampleInfo.Well;
                //        }
                //    }
                //}

            }
            catch (Exception ex)
            {
                string errMsg = "{RepoSQLOracle.GetPlateSamples()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }

            return inputs;
        }
    

        public List<InputFile> GetShortSamples(List<InputFile> inputs)
        {
            ClientBackend backendService = new ClientBackend();
            List<Batch> batch = new List<Batch>();
            List<ClientBackend.DtoSample> dtoSamples = new List<ClientBackend.DtoSample>();
            StringBuilder sbShortIds = new StringBuilder();

            try
            {
                foreach (var input in inputs)
                {
                    sbShortIds.Append(input.ShortId + ",");
                }

                dtoSamples.AddRange(backendService.GetSamples(sbShortIds.ToString()).ToList());

                if (dtoSamples.Count > 0)
                {

                    foreach (var sid in dtoSamples)
                    {
                        inputs.Where(w => w.ShortId == sid.ShortId).ToList().ForEach(itm => itm.FullSampleId = sid.SampleId);
                    }

                    inputs.Where(w => string.IsNullOrEmpty(w.FullSampleId)).ToList().ForEach(itm => itm.FullSampleId = itm.ShortId);
                }


            }
            catch (Exception ex)
            {
                string errMsg = "{RepoSQLOracle.GetShortSamples()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }

            return inputs;
        }
    }
}
