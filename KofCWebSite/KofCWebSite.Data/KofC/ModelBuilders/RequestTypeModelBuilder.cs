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
    public static class RequestTypeModelBuilder
    {
        public static void BuildRequestType(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RequestType>(entity =>
            {
                entity.ToTable("requesttypes", "kofc");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.HasMany(e => e.Requests)
                .WithOne(d => d.RequestType)
                .HasForeignKey(x => x.Id)
                .HasConstraintName("fk_requesttype");
            });
        }
    }
}

