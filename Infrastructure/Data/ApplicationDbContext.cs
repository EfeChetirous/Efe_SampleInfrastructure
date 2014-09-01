using Infrastructure.Entity.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DataContext")
        {

        }
    }
}
