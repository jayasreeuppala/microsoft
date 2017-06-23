using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SelfServiceAdminstration.Databasecomp;
using System.Collections;
using System.Data;
using SelfServiceAdminstration.Authentication;


namespace SelfServiceAdminstration
{
    public partial class SSARegisterProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet data = null;
            DataSet sysdata = null;
            try
            {
                if (Session["username"] == null )
                {
                    Response.Redirect("SelfServiceLogin.aspx");
                }
                else
                {
                    if(Session["username"]!=null)
                    usernamelbl.Text = Session["username"].ToString();
                }
                if (!Page.IsPostBack)
                {
                    data = getQuestions();

                    questionSet1.DataSource = data;

                    questionSet1.DataTextField = "question";
                    questionSet1.DataValueField = "qid";
                    questionSet1.DataBind();


                    questionSet2.DataSource = data;

                    questionSet2.DataTextField = "question";
                    questionSet2.DataValueField = "qid";
                    questionSet2.DataBind();
                    validateUserQAs();

                    
                }
                else
                {
                  //  answer1.Text = "";
                    //validateUserQAs();
                }

            
            
                

              

            }
            catch (Exception er)
            {
                //answer1.Text = er.Message;
            }
        }

        protected void validateUserQAs()
        {
            string username = null;

            try
            {
                if (Session["userid"] != null)
                    username = Session["userid"].ToString();

                String userid = QASecurity.Encryptdata(username);
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

                question5.Text = QASecurity.Decryptdata( q5hash[1].ToString());
                question6.Text = QASecurity.Decryptdata( q5hash[3].ToString());
                questionSet1.SelectedValue = q3hash[1].ToString();
                questionSet2.SelectedValue = q4hash[1].ToString();

                //answer1.Text= q1hash[3].ToString();
                answer1.Attributes["value"] = QASecurity.Encryptdata(q1hash[3].ToString());
                //answer2.Attributes["value"] = q2hash[3].ToString();
                answer2.Attributes["value"] = QASecurity.Encryptdata(q2hash[3].ToString());
                //answer2.Text= q2hash[3].ToString();
                //answer3.Text = q3hash[3].ToString();
                answer3.Attributes["value"] = QASecurity.Encryptdata(q3hash[3].ToString());
               // answer4.Text=q4hash[3].ToString();
                answer4.Attributes["value"] = QASecurity.Encryptdata(q4hash[3].ToString());
                //answer5.Text=q5hash[2].ToString();
                answer5.Attributes["value"] = QASecurity.Encryptdata(q5hash[2].ToString());

                //answer6.Text=q5hash[4].ToString();
                answer6.Attributes["value"] = QASecurity.Encryptdata(q5hash[4].ToString());
                

            }
            catch (Exception er)
            {
            }
        }
        protected bool checkUserExistance()
        {


            return false;
        }

        private DataSet getQuestions()
        {
            DataSet data = null;
            try
            {


                DatabaseLayer d1 = new DatabaseLayer();
                string query = "select qid,question from ssaquestions where sysquestion=0";
                
                data = d1.getTableDataGrid(query);


            }
            catch (Exception er)
            {
            }
            return data;
        }

      

       
       
        protected void insertQA()
        {
            
            
            Hashtable userOwnHash = null;
            DatabaseLayer dataObj = null;

            try
            {

               
                userOwnHash = new Hashtable();
                dataObj = new DatabaseLayer();
                
                userOwnHash.Add("question1", "18");
                ;
                userOwnHash.Add("answer1", QASecurity.Encryptdata(answer1.Text));

                userOwnHash.Add("question2", "19");
                userOwnHash.Add("answer2", QASecurity.Encryptdata(answer2.Text));

                userOwnHash.Add("question3", questionSet1.SelectedValue);
                userOwnHash.Add("answer3", QASecurity.Encryptdata(answer3.Text));

                userOwnHash.Add("question4", questionSet2.SelectedValue);
                userOwnHash.Add("answer4", QASecurity.Encryptdata(answer4.Text));


                userOwnHash.Add("question5", QASecurity.Encryptdata(question5.Text));
                userOwnHash.Add("answer5", QASecurity.Encryptdata(answer5.Text));

                userOwnHash.Add("question6", QASecurity.Encryptdata(question6.Text));
                userOwnHash.Add("answer6", QASecurity.Encryptdata(answer6.Text));

                //string userid = Session["userid"].ToString().ToLower();
                userOwnHash.Add("username", QASecurity.Encryptdata(Session["userid"].ToString()));

                if (dataObj.insertTableDataStatus("userquestionanswers", userOwnHash))
                {
                    Response.Redirect("SSAHome.aspx");
                }
                else
                {

                }
                


            }
            catch (Exception er)
            {

            }
        }

        protected void updateQA()
        {


            Hashtable userOwnHash = null;
            DatabaseLayer dataObj = null;

            try
            {

                userOwnHash = new Hashtable();
                dataObj = new DatabaseLayer();

                userOwnHash.Add("question1", "18");
                userOwnHash.Add("answer1", QASecurity.Encryptdata(answer1.Text));

                userOwnHash.Add("question2", "19");
                userOwnHash.Add("answer2", QASecurity.Encryptdata(answer2.Text));

                userOwnHash.Add("question3", questionSet1.SelectedValue);
                userOwnHash.Add("answer3", QASecurity.Encryptdata(answer3.Text));

                userOwnHash.Add("question4", questionSet2.SelectedValue);
                userOwnHash.Add("answer4", QASecurity.Encryptdata(answer4.Text));


                userOwnHash.Add("question5", QASecurity.Encryptdata(question5.Text));
                userOwnHash.Add("answer5", QASecurity.Encryptdata(answer5.Text));

                userOwnHash.Add("question6", QASecurity.Encryptdata(question6.Text));
                userOwnHash.Add("answer6", QASecurity.Encryptdata(answer6.Text));


                string username = QASecurity.Encryptdata(Session["userid"].ToString());
                userOwnHash.Add("username", username);

                if (dataObj.updateTableDataStatus("userquestionanswers", userOwnHash, "username='" + username + "'"))
                {
                    Response.Redirect("SSAHome.aspx");

                    
                }
                else
                {

                }



            }
            catch (Exception er)
            {

            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("SSAHome.aspx");
        }

        protected void save_Click(object sender, ImageClickEventArgs e)
        {
            string update = Session["update"].ToString();


            DatabaseLayer dataObj = new DatabaseLayer();
            string userName2 = Session["userid"].ToString();
            string userName = QASecurity.Encryptdata(userName2);
            if (questionSet1.SelectedValue.Equals(questionSet2.SelectedValue))
            {
                
            }
            else
            {
                if (dataObj.getTablerowCount("userquestionanswers", "username='" + userName + "'"))
                {
                    updateQA();
                }
                else
                {
                    insertQA();
                }
            }
        }

        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("SelfServiceLogin.aspx");
        }
    }
}