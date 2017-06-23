using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SelfServiceAdminstration.Authentication;
using SelfServiceAdminstration.Databasecomp;
using System.Configuration;
namespace SelfServiceAdminstration
{
    public partial class SelfServiceLoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //  Session.RemoveAll();

            if (ConfigurationManager.AppSettings["captchavalidation"].ToString().Equals("yes"))
                captchadiv.Visible = true;
            else
                captchadiv.Visible = false;
        }

        protected void Login_Click(object sender, EventArgs e)
        {
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
                SSAErrorLog logObj = new SSAErrorLog();
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "Loggedin");

                LdapAuthentication ldapObj = new LdapAuthentication();
                string domainName = ConfigurationManager.AppSettings["domain"];
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

                    DatabaseLayer dataObj = new DatabaseLayer();
                    if (dataObj.getTablerowCount("userquestionanswers", "username='" + userNameTxt.Text + "'"))
                    {
                        Session["update"] = "yes";
                    }
                    else
                    {
                        Session["update"] = "no";
                    }

                    // Server.Transfer("SSAHome.aspx",true);
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
            catch (Exception er)
            {

                //userNameTxt.Text = "err";
                //  System.IO.File.WriteAllText(@"C:\SelfServiceAdminstration\login3.txt", "displayVal " + er.Message + "  StackTrace  " + er.StackTrace);
                Errorlabel.Text = "Authentication Failed !!!";
                //Session.RemoveAll();
            }
        }
    }
}