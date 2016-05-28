using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sdlife.web.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace sdlife.web.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

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
            builder.Entity<IdentityUserToken<int>>().ToTable("UserToken");

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

            builder.Entity<AccountingTitle>()
                .HasIndex(x => x.Title)
                .IsUnique(true);
            builder.Entity<AccountingTitle>()
                .HasIndex(x => x.ShortCut);
            builder.Entity<AccountingTitle>()
                .HasMany(x => x.Accountings)
                .WithOne(x => x.Title)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AccountingUserAuthorization>()
                .HasKey(x => new { x.UserId, x.AuthorizedUserId });
        }

        public DbSet<Accounting> Accounting { get; set; }

        public DbSet<AccountingTitle> AccountingTitle { get; set; }

        public DbSet<AccountingComment> AccountingComment { get; set; }

        public DbSet<AccountingUserAuthorization> AccountingUserAuthorization { get; set; }
    }
}
