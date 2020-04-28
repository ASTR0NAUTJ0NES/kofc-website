using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Interfaces
{
    public interface IAlbumsService
    {
        IEnumerable<Album> GetAllAlbums();
        Task<Album> GetAlbumByIdAsync(int id);
        Task<Album> InsertAlbumAsync(AlbumCreateViewModel model);
        Task DeleteAlbumByIdAsync(int id);
    }
}
