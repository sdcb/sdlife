using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using sdlife.web.Data;

namespace sdlife.web.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160516144017_0000")]
    partial class _0000
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken");
                });

            modelBuilder.Entity("sdlife.web.Models.Accounting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.Property<int>("CreateUserId");

                    b.Property<DateTime>("EventTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.Property<int>("TitleId");

                    b.HasKey("Id")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("CreateUserId");

                    b.HasIndex("EventTime")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("TitleId");

                    b.ToTable("Accounting");
                });

            modelBuilder.Entity("sdlife.web.Models.AccountingComment", b =>
                {
                    b.Property<int>("AccountingId");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 4000);

                    b.HasKey("AccountingId");

                    b.HasIndex("AccountingId");

                    b.ToTable("AccountingComment");
                });

            modelBuilder.Entity("sdlife.web.Models.AccountingTitle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("CreateUserId");

                    b.Property<string>("ShortCut")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.HasKey("Id");

                    b.HasIndex("CreateUserId");

                    b.HasIndex("ShortCut");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("AccountingTitle");
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
                        .HasName("RoleNameIndex");

                    b.ToTable("Role");
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
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasName("UserNameIndex");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("sdlife.web.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("sdlife.web.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("sdlife.web.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.HasOne("sdlife.web.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("sdlife.web.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("sdlife.web.Models.Accounting", b =>
                {
                    b.HasOne("sdlife.web.Models.User")
                        .WithMany()
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("sdlife.web.Models.AccountingTitle")
                        .WithMany()
                        .HasForeignKey("TitleId");
                });

            modelBuilder.Entity("sdlife.web.Models.AccountingComment", b =>
                {
                    b.HasOne("sdlife.web.Models.Accounting")
                        .WithOne()
                        .HasForeignKey("sdlife.web.Models.AccountingComment", "AccountingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("sdlife.web.Models.AccountingTitle", b =>
                {
                    b.HasOne("sdlife.web.Models.User")
                        .WithMany()
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
