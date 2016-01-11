using DashboardAPI.Dal;
using DashboardAPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web.Http;

namespace DashboardAPI.Controllers
{
    public class DBController : ApiController
    {
        [HttpPost]
        public string ProcessDataSync(int envID,string userName)
        {
            string msg = "SUCCESS";

            DBData dataSyncObj = new DBData();
            string bFileLoc = string.Empty;
            string attachmentFileLocations = string.Empty;

            switch (envID)
            {
                case (int)DataSyncEnvironment.PreProd:
                    bFileLoc = Constants.PreprodDataSyncBatchFileLocation;
                    attachmentFileLocations = Constants.PreprodDataSyncAttachments;
                    break;
                case (int)DataSyncEnvironment.Prod:
                    bFileLoc = Constants.ProdDataSyncBatchFileLocation;
                    attachmentFileLocations = Constants.ProdDataSyncAttachments;
                    break;
            }

            if (!string.IsNullOrEmpty(bFileLoc))
            {
                dataSyncObj.BatchFileLocations = bFileLoc;

                string emailSubject = string.Empty;
                dataSyncObj.ProcessDataSync(out emailSubject);

                //Sending Email with log attachment
                EmailController ec = new EmailController();
                ec.SendEmail(emailSubject, "Test body", Constants.EmailTo, attachmentFileLocations);
            }
            else
            {
                msg = "Please provide correct environment id";
            }

            return msg;
        }
        public string Get()
        {
            return "Please provide required parameters";
        }
    }
}
