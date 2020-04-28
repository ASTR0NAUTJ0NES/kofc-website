using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Interfaces
{
    public interface INewsItemsService
    {
        IEnumerable<NewsItem> GetAllNewsItems();
        Task<NewsItem> GetNewsItemByIdAsync(int id);
        Task<NewsItem[]> GetAllVisiblePinnedNewsItems();
        Task<NewsItem[]> GetAllVisibleNewsItems();
        void DeleteNewsItemByIdAsync(int id);
        Task<NewsItem> InsertNewsItemAsync(NewsItemCreateViewModel model);
        Task UpdateNewsItemAsync(NewsItemEditViewModel model);
    }
}
