using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Interfaces;
using KofCWebSite.Data.KofC;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KofCWebSite.Data.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private KofCDbContext _db = null;
        private DbSet<Event> table = null;
        public IQueryable<Event> Items => table;

        public EventsRepository(KofCDbContext kofCDbContext)
        {
            _db = kofCDbContext;
        }

        public void DeleteById(int Id)
        {
            Event entityObj = _db.Events.FirstOrDefault(x => x.Id == Id);
            if (entityObj != null)
            {
                _db.Remove(entityObj);
                SaveChanges();
            }
        }

        public void DeActivateById(int Id)
        {
            Event entityObj = _db.Find<Event>(Id);
            var statusProp = entityObj.GetType().GetProperty("Status");

            if (statusProp == null)
            {
                throw new DbUpdateException("Entity does not have a column named 'Status'");
            }

            statusProp.SetValue(statusProp, 'X');
            SaveChanges();
        }

        public IQueryable<Event> GetAll()
        {
            return _db.Events;
        }

        public async Task<Event> GetEntityByIdAsync(int Id)
        {
            table = _db.Set<Event>();
            return await table.FindAsync(Id);
        }

        public void Insert(Event entity)
        {
            _db.Add(entity);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }


        public void Update(Event entity)
        {
            table.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<Event> GetAllEvents(bool fullyHydrate = true)
        {
            if (fullyHydrate)
            {
                return _db.Events.Include(_event => _event.Contact);
            } else
            {
                return table;
            }
        }
    }
}
