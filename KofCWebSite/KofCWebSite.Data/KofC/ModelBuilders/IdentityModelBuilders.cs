using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pomelo.EntityFrameworkCore.MySql;
using Pomelo.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace KofCWebSite.Data.KofC.ModelBuilders
{
    public static class IdentityModelBuilders
    {
        public static void BuildEvent(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>(entity => 
            {
                entity.Property(e => e.Id).HasColumnName("Id")
                    .HasColumnType("VARCHAR(450)")
                    .HasMaxLength(450)
                    .HasCharSet("utf8");

                entity.Property(e => e.ConcurrencyStamp).HasColumnName("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("LONGTEXT");

                entity.Property(e => e.Name).HasColumnName("Name")
                    .HasColumnType("VARCHAR(256)")
                    .HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasColumnName("NormalizedName")
                    .HasColumnType("nvarchar(256)")
                    .HasMaxLength(256);

                entity.HasKey("Id");

                entity.HasIndex("NormalizedName").IsUnique()
                    .HasName("RoleNameIndex")
                    .HasFilter("[NormalizedName] IS NOT NULL");

                entity.ToTable("aspnetroles");
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.Property(e => e.Id).HasColumnName("Id")
                    .HasColumnType("INT")
                    .ValueGeneratedOnAdd();

                b.Property(e => e.ClaimType).HasColumnName("ClaimType")
                    .HasColumnType("LONGTEXT").HasCharSet("utf8");

                b.Property(e => e.ClaimValue).HasColumnName("ClaimValue")
                    .HasColumnType("LONGTEXT").HasCharSet("utf8");

                b.Property<string>("RoleId")
                    .IsRequired()
                    .HasColumnType("VARCHAR(450)")
                    .HasMaxLength(450)
                    .HasCharSet("utf8");

                b.HasKey("Id");

                b.HasIndex("RoleId");

                b.ToTable("aspnetroleclaims");
            });

            modelBuilder.Entity<IdentityUser>(b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("VARCHAR(450)")
                    .HasMaxLength(450)
                    .HasCharSet("utf8");

                b.Property<int>("AccessFailedCount").HasColumnType("int");
                b.Property<string>("ConcurrencyStamp").IsConcurrencyToken().HasColumnType("LONGTEXT");
                b.Property<string>("Email").HasColumnType("VARCHAR(256)").HasMaxLength(256).HasCharSet("utf8");
                b.Property<bool>("EmailConfirmed").HasColumnType("TINYINT");
                b.Property<bool>("LockoutEnabled").HasColumnType("TINYINT");
                b.Property<DateTimeOffset?>("LockoutEnd").HasColumnType("TIMESTAMP");
                b.Property<string>("NormalizedEmail").HasColumnType("VARCHAR(256)").HasMaxLength(256).HasCharSet("utf8");
                b.Property<string>("NormalizedUserName").HasColumnType("VARCHAR(256)").HasMaxLength(256).HasCharSet("utf8");
                b.Property<string>("PasswordHash").HasColumnType("LONGTEXT");
                b.Property<string>("PhoneNumber").HasColumnType("LONGTEXT");
                b.Property<bool>("PhoneNumberConfirmed").HasColumnType("TINYINT");
                b.Property<string>("SecurityStamp").HasColumnType("LONGTEXT");
                b.Property<bool>("TwoFactorEnabled").HasColumnType("TINYINT");
                b.Property<string>("UserName").HasColumnType("VARCHAR(256)").HasMaxLength(256).HasCharSet("utf8");

                b.HasKey("Id");

                b.HasIndex("NormalizedEmail").HasName("EmailIndex");

                b.HasIndex("NormalizedUserName").IsUnique().HasName("UserNameIndex")
                    .HasFilter("[NormalizedUserName] IS NOT NULL");

                b.ToTable("aspnetusers");
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INT");

                b.Property<string>("ClaimType")
                    .HasColumnType("LONGTEXT");

                b.Property<string>("ClaimValue")
                    .HasColumnType("LONGTEXT");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("VARCHAR(450)");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("aspnetuserclaims");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.Property<string>("LoginProvider")
                    .HasColumnType("VARCHAR(128)")
                    .HasMaxLength(128);

                b.Property<string>("ProviderKey")
                    .HasColumnType("VARCHAR(128)")
                    .HasMaxLength(128);

                b.Property<string>("ProviderDisplayName")
                    .HasColumnType("LONGTEXT");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("VARCHAR(450)");

                b.HasKey("LoginProvider", "ProviderKey");

                b.HasIndex("UserId");

                b.ToTable("aspnetuserlogins");
            });

            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.Property<string>("UserId").HasColumnType("VARCHAR(288)").HasMaxLength(288);
                b.Property<string>("RoleId").HasColumnType("VARCHAR(288)").HasMaxLength(288);

                b.HasKey("UserId", "RoleId");

                b.HasIndex("RoleId");

                b.ToTable("aspnetuserroles");
            });

            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.Property<string>("UserId").HasColumnType("VARCHAR(450)");

                b.Property<string>("LoginProvider").HasColumnType("VARCHAR(128)").HasMaxLength(128);
                b.Property<string>("Name").HasColumnType("VARCHAR(128)").HasMaxLength(128);
                b.Property<string>("Value").HasColumnType("LONGTEXT");

                b.HasKey("UserId", "LoginProvider", "Name");

                b.ToTable("aspnetusertokens");
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        }
    }
}
