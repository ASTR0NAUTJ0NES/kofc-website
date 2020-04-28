using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KofCWebSite.Core.Extensions
{
    public static class EventExtensions
    {
        public static EventEditViewModel ToEditViewModel(this Event _event)
        {
            return new EventEditViewModel()
            {
                Address1 = _event.Address1,
                Address2 = _event.Address2,
                City = _event.City,
                State = _event.State,
                Zip = _event.Zip,
                Id = _event.Id,
                Title = "Edit Event",
                EventTitle = _event.Title,
                ContactId = _event.Contactid,
                Description = _event.Description,
                EventEnd = _event.EventEnd,
                EventStart = _event.EventStart,
                Location = _event.Location
            };
        }
    }
}
