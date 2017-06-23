using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Configuration;
using System.DirectoryServices;
using System.Text;
using SelfServiceAdminstration.Databasecomp;
using SelfServiceAdminstration.Authentication;

namespace SelfServiceAdminstration
{
    public class ADUserDetails
    {

        private static DirectoryEntry GetDirectoryObject(string domainReference)
        {
            string adminUser = ConfigurationManager.AppSettings["adminuser"];  //WebConfigurationManager.AppSettings["adAdminUser"];
            string adminPassword = ConfigurationManager.AppSettings["adminpwd"];  //WebConfigurationManager.AppSettings["adAdminPassword"];
            string fullPath = "LDAP://" + domainReference;

            var directoryEntry = new DirectoryEntry(fullPath, adminUser, adminPassword);



            return directoryEntry;
        }


        public static DirectoryEntry GetDirectoryEntryByUserName(string userName)
        {
            //  var de = GetDirectoryObj();// (GetDomain());
            SSAErrorLog logObj = new SSAErrorLog();
            try
            {
                string domain = ConfigurationManager.AppSettings["domain"];
                var de = GetDirectoryObject(domain);
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "GetDirectoryObject1   " + de.Name);


                var search = new DirectorySearcher(de);// { SearchRoot = de, Filter = "(&(objectCategory=user)(cn=" + userName + "))" };

                search.Filter = "(SAMAccountName=" + userName + ")";

                search.PropertiesToLoad.Add("cn");

                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "GetDirectoryObject2   ");

                //SearchResult result = search.FindOne();


                var results = search.FindOne();

                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "GetDirectoryObject3   " + results.Properties.Count);

                return results != null ? results.GetDirectoryEntry() : null;
            }
            catch (Exception er)
            {
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "GetDirectoryObject4   " + er.Message);
                return null;
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
                    logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "searching propss...1 ");
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
                    logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "searching propss...2 ");
                    ResultPropertyValueCollection mobileno =
                           searchResult.Properties["mobile"];

                    logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "searching propss...3 " + "mobileno obj " + mobileno);
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
                   // getData.Add("emailid", emailid[0].ToString());
                   // logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "mobileno count  ??   " + mobileno.Count);
                    if (mobileno != null)

                    {
                        if (mobileno.Count > 0)
                        {
                           // logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "mobileno count  ??   " + mobileno.Count);

                            //logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "mobileno[0].ToString()  " + mobileno[0].ToString());
                            getData.Add("mobileno", mobileno[0].ToString());
                        }
                    }
                    else
                    {
                        logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "no mobileno  ()  " );
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

      


        public string getuserMobileNo(string userid)
        {
            Hashtable getData = null;
            string mobileno = null;
            SSAErrorLog logObj = new SSAErrorLog();
            try
            {

               

                string domainName = ConfigurationManager.AppSettings["domain"];
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "getAccountDetailsuserid 1" + userid);
                getData = IsAuthenticateduserinfo(domainName, userid, "");
                //DateTime.FromFileTime((long)searchResult.Properties["lastLogon"][0]);
                //displayuser.Text = getData["principalname"].ToString();
                //username.Text = getData["principalname"].ToString(); //Session["username"].ToString();

                //lastlogon.Text = getData["lastlogontimestamp"].ToString();
                //pwdstatus.Text = getData["passwordexpired"].ToString();
                //pwdlastchange.Text = getData["pwdlastchanged"].ToString();
                ////passwordexpire.Text = getData["passwordexpires"].ToString();
                //accountcreated.Text = getData["whencreated"].ToString();
                //activestatus.Text = getData["lockouttime"].ToString();
                //HiddenField1.Value = getData["emailid"].ToString();
               if(getData["mobileno"] !=null)
               mobileno = getData["mobileno"].ToString();
            }
            catch (Exception er)
            {

                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "getAccountDetails" + er.Message);
            }

            return mobileno;
        }

        public Hashtable getuserDetails(string userid)
        {
            Hashtable getData = null;
            string mobileno = null;
            SSAErrorLog logObj = new SSAErrorLog();
            try
            {



                string domainName = ConfigurationManager.AppSettings["domain"];
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "getAccountDetailsuserid 1" + userid);
                getData = IsAuthenticateduserinfo(domainName, userid, "");
                //DateTime.FromFileTime((long)searchResult.Properties["lastLogon"][0]);
                //displayuser.Text = getData["principalname"].ToString();
                //username.Text = getData["principalname"].ToString(); //Session["username"].ToString();

                //lastlogon.Text = getData["lastlogontimestamp"].ToString();
                //pwdstatus.Text = getData["passwordexpired"].ToString();
                //pwdlastchange.Text = getData["pwdlastchanged"].ToString();
                ////passwordexpire.Text = getData["passwordexpires"].ToString();
                //accountcreated.Text = getData["whencreated"].ToString();
                //activestatus.Text = getData["lockouttime"].ToString();
                //HiddenField1.Value = getData["emailid"].ToString();

                mobileno = getData["mobileno"].ToString();
            }
            catch (Exception er)
            {

                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "getAccountDetails" + er.Message);
            }

            return getData;
        }


        private static Random random = new Random((int)DateTime.Now.Ticks);
        private string RandomString(int size)
        {

            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            return r;
            //StringBuilder builder = new StringBuilder();
            //char ch;
            //for (int i = 0; i < size; i++)
            //{
            //    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            //    builder.Append(ch);
            //}

            //return builder.ToString();
        }

        public bool sendSMSDetails(string username)
        {

            try
            {
                //generate random string
                string otpstr = RandomString(6);
                SMSRequest smsObj = new SMSRequest();
                DatabaseLayer dbObj = new DatabaseLayer();
               string mobileno = getuserMobileNo(username);
               SSAErrorLog logObj = new SSAErrorLog();
               
               if (mobileno != null)
               {
                   string query = "delete from userotp where username='" + QASecurity.Encryptdata(username) + "'";
                   logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "query delete " + query);
                   dbObj.deleteTableData(query);
                  
                   string str = String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
                   dbObj.insertTableData("insert into userotp (username,otp,otpcreatedatetime,otpactivate) values ('" + QASecurity.Encryptdata(username) + "','" + otpstr + "','" + String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now) + "' , 0) ");
                   //insert into db
                   //send SMS
                   smsObj.sendSMS(mobileno, ConfigurationManager.AppSettings["otpmessage"].ToString() + " " + otpstr);
                   return true;
               }
               else
               {
                   return false;
               }
               

            }
            catch (Exception er)
            {
                return false;
            }
        }

    }
}