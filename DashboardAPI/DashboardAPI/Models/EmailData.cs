using DashboardAPI.Dal;
using System;
using System.Net.Mail;
using System.Runtime.Serialization;
namespace DashboardAPI.Models
{
    [Serializable]
    [DataContract]
    public class EmailData
    {
        internal static void SendEmail(string subject,string body,string toAddresses,string attachmentFileLocations)
        {
            try
            {
                //Logger.SetLogWriter(new LogWriterFactory().Create());
                //Logger.Write("Entering SendEmail Method");
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(Constants.SmtpHostAddress);
                mail.From = new MailAddress(Constants.EmailFrom);
                mail.To.Add(toAddresses);
                mail.Subject = subject;
                mail.Body = body;

                foreach (string fileLocation in attachmentFileLocations.Split(','))
                {
                    Attachment a = new Attachment(fileLocation);
                    mail.Attachments.Add(a);
                }
                SmtpServer.Send(mail);
                //Logger.Write("Exiting SendEmail Method");
            }
            catch (Exception ex)
            {
                //Logger.Write(ex.Message);
            }
        }
    }
}