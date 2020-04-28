using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Dto.Results;
using KofCWebSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Interfaces
{
    public interface IEventsService
    {
        IEnumerable<Event> GetAllEvents(bool fullHydrate = true);
        Task<Event> GetEventByIdAsync(int id);
        Task<Event[]> GetAllRecentAndUpcomingEvents(int takeQuantity = -1, bool isIncludeInactive = false);
        void DeleteEventByIdAsync(int id);
        //AddEventResult Add(Event _event);
        Task<Event> InsertEventAsync(EventCreateViewModel model);
        Task UpdateEventAsync(EventEditViewModel model);
    }
}
