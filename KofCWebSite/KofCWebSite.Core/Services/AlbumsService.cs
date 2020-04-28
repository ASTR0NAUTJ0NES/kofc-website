using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Interfaces;
using KofCWebSite.Core.Models;

namespace KofCWebSite.Core.Services
{
    public class AlbumsService : IAlbumsService
    {
        private IKofCRepository<Album> _AlbumRepo;
        public AlbumsService(IKofCRepository<Album> albumRepository)
        {
            _AlbumRepo = albumRepository;
        }

        public IEnumerable<Album> GetAllAlbums()
        {
            return _AlbumRepo.GetAll();
        }

        public async Task<Album> GetAlbumByIdAsync(int Id)
        {
            return await _AlbumRepo.GetEntityByIdAsync(Id);
        }

        public Task<Album> InsertAlbumAsync(AlbumCreateViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAlbumByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
