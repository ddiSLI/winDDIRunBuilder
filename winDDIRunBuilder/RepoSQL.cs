using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using winDDIRunBuilder.Models;

namespace winDDIRunBuilder
{
    public class RepoSQL : ISQLService
    {
        string CnsSQL = "";

        public string RunCondition { get; set; } = "DEV";
        public string ErrMsg { set; get; } = "";

        public RepoSQL()
        {
            RunCondition = ConfigurationManager.AppSettings["RunCondition"];

            if (RunCondition == "PROD")
            {
                CnsSQL = ConfigurationManager.ConnectionStrings["cnnSQLReporting_PROD"].ToString();
            }
            else
            {
                CnsSQL = ConfigurationManager.ConnectionStrings["cnnSQLReporting_DEV"].ToString();

            }
        }

        public List<Janus> GetJanus(string hostName)
        {
            List<Janus> allJanus = new List<Janus>();
            Janus curJanus = new Janus();

            using (SqlConnection conn = new SqlConnection(CnsSQL))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "uspRunBld_SelJanus";

                try
                {
                    cmd.Parameters.Add("@pHostName", SqlDbType.VarChar).Value = hostName;

                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            curJanus = new Janus();
                            curJanus.Department = rdr["Department"].ToString();
                            curJanus.JanusName = rdr["JanusName"].ToString();
                            curJanus.BCROutput = rdr["BCROutput"].ToString();
                            curJanus.RunBuilderOutput = rdr["RunBuilderOutput"].ToString();
                            curJanus.RunBuilderOutputArchive = rdr["RunBuilderOutputArchive"].ToString();
                            curJanus.RunBuilderExport = rdr["RunBuilderExport"].ToString();
                            curJanus.JanusOutPut = rdr["JanusOutput"].ToString();
                            curJanus.Description = rdr["Description"].ToString();

                            allJanus.Add(curJanus);
                        }
                    }

                    rdr.Close();
                }

                catch (Exception ex)
                {
                    string msgEx = "SQL: GetJanus() met issue: ";
                    msgEx += Environment.NewLine;
                    msgEx += Environment.NewLine;
                    msgEx += ex.Message;
                    ErrMsg = "SQLService.GetJanus() Exception: " + ex.Message;
                }
                finally
                {
                    conn.Close();
                }

            }


            return allJanus;
        }

        public List<Protocol> GetProtocols(string dept = "")
        {
            List<Protocol> protocols = new List<Protocol>();
            Protocol prot = new Protocol();


            using (SqlConnection conn = new SqlConnection(CnsSQL))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "uspRunBld_SelProtocols";

                try
                {
                    cmd.Parameters.Add("@pDept", SqlDbType.VarChar).Value = dept;

                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();


                    if (rdr.HasRows)
                    {
                        int protId = 0;
                        while (rdr.Read())
                        {
                            prot = new Protocol();
                            prot.Id = "P" + protId.ToString().Trim();
                            prot.Department = rdr["Department"].ToString();
                            prot.PlateId = rdr["PlateId"].ToString();
                            prot.SourcePlate = rdr["SourcePlate"].ToString();
                            prot.ProtocolName = rdr["Protocol"].ToString();
                            prot.WorklistName = rdr["WorkListName"].ToString();
                            prot.HasAliasId = (bool)rdr["HasAliasId"] == true ? true : false;
                            prot.Pooling = (bool)rdr["Pooling"] == true ? true : false;
                            prot.DBTest = rdr["DBTest"].ToString();
                            prot.PlateRotated = (bool)rdr["PlateRotated"] == false ? false : true;
                            prot.StartPos = rdr["FirstPos"].ToString();     //PlateSize
                            prot.EndPos = rdr["LastPos"].ToString();        //PlateSize
                            prot.ExcludeWells = rdr["ExcludeWells"].ToString();
                            prot.Sample = rdr["sample"].ToString();
                            prot.Diluent = rdr["Diluent"].ToString();
                            prot.Opt1 = rdr["Opt1"].ToString();
                            prot.Opt2 = rdr["Opt2"].ToString();
                            prot.Opt3 = rdr["Opt3"].ToString();
                            prot.Opt4 = rdr["Opt4"].ToString();
                            prot.Opt5 = rdr["Opt5"].ToString();

                            protocols.Add(prot);
                        }

                        protId += 1;
                    }

                    rdr.Close();
                }

                catch (Exception ex)
                {
                    string msgEx = "SQL: GetProtocols() met issue: ";
                    msgEx += Environment.NewLine;
                    msgEx += Environment.NewLine;
                    msgEx += ex.Message;
                    ErrMsg = "SQLService.GetProtocols() Exception: " + ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }

            return protocols;
        }

        public string GetSeries(string Dept = "DDI")
        {
            string curSeries = "";

            using (SqlConnection conn = new SqlConnection(CnsSQL))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "uspRunBld_SelSeries";

                try
                {
                    cmd.Parameters.Add("@pDepartment", SqlDbType.VarChar).Value = Dept;

                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            curSeries = rdr["TodayDate"].ToString();
                            curSeries += rdr["letter1"].ToString();
                            curSeries += rdr["letter2"].ToString();
                        }
                    }

                    rdr.Close();
                }

                catch (Exception ex)
                {
                    string msgEx = "SQL: GetSeries() met issue: ";
                    msgEx += Environment.NewLine;
                    msgEx += Environment.NewLine;
                    msgEx += ex.Message;
                    ErrMsg = "SQLService.GetSeries() Exception: " + ex.Message;
                }
                finally
                {
                    conn.Close();
                }

            }

            return curSeries;

        }
        public List<DBPlate> GetPlates(string plateId, string plateVersion = null)
        {
            List<DBPlate> plates = new List<DBPlate>();

            using (SqlConnection conn = new SqlConnection(CnsSQL))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "uspRunBld_SelPlates";

                try
                {
                    cmd.Parameters.Add("@pPlateId", SqlDbType.VarChar).Value = plateId;
                    cmd.Parameters.Add("@pPlateVersion", SqlDbType.VarChar).Value = "";  // plateVersion;

                    //if (!string.IsNullOrEmpty(batchVersion))
                    //    cmd.Parameters.Add("@pBatchVersion", SqlDbType.Int).Value = Convert.ToInt16(batchVersion);

                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    bool plateTatated = false;

                    if (rdr.HasRows)
                    {
                        //DataTable dtPlt = new DataTable();
                        //dtPlt.Load(rdr);

                        while (rdr.Read())
                        {
                            plateTatated = (bool)rdr["PlateRotated"] == false ? false : true;

                            plates.Add(
                                new DBPlate
                                {
                                    Department = rdr["Department"].ToString(),
                                    PlateName = rdr["PlateName"].ToString(),
                                    PlateId = rdr["PlateId"].ToString(),
                                    HasQC = rdr["HasQC"].ToString(),
                                    PlateDesc = rdr["PlateDesc"] == null ? "" : rdr["PlateDesc"].ToString(),
                                    SizeStartWell = rdr["PlateSizeStart"].ToString(),
                                    SizeEndWell = rdr["PalteSizeEnd"].ToString(),
                                    StartPos = rdr["StartPos"].ToString(),
                                    EndPos = rdr["EndPos"].ToString(),
                                    ExcludeWells = string.IsNullOrEmpty(rdr["ExcludeWells"].ToString()) ? "" : rdr["ExcludeWells"].ToString(),
                                    Diluent = rdr["Diluent"].ToString(),
                                    Sample = rdr["Sample"].ToString(),
                                    PlateRotated = (bool)rdr["PlateRotated"] == false ? false : true,
                                    SourcePlateId = string.IsNullOrEmpty(rdr["SourcePlateId"].ToString()) ? "" : rdr["SourcePlateId"].ToString(),
                                    SourcePlateVersion = string.IsNullOrEmpty(rdr["SourcePlateVersion"].ToString()) ? "" : rdr["SourcePlateVersion"].ToString(),
                                    Accept = rdr["Accept"].ToString() == null ? "" : rdr["Accept"].ToString(),
                                    Opt1 = rdr["Opt1"].ToString() == null ? "" : rdr["Opt1"].ToString(),
                                    Opt2 = rdr["Opt2"].ToString() == null ? "" : rdr["Opt2"].ToString(),
                                    Opt3 = rdr["Opt3"].ToString() == null ? "" : rdr["Opt3"].ToString(),
                                    Opt4 = rdr["Opt4"].ToString() == null ? "" : rdr["Opt5"].ToString(),
                                    Opt5 = rdr["Opt5"].ToString() == null ? "" : rdr["Opt5"].ToString(),
                                    ModifiedDate = rdr["ModifiedDate"].ToString(),
                                    PlateVersion = rdr["PlateTimeVersion"].ToString()
                                }
                                );
                        }
                    }

                    rdr.Close();
                }

                catch (Exception ex)
                {
                    string msgEx = "SQL: GetPlate(plateId,plateVersion) met issue: ";
                    msgEx += Environment.NewLine;
                    //msgEx += "shortId: " + shortId.ToString() + " ;";
                    msgEx += Environment.NewLine;
                    msgEx += ex.Message;
                    ErrMsg = "SQLService.GetPlates() Exception: " + ex.Message;
                }
                finally
                {
                    conn.Close();
                }

            }

            return plates;
        }

        public List<PlateSample> GetPlateSamples(string plateId, string plateVersion = null)
        {
            List<PlateSample> samples = new List<PlateSample>();
            PlateSample sample = new PlateSample();

            using (SqlConnection conn = new SqlConnection(CnsSQL))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "uspRunBld_SelSamples";

                try
                {
                    cmd.Parameters.Add("@pPlateId", SqlDbType.VarChar).Value = plateId;

                    if (!string.IsNullOrEmpty(plateVersion))
                        cmd.Parameters.Add("@pPlateVersion", SqlDbType.VarChar).Value = plateVersion;

                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    //DataTable ss = new DataTable();
                    //ss.Load(rdr);


                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            sample = new PlateSample();
                            sample.PlateId = rdr["PlateId"].ToString();
                            sample.Well = rdr["Well"].ToString();
                            //sample.Sequence = (int)rdr["Position"];
                            sample.SampleId = rdr["SampleId"].ToString();
                            sample.PlateVersion = rdr["PlateTimeVersion"].ToString();
                            sample.SampleType = rdr["SampleType"].ToString();
                            sample.Status = rdr["Status"].ToString();
                            samples.Add(sample);
                        }
                    }

                    rdr.Close();
                }

                catch (Exception ex)
                {
                    string msgEx = "SQL: GetPlateSamples(plateId,plateVersion) met issue: ";
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

            return samples;
        }

        public string AddSamples(List<OutputPlateSample> plateSamples, string user = "")
        {
            string actionResults = "SUCCESS";

            using (SqlConnection conn = new SqlConnection(CnsSQL))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "uspRunBld_InsSample";

                try
                {
                    cmd.Parameters.Add("@pPlateId", SqlDbType.VarChar);
                    cmd.Parameters.Add("@pSampleId", SqlDbType.VarChar);
                    cmd.Parameters.Add("@pSampleType", SqlDbType.VarChar);

                    cmd.Parameters.Add("@pWell", SqlDbType.VarChar);
                    cmd.Parameters.Add("@pPosition", SqlDbType.Int);
                    cmd.Parameters.Add("@pPlateTimeVersion", SqlDbType.VarChar);
                    cmd.Parameters.Add("@pSourePlateId", SqlDbType.VarChar);
                    cmd.Parameters.Add("@pSourePlateVersion", SqlDbType.VarChar);
                    cmd.Parameters.Add("@pSoureWell", SqlDbType.VarChar);
                    cmd.Parameters.Add("@pUser", SqlDbType.VarChar);
                    conn.Open();

                    foreach (var smp in plateSamples)
                    {
                        cmd.Parameters["@pPlateId"].Value = smp.DestNewPlateId == null ? smp.DestPlateId : smp.DestNewPlateId;
                        cmd.Parameters["@pSampleId"].Value = smp.SampleId;
                        cmd.Parameters["@pSampleType"].Value = string.IsNullOrEmpty(smp.SampleType) ? "" : smp.SampleType;
                        cmd.Parameters["@pWell"].Value = smp.DestWellId;
                        cmd.Parameters["@pPosition"].Value = smp.Sequence;
                        cmd.Parameters["@pPlateTimeVersion"].Value = smp.DestPlateVersion;

                        cmd.Parameters["@pSourePlateId"].Value = smp.SourcePlateId == null ? "" : smp.SourcePlateId;
                        cmd.Parameters["@pSourePlateVersion"].Value = smp.SourcePlateVersion == null ? "" : smp.SourcePlateVersion;
                        cmd.Parameters["@pSoureWell"].Value = smp.SourceWellId == null ? "" : smp.SourceWellId;
                        cmd.Parameters["@pUser"].Value = user;

                        cmd.ExecuteNonQuery();
                    }

                }

                catch (Exception ex)
                {
                    string msgEx = "SQL: AddSamples(List<PlateSample> plateSamples) met issue: ";
                    msgEx += Environment.NewLine;
                    //msgEx += "shortId: " + shortId.ToString() + " ;";
                    msgEx += Environment.NewLine;
                    msgEx += ex.Message;
                    actionResults = "ERROR:" + ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }

            return actionResults;
        }

        public string AddPlate(DBPlate dbPlate, string user = "")
        {
            string resultSaveDB = "SUCCESS";

            using (SqlConnection conn = new SqlConnection(CnsSQL))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "uspRunBld_InsPlate";

                try
                {
                    cmd.Parameters.Add("@pDept", SqlDbType.VarChar).Value = dbPlate.Department == null ? "" : dbPlate.Department;
                    cmd.Parameters.Add("@pPlateName", SqlDbType.VarChar).Value = dbPlate.PlateName == null ? "" : dbPlate.PlateName;
                    cmd.Parameters.Add("@pPlateId", SqlDbType.VarChar).Value = dbPlate.PlateId;
                    cmd.Parameters.Add("@pPlateDesc", SqlDbType.VarChar).Value = dbPlate.PlateDesc;
                    cmd.Parameters.Add("@pSizeFirstPos", SqlDbType.VarChar).Value = dbPlate.SizeStartWell;
                    cmd.Parameters.Add("@pSizeLastPos", SqlDbType.VarChar).Value = dbPlate.SizeEndWell;
                    cmd.Parameters.Add("@pStartPos", SqlDbType.VarChar).Value = dbPlate.StartPos;
                    cmd.Parameters.Add("@pEndPos", SqlDbType.VarChar).Value = dbPlate.EndPos;
                    cmd.Parameters.Add("@pExcludeWells", SqlDbType.VarChar).Value = dbPlate.ExcludeWells;
                    cmd.Parameters.Add("@pDiluent", SqlDbType.Decimal).Value = Convert.ToDecimal(dbPlate.Diluent);
                    cmd.Parameters.Add("@pSample", SqlDbType.Int).Value = Convert.ToInt16(dbPlate.Sample);
                    cmd.Parameters.Add("@pAccept", SqlDbType.VarChar).Value = dbPlate.Accept;
                    cmd.Parameters.Add("@pPlateRotated", SqlDbType.Bit).Value = dbPlate.PlateRotated == false ? 0 : 1;
                    cmd.Parameters.Add("@pSourcePlateId", SqlDbType.VarChar).Value = dbPlate.SourcePlateId;
                    cmd.Parameters.Add("@pSourcePlateVersion", SqlDbType.VarChar).Value = dbPlate.SourcePlateVersion;
                    cmd.Parameters.Add("@pPlateTimeVersion", SqlDbType.VarChar).Value = dbPlate.PlateVersion;
                    cmd.Parameters.Add("@pUser", SqlDbType.VarChar).Value = user;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    string msgEx = "SQL: AddPlate(newPlate) met issue: ";
                    msgEx += Environment.NewLine;
                    //msgEx += "shortId: " + shortId.ToString() + " ;";
                    msgEx += Environment.NewLine;
                    msgEx += ex.Message;
                    resultSaveDB = msgEx;
                    ErrMsg = "SQLService.AddPlate() Exception: " + ex.Message;

                    resultSaveDB = "ERROR:" + ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }

            return resultSaveDB;
        }

        public List<QCSample> GetQCSamples(string plateName)
        {
            List<QCSample> qcSamples = new List<QCSample>();
            QCSample qcSmp = new QCSample();

            using (SqlConnection conn = new SqlConnection(CnsSQL))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "uspRunBld_SelQCSamples";

                try
                {
                    cmd.Parameters.Add("@pPlate", SqlDbType.VarChar).Value = plateName;

                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            qcSmp = new QCSample();
                            qcSmp.Plate = rdr["Plate"].ToString();
                            qcSmp.Sample = rdr["Sample"].ToString();
                            qcSmp.Prefix = rdr["Prefix"].ToString();
                            qcSmp.HarvestId = rdr["HarvestId"].ToString();
                            qcSmp.Well = rdr["Well"].ToString();
                            qcSmp.PlateDesc = rdr["Description"].ToString();

                            qcSamples.Add(qcSmp);
                        }
                    }

                    rdr.Close();
                }

                catch (Exception ex)
                {
                    string msgEx = "SQL: GetQCSamples() met issue: ";
                    msgEx += Environment.NewLine;
                    msgEx += Environment.NewLine;
                    msgEx += ex.Message;
                    ErrMsg = "SQLService.GetQCSamples() Exception: " + ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }


            return qcSamples;
        }

        public string AddPlateQCSamples(List<PlateSample> plateQCSamples, string user)
        {
            string actionResults = "SUCCESS";

            using (SqlConnection conn = new SqlConnection(CnsSQL))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "uspRunBld_InsPlateQCSample";

                try
                {
                    cmd.Parameters.Add("@pPlateId", SqlDbType.VarChar);
                    cmd.Parameters.Add("@pSampleId", SqlDbType.VarChar);
                    cmd.Parameters.Add("@pWell", SqlDbType.VarChar);
                    cmd.Parameters.Add("@pPlateVersion", SqlDbType.VarChar);
                    cmd.Parameters.Add("@pUser", SqlDbType.VarChar);

                    conn.Open();
                    foreach (var smp in plateQCSamples)
                    {
                        cmd.Parameters["@pPlateId"].Value = smp.PlateId;
                        cmd.Parameters["@pSampleId"].Value = smp.SampleId;
                        cmd.Parameters["@pPlateVersion"].Value = smp.PlateVersion;
                        cmd.Parameters["@pWell"].Value = smp.Well;
                        cmd.Parameters["@pUser"].Value = user;

                        cmd.ExecuteNonQuery();
                    }

                }

                catch (Exception ex)
                {
                    string msgEx = "SQL: AddPlateQCSamples() met issue: ";
                    msgEx += Environment.NewLine;
                    //msgEx += "shortId: " + shortId.ToString() + " ;";
                    msgEx += Environment.NewLine;
                    msgEx += ex.Message;
                    actionResults = "ERROR:" + ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }

            return actionResults;
        }

        public string UpdatePlateQC(string plateId, string plateVersion, bool hasQC, string user)
        {
            string resultSaveDB = "SUCCESS";

            using (SqlConnection conn = new SqlConnection(CnsSQL))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "uspRunBld_UpdPlate";

                try
                {
                    cmd.Parameters.Add("@pPlateId", SqlDbType.VarChar).Value = plateId;
                    cmd.Parameters.Add("@pPlateVersion", SqlDbType.VarChar).Value = plateVersion;
                    cmd.Parameters.Add("@pHasQC", SqlDbType.Bit).Value = hasQC;
                    cmd.Parameters.Add("@pUser", SqlDbType.VarChar).Value = user;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    string msgEx = "SQL:UpdatePlateQC() met issue: ";
                    msgEx += Environment.NewLine;
                    //msgEx += "shortId: " + shortId.ToString() + " ;";
                    msgEx += Environment.NewLine;
                    msgEx += ex.Message;
                    resultSaveDB = msgEx;
                    ErrMsg = "SQLService.AddPlate() Exception: " + ex.Message;

                    resultSaveDB = "ERROR:" + ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }

            return resultSaveDB;
        }

        public string UpdateSampleStatus(List<SampleStatus> sampleStatus)
        {
            string resultUpdSample = "SUCCESS";

            using (SqlConnection conn = new SqlConnection(CnsSQL))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "uspRunBld_UpdSampleStatus";

                try
                {
                    cmd.Parameters.Add("@pId", SqlDbType.Int);
                    cmd.Parameters.Add("@pStatus", SqlDbType.VarChar);
                    cmd.Parameters.Add("@pUser", SqlDbType.VarChar);
                    cmd.Parameters.Add("@pPlateId", SqlDbType.VarChar);
                    cmd.Parameters.Add("@pPlateVersion", SqlDbType.VarChar);
                    cmd.Parameters.Add("@pSampleId", SqlDbType.VarChar);

                    conn.Open();
                    foreach (var smp in sampleStatus)
                    {
                        cmd.Parameters["@pStatus"].Value = smp.Status;
                        cmd.Parameters["@pUser"].Value = smp.User;

                        if (string.IsNullOrEmpty(smp.Id))
                        {
                            cmd.Parameters["@pPlateId"].Value = smp.PlateId;
                            cmd.Parameters["@pPlateVersion"].Value = smp.PlateVersion;
                            cmd.Parameters["@pSampleId"].Value = smp.SampleId;
                        }
                        else
                        {
                            cmd.Parameters["@pId"].Value = Convert.ToInt32(smp.Id);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    string msgEx = "SQLService.UpdateSampleStatus() Exception: ";
                    msgEx += Environment.NewLine;
                    msgEx += ex.Message;
                    resultUpdSample = msgEx;
                }
                finally
                {
                    conn.Close();
                }
            }

            return resultUpdSample;
        }

        public List<PlateSample> GetDeptSamples(DeptSample deptSample)
        {
            List<PlateSample> samples = new List<PlateSample>();
            PlateSample sample = new PlateSample();

            using (SqlConnection conn = new SqlConnection(CnsSQL))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "uspRunBld_SelDeptSamples";

                try
                {
                    cmd.Parameters.Add("@pDept", SqlDbType.VarChar).Value = deptSample.Dept;
                    cmd.Parameters.Add("@pDBTest", SqlDbType.VarChar).Value = deptSample.DBTest;
                    cmd.Parameters.Add("@pStatus", SqlDbType.VarChar).Value = deptSample.Status;
                    cmd.Parameters.Add("@pDateRangeMonths", SqlDbType.Int).Value = deptSample.DateRangeMonths;

                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // DataTable ss = new DataTable();
                    //ss.Load(rdr);


                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            sample = new PlateSample();
                            sample.Id = rdr["Id"].ToString();
                            sample.PlateId = rdr["PlateId"].ToString();
                            sample.Well = rdr["Well"].ToString();
                            sample.SampleId = rdr["SampleId"].ToString();
                            sample.PlateVersion = rdr["PlateTimeVersion"].ToString();
                            sample.SampleType = rdr["SampleType"].ToString();
                            sample.Status = rdr["Status"].ToString();
                            sample.DBTest = rdr["Accept"].ToString();
                            sample.ModifiedDate = rdr["ModifiedDate"].ToString();
                            samples.Add(sample);
                        }
                    }

                    rdr.Close();
                }

                catch (Exception ex)
                {
                    string msgEx = "SQLService.GetDeptSamples() Exception: ";
                    msgEx += Environment.NewLine;
                    msgEx += ex.Message;
                }
                finally
                {
                    conn.Close();
                }

            }

            return samples;
        }

        public List<ExportFile> GetExportFiles(string range="ACTIVE")
        {
            List<ExportFile> exports = new List<ExportFile>();
            ExportFile exp = new ExportFile();

            using (SqlConnection conn = new SqlConnection(CnsSQL))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "uspRunBld_SelExports";

                try
                {
                    cmd.Parameters.Add("@pRange", SqlDbType.VarChar).Value = range;

                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    //DataTable dt = new DataTable();
                    //dt.Load(rdr);

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            exp = new ExportFile();
                            exp.Id = rdr["Id"].ToString();
                            exp.Name = rdr["SysName"]==null? "": rdr["SysName"].ToString();
                            exp.WithHeader=(Boolean)rdr["WithHeader"];
                            exp.Field0 = rdr["Field0"] == null ? "" : rdr["Field0"].ToString();
                            exp.Field1 = rdr["Field1"] == null ? "" : rdr["Field1"].ToString();
                            exp.Field2 = rdr["Field2"] == null ? "" : rdr["Field2"].ToString();
                            exp.Field3 = rdr["Field3"] == null ? "" : rdr["Field3"].ToString();
                            exp.Field4 = rdr["Field4"] == null ? "" : rdr["Field4"].ToString();
                            exp.Field5 = rdr["Field5"] == null ? "" : rdr["Field5"].ToString();
                            exp.Field6 = rdr["Field6"] == null ? "" : rdr["Field6"].ToString();
                            exp.Field7 = rdr["Field7"] == null ? "" : rdr["Field7"].ToString();
                            exp.Field8 = rdr["Field8"] == null ? "" : rdr["Field8"].ToString();
                            exp.Field9 = rdr["Field9"] == null ? "" : rdr["Field9"].ToString();
                            exp.ModifiedDate = rdr["ModifiedDate"].ToString();
                            exp.ModifiedBy = rdr["ModifiedBy"].ToString();
                            exp.IsActive = (Boolean)rdr["IsAvtive"];

                            exports.Add(exp);
                        }
                    }

                    rdr.Close();
                }

                catch (Exception ex)
                {
                    string msgEx = "SQL: GetExportFiles() met issue: ";
                    msgEx += Environment.NewLine;
                    msgEx += Environment.NewLine;
                    msgEx += ex.Message;
                    ErrMsg = ex.Message;
                }
                finally
                {
                    conn.Close();
                }

            }


            return exports;
        }
    }
}



