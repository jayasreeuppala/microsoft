using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Helpers;
//using Microsoft.

namespace SelfServiceAdminstration
{
    public partial class SSAHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //AntiForgery.Validate();
            
            if (Session["username"] != null)
            {
                userLabel.Text = "User: " + Session["username"].ToString();
                string userid = Session["userid"].ToString();
                getUserDetails(userid);
                Panel1.Visible = true;
            }
        }

        protected void getUserDetails(string userid)
        {
            ADUserDetails adObj = new ADUserDetails();
            Hashtable getData = adObj.getuserDetails(userid);
            //DateTime.FromFileTime((long)searchResult.Properties["lastLogon"][0]);
            //displayuser.Text = getData["principalname"].ToString();
            username.Text = getData["principalname"].ToString(); //Session["username"].ToString();

           // lastlogon.Text = getData["lastlogontimestamp"].ToString();
           // pwdstatus.Text = getData["passwordexpired"].ToString();
           // pwdlastchange.Text = getData["pwdlastchanged"].ToString();
            ////passwordexpire.Text = getData["passwordexpires"].ToString();
          //  accountcreated.Text = getData["whencreated"].ToString();
           // activestatus.Text = getData["lockouttime"].ToString();
            if (getData["mobileno"] != null)
            {
                string mobile = getData["mobileno"].ToString();
                //mobile = mobile.Substring(0, mobile.Length - 4) + "XXXX";
                mobile = "XX XX XX"+mobile.Substring(mobile.Length-4) ;
                mobileno.Text = mobile;
            }
            else
            {
                mobileno.Text = "Mobile Number not available/configured, Please Contact Adminstrator ";
                mobileno.ForeColor = System.Drawing.Color.Red; 
            }
            //string mobile = getData["mobileno"].ToString();
            //mobile = mobile.Substring(0, mobile.Length - 4) + "XXXX";
            //mobileno.Text = mobile;
            //HiddenField1.Value = getData["emailid"].ToString();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (Session["username"] != null)
            {
                Response.Redirect("RestPasswordQA.aspx");
            }
            else
            {
                Response.Redirect("PasswordAuth.aspx");
                
            }
        }

        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("SelfServiceLogin.aspx");
        }
    }
}