using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SelfServiceAdminstration.Authentication;
using System.Collections;
using System.Configuration;
using System.DirectoryServices;
using SelfServiceAdminstration.Databasecomp;

namespace SelfServiceAdminstration
{
    public partial class wer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            if(!validateOTP())
            {
                Response.Redirect("InvalidOTP.aspx");
            }
            */
            if (!IsPostBack)
            {
                if (!validateOTP())
                {
                    Response.Redirect("InvalidOTP.aspx");
                }
                if (Session["username"] != null)
                {
                     getAccountDetails();
                }
                else if (Session["forgetpwduser"] != null)
                {

                    getAccountDetails();
                }
                else
                    Response.Redirect("SelfServiceLogin.aspx");
            }
            else
            {
               // Session.RemoveAll();
               // Response.Redirect("SelfServiceLogin.aspx");
            }

            if (ConfigurationManager.AppSettings["captchavalidation"].ToString().Equals("yes"))
                captchadiv.Visible = true;
            else
                captchadiv.Visible = false;
        }
        protected bool validateOTP()
        {
            SSAErrorLog logObj = new SSAErrorLog();
            string userotp = null;

            string userid = null;
            try
            {
                if (Session["userid"] != null)
                {
                    userid = Session["userid"].ToString();

                }
                else if (Session["forgetpwduser"] != null)
                {
                    userid = Session["forgetpwduser"].ToString();
                }

                DatabaseLayer dataObj = new DatabaseLayer();
                userid = QASecurity.Encryptdata(userid);

                ArrayList colNames = new ArrayList();
                colNames.Add("iduserotp");
                colNames.Add("username");
                colNames.Add("otp");
                colNames.Add("otpcreatedatetime");
                colNames.Add("otpactivate");

                Hashtable updateHash = new Hashtable();
                updateHash.Add("otpactivate", 1);
                ArrayList resulthash = dataObj.getTableDataQuery("iduserotp,username,otp,otpcreatedatetime,otpactivate from userotp where username='" + userid + "'", null, "iduserotp", colNames);


                string dbotp = resulthash[2].ToString();
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "User idd " + userid + " << dbotp >>>" + dbotp);
                DateTime otpdateObj = Convert.ToDateTime(resulthash[3].ToString());

                string activate = resulthash[4].ToString();
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), " << activate >>>" + activate);
                DateTime current = DateTime.Now;

                TimeSpan ts = current - otpdateObj;
                int mins = ts.Minutes;
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "difference mins   " + mins);
                string otpvalidation = ConfigurationManager.AppSettings["otpdurationvalidation"].ToString();
                string otpdurationinmins = ConfigurationManager.AppSettings["otpdurationinmins"].ToString();
                int otpduration = Convert.ToInt32(otpdurationinmins);
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "otpduration    " + otpduration);
                if(Session["userotp"] != null)
                {
                    logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "OTP session val     " + Session["userotp"].ToString());
                    userotp = Session["userotp"].ToString();
                }
                else
                {
                    logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "OTP session valis not there     " );
                }
                   
                
                if (otpvalidation.Equals("yes"))
                {
                    if (mins > otpduration)
                    {

                        return false;
                    }
                }
                if (dbotp.Equals(userotp) && activate.Equals("False"))
                {
                    //Response.Redirect("wer.aspx");
                    logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "done   ");
                    //here it shoud deactivate the OTP, update the table
                    dataObj.updateTableData("userotp", updateHash, "username='" + userid + "'");

                    return true;

                }
                else
                {
                    logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), " Not a valid OTP...");
                    return false;
                }

                //dataObj.getTableData("",
            }
            catch (Exception er)
            {
                return false;
            }

        }
        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                if (ConfigurationManager.AppSettings["captchavalidation"].ToString().Equals("yes"))
                {
                    if (this.txtimgcode.Text == this.Session["CaptchaImageText"].ToString())
                    {
                        //lblmsg.Text = "Excellent.......";
                    }
                    else
                    {
                        lblmsg.Text = "Please Enter valid Captcha.";
                        return;

                    }
                    this.txtimgcode.Text = "";
                }

                if (ResetUserPassword(username.Text))
                {
                    
                    Response.Redirect("ResetSucess.aspx",false);
                }
                else
                {
                    Response.Redirect("AuthFailed.aspx",false);
                }
                Session.RemoveAll();
            }
            catch (Exception er)
            {
                SSAErrorLog logObj = new SSAErrorLog();
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), er.Message);
                Session.RemoveAll();
                Response.Redirect("AuthFailed.aspx",false);
            }
        }


        protected void getAccountDetails()
        {
            Hashtable getData = null;
            string userid = "";
            SSAErrorLog logObj = new SSAErrorLog();
            try
            {

                if (Session["userid"] != null)
                    userid = Session["userid"].ToString();
                else if (Session["forgetpwduser"] != null)
                    userid = Session["forgetpwduser"].ToString();
                else
                    Response.Redirect("SelfServiceLogin.aspx");

                string domainName = ConfigurationManager.AppSettings["domain"];
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "getAccountDetailsuserid 1" + userid);
                getData = IsAuthenticateduserinfo(domainName, userid, "");
                //DateTime.FromFileTime((long)searchResult.Properties["lastLogon"][0]);
                displayuser.Text = getData["principalname"].ToString();
                username.Text = getData["principalname"].ToString(); //Session["username"].ToString();

                //lastlogon.Text = getData["lastlogontimestamp"].ToString();
               // pwdstatus.Text = getData["passwordexpired"].ToString();
                //pwdlastchange.Text = getData["pwdlastchanged"].ToString();
                //passwordexpire.Text = getData["passwordexpires"].ToString();
               // accountcreated.Text = getData["whencreated"].ToString();
               // activestatus.Text = getData["lockouttime"].ToString();
                //mobileno.Text = getData["mobile"].ToString();
                if (getData["mobile"] != null)
                {
                    string mobilestr = getData["mobile"].ToString();
                    //mobilestr = mobilestr.Substring(0, mobilestr.Length - 4) + "XXXX";
                    //mobilestr = mobilestr.Substring(0, mobilestr.Length - 4) + "XXXX";
                    mobilestr = "XX XX XX" + mobilestr.Substring(mobilestr.Length - 4);
                    mobileno.Text = mobilestr;
                }
                else
                    mobileno.Text = "Mobile Number not available/configured, Please contact Administrator";

                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "get mail details . .." + getData["emailid"].ToString());
                HiddenField1.Value = getData["mail"].ToString();
            }
            catch (Exception er)
            {
                
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "getAccountDetails" + er.Message);
            }
        }

        public Hashtable IsAuthenticateduserinfo(string domain, string username, string pwd)
        {
            SSAErrorLog logObj = new SSAErrorLog();
            logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "IsAuthenticateduserinfoe1");
            string domainAndUsername = domain + @"\" + username;
            string displayName = null;
            DirectoryEntry entry = GetDirectoryEntryByUserName(username);
            
            logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "IsAuthenticateduserinfoe2 entry " + entry);
            string _path = "";
            
            logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "IsAuthenticateduserinfo");

            Hashtable getDataHash = null;


            try
            {
                //Bind to the native AdsObject to force authentication.
                object obj = entry.NativeObject;
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "IsAuthenticateduserinfo2");
                DirectorySearcher search = new DirectorySearcher(entry);
                if (entry != null)
                {
                    logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "IsAuthenticateduserinfo21" + entry.Name);
                }
                else
                    logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "IsAuthenticateduserinfo21 entryis nullm ");

                if (search != null)
                {
                    logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "IsAuthenticateduserinfo22" + search.Filter);
                }
                else
                    logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "IsAuthenticateduserinfo22 search nulll m ");
                search.Filter = "(SAMAccountName=" + username + ")";
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "IsAuthenticateduserinfoe123  " + search.Filter);
                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("samaccountname");

                SearchResult result = search.FindOne();
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "IsAuthenticateduserinfoe124  " + result.Path);
                DirectoryEntry userentry = result.GetDirectoryEntry();
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "IsAuthenticateduserinfo3");
                if (null == result)
                {
                    return null;
                }

                //Update the new path to the user in the directory.
                _path = result.Path;
                displayName = (string)result.Properties["cn"][0];
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "IsAuthenticateduserinfo4");
                getDataHash = GetUserInfo(displayName, result.Path);
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "IsAuthenticateduserinfo5");
                // userentry.Invoke("SetPassword", new object[] { "ooty@4567" });
                // userentry.Properties["LockOutTime"].Value = 0;

                userentry.Close();
            }
            catch (Exception ex)
            {
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "IsAuthenticateduserinfo6" + ex.Message);
                throw new Exception("Error authenticating user. " + ex.Message);
            }

            return getDataHash;
        }

        public Hashtable GetUserInfo(string userName, string path)
        {
            DirectorySearcher search = new DirectorySearcher(path);

            SSAErrorLog logObj = new SSAErrorLog();
            logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "get user info 1");

            search.Filter = "(&(objectClass=user)(cn=" + userName + "))";
            logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "get user info 2" + userName);
            // search.Filter = "(&(objectClass=user)(samaccountname=" + userName + "))";

            //search.Filter = "(cn=" + _filterAttribute + ")";
            SearchResultCollection sResults = null;
            string colStr = "";
            Hashtable getData = null;

            try
            {
                getData = new Hashtable();
                sResults = search.FindAll();

                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "get user info 2" + sResults.Count);

                //loop through results of search
                foreach (SearchResult searchResult in sResults)
                {
                    int propCount = searchResult.Properties.Count;
                    ICollection coll = searchResult.Properties.PropertyNames;

                    //ResultPropertyValueCollection valueCollection =
                    //        searchResult.Properties["lastlogontimestamp"];
                    //ResultPropertyValueCollection passwordExpired =
                    //        searchResult.Properties["userAccountControl"];

                    //ResultPropertyValueCollection passwordchanged =
                    //        searchResult.Properties["whenchanged"];

                    //ResultPropertyValueCollection passwordexpires =
                    //        searchResult.Properties["accountexpires"];

                    //ResultPropertyValueCollection whencreated =
                    //        searchResult.Properties["whencreated"];

                    //ResultPropertyValueCollection lockouttime =
                    //        searchResult.Properties["lockouttime"];

                    ResultPropertyValueCollection principalname =
                            searchResult.Properties["name"];
                    //ResultPropertyValueCollection emailid =
                    //        searchResult.Properties["mail"];
                    ResultPropertyValueCollection mobile =
                            searchResult.Properties["mobile"];
                    ResultPropertyValueCollection mail =
                            searchResult.Properties["mail"];
                    //int m_Val1 = (int)searchResult.Properties[""]..Properties["userAccountControl"]..Value;

                    //int m_Val1 = Int32.Parse(passwordExpired[0].ToString());
                    //int m_Val2 = (int)0x10000;
                    //bool m_Check = false;
                    //if (Convert.ToBoolean(m_Val1 & m_Val2))
                    //{
                    //    m_Check = true;
                    //} //end
                    //if (m_Check)
                    //    getData.Add("passwordexpired", "Expired");
                    //else
                    //    getData.Add("passwordexpired", "Not Expired");

                    //getData.Add("lastlogontimestamp", DateTime.FromFileTime((long)valueCollection[0]).ToLongDateString());

                    //getData.Add("whencreated", whencreated[0].ToString());
                    //if (lockouttime[0].ToString().Equals("0"))
                    //{
                    //    getData.Add("lockouttime", "Active, Not Locked");
                    //}
                    //else
                    //    getData.Add("lockouttime", "Not Active, Locked");

                    //getData.Add("pwdlastchanged", passwordchanged[0].ToString());
                    getData.Add("principalname", principalname[0].ToString());
                    //getData.Add("emailid", emailid[0].ToString());
                    if (mobile != null)
                    {
                        if (mobile.Count > 0)
                        {
                            getData.Add("mobile", mobile[0].ToString());
                        }
                    }
                    if (mail != null)
                    {
                        if (mail.Count > 0)
                        {
                            getData.Add("mail", mail[0].ToString());
                        }
                    }





                }
            }
            catch (Exception ex)
            {
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "get user info excep" + ex.Message);
                throw new Exception("Error obtaining group names. " + ex.Message);
            }
            return getData;
        }

        protected bool  ResetUserPassword(string usernametxt)
        {
            SSAErrorLog logObj = new SSAErrorLog();
            try
            {
                
                var userDn = "";
                var pwd = TextBox1.Text;
                if (Session["userid"] != null)
                    userDn = Session["userid"].ToString();
                else
                    userDn = Session["forgetpwduser"].ToString();

                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "userDn   >> " + userDn);
                var directoryEntry = GetDirectoryEntryByUserName(userDn);
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "userDn 2  >> " + userDn);
                directoryEntry.Invoke("SetPassword", new object[] { pwd });
                directoryEntry.CommitChanges();
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "userDn  3 >> " + userDn);
                directoryEntry.Properties["LockOutTime"].Value = 0;
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "userDn  4 >> " + userDn);
                directoryEntry.CommitChanges();
                directoryEntry.Close();
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "userDn  5 >> " + userDn);
                //send email here..
                SSAEmail emailObj = new SSAEmail();
                string portnostr  = ConfigurationManager.AppSettings["port"].ToString();
                int portNum = Int32.Parse(portnostr);
                string msg = ConfigurationManager.AppSettings["emailmsg"].ToString();
                string username = usernametxt;
                string emailMsg = string.Format(msg, username);
                emailObj.sendEmail(HiddenField1.Value, "Password Reset Sucessful", emailMsg, ConfigurationManager.AppSettings["emailusername"].ToString(), ConfigurationManager.AppSettings["emailpwd"].ToString(), ConfigurationManager.AppSettings["serverip"].ToString(), portNum, ConfigurationManager.AppSettings["fromemailid"].ToString());


                return true;
            }
            catch (Exception er)
            {
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "Exception in ResetUserPwd  "+er.Message +"Source  "+er.Source +"excep " +er.StackTrace + " inner exceptionnn  "+er.InnerException.Message);
                return false;
            }
        }

        private static string GetDomain()
        {
            //string adDomain =""; //Configuration

            string adDomain = ConfigurationManager.AppSettings["domainname"];

            var domain = new System.Text.StringBuilder();
            string[] dcs = adDomain.Split('.');
            for (var i = 0; i < dcs.GetUpperBound(0) + 1; i++)
            {
                domain.Append("DC=" + dcs[i]);
                if (i < dcs.GetUpperBound(0))
                {
                    domain.Append(",");
                }
            }
            return domain.ToString();
        }
        public static DirectoryEntry GetDirectoryEntryByUserName(string userName)
        {
          //  var de = GetDirectoryObj();// (GetDomain());
            SSAErrorLog logObj = new SSAErrorLog();
            try
            {
                string domain = ConfigurationManager.AppSettings["domain"];
                var de = GetDirectoryObject(domain);
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "GetDirectoryObject1   " +de.Name );


                var search = new DirectorySearcher(de);// { SearchRoot = de, Filter = "(&(objectCategory=user)(cn=" + userName + "))" };

                search.Filter = "(SAMAccountName=" + userName + ")";

                search.PropertiesToLoad.Add("cn");

                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "GetDirectoryObject2   " );

                //SearchResult result = search.FindOne();


                var results = search.FindOne();

                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "GetDirectoryObject3   " + results.Properties.Count );

                return results != null ? results.GetDirectoryEntry() : null;
            }
            catch (Exception er)
            {
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "GetDirectoryObject4   " + er.Message);
                return null;
            }
        }

        private static DirectoryEntry GetDirectoryObject(string domainReference)
        {
            string adminUser = ConfigurationManager.AppSettings["adminuser"];  //WebConfigurationManager.AppSettings["adAdminUser"];
            string adminPassword = ConfigurationManager.AppSettings["adminpwd"];  //WebConfigurationManager.AppSettings["adAdminPassword"];
            string fullPath = "LDAP://" + domainReference;

            var directoryEntry = new DirectoryEntry(fullPath, adminUser, adminPassword);



            return directoryEntry;
        }

        public static DirectoryEntry GetDirectoryObj()
        {

            SSAErrorLog logObj = new SSAErrorLog();
            logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "GetDirectoryObj1");

            string username = ConfigurationManager.AppSettings["adminuser"];  //WebConfigurationManager.AppSettings["adAdminUser"];
            string pwd = ConfigurationManager.AppSettings["adminpwd"];
            string domain = ConfigurationManager.AppSettings["domain"];

            string domainAndUsername = domain + @"\" + username;

            DirectoryEntry entry = new DirectoryEntry("", domainAndUsername, pwd);
            logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "GetDirectoryObj12" + entry);
            DirectoryEntry userentry = null;


            try
            {
                //Bind to the native AdsObject to force authentication.
                object obj = entry.NativeObject;
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "GetDirectoryObj121" + obj);
                DirectorySearcher search = new DirectorySearcher(entry);
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "GetDirectoryObj122 " + search.Filter);
                search.Filter = "(SAMAccountName=" + username + ")";
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "GetDirectoryObj123 " + search.Filter);
                search.PropertiesToLoad.Add("cn");
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "GetDirectoryObj124 " + search.Filter);
                SearchResult result = search.FindOne();
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "GetDirectoryObj125 " + result);
                userentry = result.GetDirectoryEntry();
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "GetDirectoryObj126 " + userentry.Name);
                if (null == result)
                {
                    logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "GetDirectoryObj127 null " );
                    return null;
                }
                else
                {

                    userentry.Close();
                    logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "GetDirectoryObj128 " + userentry);
                    return userentry;
                }


            }
            catch (Exception ex)
            {

                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "GetDirectoryObj  " + ex.Message);
                throw new Exception("Error authenticating user. " + ex.Message);
            }


        }
    }
}