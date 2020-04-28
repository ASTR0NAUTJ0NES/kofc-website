using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Models.Popup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Models
{
    public class HomeViewModel : BaseViewModel
    {
        public PopupModel PopupAlert { get; set; } = null;

        public Event[] UpcomingEvents { get; set; } = Array.Empty<Event>();

        public NewsItem[] VisiblePinnedNewsItems { get; set; } = null;

        public UploadedFile Newsletter { get; set; } = null;
    }
}
