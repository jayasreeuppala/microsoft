using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Collections;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Configuration;



namespace SelfServiceAdminstration.Authentication
{
    public class LdapAuthentication
    {
        private string _path;
        private string _filterAttribute;
        public LdapAuthentication()
        {
            
        }
        public LdapAuthentication(string path)
        {
            _path = path;
        }

        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            string domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);

            try
            {
                //Bind to the native AdsObject to force authentication.
                object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                if (null == result)
                {
                    return false;
                }

                //Update the new path to the user in the directory.
                _path = result.Path;
                _filterAttribute = (string)result.Properties["cn"][0];
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. " + ex.Message);
            }

            return true;
        }


        public string IsAuthenticatedStr(string domain, string username, string pwd)
        {
            string domainAndUsername = domain + @"\" + username;
            
            string displayName = null;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);
            //System.IO.File.WriteAllText(@"C:\SelfServiceAdminstration\log2.txt", "domain " + domain + "username " + username);

            Hashtable getDataHash = null;
            SSAErrorLog logObj = new SSAErrorLog();

            try
            {
                //Bind to the native AdsObject to force authentication.
                object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "locked ?? 2  >> " + entry.Properties["LockOutTime"].Value); 
               // entry.Properties["LockOutTime"].Value = 0;
               // entry.CommitChanges();
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "locked ?? 3  >> " ); 
                search.Filter = "(SAMAccountName=" + username + ")";
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "locked ?? 4  >> "); 
                search.PropertiesToLoad.Add("cn");
               
                SearchResult result = search.FindOne();
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "locked ?? 5  >> " +result); 
              //  System.IO.File.WriteAllText(@"C:\SelfServiceAdminstration\log6.txt", "result.Properties.Count " + result.Properties.Count);
                
                DirectoryEntry userentry = result.GetDirectoryEntry();
                if (null == result)
                {
                    return null;
                }

                //Update the new path to the user in the directory.
                _path = result.Path;
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "locked ?? 6  >> " +_path); 
                //System.IO.File.WriteAllText(@"C:\SelfServiceAdminstration\log3.txt", "result.Path " + result.Path);
                displayName = (string)result.Properties["cn"][0];
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "locked ?? 7  >> " + displayName); 


                //System.IO.File.WriteAllText(@"C:\SelfServiceAdminstration\log4.txt", "result.displayName " + displayName);

              // getDataHash = GetUserInfo(displayName, result.Path);
               // userentry.Invoke("SetPassword", new object[] { "ooty@4567" });
               // userentry.Properties["LockOutTime"].Value = 0;

                userentry.Close();
            }
            catch (Exception ex)
            {
                //System.IO.File.WriteAllText(@"C:\SelfServiceAdminstration\log5.txt", ex.Message + " stack  "+ ex.StackTrace );
                throw new Exception("Error authenticating user. " + ex.Message);
            }

            return displayName;
        }

        public Hashtable IsAuthenticateduserinfo(string domain, string username, string pwd)
        {
            string domainAndUsername = domain + @"\" + username;
            string displayName = null;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);

            Hashtable getDataHash = null;

            try
            {
                //Bind to the native AdsObject to force authentication.
                object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("samaccountname");

                SearchResult result = search.FindOne();
                DirectoryEntry userentry = result.GetDirectoryEntry();
                if (null == result)
                {
                    return null;
                }

                //Update the new path to the user in the directory.
                _path = result.Path;
                displayName = (string)result.Properties["cn"][0];

                getDataHash = GetUserInfo(displayName, result.Path);
                // userentry.Invoke("SetPassword", new object[] { "ooty@4567" });
                // userentry.Properties["LockOutTime"].Value = 0;

                userentry.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. " + ex.Message);
            }

            return getDataHash;
        }

        public Hashtable getADUserInfo(string domain, string adminusername, string pwd,string username)
        {
            string domainAndUsername = domain + @"\" + adminusername;
            string displayName = null;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);

            Hashtable getDataHash = null;

            try
            {
                //Bind to the native AdsObject to force authentication.
                object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();
                DirectoryEntry userentry = result.GetDirectoryEntry();
                if (null == result)
                {
                    return null;
                }

                //Update the new path to the user in the directory.
                _path = result.Path;
                displayName = (string)result.Properties["cn"][0];

                getDataHash = GetUserInfo(displayName, result.Path);
                // userentry.Invoke("SetPassword", new object[] { "ooty@4567" });
                // userentry.Properties["LockOutTime"].Value = 0;

                userentry.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. " + ex.Message);
            }

            return getDataHash;
        }


        public string GetGroups()
        {
            DirectorySearcher search = new DirectorySearcher(_path);
            search.Filter = "(cn=" + _filterAttribute + ")";
            
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupNames = new StringBuilder();

            try
            {
                SearchResult result = search.FindOne();
                int propertyCount = result.Properties["memberOf"].Count;
                string dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    dn = (string)result.Properties["memberOf"][propertyCounter];
                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }
                    groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                    groupNames.Append("|");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error obtaining group names. " + ex.Message);
            }
            return groupNames.ToString();
        }

        public Hashtable GetUserInfo(string userName,string path)
        {
            DirectorySearcher search = new DirectorySearcher(path);

            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
            //UserPrincipal qbeUser = new UserPrincipal(ctx);
            //qbeUser.SamAccountName = userName;
            //PrincipalSearcher srch = new PrincipalSearcher(qbeUser);

            //foreach (var found in srch.FindAll())
            //{
            //    // do whatever here - "found" is of type "Principal" - 
            //    // it could be user, group, computer.....          
                
            //}


            search.Filter = "(&(objectClass=user)(cn="+userName+"))";
           // search.Filter = "(&(objectClass=user)(samaccountname=" + userName + "))";

            //search.Filter = "(cn=" + _filterAttribute + ")";
            SearchResultCollection sResults = null;
            string colStr = "";
            Hashtable getData = null;

            try
            {
                getData = new Hashtable();
                sResults = search.FindAll();

            //loop through results of search
            foreach (SearchResult searchResult in sResults)
            {
                int propCount = searchResult.Properties.Count;
                ICollection coll =  searchResult.Properties.PropertyNames;

                ResultPropertyValueCollection valueCollection =
                        searchResult.Properties["lastlogontimestamp"];
                ResultPropertyValueCollection passwordExpired =
                        searchResult.Properties["userAccountControl"];

                ResultPropertyValueCollection passwordchanged =
                        searchResult.Properties["whenchanged"];

                ResultPropertyValueCollection passwordexpires =
                        searchResult.Properties["accountexpires"];

                ResultPropertyValueCollection whencreated =
                        searchResult.Properties["whencreated"];

                ResultPropertyValueCollection lockouttime =
                        searchResult.Properties["lockouttime"];

                //int m_Val1 = (int)searchResult.Properties[""]..Properties["userAccountControl"]..Value;

                int m_Val1 = Int32.Parse(passwordExpired[0].ToString());
                int m_Val2 = (int)0x10000;
                bool m_Check = false;
                if (Convert.ToBoolean(m_Val1 & m_Val2))
                {
                    m_Check = true;
                } //end
                if(m_Check)
                    getData.Add("passwordexpired", "Expired");
                else
                    getData.Add("passwordexpired", "Not Expired");
                
                getData.Add("lastlogontimestamp", DateTime.FromFileTime((long)valueCollection[0]).ToLongDateString());

                getData.Add("whencreated", whencreated[0].ToString());
                if (lockouttime[0].ToString().Equals("0"))
                {
                    getData.Add("lockouttime", "Active, Not Locked");
                }
                else
                    getData.Add("lockouttime", "Not Active, Locked");

                getData.Add("pwdlastchanged", passwordchanged[0].ToString());
                
               
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error obtaining group names. " + ex.Message);
            }
            return getData;
        }

        public Hashtable GetUserInfoOld(string userName, string path)
        {
            DirectorySearcher search = new DirectorySearcher(path);
            //search.Filter = "(&(objectClass=user)(cn="+userName+"))";
            search.Filter = "(&(objectClass=user)(samaccountname=" + userName + "))";

            //search.Filter = "(cn=" + _filterAttribute + ")";
            SearchResultCollection sResults = null;
            string colStr = "";
            Hashtable getData = null;

            try
            {
                getData = new Hashtable();
                sResults = search.FindAll();

                //loop through results of search
                foreach (SearchResult searchResult in sResults)
                {
                    int propCount = searchResult.Properties.Count;
                    ICollection coll = searchResult.Properties.PropertyNames;

                    ResultPropertyValueCollection valueCollection =
                            searchResult.Properties["lastlogontimestamp"];
                    ResultPropertyValueCollection passwordExpired =
                            searchResult.Properties["userAccountControl"];

                    ResultPropertyValueCollection passwordchanged =
                            searchResult.Properties["whenchanged"];

                    ResultPropertyValueCollection passwordexpires =
                            searchResult.Properties["accountexpires"];

                    ResultPropertyValueCollection whencreated =
                            searchResult.Properties["whencreated"];

                    ResultPropertyValueCollection lockouttime =
                            searchResult.Properties["lockouttime"];

                    //int m_Val1 = (int)searchResult.Properties[""]..Properties["userAccountControl"]..Value;

                    int m_Val1 = Int32.Parse(passwordExpired[0].ToString());
                    int m_Val2 = (int)0x10000;
                    bool m_Check = false;
                    if (Convert.ToBoolean(m_Val1 & m_Val2))
                    {
                        m_Check = true;
                    } //end
                    if (m_Check)
                        getData.Add("passwordexpired", "Expired");
                    else
                        getData.Add("passwordexpired", "Not Expired");

                    getData.Add("lastlogontimestamp", DateTime.FromFileTime((long)valueCollection[0]).ToLongDateString());

                    getData.Add("whencreated", whencreated[0].ToString());
                    if (lockouttime[0].ToString().Equals("0"))
                    {
                        getData.Add("lockouttime", "Active, Not Locked");
                    }
                    else
                        getData.Add("lockouttime", "Not Active, Locked");

                    getData.Add("pwdlastchanged", passwordchanged[0].ToString());



                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error obtaining group names. " + ex.Message);
            }
            return getData;
        }

    }
}