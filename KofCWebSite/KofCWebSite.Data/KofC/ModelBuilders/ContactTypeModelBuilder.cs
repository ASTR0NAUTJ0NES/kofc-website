using KofCWebsite.Core.Entities.KofC;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KofCWebSite.Data.KofC.ModelBuilders
{
    public static class ContactTypeModelBuilder
    {
        public static void BuildContactType(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactType>(entity =>
            {
                entity.ToTable("contacttypes", "kofc");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("VARCHAR(45)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("VARCHAR(256)");

            });
        }
    }
}
