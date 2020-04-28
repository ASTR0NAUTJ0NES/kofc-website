using KofCWebsite.Core.Entities.KofC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Models
{
    public class EventsListViewModel : BaseViewModel
    {
        public IEnumerable<Event> Events { get; set; }

        public bool ShowAdminButtons { get; set; }
    }
}
