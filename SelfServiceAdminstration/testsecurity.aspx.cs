using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SelfServiceAdminstration.Authentication;

namespace SelfServiceAdminstration
{
    public partial class testsecurity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            SSAEmail emailObj = new SSAEmail();
            emailObj.sendEmail("ss0087061@techmahindra.com", "hello ", "hell there ", "none", "", "localhost", 25, "dontreply@techmahindra.com");

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
           
        }
    }
}