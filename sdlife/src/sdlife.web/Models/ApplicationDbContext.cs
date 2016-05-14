using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace sdlife.web.Models
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Identity Tables
            builder.Entity<User>().ToTable("User");
            builder.Entity<Role>().ToTable("Role");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogin");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRole");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaim");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaim");

            builder.Entity<Accounting>()
                .HasIndex(x => x.EventTime)
                .ForSqlServerIsClustered(true)
                .IsUnique(true);
            builder.Entity<Accounting>()
                .HasKey(x => x.Id)
                .ForSqlServerIsClustered(false);
            builder.Entity<Accounting>()
                .Property(x => x.EventTime)
                .HasDefaultValueSql("SYSDATETIME()");
            builder.Entity<Accounting>()
                .Property(x => x.CreateTime)
                .HasDefaultValueSql("SYSDATETIME()");

            builder.Entity<AccountingTitle>()
                .HasIndex(x => x.Title)
                .IsUnique(true);
            builder.Entity<AccountingTitle>()
                .HasIndex(x => x.ShortCut);
            builder.Entity<AccountingTitle>()
                .HasMany(x => x.Accountings)
                .WithOne(x => x.Title)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Accounting> Accounting { get; set; }

        public DbSet<AccountingTitle> AccountingTitle { get; set; }

        public DbSet<AccountingComment> AccountingComment { get; set; }
    }
}
