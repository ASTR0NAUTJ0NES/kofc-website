using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KofCWebSite.Core.Dto;
using KofCWebSite.Core.Dto.Results;
using KofCWebSite.Core.Interfaces;
using KofCWebSite.Core.Extensions;
using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Models;
using System.IO;
using System.IO.MemoryMappedFiles;
using SkiaSharp;
using Microsoft.Extensions.Hosting;

namespace KofCWebSite.Core.Services
{
    public class ContactsService : IContactsService
    {
        private IKofCRepository<Contact> _ContactsRepository;
        private IKofCRepository<ContactAddress> _ContactAddressesRepository;
        private IKofCRepository<AlbumImage> _AlbumImageRepository;
        private IKofCRepository<ContactType> _ContactTypeRepository;
        private readonly IHostEnvironment _HostingEnvironment;
        public ContactsService(IKofCRepository<Contact> contactsRepository, 
                               IKofCRepository<ContactType> contactTypeRepository, 
                               IKofCRepository<ContactAddress> contactAddressesRepository,
                               IKofCRepository<AlbumImage> albumImageRepository,
                               IHostEnvironment hostEnvironment)
        {
            _ContactsRepository         = contactsRepository;
            _ContactTypeRepository      = contactTypeRepository;
            _ContactAddressesRepository = contactAddressesRepository;
            _AlbumImageRepository       = albumImageRepository;
            _HostingEnvironment         = hostEnvironment;
        }

        public IEnumerable<Contact> GetAllContacts(bool activeContactsOnly = false)
        {
            return activeContactsOnly ? _ContactsRepository.Items.Where(x => x.Status == 'A') : _ContactsRepository.Items;
        }

        public async Task<Contact> GetContactByIdAsync(int Id)
        {
            var contact = await Task.FromResult(_ContactsRepository.GetAll().FirstOrDefault(x => x.Id == Id));

            if (contact == null) return contact;

            //if there is no image for the contact then use the no-contact-image.png
            if (contact.AlbumImage == null)
            {
                if (contact.AlbumImageId != null)
                {
                    contact.AlbumImage = await _AlbumImageRepository.GetEntityByIdAsync(contact.AlbumImageId.Value);
                }
                else
                {
                    var webRoot = _HostingEnvironment.ContentRootPath;
                    var noImageFile = Path.Combine(_HostingEnvironment.ContentRootPath, "wwwroot", "images", "no-contact-image.png");

                    using var fs = new FileStream(noImageFile, FileMode.Open, FileAccess.Read);
                    byte[] bytes = System.IO.File.ReadAllBytes(noImageFile);
                    fs.Read(bytes, 0, System.Convert.ToInt32(fs.Length));
                    fs.Close();
                    contact.AlbumImage = new AlbumImage()
                    {
                        Filename = "no-contact-image.png",
                        Filetype = "image/png",
                        ImageBytes = bytes
                    };
                }
            }
            return contact; 
        }

        public void DeleteContactByIdAsync(int id)
        {
            _ContactsRepository.DeleteById(id);
            _ContactsRepository.SaveChanges();
        }

        //public AddContactResult Add(Contact contact)
        //{
        //    try
        //    {
        //        var existingContact = _ContactsRepository.GetAll().FirstOrDefault(x => 
        //                                            x.IsFullNameTrimmedEqual(contact.Firstname, contact.Lastname, contact.Middlename, contact.Suffix)                                                    
        //                                            ||
        //                                            x.Email.IsTrimmedEquals(contact.Email));

        //        if (existingContact != null)
        //        {
        //            return new AddContactResult()
        //            {
        //                Status = Dto.Enums.AddResultStatus.AlreadyExists,
        //                Entity = existingContact
        //            };
        //        }

        //        if (contact.AlbumImage != null)
        //        {
        //            var albumImage = new AlbumImage()
        //            {
        //                Filename = contact.AlbumImage.Filename,
        //                Filetype = contact.AlbumImage.Filetype,
        //                ImageBytes = contact.AlbumImage.ImageBytes
        //            };
        //            _AlbumImageRepository.Insert(albumImage);
        //            _AlbumImageRepository.SaveChanges();
        //            contact.AlbumImageId = albumImage.Id;
        //        }
        //        _ContactsRepository.Insert(contact);
        //        _ContactsRepository.SaveChanges();

        //        return new AddContactResult()
        //        {
        //            Status = Dto.Enums.AddResultStatus.Added,
        //            Entity = contact
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new AddContactResult()
        //        {
        //            Status = Dto.Enums.AddResultStatus.Error,
        //            Entity = contact,
        //            Exception = ex
        //        };
        //    }
        //}

        public async Task<Contact> InsertContactAsync(ContactCreateViewModel model)
        {
            var ms = new MemoryStream();
            model.Photo.CopyTo(ms);
            var newContact = new Contact()
            {
                Firstname = model.Firstname,
                Middlename = model.Middlename,
                Lastname = model.Lastname,
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                HomePhone = model.HomePhone,
                CellPhone = model.CellPhone,
                Occupation = model.Occupation,
                Suffix = model.Suffix,
                ContactAddress = new ContactAddress()
                {
                    Address1 = model.Address1,
                    Address2 = model.Address2,
                    City = model.City,
                    State = model.State,
                    Zip = model.Zip,
                    IsPrimary = true
                },
                AlbumImage = new AlbumImage()
                {
                    Filename = model.Photo.FileName,
                    Filetype = model.Photo.ContentType,
                    ImageBytes = ms.ToArray()
                }
            };

            _ContactsRepository.Insert(newContact);
            await _ContactsRepository.SaveChangesAsync();
            return newContact;
        }

        public async Task UpdateContactAsync(ContactEditViewModel model)
        {
            var contact = await GetContactByIdAsync(model.Id);

            contact.Firstname = model.Firstname;
            contact.Middlename = model.Middlename;
            contact.Lastname = model.Lastname;
            contact.CellPhone = model.CellPhone;
            contact.HomePhone = model.HomePhone;
            contact.DateOfBirth = model.DateOfBirth;
            contact.Email = model.Email;
            contact.Occupation = model.Occupation;
            contact.Suffix = model.Suffix;
            if (contact.ContactAddress == null)
                contact.ContactAddress = new ContactAddress();

            contact.ContactAddress.Address1 = model.Address1;
            contact.ContactAddress.Address2 = model.Address2;
            contact.ContactAddress.City = model.City;
            contact.ContactAddress.State = model.State;
            contact.ContactAddress.Zip = model.Zip;

            bool userUploadedAnImage = (model.Photo != null);
            if (userUploadedAnImage)
            {
                if (contact.AlbumImage == null)
                {
                    contact.AlbumImage = new AlbumImage();
                }

                var ms = new MemoryStream();
                model.Photo.CopyTo(ms);
                contact.AlbumImage.Filename = model.Photo.FileName;
                contact.AlbumImage.Filetype = model.Photo.ContentType;
                contact.AlbumImage.ImageBytes = ms.ToArray();
            }

            await _ContactsRepository.SaveChangesAsync();
        }

        public async Task<Contact> FindContactByEmailAsync(string email)
        {
            var contact = await Task.FromResult(_ContactsRepository.GetAll().FirstOrDefault(x => x.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase)));
            if (contact == null) return null;

            return await GetContactByIdAsync(contact.Id);
        }

        private async Task<AlbumImage> LoadAlbumImageAsync(int? albumId)
        {
            AlbumImage returnImage;
            if (albumId != null)
            {
                returnImage = await _AlbumImageRepository.GetEntityByIdAsync(albumId.Value);
            }
            else
            {
                var webRoot = _HostingEnvironment.ContentRootPath;
                var noImageFile = Path.Combine(_HostingEnvironment.ContentRootPath, "wwwroot", "images", "no-contact-image.png");

                using var fs = new FileStream(noImageFile, FileMode.Open, FileAccess.Read);
                byte[] bytes = System.IO.File.ReadAllBytes(noImageFile);
                fs.Read(bytes, 0, System.Convert.ToInt32(fs.Length));
                fs.Close();
                returnImage = new AlbumImage()
                {
                    Filename = "no-contact-image.png",
                    Filetype = "image/png",
                    ImageBytes = bytes
                };
            }
            return returnImage;
        }

        private ContactAddress LoadContactAddress(int Id)
        {
            return _ContactAddressesRepository.GetAll().FirstOrDefault(x => x.ContactId == Id && x.Status == 'A');
        }
    }
}
