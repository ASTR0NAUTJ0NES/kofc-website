using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Data.KofC.ModelBuilders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KofCWebSite.Data.KofC
{
    public partial class KofCDbContext : IdentityDbContext
    {
        public KofCDbContext()
        {
        }

        public KofCDbContext(DbContextOptions<KofCDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<AlbumImage> Images { get; set; }
        public virtual DbSet<NewsItem> NewsItems { get; set; }
        public virtual DbSet<Request> Request { get; set; }
        public virtual DbSet<RequestType> RequestTypes { get; set; }
        public virtual DbSet<UploadedFile> UploadedFiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            AlbumModelBuilder.BuildAlbum(modelBuilder);
            ContactModelBuilder.BuildContact(modelBuilder);
            ContactAddressModelBuilder.BuildContactAddress(modelBuilder);
            EventModelBuilder.BuildEvent(modelBuilder);
            AlbumImageModelBuilder.BuildAlbumImage(modelBuilder);
            NewsItemModelBuilder.BuildNewsItem(modelBuilder);
            RequestModelBuilder.BuildRequest(modelBuilder);
            RequestTypeModelBuilder.BuildRequestType(modelBuilder);
            IdentityModelBuilders.BuildEvent(modelBuilder);
            UploadedFileModelBuilder.BuildUploadedFile(modelBuilder);
        }
    }
}
