using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using winDDIRunBuilder.Models;

namespace winDDIRunBuilder
{
    public class RepoOracle : IOracleService
    {
        string CnsOracle = "";
        public RepoOracle()
        {
            if (ConfigurationManager.AppSettings["RunCondition"].ToString() == "DEV")
            {
                CnsOracle = ConfigurationManager.ConnectionStrings["cnnOracle_DEV"].ToString();
            }
            else
            {
                CnsOracle = ConfigurationManager.ConnectionStrings["cnnOracle_PROD"].ToString();
            }
        }

        [Obsolete]
        public List<OrcShortIdRef> GetShortIdRef(List<string> inputShortIdGrp)
        {
            List<OrcShortIdRef> orcShortIdRefs = new List<OrcShortIdRef>();
            string sqlOracle = "";

            using (OracleConnection conn = new OracleConnection(CnsOracle))
            {
                try
                {

                    if (inputShortIdGrp != null && inputShortIdGrp.Count > 0)
                    {
                        conn.Open();
                        OracleCommand cmd = conn.CreateCommand();

                        foreach (var shortIds in inputShortIdGrp)
                        {
                            sqlOracle = "select U_shortId,sampleid ";
                            sqlOracle += " from u_shortidxref ";
                            sqlOracle += " where (u_shortid In (" + shortIds + ")) ";
                            sqlOracle += " order by sampleid ";
                            cmd.CommandText = sqlOracle;

                            OracleDataReader rdr = cmd.ExecuteReader();
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    OrcShortIdRef shortIdRef = new OrcShortIdRef();
                                    shortIdRef.ShortId = rdr["U_shortId"].ToString();
                                    shortIdRef.SampleId = rdr["sampleid"].ToString();
                                    orcShortIdRefs.Add(shortIdRef);
                                }
                            }

                           rdr.Close();
                        }
                    }
                }

                catch (Exception ex)
                {
                    string msgEx = "Oracele: GetShortIdRef(shortId) met issue: ";
                    msgEx += Environment.NewLine;
                    //msgEx += "shortId: " + shortId.ToString() + " ;";
                    msgEx += Environment.NewLine;
                    msgEx += ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }

            return orcShortIdRefs;
        }

        [Obsolete]
        public string CreateBatch(Batch newBatch)
        {
            string result = "NA";






        //    sSQL = "INSERT into u_ddibatch (batchid, usersequence, sampleid, modby, moddt, modtool, well, version) VALUES ("
        //sSQL = sSQL + "'" + Trim(msBatchID) + "', "
        //sSQL = sSQL + Trim(CStr(i)) + ", "
        //sSQL = sSQL + "'" + RemoveRowNumber(lstBatch.List(i)) + "', "
        //sSQL = sSQL + "'" + Trim(GetLoggedInUserName()) + "', "
        //sSQL = sSQL + "TO_DATE('" + Format(Now, "mm/dd/yyyy hh:mm:ss AMPM") + "','MM/DD/YYYY hh:mi:ss am'), "
        //sSQL = sSQL + "'" + Trim(App.EXEName) + "',"
        //sSQL = sSQL + "'" + RemoveSampleID(lstBatch.List(i)) + "',"
        //sSQL = sSQL + Trim(CStr(iVersion)) + ")";

            return result;

        }


        [Obsolete]
        public List<OrcWebProfile> GetWebProfile(string webProfileDesc)
        {
            throw new NotImplementedException();
        }

        [Obsolete]
        public DataTable GetRptInfoByState(string stateID = "NY", string clusterName = "REGULAR")
        {
            DataTable dtStateRpt = new DataTable();
            string sqlOracle = "";

            using (OracleConnection conn = new OracleConnection(CnsOracle))
            {
                if (clusterName == "REGULAR")
                {
                    sqlOracle = "select eRptByStateID, SampleID, Createdt, ProfileID, ParamID, Results, Reportedt, State, ProcessDate, Units ";
                    sqlOracle += " from u_erptbystate ";
                    sqlOracle += " where (State='" + stateID + "') and (processdate Is Null) ";
                    sqlOracle += " order by PROFILEID, SAMPLEID ";
                }
                else if (clusterName == "COVID19")
                {
                    sqlOracle = "select eRptByStateID, SampleID, Createdt, ProfileID, ParamID, Results, Reportedt, State, ProcessDate, Units ";
                    sqlOracle += " from u_erptbystate ";
                    sqlOracle += " where (processdate Is Null) ";
                    sqlOracle += " order by PROFILEID, SAMPLEID ";
                    //sqlOracle += " where (State='" + stateID + "') and (processdate Is Null) order by PROFILEID, SAMPLEID ";

                    //sqlOracle = "select eRptByStateID, SampleID, Createdt, ProfileID, ParamID, Results, Reportedt, State, ProcessDate, Units ";
                    //sqlOracle += "from u_erptbystate ";
                    //sqlOracle += "where processdate > to_date('5/17,21', 'mm/dd/yy') ";
                    //sqlOracle += "      and ProfileID in ('COV2PCRsaliva', ";
                    //sqlOracle += "                        'SARS CoV2 Saliva',";
                    //sqlOracle += "                        'SARS CoV2 AN',";
                    //sqlOracle += "                        'SARS CoV2 NP',";
                    //sqlOracle += "                        'COV2PCRswab',";
                    //sqlOracle += "                        'COV2 IgG Antibody') ";
                    //sqlOracle += "order by PROFILEID, SAMPLEID";
                }
                else if (clusterName == "BLOODMETALS")
                {
                    sqlOracle = "select eRptByStateID, SampleID, Createdt, ProfileID, ParamID, Results, Reportedt, State, ProcessDate, Units ";
                    sqlOracle += " from u_erptbystate ";
                    sqlOracle += " where (State='" + stateID + "') and (processdate Is Null) ";
                    sqlOracle += " and (createdt >= ('01-Jun-2021')) ";
                    sqlOracle += " order by PROFILEID, SAMPLEID ";
                }

                else if (clusterName == "MICRO")
                {
                    sqlOracle = "select eRptByStateID, SampleID, Createdt, ProfileID, ParamID, Results, Reportedt, State, ProcessDate, Units ";
                    sqlOracle += " from u_erptbystate ";
                    sqlOracle += " where (State='" + stateID + "') ";
                    sqlOracle += "        and (processdate Is Null) ";
                    sqlOracle += "        and paramid='EMRMODULE' ";
                    sqlOracle += "        and results='MICRO' ";
                    sqlOracle += " order by PROFILEID, SAMPLEID ";
                }

                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlOracle;

                try
                {
                    conn.Open();
                    OracleDataReader rdr = cmd.ExecuteReader();

                    //while (rdr.Read())
                    //{
                    //    Console.WriteLine("\t{0}\t{1}", rdr[0], rdr[1]);
                    //}

                    dtStateRpt.Load(rdr);

                    rdr.Close();
                }

                catch (Exception ex)
                {
                    string msgEx = "Oracele: GetRptInfoByState() met issue: ";
                    msgEx += ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }

            return dtStateRpt;
        }

        [Obsolete]
        public string UpdRptInfoByState(string sampleId, string paramId,
                                           string processDate, string reportedDate,
                                           bool isReported, bool isProcessed = true)
        {
            string actionResult = "NA";

            DataTable dtStateRpt = new DataTable();
            string sqlOracle = "";

            using (OracleConnection conn = new OracleConnection(CnsOracle))
            {

                if (isReported && isProcessed)
                {
                    sqlOracle = "update u_erptbystate ";
                    sqlOracle += " set processdate = TO_DATE('" + processDate + "', 'YYYY/MM/DD HH24:mi:ss') ";
                    sqlOracle += "    ,reportedt = TO_DATE('" + reportedDate + "', 'YYYY/MM/DD HH24:mi:ss') ";
                    sqlOracle += " where Sampleid = '" + sampleId + "' and paramid = '" + paramId + "'";
                }
                else if (isProcessed)
                {
                    sqlOracle = "update u_erptbystate ";
                    sqlOracle += " set processdate = TO_DATE('" + processDate + "', 'YYYY/MM/DD HH24:mi:ss') ";
                    sqlOracle += " where Sampleid = '" + sampleId + "' and paramid = '" + paramId + "'";
                }

                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlOracle;

                try
                {
                    conn.Open();
                    //OracleDataReader rdr = cmd.ExecuteReader();
                    cmd.ExecuteNonQuery();

                    //while (rdr.Read())
                    //{
                    //    Console.WriteLine("\t{0}\t{1}", rdr[0], rdr[1]);
                    //}

                    //dtStateRpt.Load(rdr);

                    //rdr.Close();
                }

                catch (Exception ex)
                {
                    string msgEx = "Oracele: UpdRptInfoByState() met issue: ";
                    msgEx += ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }

            return actionResult;
        }

        [Obsolete]
        public DataTable GetRptInfoByState_HIS(string stateID = "IL", string processCategory = "COVID19")
        {
            DataTable dtStateRpt = new DataTable();
            string sqlOracle = "";

            using (OracleConnection conn = new OracleConnection(CnsOracle))
            {
                if (processCategory == "COVID19")
                {
                    sqlOracle = "select ERPTBYSTATEID, SAMPLEID, CREATEDT, PROFILEID, PARAMID, RESULTS, REPORTEDT, STATE, PROCESSDATE, UNITS ";
                    sqlOracle += " from u_erptbystate ";
                    sqlOracle += " where (u_erptbystate.processdate Is Null ) ";
                    sqlOracle += "       and (profileid in ('COV2PCRsaliva', 'COV2PCRswab', 'COV2 IgG Antibody', 'SARS CoV2 Saliva', 'SARS CoV2 AN', 'SARS CoV2 NP')) ";
                    sqlOracle += " order by PROFILEID, SAMPLEID ";
                }

                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlOracle;

                try
                {
                    conn.Open();
                    OracleDataReader rdr = cmd.ExecuteReader();

                    //while (rdr.Read())
                    //{
                    //    Console.WriteLine("\t{0}\t{1}", rdr[0], rdr[1]);
                    //}

                    dtStateRpt.Load(rdr);

                    rdr.Close();
                }

                catch (Exception ex)
                {
                    string msgEx = "Oracele: GetRptInfoByState(IL, COVID-19) met issue: ";
                    msgEx += ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }

            return dtStateRpt;
        }

        
    }
}
