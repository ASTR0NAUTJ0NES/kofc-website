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
    public static class NewsItemModelBuilder
    {
        public static void BuildNewsItem(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewsItem>(entity =>
            {
                entity.ToTable("newsitems", "kofc");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Article)
                    .HasColumnName("article")
                    .HasColumnType("longtext");

                entity.Property(e => e.Createdon).HasColumnName("createdon");

                entity.Property(e => e.Headline)
                    .IsRequired()
                    .HasColumnName("headline")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.IsPinned)
                    .HasColumnName("ispinned")
                    .HasColumnType("TINYINT(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.IsVisible)
                    .HasColumnName("isvisible")
                    .HasColumnType("TINYINT(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.PublishStart).HasColumnName("publishstart");

                entity.Property(e => e.PublishStop).HasColumnName("publishstop");

                entity.Property(e => e.AuthorFirstName)
                    .IsRequired()
                    .HasColumnName("authorfirstname")
                    .HasColumnType("VARCHAR(45)")
                    .HasMaxLength(45);

                entity.Property(e => e.AuthorMiddleName)
                    .HasColumnName("authormiddlename")
                    .HasColumnType("VARCHAR(45)")
                    .HasMaxLength(45);

                entity.Property(e => e.AuthorLastName)
                    .IsRequired()
                    .HasColumnName("authorlastname")
                    .HasColumnType("VARCHAR(45)")
                    .HasMaxLength(45);
            });
        }
    }
}

