using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using winDDIRunBuilder.Models;

namespace winDDIRunBuilder
{
    public class ClientRunBuilder
    {
        public string RunCondition { get; set; }
        public string ProcessCategory { get; set; }
        public string CnsSQL { get; set; }
        public string CnsOracle { get; set; }
        public string Department { get; set; }
        public string JanusName { get; set; }
        public string BCRArchive { get; set; }
        public string BCROutput { get; set; }
        public string RunBuilderOutput { get; set; }
        public string RunBuilderOutputArchive { get; set; }
        public string RunBuilderExport { get; set; }
        public string PlateSettingFile { get; set; }
        public string PlatePrinterName { get; set; }
        public string BuilReportPlace { get; set; }
        public string BuilReportTemplate { get; set; }
        public ClientRunBuilder(string processCategory =null)
        {
            RunCondition = ConfigurationManager.AppSettings["RunCondition"];
            ProcessCategory = processCategory;

            if (RunCondition=="PROD")
            {
                //BCROutput = ConfigurationManager.AppSettings["BCROutput_PROD"];
                //RunBuilderOutput = ConfigurationManager.AppSettings["RunBuilderOutput_PROD"];
                PlateSettingFile= ConfigurationManager.AppSettings["PlateSettingFile_Prod"];
                PlatePrinterName = ConfigurationManager.AppSettings["PlatePrinterName_Prod"];

                BuilReportPlace= ConfigurationManager.AppSettings["BuildReport_Prod"];
                BuilReportTemplate = ConfigurationManager.AppSettings["BuildReport_Template_Prod"];
            }
            else
            {
                //BCROutput= ConfigurationManager.AppSettings["BCROutput_DEV"];
                //RunBuilderOutput = ConfigurationManager.AppSettings["RunBuilderOutput_DEV"];
                PlateSettingFile = ConfigurationManager.AppSettings["PlateSettingFile_Dev"];
                PlatePrinterName = ConfigurationManager.AppSettings["PlatePrinterName_Dev"];
                BuilReportPlace = ConfigurationManager.AppSettings["BuildReport_Dev"];
                BuilReportTemplate = ConfigurationManager.AppSettings["BuildReport_Template_Dev"];
            }

            List<Janus> curJanus = new List<Janus>();
            RepoSQL sqlService = new RepoSQL();
            curJanus = sqlService.GetJanus(System.Net.Dns.GetHostName());

            if (curJanus == null)
            {
                Department = "";
                JanusName = "";
                BCRArchive = "";
                BCROutput = "";
                RunBuilderOutput = "";
                RunBuilderOutputArchive = "";
             }
            else
            {
                Department = curJanus.FirstOrDefault().Department;
                JanusName =curJanus.FirstOrDefault().JanusName;
                BCRArchive= curJanus.FirstOrDefault().BCRArchive;
                BCROutput = curJanus.FirstOrDefault().BCROutput;
                RunBuilderOutput = curJanus.FirstOrDefault().RunBuilderOutput;
                RunBuilderOutputArchive = curJanus.FirstOrDefault().RunBuilderOutputArchive;
                RunBuilderExport = curJanus.FirstOrDefault().RunBuilderExport;
            }
                        
        }
    }
}
