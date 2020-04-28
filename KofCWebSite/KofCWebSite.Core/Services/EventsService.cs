using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Dto.Results;
using KofCWebSite.Core.Interfaces;
using KofCWebSite.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace KofCWebSite.Core.Services
{
    public class EventsService : IEventsService
    {
        private IEventsRepository _EventsRepository;
        public EventsService(IEventsRepository EventRepository)
        {
            _EventsRepository = EventRepository;
        }

        public IEnumerable<Event> GetAllEvents(bool fullyHydrate = true)
        {
            return _EventsRepository.GetAllEvents(fullyHydrate);
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _EventsRepository.GetEntityByIdAsync(id);
        }

        public async Task<Event[]> GetAllRecentAndUpcomingEvents(int takeQuantity = -1, bool isIncludeInactive = false)
        {
            var qry = _EventsRepository.GetAll()
                .Where(x => x.Status == 'A' && x.EventStart >= DateTime.Today.AddDays(-7));

            qry = takeQuantity >= 0
                ? qry.OrderBy(x => x.EventStart).Take(takeQuantity)
                : qry.OrderBy(x => x.EventStart);

            return await qry.ToArrayAsync();
        }

        public void DeleteEventByIdAsync(int id)
        {
            _EventsRepository.DeleteById(id);
            _EventsRepository.SaveChanges();
        }

        public async Task<Event> InsertEventAsync(EventCreateViewModel model)
        {
            var ms = new MemoryStream();
            var newEvent = new Event()
            {
                Address1 = model.Address1,
                Address2 = model.Address2,
                City = model.City,
                State = model.State,
                Zip = model.Zip,
                Contactid = model.ContactId,
                Description = model.Description,
                Location = model.Location,
                EventStart = model.EventStart,
                EventEnd = model.EventEnd,
                Status = 'A',
                Title = model.EventTitle                
            };

            _EventsRepository.Insert(newEvent);
            await _EventsRepository.SaveChangesAsync();
            return newEvent;
        }

        public async Task UpdateEventAsync(EventEditViewModel model)
        {
            var _event = await GetEventByIdAsync(model.Id);

            _event.Address1 = model.Address1;
            _event.Address2 = model.Address2;
            _event.City = model.City;
            _event.State = model.State;
            _event.Location = model.Location;
            _event.Zip = model.Zip;
            _event.Title = model.EventTitle;
            _event.Contactid = model.ContactId;
            _event.Description = model.Description;
            _event.EventEnd = model.EventEnd;
            _event.EventStart = model.EventStart;

            await _EventsRepository.SaveChangesAsync();
        }
    }
}
