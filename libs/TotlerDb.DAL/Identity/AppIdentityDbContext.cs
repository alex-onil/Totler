using System;
using System.Security.Claims;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.ChangeTracking;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using TotlerRepository.Models.Entity;
using TotlerRepository.Models.Identity;
using System.Linq;
using Microsoft.Data.Entity.Metadata;

namespace TotlerDb.DAL.Identity
    {
    public class AppIdentityDbContext : IdentityDbContext<ApplicationUser>
        {
        public DbSet<ContactRecord> Contacts { get; set; }

        public AppIdentityDbContext(DbContextOptions options)
            {
            //Create the database and execute migrations
            Database.EnsureCreated(); //here
            //Database.Migrate();
            }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<ContactRecord>()
                .HasKey(k => k.Id);

            builder.Entity<ApplicationUser>()
                .HasOne(b => b.Contact)
                .WithOne()
                .IsRequired();

            base.OnModelCreating(builder);
            }

        }
    }
