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
    public static class RequestModelBuilder
    {
        public static void BuildRequest(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("requests", "kofc");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.RequestTypeId)
                    .HasColumnName("requesttypeid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Subject)
                    .HasColumnName("subject")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.RequestBody)
                    .HasColumnName("requestbody")
                    .HasColumnType("longtext");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.Createdon)
                    .HasColumnName("createdon")
                    .HasColumnType("datetime");

                entity.Property(e => e.Receivedon)
                .HasColumnName("receivedon")
                    .HasColumnType("datetime");

                entity.Property(e => e.Closedon)
                .HasColumnName("closedon")
                    .HasColumnType("datetime");

                entity.Property(e => e.ContactId)
                .HasColumnName("contactid")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.RequestType)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.RequestTypeId)
                    .HasConstraintName("fk_requesttype");
            });
        }
    }
}

