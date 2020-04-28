using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Data.Dto.Posts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KofCWebSite.Core.Dto.Enums;

namespace KofCWebSite.Core.Interfaces
{
    public interface IRequestsService
    {
        IEnumerable<Request> GetAllRequests();
        Task<Request> GetRequestByIdAsync(int Id);
        Task<AddResultStatus> InsertAPrayerRequestAsync(PrayerRequest prayerRequest);
        Task<AddResultStatus> InsertAContactUsMessageAsync(ContactUsMessage contactUsMessage);

    }
}
