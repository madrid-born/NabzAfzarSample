using System;
using System.Web;
using Microsoft.Owin.Security;

namespace NabzAfzarSample.Account
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var auth = Context.GetOwinContext().Authentication;
            auth.SignOut();
            Response.Redirect("~/Shop.aspx");
        }
    }
}