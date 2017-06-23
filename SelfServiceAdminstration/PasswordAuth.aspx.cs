using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Collections;
using SelfServiceAdminstration.Databasecomp;
using SelfServiceAdminstration.Authentication;
using System.Configuration;

namespace SelfServiceAdminstration
{
    public partial class PasswordAuth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if(Session["username"] !=null)
            //userLabel.Text = "User: "+Session["username"].ToString();

            if (ConfigurationManager.AppSettings["captchavalidation"].ToString().Equals("yes"))
                captchadiv.Visible = true;
            else
                captchadiv.Visible = false;

            //Session["nocheck"] = "no";
        }

        protected void validate_Click(object sender, EventArgs e)
        {
            


        }
        protected void validateCaptch()
        {
            //if (Session["CaptchaCode"] != null && CaptchaControl1.Text == Session["CaptchaCode"].ToString() )
            //{
            //    lblMessage.ForeColor = Color.Green;
            //    lblMessage.Text = "Captcha code validated successfully!!";
            //    return true;
            //}
            //else
            //{
            //    lblMessage.ForeColor = Color.Red;
            //    lblMessage.Text = "Captcha code/Login ID is wrong!!";
            //    return false;
            //}
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DatabaseLayer dataObj = new DatabaseLayer();
            SSAErrorLog logObj = new SSAErrorLog();
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

            


            if (txtloginid.Text == "")
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Please enter valid Login ID!!";
            }
            else
            {
                //here is what we need to check whether user logged in or not
                string str = String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
                if (!dataObj.getTablerowCount("usersession", "userid='" + txtloginid.Text + "'"))
                {

                    ArrayList userArray1 = new ArrayList();
                    userArray1.Add("userid");
                    userArray1.Add("sessionobj");
                    userArray1.Add("createddate");
                    userArray1.Add("logincounter");



                    ArrayList userArray = dataObj.getTableDataQuery("userid,sessionobj,createddate,logincounter from usersession", "userid='" + txtloginid.Text.ToLower() + "'", "idusersession", userArray1);
                    int counter = (int)Convert.ToInt64(userArray[3].ToString());
                    DateTime createDate = Convert.ToDateTime(userArray[2].ToString());
                    DateTime currentDate = DateTime.Now;
                    int configCounter = (int)Convert.ToInt64(ConfigurationManager.AppSettings["nooftries"].ToString());
                    int sessionLock = (int)Convert.ToInt64(ConfigurationManager.AppSettings["sessionlock"].ToString());
                    string err = ConfigurationManager.AppSettings["sessionlockmsg"].ToString();

                    if (((currentDate - createDate).Minutes <= sessionLock) && (counter >= configCounter))
                    {
                        int diffDate = (currentDate - createDate).Minutes;
                        int remainingTime = sessionLock - diffDate;
                        string errorMsg = string.Format(err, remainingTime);
                        Errorlbl.Text = errorMsg;//"Please try after some time, User is locked due to no of tries are exceeded..";
                                                 //Response.Redirect("SSAHome.aspx");
                                                 // Session.RemoveAll();
                        return;
                    }

                    logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), " Now rows.. insert ...  ");
                    dataObj.insertTableData("insert into usersession (userid,sessionobj,createddate,logincounter) values('" + txtloginid.Text + "','" + Session["__AntiXsrfToken"] + "','" + str + "',0)");
                    if (getUserQAs(txtloginid.Text.ToLower()))
                    {
                        Session["forgetpwduser"] = txtloginid.Text.ToLower();
                        Response.Redirect("RestPasswordQA.aspx");
                    }
                      
                    else
                    {
                        Errorlbl.Text = "You have not registered on the portal. Please contact local MSOLVE team to get your password reset." + "<br />" + " Please register yourself for self service portal use";
                        Errorlbl.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), " are we hereee???...  " );
                    Session["forgetpwduser"] = txtloginid.Text.ToLower();
                    logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), " oneee loginid???...  " + txtloginid.Text.ToLower());


                   
                    ArrayList userArray1 = new ArrayList();
                    userArray1.Add("userid");
                    userArray1.Add("sessionobj");
                    userArray1.Add("createddate");
                    userArray1.Add("logincounter");

                   

                    ArrayList userArray = dataObj.getTableDataQuery("userid,sessionobj,createddate,logincounter from usersession", "userid='" + txtloginid.Text.ToLower() + "'", "idusersession", userArray1);
                    int counter = (int)Convert.ToInt64(userArray[3].ToString());
                    DateTime createDate = Convert.ToDateTime(userArray[2].ToString());
                    DateTime currentDate = DateTime.Now;
                    int configCounter = (int)Convert.ToInt64(ConfigurationManager.AppSettings["nooftries"].ToString());
                    int sessionLock = (int)Convert.ToInt64(ConfigurationManager.AppSettings["sessionlock"].ToString());
                    string err = ConfigurationManager.AppSettings["sessionlockmsg"].ToString();

                    if (((currentDate - createDate).Minutes <= sessionLock) && (counter >= configCounter))
                    {
                        int diffDate = (currentDate - createDate).Minutes;
                        int remainingTime = sessionLock - diffDate;
                        string errorMsg = string.Format(err, remainingTime);
                        Errorlbl.Text = errorMsg;//"Please try after some time, User is locked due to no of tries are exceeded..";
                                                   //Response.Redirect("SSAHome.aspx");
                                                   // Session.RemoveAll();
                        return;
                    }
                    else
                    {
                        mp1.Show();
                        return;
                    }
                    //mp1.Show();
                    //return;
                }
                    
             

            }
        }

        public Boolean getUserQAs(string userid)
        {
            //string userid = null;
            try
            {
                //if (Session["userid"] != null)
                //{
                //    userid = Session["userid"].ToString();
                //    displayuser.Text = "User: " + Session["username"].ToString();
                //    ;

                //}
                //else if (Session["forgetpwduser"] != null)
                //{
                //    userid = Session["forgetpwduser"].ToString();
                //}

                // userid = "ss0087061";
                userid = QASecurity.Encryptdata(userid);
                DatabaseLayer dbObj = new DatabaseLayer();
                string q1 = " userquestionanswers.id as id,userquestionanswers.question1 as questionid,ssaquestions.question as question,userquestionanswers.answer1 as answer from userquestionanswers,ssaquestions where userquestionanswers.question1 = ssaquestions.qid and username='" + userid + "' and ssaquestions.qid=18";
                string q2 = " userquestionanswers.id as id,userquestionanswers.question2 as questionid,ssaquestions.question as question,userquestionanswers.answer2 as answer from userquestionanswers,ssaquestions where userquestionanswers.question2 = ssaquestions.qid and username='" + userid + "' and ssaquestions.qid=19";
                //string q3 = "select userquestionanswers.id,userquestionanswers.question3,ssaquestions.question,ssaquestions.question from userquestionanswers,ssaquestions where userquestionanswers.question3 = ssaquestions.qid  and username='" + userid + "'";
                string q3 = " userquestionanswers.id as id,userquestionanswers.question3 as questionid,ssaquestions.question as question,userquestionanswers.answer3 as answer from userquestionanswers,ssaquestions where userquestionanswers.question3 = ssaquestions.qid and username='" + userid + "'";
                //string q3 = " userquestionanswers.id,userquestionanswers.question3,ssaquestions.question,ssaquestions.question from userquestionanswers,ssaquestions where userquestionanswers.question3 = ssaquestions.qid  and username='ss0087061'";
                string q4 = " userquestionanswers.id as id,userquestionanswers.question4 as questionid,ssaquestions.question as question,userquestionanswers.answer4 as answer from userquestionanswers,ssaquestions where userquestionanswers.question4 = ssaquestions.qid and username='" + userid + "'";
                //string q5 = " userquestionanswers.question5 as question5, " +
                //    "userquestionanswers.answer5 as answer5, " +
                //    " userquestionanswers.question6 as question6,userquestionanswers.answer6 as answer6 from userquestionanswers  "+
                //    "where username ='"+userid+"'";

                string q5 = "userquestionanswers.id as id, userquestionanswers.question5 as question5, " +
                    "userquestionanswers.answer5 as answer5, 'ANSWER5' as ANSWER5  from userquestionanswers  " +
                    "where username ='" + userid + "'";

                string q6 = "userquestionanswers.id as id, userquestionanswers.question6 as question6, " +
                    "userquestionanswers.answer6 as answer6, 'ANSWER6' as ANSWER6  from userquestionanswers  " +
                    "where username ='" + userid + "'";

                //string q6 = " userquestionanswers.id as id,userquestionanswers.question6 as question6,ssaquestions.question as question,userquestionanswers.answer6 as answer6 from userquestionanswers,ssaquestions where userquestionanswers.question6 = ssaquestions.qid and username='" + userid + "'";
                //  ArrayList q1hash = dbObj.getTableDataQuery(q1, null, "id");


                ArrayList colNames = new ArrayList();
                colNames.Add("id");
                colNames.Add("questionid");
                colNames.Add("question");
                colNames.Add("answer");


               

                
                ArrayList q3hash = dbObj.getTableDataQuery(q3, null, "id", colNames);

                if (q3hash.Count == 0)
                {
                    
                    return false;

                }
                else
                {
                    return true;
                }
               

                





            }
            catch (Exception er)
            {
                return false;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
        
        }

        protected void Button2_Click1(object sender, EventArgs e)
        {

        }

        protected void continue_Click(object sender, EventArgs e)
        {
            mp1.Hide();
            SSAErrorLog logObj = new SSAErrorLog();
            logObj.ErrorLog(ConfigurationManager.AppSettings["logfilepath"].ToString(), " oneee loginid.session..  " + Session["forgetpwduser"]);
            DatabaseLayer dataObj = new DatabaseLayer();
            
            if (getUserQAs(Session["forgetpwduser"].ToString().ToLower()))
            {
                string updateStr = "update usersession set sessionobj='" + Session["__AntiXsrfToken"] + "' where userid='" + Session["forgetpwduser"].ToString() + "'";
                dataObj.insertTableData(updateStr);
                Session["nocheck"] = "yes";
                Response.Redirect("RestPasswordQA.aspx");
            }
            else
            {
                Session["nocheck"] = null;
                Errorlbl.Text = "You have not registered on the portal. Please contact local MSOLVE team to get your password reset." + "<br />" + " Please register yourself for self service portal use";
                Errorlbl.ForeColor = Color.Red;
                return;
            }
        }
    }
}