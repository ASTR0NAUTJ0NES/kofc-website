using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Interfaces;
using KofCWebSite.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace KofCWebSite.Core.Services
{
    public class NewsItemsService : INewsItemsService
    {
        private IKofCRepository<NewsItem> _NewsItemsRepository;
        public NewsItemsService(IKofCRepository<NewsItem> newsitemRepository)
        {
            _NewsItemsRepository = newsitemRepository;
        }

        public IEnumerable<NewsItem> GetAllNewsItems()
        {
            return _NewsItemsRepository.GetAll();
        }

        public async Task<NewsItem> GetNewsItemByIdAsync(int id)
        {
            return await _NewsItemsRepository.GetEntityByIdAsync(id);
        }

        public async Task<NewsItem[]> GetAllVisiblePinnedNewsItems()
        {
            var qry = _NewsItemsRepository.GetAll()
                .Where(x => x.IsPinned && x.IsVisible);

            return await qry.ToArrayAsync();
        }

        public async Task<NewsItem[]> GetAllVisibleNewsItems()
        {
            var qry = _NewsItemsRepository.GetAll()
                .Where(x => x.IsVisible);

            return await qry.ToArrayAsync();
        }

        public void DeleteNewsItemByIdAsync(int id)
        {
            _NewsItemsRepository.DeleteById(id);
            _NewsItemsRepository.SaveChanges();
        }

        public async Task<NewsItem> InsertNewsItemAsync(NewsItemCreateViewModel model)
        {
            var newNewsItem = new NewsItem()
            {
                Article = model.Article,
                AuthorFirstName = model.AuthorFirstName,
                AuthorMiddleName = model.AuthorMiddleName,
                AuthorLastName = model.AuthorLastName,
                Createdon = DateTime.Now,
                Headline = model.Headline,
                IsPinned = model.IsPinned,
                IsVisible = model.IsVisible,
                PublishStart = model.PublishStart,
                PublishStop = model.PublishStop
            };

            _NewsItemsRepository.Insert(newNewsItem);
            await _NewsItemsRepository.SaveChangesAsync();
            return newNewsItem;
        }

        public async Task UpdateNewsItemAsync(NewsItemEditViewModel model)
        {
            var newsItem = await GetNewsItemByIdAsync(model.Id);

            newsItem.Headline = model.Headline;
            newsItem.Article = model.Article;
            newsItem.AuthorFirstName = model.AuthorFirstName;
            newsItem.AuthorMiddleName = model.AuthorMiddleName;
            newsItem.AuthorLastName = model.AuthorLastName;
            newsItem.IsPinned = model.IsPinned;
            newsItem.IsVisible = model.IsVisible;
            newsItem.PublishStart = model.PublishStart;
            newsItem.PublishStop = model.PublishStop;

            await _NewsItemsRepository.SaveChangesAsync();
        }
    }
}
