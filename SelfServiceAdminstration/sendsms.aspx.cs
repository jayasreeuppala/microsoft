using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SelfServiceAdminstration
{
    public partial class sendsms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SMSRequest obj = new SMSRequest();
            obj.sendSMS(TextBox1.Text, TextBox2.Text);
        }
    }
}