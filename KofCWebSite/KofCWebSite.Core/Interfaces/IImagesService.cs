using KofCWebsite.Core.Entities.KofC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Interfaces
{
    public interface IImagesService
    {
        Task<AlbumImage> GetImageByIdAsync(int Id);
    }
}
