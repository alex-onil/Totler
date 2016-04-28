using System;
using System.Security.Claims;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.ChangeTracking;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using TotlerRepository.Models.Entity;
using TotlerRepository.Models.Identity;
using System.Linq;

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
        //public EntityEntry<TEntity> Add<TEntity>(TEntity entity, ClaimsPrincipal user)
        //    where TEntity: BaseEntity
        //    {
        //     entity.AuthorId = user.Identity.Name;
        //     entity.CreationDate = DateTime.Now;
        //     entity.LastEditorId = user.Identity.Name;
        //     entity.LastEditDate = DateTime.Now;
        //     return Add(entity);
        // }

        //public int SaveChanges(ClaimsPrincipal user)
        //    {
        //    this.ChangeTracker.DetectChanges();

        //    var entriesModified = this.ChangeTracker.Entries<BaseEntity>()
        //        .Where(e => e.State == EntityState.Modified);

        //    foreach (var entry in entriesModified)
        //        {
        //        entry.Property("LastEditDate").CurrentValue = DateTime.UtcNow;
        //        entry.Property("LastEditorId").CurrentValue = user.Identity.Name;
        //        }

        //    var entriesAdded = this.ChangeTracker.Entries<BaseEntity>()
        //          .Where(e => e.State == EntityState.Added);

        //    foreach (var entry in entriesAdded)
        //        {
        //        entry.Property("CreationDate").CurrentValue = DateTime.UtcNow;
        //        entry.Property("LastEditDate").CurrentValue = DateTime.UtcNow;
        //        entry.Property("AuthorId").CurrentValue = user.Identity.Name;
        //        entry.Property("LastEditorId").CurrentValue = user.Identity.Name;
        //        }

        //    return base.SaveChanges();


        //    }

        //public override int SaveChanges()
        //    {
        //    this.ChangeTracker.DetectChanges();

        //    var entriesModified = this.ChangeTracker.Entries<BaseEntity>()
        //        .Where(e => e.State == EntityState.Modified);

        //    foreach (var entry in entriesModified)
        //        {
        //        entry.Property("LastEditDate").CurrentValue = DateTime.UtcNow;
        //        }

        //    var entriesAdded = this.ChangeTracker.Entries<BaseEntity>()
        //          .Where(e => e.State == EntityState.Added);

        //    foreach (var entry in entriesAdded)
        //        {
        //        entry.Property("CreationDate").CurrentValue = DateTime.UtcNow;
        //        entry.Property("LastEditDate").CurrentValue = DateTime.UtcNow;
        //        }

        //    return base.SaveChanges();


        //    }
        }
    }
