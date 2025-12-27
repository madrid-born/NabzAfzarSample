using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NabzAfzarSample.Identity
{
    public static class IdentitySeeder
    {
        public static void Seed()
        {
            using (var context = new ApplicationIdentityDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(context));

                var userManager = new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(context));

                if (!roleManager.RoleExists("Admin"))
                    roleManager.Create(new IdentityRole("Admin"));

                if (!roleManager.RoleExists("Customer"))
                    roleManager.Create(new IdentityRole("Customer"));

                var adminEmail = "admin@nabzafzar.local";
                var admin = userManager.FindByEmail(adminEmail);

                if (admin == null)
                {
                    admin = new ApplicationUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail
                    };

                    userManager.Create(admin, "Admin#12345");
                    userManager.AddToRole(admin.Id, "Admin");
                }
            }
        }
    }
}