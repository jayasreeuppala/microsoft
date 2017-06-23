using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;
using System.Text;
using System.IO;

namespace SelfServiceAdminstration
{
    public class SMSRequest
    {


        public void sendSMS(string mobileno, string message)
        {
            SSAErrorLog logObj = new SSAErrorLog();
            try
            {

                string webTarget = ConfigurationManager.AppSettings["smsurl"].ToString() + "&tname=tqbook&login=tqbook&to=" + mobileno + "&text=" + message;
                //string url = "http://172.32.0.175:8080/mConnector/dispatchapi?cname=tqbook&tname=tqbook&login=tqbook&to=mobilenumber&text=textmessage"


                //string url = String.Format(webTarget, mobileno);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(webTarget);
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "webtarget url  " +webTarget);
                

               // req.Proxy = GlobalProxySelection.GetEmptyWebProxy();
                string proxyAddress;
                proxyAddress = ConfigurationManager.AppSettings["proxyadd"].ToString();
                //proxyadd
                IWebProxy proxy = req.Proxy;
                // Print the Proxy Url to the console.
                if (proxy != null)
                {
                    Console.WriteLine("Proxy: {0}", proxy.GetProxy(req.RequestUri));

                    logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "proxy is there  ...  " + proxy.GetProxy(req.RequestUri));

                    WebProxy myProxy = new WebProxy();
                    Uri newUri = new Uri(proxyAddress);
                    myProxy.Address = newUri;
                    //adminuser
                    //adminpwd
                    myProxy.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["adminuser"].ToString(), ConfigurationManager.AppSettings["adminpwd"].ToString());
                    req.Proxy = myProxy;

                }
                else
                {
                    Console.WriteLine("Proxy is null; no proxy will be used");
                    logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "Proxy is null; no proxy will be used  ");
                }




                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "Connected to sms server " );
                req.Method = "POST";
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "Connected to sms server 2");
                req.ContentType = "application/x-www-form-urlencoded";
                byte[] postData = Encoding.ASCII.GetBytes(message);
                req.ContentLength = postData.Length;
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "Connected to sms server 3");
                // Set HTTP authorization header.
                //string authInfo = userName + ":" + password;
                //authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                //req.Headers["Authorization"] = "Basic " + authInfo;

                // Send HTTP request.
                Stream PostStream = req.GetRequestStream();
                //HttpWebResponse myWebResponse = (HttpWebResponse)req.GetResponse();
                PostStream.Write(postData, 0, postData.Length);
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "request sent  ");
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "get request ");
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), res.StatusDescription + " " + res.StatusCode);
                



            }
            catch (Exception er)
            {
                
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), er.Message);
            }
        }
    }
    

}