using KofCWebsite.Core.Entities.KofC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Models
{
    public class NewsItemsCommunityListViewModel : BaseViewModel
    {
        public NewsItem[] VisibleNewsItems { get; set; } = null;
    }
}
