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

        public RepoSQL()
        { 
            RunCondition = ConfigurationManager.AppSettings["RunCondition"];
    
            if (RunCondition=="PROD")
            {
                CnsSQL = ConfigurationManager.ConnectionStrings["cnnSQLReporting_PROD"].ToString();
            }
            else
            {
                CnsSQL = ConfigurationManager.ConnectionStrings["cnnSQLReporting_DEV"].ToString();

            }
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

                    //if (!string.IsNullOrEmpty(batchVersion))
                    //    cmd.Parameters.Add("@pBatchVersion", SqlDbType.Int).Value = Convert.ToInt16(batchVersion);

                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    bool plateTatated = false;

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            plateTatated = rdr["PlateRotated"].ToString() == "0" ? false : true;

                            plates.Add(
                                new DBPlate
                                {
                                    PlateId = rdr["PlateId"].ToString(),
                                    StartPos = rdr["StartPos"].ToString(),
                                    EndPos = rdr["EndPos"].ToString(),
                                    Diluent = rdr["Diluent"].ToString(),
                                    Sample = rdr["Sample"].ToString(),
                                    PlateRotated = rdr["PlateRotated"].ToString() == "0" ? false : true,
                                    ModifiedDate = rdr["ModifiedDate"].ToString(),
                                    PlateVersion = rdr["PlateVersion"].ToString()
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

            using (SqlConnection conn = new SqlConnection(CnsSQL))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "uspRunBld_SelSamples";

                try
                {
                    cmd.Parameters.Add("@pPlateId", SqlDbType.VarChar).Value = plateId;

                    if (!string.IsNullOrEmpty(plateVersion))
                        cmd.Parameters.Add("@pPlateVersion", SqlDbType.Int).Value = Convert.ToInt16(plateVersion);

                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            samples.Add(
                                new PlateSample
                                {
                                    PlateId = rdr["PlateId"].ToString(),
                                    Well = rdr["Well"].ToString(),
                                    Sequence = (int)rdr["Position"],
                                    SampleId = rdr["Samples"].ToString(),
                                    PlateVersion = rdr["PlateVersion"].ToString()
                                }
                                );
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

        public DBPlate AddSamples(List<PlateSample> plateSamples)
        {
            DBPlate addedPlate = new DBPlate();

            using (SqlConnection conn = new SqlConnection(CnsSQL))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "uspRunBld_InsSample";

                try
                {
                    cmd.Parameters.Add("@pPlateId", SqlDbType.VarChar);
                    cmd.Parameters.Add("@pSampleId", SqlDbType.VarChar);
                    cmd.Parameters.Add("@pWell", SqlDbType.VarChar);
                    cmd.Parameters.Add("@pPosition", SqlDbType.Int);
                    cmd.Parameters.Add("@pPlateVersion", SqlDbType.Int);

                    //cmd.Parameters.Add("@pPlateId", SqlDbType.VarChar, 30);
                    //cmd.Parameters.Add("@pSampleId", SqlDbType.VarChar, 30);
                    //cmd.Parameters.Add("@pWell", SqlDbType.VarChar, 4);
                    //cmd.Parameters.Add("@pPosition", SqlDbType.Int);
                    //cmd.Parameters.Add("@pPlateVersion", SqlDbType.Int);


                    conn.Open();

                    foreach(var smp in plateSamples)
                    {
                        cmd.Parameters["@pPlateId"].Value= smp.PlateId;
                    cmd.Parameters["@pSampleId"].Value=smp.SampleId;
                    cmd.Parameters["@pWell"].Value=smp.Well;
                    cmd.Parameters["@pPosition"].Value=smp.Sequence;
                    cmd.Parameters["@pPlateVersion"].Value=smp.PlateVersion;

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
                }
                finally
                {
                    conn.Close();
                }
            }

            return addedPlate;
        }
       
        public string AddPlate(DBPlate dbPlate)
        {
            string resultSaveDB="SUCCESS";

            using (SqlConnection conn = new SqlConnection(CnsSQL))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "uspRunBld_InsPlate";

                try
                {
                    cmd.Parameters.Add("@pPlateName", SqlDbType.VarChar).Value = dbPlate.PlateName;
                    cmd.Parameters.Add("@pPlateId", SqlDbType.VarChar).Value = dbPlate.PlateId;
                    cmd.Parameters.Add("@pStartPos", SqlDbType.VarChar).Value = dbPlate.StartPos;
                    cmd.Parameters.Add("@pEndPos", SqlDbType.VarChar).Value = dbPlate.EndPos;
                    cmd.Parameters.Add("@pDiluent", SqlDbType.Int).Value = Convert.ToInt16(dbPlate.Diluent);
                    cmd.Parameters.Add("@pSample", SqlDbType.Int).Value = Convert.ToInt16(dbPlate.Sample);
                    cmd.Parameters.Add("@pPlateRotated", SqlDbType.Bit).Value = dbPlate.PlateRotated==false? 0: 1;
                    cmd.Parameters.Add("@pPlateVersion", SqlDbType.Int).Value = Convert.ToInt16(dbPlate.PlateVersion);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    resultSaveDB = "SUCCESS";
                }

                catch (Exception ex)
                {
                    string msgEx = "SQL: AddPlate(newPlate) met issue: ";
                    msgEx += Environment.NewLine;
                    //msgEx += "shortId: " + shortId.ToString() + " ;";
                    msgEx += Environment.NewLine;
                    msgEx += ex.Message;
                    resultSaveDB = msgEx;
                }
                finally
                {
                    conn.Close();
                }
            }

            return resultSaveDB;
        }

    }


}
