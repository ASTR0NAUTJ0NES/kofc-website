using KofCWebsite.Core.Entities.KofC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Models
{
    public class ContactsListViewModel : BaseViewModel
    {
        public IEnumerable<Contact> Contacts { get; set; }

        public bool ShowAdminButtons { get; set; }
    }
}
