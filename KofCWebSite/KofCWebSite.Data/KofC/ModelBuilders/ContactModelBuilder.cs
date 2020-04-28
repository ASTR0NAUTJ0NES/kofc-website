using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pomelo.EntityFrameworkCore.MySql;
using Pomelo.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using KofCWebsite.Core.Entities.KofC;

namespace KofCWebSite.Data.KofC.ModelBuilders
{
    public static class ContactModelBuilder
    {
        public static void BuildContact(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("contacts", "kofc");

                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Status);

                entity.Property(e => e.Id).HasColumnName("id").HasColumnType("int(11)");

                entity.Property(e => e.DateOfBirth).HasColumnName("dateofbirth").HasColumnType("DATE");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Middlename)
                    .HasColumnName("middlename")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Suffix)
                    .HasColumnName("suffix")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Occupation)
                    .HasColumnName("occupation")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.AlbumImageId)
                    .HasColumnName("imageid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ContactTypeId)
                    .HasColumnName("contacttypeid")
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .IsUnicode(false);

                entity.Property(e => e.HomePhone)
                    .HasColumnName("homephone")
                    .IsUnicode(false);

                entity.Property(e => e.CellPhone)
                    .HasColumnName("cellphone")
                    .IsUnicode(false);

                entity.HasOne(d => d.ContactType)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.ContactTypeId);

            });
        }
    }
}
