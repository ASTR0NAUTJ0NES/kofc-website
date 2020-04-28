using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Dto.Results;
using KofCWebSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Interfaces
{
    public interface IContactsService 
    {
        IEnumerable<Contact> GetAllContacts(bool activeContactsOnly = false);
        Task<Contact> GetContactByIdAsync(int Id);
        void DeleteContactByIdAsync(int id);
        //AddContactResult Add(Contact contact);
        Task<Contact> InsertContactAsync(ContactCreateViewModel model);
        Task UpdateContactAsync(ContactEditViewModel model);
        Task<Contact> FindContactByEmailAsync(string email);
    }
}
