using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;

namespace SelfServiceAdminstration.Authentication
{
    public class SSAEmail
    {

        public void sendEmail(string useremail,string subject,string messagebody,string username,string pwd,string serverip,int portno,string fromemailid)
        {
            SSAErrorLog logObj = new SSAErrorLog();
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(serverip);

                mail.From = new MailAddress(fromemailid);
                mail.To.Add(useremail);
                mail.Subject = subject;
                mail.Body = messagebody;

                SmtpServer.Port = portno;
                if(!username.Equals("none"))
                SmtpServer.Credentials = new System.Net.NetworkCredential(username, pwd);
                SmtpServer.UseDefaultCredentials = false;
               // SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                
            }
            catch (Exception er)
            {
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "Error while sending mail "+er.Message);
            }
        }
    }
    

}