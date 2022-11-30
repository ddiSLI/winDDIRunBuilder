using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace winDDIRunBuilder
{
    public class ClientRunBuilder
    {
        public string RunCondition { get; set; }
        public string ProcessCategory { get; set; }
        public string CnsSQL { get; set; }
        public string CnsOracle { get; set; }
        public string ReadFilePath { get; set; }
        public string ExportFilePath { get; set; }
        public string PlateSettingFile { get; set; }
        public string PlatePrinterName { get; set; }
        public ClientRunBuilder(string processCategory =null)
        {
            RunCondition = ConfigurationManager.AppSettings["RunCondition"];
            ProcessCategory = processCategory;

            if (RunCondition=="PROD")
            {
                ReadFilePath = ConfigurationManager.AppSettings["ReadFilePath_PROD"];
                ExportFilePath = ConfigurationManager.AppSettings["ExportFilePath_PROD"];
                PlateSettingFile= ConfigurationManager.AppSettings["PlateSettingFile_Prod"];
                PlatePrinterName = ConfigurationManager.AppSettings["PlatePrinterName_Prod"];
            }
            else
            {
                ReadFilePath= ConfigurationManager.AppSettings["ReadFilePath_DEV"];
                ExportFilePath = ConfigurationManager.AppSettings["ExportFilePath_DEV"];
                PlateSettingFile = ConfigurationManager.AppSettings["PlateSettingFile_Dev"];
                PlatePrinterName = ConfigurationManager.AppSettings["PlatePrinterName_Dev"];
            }

            //RepoOracle = new SQLService(RunCondition);

                //if (string.IsNullOrEmpty(paramDateEnd))
                //{
                //    string syncDateStart = DateTime.Now.ToShortDateString();

                //    //SQLService sqlService = new SQLService(RunCondition);
                //    string[] syncStatus = { "Id", "LastSyncDT", "LastSyncStauts", "LastSyncDesc" };

                //    DateTime dateValue;

                //    if (ProcessCategory == "ALL")
                //    {
                //        //syncStatus = sqlService.GetLastSyncInfo("DOCTORS");
                //        //if (string.IsNullOrEmpty(TransDateEnd) && DateTime.TryParse(syncStatus[1], out dateValue))
                //        //    TransDateEnd = Convert.ToDateTime(syncStatus[1]).ToShortDateString();

                //        //syncStatus = sqlService.GetLastSyncInfo("SALES");
                //        //if (string.IsNullOrEmpty(TransDateStart) && DateTime.TryParse(syncStatus[1], out dateValue))
                //        //    TransDateStart = Convert.ToDateTime(syncStatus[1]).ToShortDateString();
                //    }
                //    else if (ProcessCategory == "PCRWork")
                //    {
                //        //syncStatus = sqlService.GetLastSyncInfo("DOCTORS");
                //        //if (string.IsNullOrEmpty(TransDateEnd) && DateTime.TryParse(syncStatus[1], out dateValue))
                //        //    TransDateEnd = Convert.ToDateTime(syncStatus[1]).ToShortDateString();
                //        //else
                //        //    TransDateEnd = DateTime.Now.AddMinutes((24 * 60) * -1).ToShortDateString();
                //    }

                //}
        }
    }
}
