using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EverythingFootballDemo
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void loginUser_Authenticate(object sender, AuthenticateEventArgs e)
        {
            var userBLL = new EverythingFootballDemo.BLL.Users();
            var userInfo = userBLL.GetUserInfo(loginUser.UserName, loginUser.Password);
            if (userInfo != null)
            {
                e.Authenticated = true;
                Session["userInfo"] = userInfo;
                Response.Redirect("HomePage.aspx");
            }
            else {
                e.Authenticated = false;
            }
        }
    }
}