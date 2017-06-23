using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using SelfServiceAdminstration.Databasecomp;
using System.Collections;

namespace SelfServiceAdminstration
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {


        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;


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

            //check here whether user session object is valid or not
            //   if(!IsPostBack)
            // {
            DatabaseLayer dataObj = new DatabaseLayer();
                string userid = "";
                if (Session["username"] != null || Session["forgetpwduser"] != null)
                {
                    //  Session["__AntiXsrfToken"]

                    if (Session["__AntiXsrfToken"] != null && Session["nocheck"] != null && !Session["nocheck"].ToString().Equals("yes"))
                    {
                        string sessionObj = Session["__AntiXsrfToken"].ToString();
                        if (Session["forgetpwduser"] != null)
                            userid = Session["forgetpwduser"].ToString();
                        if (Session["userid"] != null)
                            userid = Session["userid"].ToString();
                        ArrayList userArray = new ArrayList();
                        userArray.Add("userid");
                        userArray.Add("sessionobj");

                        ArrayList userObj = dataObj.getTableDataQuery("userid,sessionobj from usersession  ", "userid='" + userid + "'", "idusersession", userArray);
                        if (userObj != null)
                        {
                            string dbSession = userObj[1].ToString();
                            if (!dbSession.Equals(Session["__AntiXsrfToken"].ToString()))
                            {
                                Response.Redirect("SSAErrorPage.aspx");
                            }
                        }

                        //if (!dataObj.getTablerowCount("usersession", "userid='" + txtloginid.Text + "'"))
                    }
                }
               
          //  }
           
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            //if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            if (Session[AntiXsrfTokenKey] != null)
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
                /*
                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);*/
            }

            Page.PreLoad += master_Page_PreLoad;
            //Page.PreInit += master_Page_PreLoad;
            // master_Page_PreLoad();
        }

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
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }
        }//ss
      

    
}
