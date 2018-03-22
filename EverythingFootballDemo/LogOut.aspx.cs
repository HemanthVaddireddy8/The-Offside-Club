using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EverythingFootballDemo
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Remove("userInfo");
            Request.Cookies.Clear();
            FormsAuthentication.SignOut();
            var test = Session["userInfo"];
            Response.Redirect("HomePage.aspx", true);
        }
    }
}