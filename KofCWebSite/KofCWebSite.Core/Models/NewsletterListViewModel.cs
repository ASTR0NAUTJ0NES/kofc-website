using KofCWebsite.Core.Entities.KofC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KofCWebSite.Core.Models
{
    public class NewsletterListViewModel : BaseViewModel
    {
        public IQueryable<UploadedFile> Newsletters { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsContributor { get; set; }
    }
}
