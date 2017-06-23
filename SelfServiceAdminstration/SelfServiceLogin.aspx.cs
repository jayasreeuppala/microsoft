using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SelfServiceAdminstration.Authentication;
using SelfServiceAdminstration.Databasecomp;
using System.Configuration;
using System.Web.Security;
using System.Collections;

namespace SelfServiceAdminstration
{

    

    public partial class SelfServiceLogin : System.Web.UI.Page
    {
        

        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // The code below helps to protect against XSRF attacks
            //var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            //if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            if(Session[AntiXsrfTokenKey] !=null)
            {
                // Use the Anti-XSRF token from the cookie
                // _antiXsrfTokenValue = requestCookie.Value;
                _antiXsrfTokenValue = (string)Session[AntiXsrfTokenKey];
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                //insert this into db...

                /*
                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };*/
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                 //   responseCookie.Secure = true;
                }
               // Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
            //Page.PreInit += master_Page_PreLoad;
           // master_Page_PreLoad();
        }

       

        //protected void master_Page_PreLoad(object sender, EventArgs e)
       protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                Session[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                Session[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
              //  ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                //ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)Session[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)Session[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    Response.Redirect("SSAErrorPage.aspx");
                   // throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            this.Response.Cache.SetExpires(DateTime.UtcNow.AddYears(-4));
            this.Response.Cache.SetValidUntilExpires(false);
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            this.Response.Cache.SetNoStore();
            this.Response.ExpiresAbsolute = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
            this.Response.Expires = 0;
            this.Response.CacheControl = "no-cache";
            this.Response.AppendHeader("Pragma", "no-cache");
            this.Response.Cache.AppendCacheExtension("must-revalidate, proxy-revalidate, post-check=0, pre-check=0");
            //  Session.RemoveAll();


            if (ConfigurationManager.AppSettings["captchavalidation"].ToString().Equals("yes"))
                captchadiv.Visible = true;
            else
                captchadiv.Visible = false;

            //check whether user sesssion obj is available or not
            if(!IsPostBack)
            {
                try
                {
            /*
                    //if userobj is not available then create new
                    if (!dataObj.getTablerowCount("usersession", "userid='" + userNameTxt.Text + "'"))
                    {
                        //Session[AntiXsrfTokenKey] 
                        //userid,sessionobj,createddate

                        //dataObj.insertTableData("insert into usersession names('userid','sessionobj','createddate') values('"+ userNameTxt.Text + "','"+ Session[AntiXsrfTokenKey] + "','"+ DateTime.Now + "'))"                    }

                    }

    */

                }
                catch(Exception er)
                {

                }
            }
        }


        protected void Login_Click(object sender, EventArgs e)
        {

            DatabaseLayer dataObj = new DatabaseLayer();
            SSAErrorLog logObj = new SSAErrorLog();
            try
            {
                if (ConfigurationManager.AppSettings["captchavalidation"].ToString().Equals("yes"))
                {
                    if (txtimgcode.Text == Session["CaptchaImageText"].ToString())
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
                
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "Loggedin");

                LdapAuthentication ldapObj = new LdapAuthentication();
                string domainName = ConfigurationManager.AppSettings["domain"];

                string str = String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);

                if (dataObj.getTablerowCount("usersession", "userid='" + userNameTxt.Text + "'"))
                {

                    ArrayList userArray1 = new ArrayList();
                   
                    userArray1.Add("userid");
                    userArray1.Add("sessionobj");
                    userArray1.Add("createddate");
                    userArray1.Add("logincounter");
                    ArrayList userArray= dataObj.getTableDataQuery("userid,sessionobj,createddate,logincounter from usersession", "userid='" + userNameTxt.Text + "'", "idusersession", userArray1);
                    int counter = (int) Convert.ToInt64( userArray[3].ToString());
                    //DateTime createDate = (DateTime) userArray[2];
                    DateTime createDate = Convert.ToDateTime(userArray[2].ToString());
                    DateTime currentDate = DateTime.Now;
                    int configCounter = (int)Convert.ToInt64(ConfigurationManager.AppSettings["nooftries"].ToString());
                    int sessionLock = (int) Convert.ToInt64( ConfigurationManager.AppSettings["sessionlock"].ToString());


                    string err = ConfigurationManager.AppSettings["sessionlockmsg"].ToString();

                    if (((currentDate - createDate).Minutes <= sessionLock) && (counter >= configCounter))
                    {
                        int diffDate = (currentDate - createDate).Minutes;
                        int remainingTime = sessionLock - diffDate;
                        string errorMsg = string.Format(err, remainingTime);
                        Errorlabel.Text = errorMsg;//"Please try after some time, User is locked due to no of tries are exceeded..";
                                                   //Response.Redirect("SSAHome.aspx");
                                                   // Session.RemoveAll();
                        return;
                    }

                    //Session[AntiXsrfTokenKey] 
                    //userid,sessionobj,createddate

                    string updateStr = "update usersession set sessionobj='" + Session[AntiXsrfTokenKey] + "' ,logincounter=0 where userid='" + userNameTxt.Text + "'";

                    //dataObj.insertTableData("insert into usersession (userid,sessionobj,createddate,logincounter) values('" + userNameTxt.Text + "','" + Session[AntiXsrfTokenKey] + "','" + str + "',0)");
                    dataObj.insertTableData(updateStr);


                    //dataObj.insertTableData("insert into usersession (userid,sessionobj,createddate,logincounter) values('" + userNameTxt.Text + "','" + Session[AntiXsrfTokenKey] + "','" + str + "',0)" )                   ;

                    string displayVal = ldapObj.IsAuthenticatedStr(domainName, userNameTxt.Text, passwordTxt.Text);



                    logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "displayVal  " + displayVal);

                    //System.IO.File.WriteAllText(@"C:\SelfServiceAdminstration\login1.txt", "displayVal " + displayVal);
                    //userNameTxt.Text = "done ";
                    //Session["username"] = userNameTxt.Text;
                    if (displayVal != null)
                    {
                        //System.IO.File.WriteAllText(@"C:\SelfServiceAdminstration\login2.txt", "displayVal " + displayVal);
                        Session["username"] = displayVal;

                        string userid = userNameTxt.Text.ToLower();
                        Session["pwd"] = passwordTxt.Text;
                        Session["userid"] = userid;

                        // DatabaseLayer dataObj = new DatabaseLayer();
                        if (dataObj.getTablerowCount("userquestionanswers", "username='" + userNameTxt.Text + "'"))
                        {
                            Session["update"] = "yes";
                        }
                        else
                        {
                            Session["update"] = "no";
                        }

                        
                        Response.Redirect("SSAHome.aspx", false);

                    }
                    else
                    {
                        //userNameTxt.Text = "err";
                        Errorlabel.Text = "Authentication Failed !!!";
                        //Response.Redirect("SSAHome.aspx");
                        Session.RemoveAll();
                    }


                }
                else
                {
                    Hashtable userHash = new Hashtable();
                    ArrayList userArray1 = new ArrayList();
                    userArray1.Add("userid");
                    userArray1.Add("sessionobj");
                    userArray1.Add("createddate");
                    userArray1.Add("logincounter");

                    //check the session here..
                    //userHash = dataObj.getTableData("usersession", userArray, "idusersession", "userid='" + userNameTxt.Text + "'");
                    //  ArrayList dataValues = dataObj.getTableDataQuery("", "userid='" + userNameTxt.Text + "'", "idusersession", userArray);
                    //get confirmation from request, If confirmed then login and update the session, 
                    //else terminate this request.


                    // dialog.Visible = true;
                    Session["pwd"] = passwordTxt.Text;

                   // ArrayList userArray = dataObj.getTableDataQuery("userid,sessionobj,createddate,logincounter from usersession", "userid='" + userNameTxt.Text + "'", "idusersession", userArray1);
                  //  int counter = (int)Convert.ToInt64(userArray[3].ToString());
                   // DateTime createDate = Convert.ToDateTime(userArray[2].ToString());
                  //  DateTime currentDate = DateTime.Now;
                    int configCounter = (int)Convert.ToInt64(ConfigurationManager.AppSettings["nooftries"].ToString());
                    int sessionLock = (int)Convert.ToInt64(ConfigurationManager.AppSettings["sessionlock"].ToString());
                    string err = ConfigurationManager.AppSettings["sessionlockmsg"].ToString();

                    //LdapAuthentication ldapObj = new LdapAuthentication();
                    //string domainName = ConfigurationManager.AppSettings["domain"];
                    //string displayVal = ldapObj.IsAuthenticatedStr(domainName, userNameTxt.Text, Session["pwd"].ToString());
                    string displayVal = ldapObj.IsAuthenticatedStr(domainName, userNameTxt.Text, passwordTxt.Text);
                    if (displayVal != null)
                    {

                        Session["username"] = displayVal;

                        string userid = userNameTxt.Text.ToLower();
                        Session["pwd"] = passwordTxt.Text;
                        Session["userid"] = userid;

                        // DatabaseLayer dataObj = new DatabaseLayer();
                        if (dataObj.getTablerowCount("userquestionanswers", "username='" + userNameTxt.Text + "'"))
                        {
                            Session["update"] = "yes";
                        }
                        else
                        {
                            Session["update"] = "no";
                        }

                        // Server.Transfer("SSAHome.aspx",true);
                        //update session object..
                        //string updateStr = "update usersession set sessionobj='" + Session[AntiXsrfTokenKey] + "' ,logincounter=0 where userid='" + userNameTxt.Text + "'";

                        dataObj.insertTableData("insert into usersession (userid,sessionobj,createddate,logincounter) values('" + userNameTxt.Text + "','" + Session[AntiXsrfTokenKey] + "','" + str + "',0)");
                        //dataObj.insertTableData(updateStr);
                        Response.Redirect("SSAHome.aspx", false);

                    }
                    else
                    {

                        Errorlabel.Text = "Authentication Failed !!!";
                        Session.RemoveAll();
                    }



                    /*
                    if (((currentDate- createDate).Minutes <= sessionLock) && (counter >= configCounter))
                    {
                        int diffDate = (currentDate - createDate).Minutes;
                        int remainingTime = sessionLock - diffDate;
                        string errorMsg = string.Format(err, remainingTime);
                        Errorlabel.Text = errorMsg;//"Please try after some time, User is locked due to no of tries are exceeded..";
                        //Response.Redirect("SSAHome.aspx");
                       // Session.RemoveAll();
                        return;
                    }
                    else
                    {
                       // mp1.Show();
                        return;
                    }
                    */


                    // userHash[]
                }


                /*

                string displayVal = ldapObj.IsAuthenticatedStr(domainName, userNameTxt.Text, passwordTxt.Text);



                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "displayVal  " + displayVal);

                //System.IO.File.WriteAllText(@"C:\SelfServiceAdminstration\login1.txt", "displayVal " + displayVal);
                //userNameTxt.Text = "done ";
                //Session["username"] = userNameTxt.Text;
               if (displayVal != null)
               {
                   //System.IO.File.WriteAllText(@"C:\SelfServiceAdminstration\login2.txt", "displayVal " + displayVal);
                   Session["username"] = displayVal;
                   
                   string userid = userNameTxt.Text.ToLower();
                   Session["pwd"] = passwordTxt.Text;
                   Session["userid"] = userid;

                  // DatabaseLayer dataObj = new DatabaseLayer();
                   if (dataObj.getTablerowCount("userquestionanswers", "username='" + userNameTxt.Text + "'"))
                   {
                       Session["update"] = "yes";
                   }
                   else
                   {
                       Session["update"] = "no";
                   }

                  // Server.Transfer("SSAHome.aspx",true);
                   Response.Redirect("SSAHome.aspx",false);

               }
               else
               {
                   //userNameTxt.Text = "err";
                   Errorlabel.Text = "Authentication Failed !!!";
                   //Response.Redirect("SSAHome.aspx");
                   Session.RemoveAll();
               }
                */
                
            }
            catch (Exception er)
            {

                //userNameTxt.Text = "err";
                //  System.IO.File.WriteAllText(@"C:\SelfServiceAdminstration\login3.txt", "displayVal " + er.Message + "  StackTrace  " + er.StackTrace);
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "Error While authenticating   " + er.Message);
                Errorlabel.Text = "Authentication Failed !!!";
                //Session.RemoveAll();
            }
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //mp1.Hide();

            try
            {
                SSAErrorLog logObj = new SSAErrorLog();
                LdapAuthentication ldapObj = new LdapAuthentication();
                string domainName = ConfigurationManager.AppSettings["domain"];
                string displayVal = ldapObj.IsAuthenticatedStr(domainName, userNameTxt.Text, Session["pwd"].ToString());
                DatabaseLayer dataObj = new DatabaseLayer();


                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "displayVal  " + displayVal);

                
                if (displayVal != null)
                {
               
                    Session["username"] = displayVal;

                    string userid = userNameTxt.Text.ToLower();
                    Session["pwd"] = passwordTxt.Text;
                    Session["userid"] = userid;

                    // DatabaseLayer dataObj = new DatabaseLayer();
                    if (dataObj.getTablerowCount("userquestionanswers", "username='" + userNameTxt.Text + "'"))
                    {
                        Session["update"] = "yes";
                    }
                    else
                    {
                        Session["update"] = "no";
                    }

                    // Server.Transfer("SSAHome.aspx",true);
                    //update session object..
                    string updateStr = "update usersession set sessionobj='"+ Session[AntiXsrfTokenKey] + "' ,logincounter=0 where userid='"+userNameTxt.Text+"'";
                    dataObj.insertTableData(updateStr);
                    Response.Redirect("SSAHome.aspx", false);

                }
                else
                {
                    
                    Errorlabel.Text = "Authentication Failed !!!";
                    Session.RemoveAll();
                }

            }
            catch(Exception er)
            {
               
                Errorlabel.Text = "Authentication Failed !!!";
            }
        }
    }
}