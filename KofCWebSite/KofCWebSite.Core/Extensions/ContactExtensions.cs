using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KofCWebSite.Core.Extensions
{
    public static class ContactExtensions
    {
        public static ContactEditViewModel ToEditViewModel(this Contact contact)
        {
            return new ContactEditViewModel()
            {
                Address1 = contact.ContactAddress?.Address1,
                Address2 = contact.ContactAddress?.Address2,
                AlbumImage = contact.AlbumImage,
                CellPhone = contact.CellPhone,
                City = contact.ContactAddress?.City,
                DateOfBirth = contact.DateOfBirth,
                Email = contact.Email,
                Firstname = contact.Firstname,
                HomePhone = contact.HomePhone,
                Lastname = contact.Lastname,
                Middlename = contact.Middlename,
                Occupation = contact.Occupation,
                State = contact.ContactAddress?.State,
                Zip = contact.ContactAddress?.Zip,
                Id = contact.Id,
                Suffix = contact.Suffix,
                Title = "Edit Contact"
            };
        }
    }
}
