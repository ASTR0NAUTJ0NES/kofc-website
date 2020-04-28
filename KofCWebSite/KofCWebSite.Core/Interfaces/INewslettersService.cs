using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Interfaces
{
    public interface INewslettersService
    {
        IQueryable<UploadedFile> GetAllNewsletters();
        Task<UploadedFile> GetMostRecentNewsletterAsync();
        Task<UploadedFile> GetNewsletterByIdAsync(int Id);
        Task<UploadedFile> InsertNewsletterAsync(NewsletterCreateViewModel model);
        Task UpdateNewsletterAsync(NewsletterEditViewModel model);
        Task DeleteNewsletterByIdAsync(int id);
    }
}
