using KofCWebsite.Core.Entities.KofC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KofCWebSite.Core.Interfaces
{
    public interface IEventsRepository : IKofCRepository<Event>
    {
        IQueryable<Event> GetAllEvents(bool fullyHydrate = true);
    }
}
