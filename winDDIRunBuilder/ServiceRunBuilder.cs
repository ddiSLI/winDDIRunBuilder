using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using winDDIRunBuilder.Models;

namespace winDDIRunBuilder
{
    public class ServiceRunBuilder : IRunBuilder
    {
        [Obsolete]
        public List<InputFile> GetSampleIds(List<InputFile> inputFileValues)
        {
            List<OrcShortIdRef> sampleIds = new List<OrcShortIdRef>();
            RepoOracle repoOracle = new RepoOracle();
            List<string> inputShortIds = new List<string>();
            List<string> inputShortIdGrpList = new List<string>();
            try
            {
                inputShortIds = inputFileValues.Select(sid => sid.ShortId).ToList();
                inputShortIdGrpList = GetShortIdGrps(inputShortIds);
                sampleIds = repoOracle.GetShortIdRef(inputShortIdGrpList);
                if (sampleIds.Count>0)
                {
                    foreach (var sid in sampleIds)
                    {
                        inputFileValues.Where(w => w.ShortId == sid.ShortId).ToList().ForEach(itm => itm.FullSampleId = sid.SampleId);
                    }

                    inputFileValues.Where(w => string.IsNullOrEmpty(w.FullSampleId)).ToList().ForEach(itm => itm.FullSampleId = itm.ShortId);
                }

            }
            catch(Exception ex)
            {
                string errMsg = "{ServiceRunBuilder.GetSampleIds()} met the following error: ";
                errMsg += Environment.NewLine;
                errMsg += ex.Message;
            }

            return inputFileValues;
        }

        List<OrcShortIdRef> IRunBuilder.GetSampleIds(List<InputFile> inputFileValues)
        {
            throw new NotImplementedException();
        }

        private List<string> GetShortIdGrps(List<string> inputShortIds)
        {
            //put several ShortIds into a string for Where-in clause

            List<string> shortIds = new List<string>();
            string shortIdGrp = "";
            int oneGrpClientIds = 9; //0-9
            int clientLength = oneGrpClientIds;

            if (inputShortIds != null && inputShortIds.Count > 0)
            {
                foreach (var odrCid in inputShortIds)
                {
                    if (clientLength >= 0)
                    {
                        if (string.IsNullOrEmpty(shortIdGrp))
                        {
                            shortIdGrp += "'" + odrCid + "'";
                        }
                        else
                        {
                            shortIdGrp += ", '" + odrCid + "'";
                        }

                        if (inputShortIds.IndexOf(odrCid) == inputShortIds.Count - 1)
                            shortIds.Add(shortIdGrp);

                        clientLength -= 1;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(shortIdGrp))
                        {
                            shortIdGrp += "'" + odrCid + "'";
                            shortIds.Add(shortIdGrp);
                        }
                        else
                        {
                            shortIdGrp += ", '" + odrCid + "'";
                            shortIds.Add(shortIdGrp);
                        }

                        clientLength = oneGrpClientIds;
                        shortIdGrp = "";
                    }
                }
            }

            return shortIds;
        }
    }
}
