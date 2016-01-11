using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace DashboardAPI.Dal
{
    public class Constants
    {
        public static string PrimaryNodeC = "ServerName";
        public static string usernameC = "BLAdmin";
        public static string passwordC = "bladmin_pw09";
        public static string authenticationMethodC = "SRP";
        public static string BLPathprimaryC = "/McAfee ARM Jobs/Major Releases/";
        public static string HostFileJobGroupC = "/McAfee ARM Jobs/Utilities";
        public static string HostFileJobNameC = "CopyHostsFile";
        public static string BLPathsecondaryC = "/CURRENT_DEPLOY_JOBS_GROUP_";
        public static string IISC = "IIS_RESET";
        public static string IISValueC = "true";
        public static string EmailTo = ConfigurationManager.AppSettings["EmailTo"];
        public static string EmailFrom = ConfigurationManager.AppSettings["EmailFrom"];
        public static string CorePath = "C:\\Users\ndhir1\\documents\\visual studio 2012\\Projects\\BLContainer\\BLContainer";
        public static string PatchServerListA = ConfigurationManager.AppSettings["PatchNodeServersA"];
        public static string PatchServerListB = ConfigurationManager.AppSettings["PatchNodeServersB"];
        public static string username = "svcacct-lbmonitor";
        public static string password = "kH;GV7dZh4VbC:M4X~W-#bGp/B,4CG]s#Q5V)f";
        public static string primarynode = "DataCenter";
        public static string LBURLPath = HostingEnvironment.MapPath(@"/App_Data/LBURL.xml");
        public static string DBConn = ConfigurationManager.ConnectionStrings["DepDBConn"].ConnectionString;
        public static string DCZoneInfo = HostingEnvironment.MapPath(@"/App_Data/DCZoneID.xml");
        public static string LogicalInfo = HostingEnvironment.MapPath(@"/App_Data/LogicalNametoID.xml");
        public static string PreprodDataSyncBatchFileLocation = ConfigurationManager.AppSettings["PreprodDataSyncBatchFileLocation"];
        public static string ProdDataSyncBatchFileLocation = ConfigurationManager.AppSettings["ProdDataSyncBatchFileLocation"];
        public static string SmtpHostAddress = ConfigurationManager.AppSettings["SmtpHostAddress"];
        public static string PreprodDataSyncAttachments = ConfigurationManager.AppSettings["PreprodDataSyncAttachments"];
        public static string ProdDataSyncAttachments = ConfigurationManager.AppSettings["ProdDataSyncAttachments"];
    }
    public enum DataSyncEnvironment
    {
        PreProd,
        Prod
    }


    
}