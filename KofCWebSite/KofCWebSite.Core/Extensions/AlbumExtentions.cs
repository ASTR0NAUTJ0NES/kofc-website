using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KofCWebSite.Core.Extensions
{
    public static class AlbumExtentions
    {
        public static AlbumEditViewModel ToEditViewModel(this Album _album)
        {
            return new AlbumEditViewModel()
            {
                Title = "Edit Album",
                IsVisible = _album.IsVisible,
                Description = _album.Description,
                Name = _album.Name
            };
        }
    }
}
