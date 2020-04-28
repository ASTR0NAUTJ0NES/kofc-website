using KofCWebsite.Core.Entities.KofC;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace KofCWebSite.Core.Models
{
    public class EventDetailsViewModel : BaseViewModel
    {
        public Event Event { get; set; }
        public List<SelectListItem> Contacts { get; set; }
    }
}
