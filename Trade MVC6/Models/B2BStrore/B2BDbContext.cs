using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity.Infrastructure;
using Trade_MVC6.Models.Identity;

namespace Trade_MVC6.Models.B2BStrore
{
    public class B2BDbContext : ApplicationDbContext
    {
       public B2BDbContext(DbContextOptions options) : base(options) { }
    }
}
