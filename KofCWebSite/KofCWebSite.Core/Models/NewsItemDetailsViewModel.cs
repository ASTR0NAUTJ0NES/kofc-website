using KofCWebsite.Core.Entities.KofC;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace KofCWebSite.Core.Models
{
    public class NewsItemDetailsViewModel : BaseViewModel
    {
        public NewsItem NewsItem { get; set; }
    }
}
