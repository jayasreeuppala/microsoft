using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SelfServiceAdminstration.Databasecomp;
using System.Collections;
using SelfServiceAdminstration.Authentication;
using System.Configuration;
using System.DirectoryServices;
namespace SelfServiceAdminstration
{
    public partial class RestPasswordQA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
               
                getUserQAs();
                if (Session["answermismatch"] != null)
                {
                    string ansmismatch = Session["answermismatch"].ToString();


                    string userid = "";
                    if (Session["userid"] != null)
                    {
                        userid = Session["userid"].ToString();
                        //  displayuser.Text = "User: "+ Session["username"].ToString();
                    }
                    else if (Session["forgetpwduser"] != null)
                    {
                        userid = Session["forgetpwduser"].ToString();

                    }

                    if (ansmismatch.Equals("true"))
                    {
                        //resultlable.Text = "Atleast 2 answers should match, Please verify again";

                        Session["counter"] = Convert.ToInt64(Session["counter"]) + 1;
                        int i = (int)Convert.ToInt64(Session["counter"]);
                        int configCounter = (int)Convert.ToInt64(ConfigurationManager.AppSettings["nooftries"].ToString());
                        resultlable.Text = "Atleast 2 answers should match, Please verify again \n, You have "+(configCounter-i) +" tries left";
                        string str = String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
                        if (Session["counter"] != null)
                        {

                            string sessionLock = ConfigurationManager.AppSettings["sessionlock"].ToString();
                            if (i >= configCounter)
                            {
                                // qcounter.Text = "Maximum tries ("+ configCounter + ") to verify questions exceeded, Please try after sessionLock mins or Contact Administrator";
                                string updateStr = "update usersession set logincounter=" + configCounter + ",createddate='"+str+"'  where userid='" + userid + "'";
                                DatabaseLayer dataObj = new DatabaseLayer();
                                dataObj.insertTableData(updateStr);
                                Response.Redirect("SSAHome.aspx");
                            }
                        }
                        else
                        {
                            Session["counter"] = 1;
                        }


                    }
                }
                if (Session["nocheck"] != null)
                {
                    if (Session["nocheck"].Equals("yes"))
                    {
                        Session["nocheck"] = "no";
                    }

                }
            }
            else
            {
                if(Session["nocheck"] !=null)
                {
                    if(Session["nocheck"] .Equals("yes"))
                    {
                        Session["nocheck"] = "no";
                    }

                }
                if (Session["answermismatch"] != null)
                {
                    string ansmismatch = Session["answermismatch"].ToString();
                    string userid = "";
                    if (Session["userid"] != null)
                    {
                        userid = Session["userid"].ToString();
                        //  displayuser.Text = "User: "+ Session["username"].ToString();
                    }
                    else if (Session["forgetpwduser"] != null)
                    {
                        userid = Session["forgetpwduser"].ToString();

                    }



                    if (ansmismatch.Equals("true"))
                    {
                        /*
                        Session["counter"] = Convert.ToInt64(Session["counter"]) + 1;

                        int i = (int)Convert.ToInt64(Session["counter"]);
                        int configCounter = (int)Convert.ToInt64(ConfigurationManager.AppSettings["nooftries"].ToString());
                    //    resultlable.Text = "Atleast 2 answers should match, Please verify again \n, You have "+(configCounter-i) +" tries left";
                        if(Session["counter"] !=null)
                        {
                            
                            string sessionLock = ConfigurationManager.AppSettings["sessionlock"].ToString();
                            if (i> configCounter)
                            {
                               // qcounter.Text = "Maximum tries ("+ configCounter + ") to verify questions exceeded, Please try after sessionLock mins or Contact Administrator";
                                string updateStr = "update usersession set logincounter="+ configCounter + " where userid='" + userid + "'";
                                DatabaseLayer dataObj = new DatabaseLayer();
                                dataObj.insertTableData(updateStr);
                                Response.Redirect("SSAHome.aspx");
                            }
                        }
                        else
                        {
                            Session["counter"] = 1;
                        }
                        */
                    }
                }
                
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Atleast 2 answers should match, Please verify again')", true);
                //this.Page.EnableViewState = true;
                //Session.Add("answermismatch", "true");
            }

        }

        protected void save_Click(object sender, EventArgs e)
        {
            try
            {
                validateUserQAs();
            }
            catch (Exception er)
            {

            }
        }

        protected void validateUserQAs()
        {
            string userid = null;
            try
            {
                if (Session["userid"] != null)
                {
                    userid = Session["userid"].ToString();
                  //  displayuser.Text = "User: "+ Session["username"].ToString();
                }
                else if (Session["forgetpwduser"] != null)
                {
                    userid = Session["forgetpwduser"].ToString();
                    
                }
                displayuser.Text = "User: " + userid;
                int j = 0;

                string str = "";
                String str2 = "";

                //if (QASecurity.Decryptdata(Label1.Attributes["answer1"].ToString()).Equals(answer1.Text, StringComparison.InvariantCultureIgnoreCase))
                //    {
                //        j++;
                //    }
                
                //    if (QASecurity.Decryptdata(Label2.Attributes["answer1"].ToString()).Equals(answer2.Text,StringComparison.InvariantCultureIgnoreCase))
                //    {
                //        j++;
                //    }

                //    if (QASecurity.Decryptdata(Label3.Attributes["answer1"].ToString()).Equals(answer3.Text, StringComparison.InvariantCultureIgnoreCase))
                //    {
                //        j++;
                //    }

                    if (QASecurity.Decryptdata(Session["answer1"].ToString()).Equals(answer1.Text, StringComparison.InvariantCultureIgnoreCase))
                    {
                        j++;
                    }

                    if (QASecurity.Decryptdata(Session["answer2"].ToString()).Equals(answer2.Text, StringComparison.InvariantCultureIgnoreCase))
                    {
                        j++;
                    }

                    if (QASecurity.Decryptdata(Session["answer3"].ToString()).Equals(answer3.Text, StringComparison.InvariantCultureIgnoreCase))
                    {
                        j++;
                    }


                

                if (j >= 2)
                {
                    //this is where SMS need to be send

                    ADUserDetails adObj = new ADUserDetails();
                    if (adObj.sendSMSDetails(userid))
                    {
                        Response.Redirect("ValidateOTP.aspx");
                    }
                    else
                    {
                        resultlable.Text = "Mobile number not available/configured, Please contact Administrator";
                        resultlable.ForeColor = System.Drawing.Color.Red;
                    }
                    //getuserMobileNo
                    
                }
                else
                {
                   // resultlable.Text = "Atleast 2 answers should match, Please verify again";
                   // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Atleast 2 answers should match, Please verify again')", true);
                    //this.Page.EnableViewState = true;
                    Session.Add("answermismatch", "true");
                    Response.Redirect("RestPasswordQA.aspx",false);
                    //return;
                }
                /*
                if (answer1.Text.Equals(q1hash[3].ToString()) && answer2.Text.Equals(q2hash[3].ToString()) && answer3.Text.Equals(q3hash[3].ToString()) && answer4.Text.Equals(q4hash[3].ToString()) && answer5.Text.Equals(q5hash[2].ToString()) && answer6.Text.Equals(q5hash[4].ToString()))                
                {
                    Response.Redirect("wer.aspx");
                }
                else
                {
                    resultlable.Text = "Answers are not matching, Please verify again";
                    return;
                }
                
                */




            }
            catch (Exception er)
            {
            }
        }



       
    
        protected void validateUserQAsOld()
        {
            string userid = null;
            try
            {
                if (Session["userid"] != null)
                {
                    userid = Session["userid"].ToString();
                    displayuser.Text = Session["username"].ToString();
                }
                else if (Session["forgetpwduser"] != null)
                {
                    userid = Session["forgetpwduser"].ToString();
                }
                userid = QASecurity.Encryptdata(userid);
                DatabaseLayer dbObj = new DatabaseLayer();
                string q1 = " userquestionanswers.id as id,userquestionanswers.question1 as questionid,ssaquestions.question as question,userquestionanswers.answer1 as answer from userquestionanswers,ssaquestions where userquestionanswers.question1 = ssaquestions.qid and username='" + userid + "' and ssaquestions.qid=18";
                string q2 = " userquestionanswers.id as id,userquestionanswers.question2 as questionid,ssaquestions.question as question,userquestionanswers.answer2 as answer from userquestionanswers,ssaquestions where userquestionanswers.question2 = ssaquestions.qid and username='" + userid + "' and ssaquestions.qid=19";
                //string q3 = "select userquestionanswers.id,userquestionanswers.question3,ssaquestions.question,ssaquestions.question from userquestionanswers,ssaquestions where userquestionanswers.question3 = ssaquestions.qid  and username='" + userid + "'";
                string q3 = " userquestionanswers.id as id,userquestionanswers.question3 as questionid,ssaquestions.question as question,userquestionanswers.answer3 as answer from userquestionanswers,ssaquestions where userquestionanswers.question3 = ssaquestions.qid and username='" + userid + "'";
                //string q3 = " userquestionanswers.id,userquestionanswers.question3,ssaquestions.question,ssaquestions.question from userquestionanswers,ssaquestions where userquestionanswers.question3 = ssaquestions.qid  and username='ss0087061'";
                string q4 = " userquestionanswers.id as id,userquestionanswers.question4 as questionid,ssaquestions.question as question,userquestionanswers.answer4 as answer from userquestionanswers,ssaquestions where userquestionanswers.question4 = ssaquestions.qid and username='" + userid + "'";
                string q5 = " userquestionanswers.id as id,userquestionanswers.question5 as question5, " +
                    "userquestionanswers.answer5 as answer5, " +
                    " userquestionanswers.question6 as question6,userquestionanswers.answer6 as answer6 from userquestionanswers  " +
                    "where username ='" + userid + "'";
                string q6 = " userquestionanswers.id as id,userquestionanswers.question6 as questionid,ssaquestions.question as question,userquestionanswers.answer6 as answer6 from userquestionanswers,ssaquestions where userquestionanswers.question6 = ssaquestions.qid and username='" + userid + "'";
                //  ArrayList q1hash = dbObj.getTableDataQuery(q1, null, "id");


                ArrayList colNames = new ArrayList();
                colNames.Add("id");
                colNames.Add("questionid");
                colNames.Add("question");
                colNames.Add("answer");


                ArrayList colNames2 = new ArrayList();
                colNames2.Add("id");
                colNames2.Add("question5");
                colNames2.Add("answer5");
                colNames2.Add("question6");
                colNames2.Add("answer6");


                ArrayList q1hash = dbObj.getTableDataQuery(q1, null, "id", colNames);
                ArrayList q2hash = dbObj.getTableDataQuery(q2, null, "id", colNames);
                ArrayList q3hash = dbObj.getTableDataQuery(q3, null, "id", colNames);
                ArrayList q4hash = dbObj.getTableDataQuery(q4, null, "id", colNames);
                ArrayList q5hash = dbObj.getTableDataQuery(q5, null, "id", colNames2);

                if (q3hash.Count == 0)
                {
                    resultlable.Text = "Please register with Security questions and answers";
                    return;
                }
                int j=0;


                //if (answer1.Text.Equals(QASecurity.Decryptdata(q1hash[3].ToString())))
                //{
                //    j++;
                //}
                //if (answer2.Text.Equals(QASecurity.Decryptdata(q2hash[3].ToString())))
                //{
                //    j++;
                //}
                //if (answer3.Text.Equals(QASecurity.Decryptdata(q3hash[3].ToString())))
                //{
                //    j++;
                //}
                //if (answer4.Text.Equals(QASecurity.Decryptdata(q4hash[3].ToString())))
                //{
                //    j++;
                //}
                //if (answer5.Text.Equals(QASecurity.Decryptdata(q5hash[2].ToString())))
                //{
                //    j++;
                //}
                //if (answer6.Text.Equals(QASecurity.Decryptdata(q5hash[4].ToString())))
                //{
                //    j++;
                //}

                if (j >= 3)
                {
                    Response.Redirect("wer.aspx");
                }
                else
                {
                    resultlable.Text = "Minimum 3 Answers should match, Please verify again";
                    return;
                }
                /*
                if (answer1.Text.Equals(q1hash[3].ToString()) && answer2.Text.Equals(q2hash[3].ToString()) && answer3.Text.Equals(q3hash[3].ToString()) && answer4.Text.Equals(q4hash[3].ToString()) && answer5.Text.Equals(q5hash[2].ToString()) && answer6.Text.Equals(q5hash[4].ToString()))                
                {
                    Response.Redirect("wer.aspx");
                }
                else
                {
                    resultlable.Text = "Answers are not matching, Please verify again";
                    return;
                }
                
                */




            }
            catch (Exception er)
            {
            }
        }
        public void getUserQAs()
        {
            string userid = null;
            try
            {
                if (Session["userid"] != null)
                {
                    userid = Session["userid"].ToString();
                    displayuser.Text = "User: " + Session["username"].ToString();
                    ; 
                   
                }
                else if (Session["forgetpwduser"] != null)
                {
                    userid = Session["forgetpwduser"].ToString();
                }
                if (userid == null)
                {
                    Response.Redirect("SelfServiceLogin.aspx", false);
                }
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


                ArrayList colNames2 = new ArrayList();
                colNames2.Add("id");
                colNames2.Add("question5");
                colNames2.Add("answer5");
               // colNames2.Add("ANSWER5");

               // colNames2.Add("question6");
                //colNames2.Add("answer6");


                ArrayList colNames3 = new ArrayList();
                colNames3.Add("id");
                colNames3.Add("question6");
                colNames3.Add("answer6");
                //colNames2.Add("ANSWER6");

                    
              ArrayList q1hash = dbObj.getTableDataQuery(q1, null, "id", colNames);
              q1hash.Add("not encrypted");
              ArrayList q2hash = dbObj.getTableDataQuery(q2, null, "id", colNames);
              q2hash.Add("not encrypted");
              ArrayList q3hash = dbObj.getTableDataQuery(q3, null, "id", colNames);

              if (q3hash.Count == 0)
              {
                  resultlable.Text = "Please register with Security questions and answers";
                  return;
              }

              q3hash.Add("not encrypted");
              ArrayList q4hash = dbObj.getTableDataQuery(q4, null, "id", colNames);
              q4hash.Add("not encrypted");  
              ArrayList q5hash = dbObj.getTableDataQuery(q5, null, "id", colNames2);
              q5hash.Add("ANSWER5");
                q5hash.Add("encrypted");

              ArrayList q6hash = dbObj.getTableDataQuery(q6, null, "id", colNames3);
              q5hash.Add("encrypted");
                 
                //dbObj.getTableDataQuery(q6, null, "id", colNames);
             // q6hash.Add(q5hash[3].ToString());
              q6hash.Add("ANSWER6");
              q6hash.Add("encrypted");

             
                //Label1.Text = "What is your Mother Maiden Name?";
                //Label2.Text = "In what town were you born?";
                //question3.Text= q3hash[2].ToString();
                //question4.Text = q4hash[2].ToString();
                //question5.Text = QASecurity.Decryptdata( q5hash[1].ToString());
                //question6.Text = QASecurity.Decryptdata( q5hash[3].ToString());

                ArrayList[] qhash = new ArrayList[6];
                qhash[0] = q1hash;
                qhash[1] = q2hash;
                qhash[2] = q3hash;
                qhash[3] = q4hash;
                qhash[4] = q5hash;
                qhash[5] = q6hash;
                
                RandomQs qobj = new RandomQs();
              ArrayList randomQs =  qobj.PickRandom(qhash, 3);
               
              ArrayList firstq = (ArrayList)randomQs[0];
             
              ArrayList secondq = (ArrayList)randomQs[1];
              ArrayList thirdq = (ArrayList)randomQs[2];

              if (firstq.Contains("not encrypted"))
              {
                  Label1.Text = firstq[2].ToString();
                  Label1.Attributes["answer1"] = firstq[3].ToString();
                  Session.Add("answer1", firstq[3].ToString());
                  Label1.Attributes["encrypt"] = "no";


              }
              else if (firstq.Contains("encrypted"))
              {
                  Label1.Text = QASecurity.Decryptdata( firstq[1].ToString());
                  Label1.Attributes["answer1"] = firstq[2].ToString();
                  Session.Add("answer1", firstq[2].ToString());
                  Label1.Attributes["encrypt"] = "yes";

              }

              if (secondq.Contains("not encrypted"))
              {
                  Label2.Text = secondq[2].ToString();
                //  Label2.Attributes["answer1"] = secondq[3].ToString();
                  Label2.Attributes["answer1"] = secondq[3].ToString();
                  Session.Add("answer2",secondq[3].ToString());
                  Label2.Attributes["encrypt"] = "no";
              }
              else if (secondq.Contains("encrypted"))
              {
                  Label2.Text = QASecurity.Decryptdata( secondq[1].ToString());
                  Label2.Attributes["answer1"] = secondq[2].ToString();
                  Session.Add("answer2", secondq[2].ToString());
                  Label2.Attributes["encrypt"] = "yes";
              }


              if (thirdq.Contains("not encrypted"))
              {
                  Label3.Text = thirdq[2].ToString();
                  Label3.Attributes["answer1"] = thirdq[3].ToString();
                  Session.Add("answer3", thirdq[3].ToString());
                  Label3.Attributes["encrypt"] = "no";
              }
              else if (thirdq.Contains("encrypted"))
              {
                  Label3.Text = QASecurity.Decryptdata( thirdq[1].ToString());
                  Label3.Attributes["answer1"] = thirdq[2].ToString();
                  Session.Add("answer3", thirdq[2].ToString());
                  Label3.Attributes["encrypt"] = "yes";
              }



              //Label1.Text =   




  
                

            }
            catch (Exception er)
            {
            }
        }




   
   
        

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SSAHome.aspx");
          

            


        }
    }
}