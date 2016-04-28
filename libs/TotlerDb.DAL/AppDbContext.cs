using Microsoft.Data.Entity.Infrastructure;
using TotlerDb.DAL.Identity;

namespace TotlerDb.DAL
{
    public class AppDbContext : AppIdentityDbContext
    {
       public AppDbContext(DbContextOptions options) : base(options) { }
    }
}
