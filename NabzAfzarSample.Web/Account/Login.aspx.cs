using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Web.UI.WebControls;
using NabzAfzarSample.Identity;

namespace NabzAfzarSample.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected TextBox Email;
        protected TextBox Password;
        protected Literal ErrorMessage;

        protected void Login_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext()
                .GetUserManager<ApplicationUserManager>();


            var user = manager.Find(Email.Text, Password.Text);

            if (user == null)
            {
                ErrorMessage.Text = "Invalid login attempt.";
                return;
            }

            var authManager = Context.GetOwinContext().Authentication;

            var identity = manager.CreateIdentity(
                user, DefaultAuthenticationTypes.ApplicationCookie);

            authManager.SignIn(new AuthenticationProperties(), identity);

            Response.Redirect("~/Default.aspx");
        }
    }
}