using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using sdlife.web.Models;

namespace sdlife.web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160509060553_Accounting")]
    partial class Accounting
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "RoleClaim");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "UserClaim");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasAnnotation("Relational:TableName", "UserLogin");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasAnnotation("Relational:TableName", "UserRole");
                });

            modelBuilder.Entity("sdlife.web.Models.Accounting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("CreateUserId");

                    b.Property<int>("TitleId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("sdlife.web.Models.AccountingComment", b =>
                {
                    b.Property<int>("AccountingId");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 4000);

                    b.HasKey("AccountingId");
                });

            modelBuilder.Entity("sdlife.web.Models.AccountingTitle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("CreateUserId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();
                });

            modelBuilder.Entity("sdlife.web.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasAnnotation("Relational:Name", "RoleNameIndex");

                    b.HasAnnotation("Relational:TableName", "Role");
                });

            modelBuilder.Entity("sdlife.web.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasAnnotation("Relational:Name", "EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasAnnotation("Relational:Name", "UserNameIndex");

                    b.HasAnnotation("Relational:TableName", "User");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("sdlife.web.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("sdlife.web.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("sdlife.web.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<int>", b =>
                {
                    b.HasOne("sdlife.web.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("sdlife.web.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("sdlife.web.Models.Accounting", b =>
                {
                    b.HasOne("sdlife.web.Models.User")
                        .WithMany()
                        .HasForeignKey("CreateUserId");

                    b.HasOne("sdlife.web.Models.AccountingTitle")
                        .WithMany()
                        .HasForeignKey("TitleId");
                });

            modelBuilder.Entity("sdlife.web.Models.AccountingComment", b =>
                {
                    b.HasOne("sdlife.web.Models.Accounting")
                        .WithOne()
                        .HasForeignKey("sdlife.web.Models.AccountingComment", "AccountingId");
                });

            modelBuilder.Entity("sdlife.web.Models.AccountingTitle", b =>
                {
                    b.HasOne("sdlife.web.Models.User")
                        .WithMany()
                        .HasForeignKey("CreateUserId");
                });
        }
    }
}
