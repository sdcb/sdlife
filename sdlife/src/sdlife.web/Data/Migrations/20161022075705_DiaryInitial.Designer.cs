using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using sdlife.web.Data;

namespace sdlife.web.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161022075705_DiaryInitial")]
    partial class DiaryInitial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
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

                    b.HasIndex("AccountingId")
                        .IsUnique();

                    b.ToTable("AccountingComment");
                });

            modelBuilder.Entity("sdlife.web.Models.AccountingTitle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsIncome");

                    b.Property<string>("ShortCut")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.HasKey("Id");

                    b.HasIndex("ShortCut");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("AccountingTitle");
                });

            modelBuilder.Entity("sdlife.web.Models.AccountingUserAuthorization", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("AuthorizedUserId");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 15);

                    b.Property<int>("Level");

                    b.HasKey("UserId", "AuthorizedUserId");

                    b.HasIndex("AuthorizedUserId");

                    b.HasIndex("UserId");

                    b.ToTable("AccountingUserAuthorization");
                });

            modelBuilder.Entity("sdlife.web.Models.DiaryContent", b =>
                {
                    b.Property<int>("DiaryId");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 4000);

                    b.HasKey("DiaryId");

                    b.HasIndex("DiaryId")
                        .IsUnique();

                    b.ToTable("DiaryContent");
                });

            modelBuilder.Entity("sdlife.web.Models.DiaryFeeling", b =>
                {
                    b.Property<int>("FeelingId");

                    b.Property<int>("DiaryId");

                    b.HasKey("FeelingId", "DiaryId");

                    b.HasIndex("DiaryId");

                    b.HasIndex("FeelingId");

                    b.ToTable("DiaryFeeling");
                });

            modelBuilder.Entity("sdlife.web.Models.DiaryHeader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("RecordTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.Property<int>("UserId");

                    b.Property<int>("WeatherId");

                    b.HasKey("Id")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("RecordTime")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("UserId");

                    b.HasIndex("WeatherId");

                    b.ToTable("DiaryHeader");
                });

            modelBuilder.Entity("sdlife.web.Models.Feeling", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Feeling");
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
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("User");
                });

            modelBuilder.Entity("sdlife.web.Models.Weather", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Weather");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("sdlife.web.Models.Role")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("sdlife.web.Models.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("sdlife.web.Models.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.HasOne("sdlife.web.Models.Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("sdlife.web.Models.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("sdlife.web.Models.Accounting", b =>
                {
                    b.HasOne("sdlife.web.Models.User", "CreateUser")
                        .WithMany("Accountings")
                        .HasForeignKey("CreateUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("sdlife.web.Models.AccountingTitle", "Title")
                        .WithMany("Accountings")
                        .HasForeignKey("TitleId");
                });

            modelBuilder.Entity("sdlife.web.Models.AccountingComment", b =>
                {
                    b.HasOne("sdlife.web.Models.Accounting", "Accounting")
                        .WithOne("Comment")
                        .HasForeignKey("sdlife.web.Models.AccountingComment", "AccountingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("sdlife.web.Models.AccountingUserAuthorization", b =>
                {
                    b.HasOne("sdlife.web.Models.User", "AuthorizedUser")
                        .WithMany("AccountingUserAuthorizationTarget")
                        .HasForeignKey("AuthorizedUserId");

                    b.HasOne("sdlife.web.Models.User", "User")
                        .WithMany("AccountingUserAuthorizationFrom")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("sdlife.web.Models.DiaryContent", b =>
                {
                    b.HasOne("sdlife.web.Models.DiaryHeader", "Diary")
                        .WithOne("Content")
                        .HasForeignKey("sdlife.web.Models.DiaryContent", "DiaryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("sdlife.web.Models.DiaryFeeling", b =>
                {
                    b.HasOne("sdlife.web.Models.DiaryHeader", "Diary")
                        .WithMany("Feelings")
                        .HasForeignKey("DiaryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("sdlife.web.Models.Feeling", "Feeling")
                        .WithMany()
                        .HasForeignKey("FeelingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("sdlife.web.Models.DiaryHeader", b =>
                {
                    b.HasOne("sdlife.web.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("sdlife.web.Models.Weather", "Weather")
                        .WithMany()
                        .HasForeignKey("WeatherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
