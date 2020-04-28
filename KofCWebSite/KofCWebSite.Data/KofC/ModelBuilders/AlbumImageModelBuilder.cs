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
    public static class AlbumImageModelBuilder
    {
        public static void BuildAlbumImage(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlbumImage>(entity =>
            {
                entity.ToTable("images", "kofc");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Filetype)
                    .IsRequired()
                    .HasColumnName("filetype")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ImageBytes)
                    .IsRequired()
                    .HasColumnName("image")
                    .HasColumnType("longblob");

                entity.Property(e => e.Filename)
                    .HasColumnName("filename")
                    .HasColumnType("VARCHAR(256)");

                entity.Property(e => e.AlbumId)
                    .HasColumnName("albumid")
                    .HasColumnType("INT");
            });
        }
    }
}
