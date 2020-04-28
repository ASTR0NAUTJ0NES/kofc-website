using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KofCWebSite.Core.Extensions
{
    public static class NewsletterExtensions
    {
        public static NewsletterEditViewModel ToEditViewModel(this UploadedFile newsletter)
        {
            return new NewsletterEditViewModel()
            {
                Title = "Edit Newsletter",
                Description = newsletter.Description,
                Id = newsletter.Id,
                Filename = newsletter.Filename
            };
        }
    }
}
