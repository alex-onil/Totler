using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Trade_MVC6.Models.Identity.AccountDetails;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using JetBrains.Annotations;
using Microsoft.Data.Entity.ChangeTracking;

namespace Trade_MVC6.Models.Identity
    {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
        {
        public DbSet<ContactRecord> Contacts { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            {
            //Create the database and execute migrations
            Database.EnsureCreated(); //here
            //Database.Migrate();
            }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<ApplicationUser>()
            //    .HasOne<ContactRecord>(p => p.Contact)
            //    .WithOne(p => p.User)
            //    .HasForeignKey<ApplicationUser>(f => f.ContactRef)
            //    .HasPrincipalKey<ContactRecord>(p => p.UserRef)
            //    .OnDelete(Microsoft.Data.Entity.Metadata.DeleteBehavior.Cascade)
            //    .IsRequired();
            builder.Entity<ContactRecord>()
                .HasKey(k => k.Id);

            builder.Entity<ApplicationUser>()
                .HasOne<ContactRecord>(p => p.Contact)
                .WithOne(w => w.User)
                .HasForeignKey<ApplicationUser>(f => f.ContactForeignKey);

            base.OnModelCreating(builder);
            }
        public EntityEntry<TEntity> Add<TEntity>(TEntity entity, ClaimsPrincipal user)
            where TEntity: EntityAbstract
         {
             entity.CreationAuthor = user.Identity.Name;
             entity.CreationDate = DateTime.Now;
             entity.LastEditor = user.Identity.Name;
             entity.LastEditDate = DateTime.Now;
             return Add(entity);
         }

        public int SaveChanges(ClaimsPrincipal user)
            {
            this.ChangeTracker.DetectChanges();

            var entriesModified = this.ChangeTracker.Entries<EntityAbstract>()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in entriesModified)
                {
                entry.Property("LastEditDate").CurrentValue = DateTime.UtcNow;
                entry.Property("LastEditor").CurrentValue = user.Identity.Name;
                }

            var entriesAdded = this.ChangeTracker.Entries<EntityAbstract>()
                  .Where(e => e.State == EntityState.Added);

            foreach (var entry in entriesAdded)
                {
                entry.Property("CreationDate").CurrentValue = DateTime.UtcNow;
                entry.Property("LastEditDate").CurrentValue = DateTime.UtcNow;
                entry.Property("CreationAuthor").CurrentValue = user.Identity.Name;
                entry.Property("LastEditor").CurrentValue = user.Identity.Name;
                }

            return base.SaveChanges();


            }

        public override int SaveChanges()
            {
            this.ChangeTracker.DetectChanges();

            var entriesModified = this.ChangeTracker.Entries<EntityAbstract>()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in entriesModified)
                {
                entry.Property("LastEditDate").CurrentValue = DateTime.UtcNow;
                }

            var entriesAdded = this.ChangeTracker.Entries<EntityAbstract>()
                  .Where(e => e.State == EntityState.Added);

            foreach (var entry in entriesAdded)
                {
                entry.Property("CreationDate").CurrentValue = DateTime.UtcNow;
                entry.Property("LastEditDate").CurrentValue = DateTime.UtcNow;
                }

            return base.SaveChanges();


            }
        }
    }
