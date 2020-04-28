using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Interfaces;
using KofCWebSite.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Services
{
    public class NewslettersService : INewslettersService
    {
        private IKofCRepository<UploadedFile> _UploadedFilesRepository;
        public NewslettersService(IKofCRepository<UploadedFile> UploadedFileRepository)
        {
            _UploadedFilesRepository = UploadedFileRepository;
        }

        public IQueryable<UploadedFile> GetAllNewsletters()
        {
            return _UploadedFilesRepository.GetAll().OrderByDescending(x => x.CreatedOn);
        }

        public async Task<UploadedFile> GetMostRecentNewsletterAsync()
        {
            return await _UploadedFilesRepository.GetAll().OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();
        }

        public async Task<UploadedFile> GetNewsletterByIdAsync(int Id)
        {
            return await _UploadedFilesRepository.GetEntityByIdAsync(Id);
        }

        public async Task<UploadedFile> InsertNewsletterAsync(NewsletterCreateViewModel model)
        {
            var ms = new MemoryStream();
            model.FormFile.CopyTo(ms);
            var newNewsletter = new UploadedFile()
            {
                Filename = model.FormFile.FileName,
                CategoryId = Dto.Enums.UploadedFileCategory.Newsletter,
                Filetype = model.FormFile.ContentType,
                CreatedOn = DateTime.Now,
                Description = model.Description,
                FileBytes = ms.ToArray()
            };

            _UploadedFilesRepository.Insert(newNewsletter);
            await _UploadedFilesRepository.SaveChangesAsync();
            return newNewsletter;
        }

        public async Task DeleteNewsletterByIdAsync(int id)
        {
            _UploadedFilesRepository.DeleteById(id);
            await _UploadedFilesRepository.SaveChangesAsync();
        }

        public async Task UpdateNewsletterAsync(NewsletterEditViewModel model)
        {
            var newsletter = await GetNewsletterByIdAsync(model.Id);

            newsletter.Description = model.Description;

            bool userUploadedANewsletter = (model.FormFile != null);
            if (userUploadedANewsletter)
            {
                var ms = new MemoryStream();
                model.FormFile.CopyTo(ms);
                newsletter.Filename = model.FormFile.FileName;
                newsletter.Filetype = model.FormFile.ContentType;
                newsletter.FileBytes = ms.ToArray();
            }

            await _UploadedFilesRepository.SaveChangesAsync();
        }
    }
}
