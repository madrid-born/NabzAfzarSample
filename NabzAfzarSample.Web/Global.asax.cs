using System;

namespace NabzAfzarSample
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Identity.IdentitySeeder.Seed();
            Seed.ShopSeeder.Seed();
        }
    }
}