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
    public static class ContactAddressModelBuilder
    {
        public static void BuildContactAddress(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactAddress>(entity =>
            {
                entity.ToTable("contactaddresses", "kofc");

                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.ContactId);
                entity.HasIndex(e => e.Status);

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int(11)");

                entity.Property(e => e.Address1)
                .HasColumnName("address1")
                .HasColumnType("VARCHAR(50)");

                entity.Property(e => e.Address2)
                .HasColumnName("address2")
                .HasColumnType("VARCHAR(50)");

                entity.Property(e => e.City)
                .HasColumnName("city")
                .HasColumnType("VARCHAR(45)");

                entity.Property(e => e.State)
                .HasColumnName("state")
                .HasColumnType("CHAR(2)");

                entity.Property(e => e.Zip)
                .HasColumnName("zip")
                .HasColumnType("VARCHAR(10)");

                entity.Property(e => e.IsPrimary)
                .HasColumnName("isprimary")
                .HasColumnType("TINYINT");
            });
        }
    }
}
