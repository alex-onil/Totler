using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity.Infrastructure;
using Trade_MVC5.Models.Identity;

namespace Trade_MVC5.Models.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
        {
        public ApplicationDbContext(DbContextOptions options)
        {
            //Create the database and execute migrations
            Database.EnsureCreated(); //here
            //Database.Migrate();
            }

        // public DbSet<Blog> Blogs { get; set; }
        }
    }
