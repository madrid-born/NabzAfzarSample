using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using NabzAfzarSample.Identity;

[assembly: OwinStartup(typeof(NabzAfzarSample.Startup))]

namespace NabzAfzarSample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // DbContext per request
            app.CreatePerOwinContext(ApplicationIdentityDbContext.Create);

            // UserManager per request ✅ THIS WAS MISSING
            app.CreatePerOwinContext<ApplicationUserManager>(
                ApplicationUserManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login.aspx")
            });
        }
    }
}