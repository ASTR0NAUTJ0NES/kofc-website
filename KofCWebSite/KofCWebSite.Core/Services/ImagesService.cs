using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Services
{
    public class ImagesService : IImagesService
    {
        private IKofCRepository<AlbumImage> _ImageRepo;
        public ImagesService(IKofCRepository<AlbumImage> ImageRepository)
        {
            _ImageRepo = ImageRepository;
        }

        public IEnumerable<AlbumImage> GetAllImages()
        {
            return _ImageRepo.GetAll();
        }

        public async Task<AlbumImage> GetImageByIdAsync(int Id)
        {
            return await _ImageRepo.GetEntityByIdAsync(Id);
        }

    }
}
