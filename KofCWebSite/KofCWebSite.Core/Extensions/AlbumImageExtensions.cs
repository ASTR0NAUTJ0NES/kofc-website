using KofCWebsite.Core.Entities.KofC;
using System;
using System.Collections.Generic;
using System.Text;

namespace KofCWebSite.Core.Extensions
{
    public static class AlbumImageExtensions
    {
        public static string ToImageSrc(this AlbumImage img)
        {
            string imgBase64Data = Convert.ToBase64String(img?.ImageBytes);
            return $"data:{img.Filetype};base64,{imgBase64Data}";
        }
    }
}
