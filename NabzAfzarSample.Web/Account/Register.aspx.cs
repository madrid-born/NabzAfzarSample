using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity.EntityFramework;
using NabzAfzarSample.Identity;

namespace NabzAfzarSample.Account
{
    public partial class Register : System.Web.UI.Page
    {
        protected TextBox Email;
        protected TextBox Password;
        protected Literal ResultMessage;
        
        protected void Register_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext()
                .GetUserManager<ApplicationUserManager>();

            var user = new Identity.ApplicationUser
            {
                UserName = Email.Text,
                Email = Email.Text
            };

            var result = manager.Create(user, Password.Text);

            if (result.Succeeded)
            {
                var identityDb = Context.GetOwinContext()
                    .Get<ApplicationIdentityDbContext>();

                var roleManager = new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(identityDb));

                if (!roleManager.RoleExists("Customer"))
                {
                    roleManager.Create(new IdentityRole("Customer"));
                }

                // ✅ Assign user to Customer role
                manager.AddToRole(user.Id, "Customer");

                ResultMessage.Text = "User registered successfully.";
            }
            else
            {
                ResultMessage.Text = string.Join("<br/>", result.Errors);
            }
        }
    }
}