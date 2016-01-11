using System;
using System.Diagnostics;
using System.Runtime.Serialization;
namespace DashboardAPI.Models
{
    [Serializable]
    [DataContract]
    public class DBData
    {
        public static int count = 0;
       
        [DataMember]
        public string BatchFileLocations { get; set; }

        internal void ProcessDataSync(out string emailSubject)
        {
            bool allFailed = true;
            bool allPassed = true;
            foreach (string bLocation in BatchFileLocations.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                Process _process = null;
                try
                {
                    var processInfo = new ProcessStartInfo(bLocation);
                    _process = Process.Start(processInfo);
                    _process.WaitForExit();

                    int exitCode = _process.ExitCode;
                    if (exitCode == 0)
                    {
                        allFailed = false;
                    }
                    else
                    {
                        allPassed = false;
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;
                    allPassed = false;
                }
                _process.Close();
            }

            if (allFailed)
            {
                emailSubject = "Data Sync Failed";
            }
            else if (allPassed)
            {
                emailSubject = "Data Sync Success";
            }
            else
            {
                emailSubject = "Data Sync Partial Success";
            }
        }
    }
}