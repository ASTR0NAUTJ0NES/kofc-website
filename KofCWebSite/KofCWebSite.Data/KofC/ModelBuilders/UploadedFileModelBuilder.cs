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
    public static class UploadedFileModelBuilder
    {
        public static void BuildUploadedFile(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UploadedFile>(entity =>
            {
                entity.ToTable("files", "kofc");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("INT");

                entity.Property(e => e.Filetype)
                    .IsRequired()
                    .HasColumnName("filetype")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.FileBytes)
                    .IsRequired()
                    .HasColumnName("filebytes")
                    .HasColumnType("LONGBLOB");

                entity.Property(e => e.Filename)
                    .HasColumnName("filename")
                    .HasColumnType("VARCHAR(256)")
                    .HasMaxLength(256);

                entity.Property(e => e.CategoryId)
                    .HasColumnName("categoryid")
                    .HasColumnType("INT");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdon")
                    .HasColumnType("DATETIME");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("VARCHAR(512)")
                    .HasMaxLength(512);
            });
        }
    }
}
