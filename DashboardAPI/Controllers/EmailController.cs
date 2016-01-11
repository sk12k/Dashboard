using DashboardAPI.Dal;
using DashboardAPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web.Http;

namespace DashboardAPI.Controllers
{
    public class EmailController : ApiController
    {

        [HttpPost]
        public string SendEmail(string subject, string body, string toAddresses, string attachmentFileLocations)
        {
            string retVal = "SUCCESS";
            try
            {
                EmailData.SendEmail(subject, body, toAddresses, attachmentFileLocations);
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                retVal = "FAILURE";
            }

            return retVal;
        }

        public string Get()
        {
            return "please provide all the required parameters";
        }
    }
}
