using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SelfServiceAdminstration.Databasecomp;
using System.Data;
using SelfServiceAdminstration.Authentication;
using System.Configuration;

namespace SelfServiceAdminstration
{
    public partial class UsersDisplay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string adminuser = ConfigurationManager.AppSettings["adminuser"].ToString();
            if (Session["username"] == null )
            {
                Response.Redirect("SelfServiceLogin.aspx",false);
            }
            else if (Session["userid"] != null)
                {
                    if (!Session["userid"].ToString().Equals(adminuser))
                    Response.Redirect("SelfServiceLogin.aspx");
                }
           if(!IsPostBack)
            getUserData("all");
        }
        public void getUserData(string queryoption)
        {
            DataSet data = null;
            string query = "";
            try
            {
                DatabaseLayer dataObj = new DatabaseLayer();
                if (queryoption.Equals("all"))
                {
                    query = "select id as 'S.No',username as 'User Name' from userquestionanswers";
                }
                else
                {
                    string liekquery = QASecurity.Encryptdata(queryoption);
                    query = "select id as 'S.No',username as 'User Name' from userquestionanswers where username like '%" + liekquery + "%'";
                }
                data = dataObj.getTableDataGrid(query);
                if (data != null)
                {
                    GridView1.DataSource = data;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = data;
                    GridView1.DataBind();
                }

            }
            catch (Exception er)
            {
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItem != null)
                {
                    DataRowView rdr = (DataRowView)e.Row.DataItem;

                    string username = rdr["User Name"].ToString();
                    //Label Label1 = (Label)e.Row.FindControl("username")
                    string usernamestr = QASecurity.Decryptdata(username.ToString());
                    e.Row.Cells[2].Text = usernamestr;
                    //Label1.Text = QASecurity.Decryptdata(username.ToString()); ;   //SymmetricEncryptionUtility.DecryptData(Address, EncryptionKeyFile);


                }
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DatabaseLayer dataObj = new DatabaseLayer();
            string autoid = GridView1.Rows[e.RowIndex].Cells[1].Text;
            if (dataObj.deleteTableData("delete from userquestionanswers where id='" + autoid + "'"))
            {
                getUserData("all");
            }

            

            
        }

        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            getUserData(TextBox1.Text);
        }
    }
}