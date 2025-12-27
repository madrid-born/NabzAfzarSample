using Microsoft.AspNet.Identity.EntityFramework;

namespace NabzAfzarSample.Identity
{
    public class ApplicationIdentityDbContext
        : IdentityDbContext<ApplicationUser>
    {
        public ApplicationIdentityDbContext()
            : base("NabzAfzarSampleDb")
        {
        }

        public static ApplicationIdentityDbContext Create()
        {
            return new ApplicationIdentityDbContext();
        }
    }
}