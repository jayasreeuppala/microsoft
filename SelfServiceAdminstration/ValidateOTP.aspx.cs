using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SelfServiceAdminstration.Databasecomp;
using SelfServiceAdminstration.Authentication;
using System.Collections;
using System.Configuration;

namespace SelfServiceAdminstration
{
    public partial class ValidateOTP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            string userid = "";
            if (ConfigurationManager.AppSettings["captchavalidation"].ToString().Equals("yes"))
                captchadiv.Visible = true;
            else
                captchadiv.Visible = false;


            if (Session["userid"] != null)
            {
                userid = Session["userid"].ToString();

            }
            else if (Session["forgetpwduser"] != null)
            {
                userid = Session["forgetpwduser"].ToString();
            }
            else
            {
                
                    Response.Redirect("SelfServiceLogin.aspx");
            }
            ADUserDetails adObj = new ADUserDetails();
            string mobileno = adObj.getuserMobileNo(userid);
            if (mobileno != null)
            {
                //string mobile = getData["mobileno"].ToString();
                ////mobile = mobile.Substring(0, mobile.Length - 4) + "XXXX";
                //mobile = "XX XX XX XX" + mobile.Substring(mobile.Length - 4);
                //mobileno.Text = mobile;
                mobileno = "XX XX XX" + mobileno.Substring(mobileno.Length - 4);
                Label2.Text = mobileno;//mobileno.Substring(0, mobileno.Length - 4) + "xxxx";
            }
                
        }

        protected void Button1_Click(object sender, EventArgs e)
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

            if (validateOTP())
            {
                Session["userotp"] = otpval.Text;
                Response.Redirect("wer.aspx");
            }
            else
            {
                Label1.Text = " OTP entered doesn't match, Please enter valid OTP, If you haven't received SMS contact Adminstrator";
                return;
            }

        }
        protected bool deactivateOTP()
        {
            bool returnval = false;
                try
                {

                }
            catch(Exception er)
                {
                    return returnval = false;
            }
                return returnval;
        }
        protected bool validateOTP()
        {
            SSAErrorLog logObj = new SSAErrorLog();

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
                ArrayList resulthash = dataObj.getTableDataQuery("iduserotp,username,otp,otpcreatedatetime,otpactivate from userotp where username='"+userid+"'", null, "iduserotp", colNames);
                

                string dbotp = resulthash[2].ToString();
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "User idd " + userid + " << dbotp >>>" + dbotp);
                DateTime otpdateObj = Convert.ToDateTime(resulthash[3].ToString());

                string activate = resulthash[4].ToString();
                DateTime current = DateTime.Now;

                TimeSpan ts = current - otpdateObj;
                int mins = ts.Minutes;
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "difference mins   " +mins);
                string otpvalidation = ConfigurationManager.AppSettings["otpdurationvalidation"].ToString();
                string otpdurationinmins = ConfigurationManager.AppSettings["otpdurationinmins"].ToString();
                int otpduration = Convert.ToInt32(otpdurationinmins);
                logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "otpduration    " + otpduration);
                if (otpvalidation.Equals("yes") )
                {
                    if (mins > otpduration)
                    {

                        return false;
                    }
                }
                if (dbotp.Equals(otpval.Text) && activate.Equals("False"))
                {
                    //Response.Redirect("wer.aspx");
                    logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), "done   ");
                    //here it shoud deactivate the OTP, update the table
                    //dataObj.updateTableData("userotp", updateHash, "username='" + userid + "'");

                    return true;

                }
                else
                {
                    logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), " none  ");
                    return false;
                }

                //dataObj.getTableData("",
            }
            catch (Exception er)
            {
                return false;
            }
            
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string userid = "";
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
                ADUserDetails adObj = new ADUserDetails();
                adObj.sendSMSDetails(userid);
                //getuserMobileNo
                Response.Redirect("ValidateOTP.aspx");
            }
            catch (Exception er)
            {
                Response.Redirect("ValidateOTP.aspx");
            }
            
        }
    }
}