using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KofCWebSite.Core.Extensions
{
    public static class NewsItemExtensions
    {
        public static NewsItemEditViewModel ToEditViewModel(this NewsItem newsItem)
        {
            return new NewsItemEditViewModel()
            {
                Title = "Edit News Item",
                Id = newsItem.Id,
                Article = newsItem.Article,
                AuthorFirstName = newsItem.AuthorFirstName,
                AuthorMiddleName = newsItem.AuthorMiddleName,
                AuthorLastName = newsItem.AuthorLastName,
                Createdon = newsItem.Createdon,
                IsPinned = newsItem.IsPinned,
                IsVisible = newsItem.IsVisible,
                Headline = newsItem.Headline,
                PublishStart = newsItem.PublishStart,
                PublishStop = newsItem.PublishStop
            };
        }
    }
}
