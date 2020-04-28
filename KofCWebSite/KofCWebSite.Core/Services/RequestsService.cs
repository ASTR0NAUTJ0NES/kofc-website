using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Interfaces;
using KofCWebSite.Data.Dto.Posts;
using KofCWebSite.Core.Extensions;
using KofCWebSite.Core.Dto.Enums;
using System.Data.Common;
using System.Data.SqlTypes;

namespace KofCWebSite.Core.Services
{
    public class RequestsService : IRequestsService
    {
        private const int PRAYER_REQUEST_ID = 1;
        private const int CONTACT_US_MESSAGE_ID = 3;
        private readonly IKofCRepository<Request> _RequestRepo;
        private readonly IKofCRepository<Contact> _ContactRepo;

        public RequestsService(IKofCRepository<Request> requestRepository, IKofCRepository<Contact> contactRepo)
        {
            _RequestRepo = requestRepository;
            _ContactRepo = contactRepo;
        }

        public async Task<AddResultStatus> InsertAPrayerRequestAsync(PrayerRequest prayerRequest)
        {
            AddResultStatus status = AddResultStatus.Error;
            try
            {
                var contacts = _ContactRepo.GetAll();
                Contact contact = contacts.FirstOrDefault(x => x.Firstname.Trim().ToUpper().Equals(prayerRequest.FirstName.Trim().ToUpper())
                                        && x.Lastname.Trim().ToUpper().Equals(prayerRequest.LastName.Trim().ToUpper())
                                        && x.Email.Trim().ToUpper().Equals(prayerRequest.Email.Trim().ToUpper()));

                //if contact was not found create a new
                if (contact == null)
                {
                    contact = new Contact()
                    {
                        Firstname = prayerRequest.FirstName,
                        Lastname = prayerRequest.LastName,
                        Email = prayerRequest.Email
                    };

                    _ContactRepo.Insert(contact);
                    await _ContactRepo.SaveChangesAsync();
                }

                var request = new Request()
                {
                    ContactId = contact.Id,
                    RequestBody = prayerRequest.RequestDetail,
                    RequestTypeId = PRAYER_REQUEST_ID,
                    Status = (int)RequestStatus.Requested,
                    Subject = "Prayer Request",
                    Receivedon = DateTime.Now
                };

                _RequestRepo.Insert(request);
                await _RequestRepo.SaveChangesAsync();
                status = AddResultStatus.Added;
            }
            catch(DbException)
            {
                status = AddResultStatus.Error;
            }

            return status;
        }

        public async Task<AddResultStatus> InsertAContactUsMessageAsync(ContactUsMessage contactUsMessage)
        {
            AddResultStatus status = AddResultStatus.Error;
            try
            {
                var contacts = _ContactRepo.GetAll();
                Contact contact = contacts.FirstOrDefault(x => x.Firstname.Trim().ToUpper().Equals(contactUsMessage.FirstName.Trim().ToUpper())
                                        && x.Lastname.Trim().ToUpper().Equals(contactUsMessage.LastName.Trim().ToUpper())
                                        && x.Email.Trim().ToUpper().Equals(contactUsMessage.Email.Trim().ToUpper()));

                //if contact was not found create a new
                if (contact == null)
                {
                    contact = new Contact()
                    {
                        Firstname = contactUsMessage.FirstName,
                        Lastname = contactUsMessage.LastName,
                        Email = contactUsMessage.Email
                    };

                    _ContactRepo.Insert(contact);
                    await _ContactRepo.SaveChangesAsync();
                }

                var request = new Request()
                {
                    ContactId = contact.Id,
                    RequestBody = contactUsMessage.MessageDetail,
                    RequestTypeId = CONTACT_US_MESSAGE_ID,
                    Status = (int)RequestStatus.Requested,
                    Subject = "Contact Message",
                    Receivedon = DateTime.Now
                };

                _RequestRepo.Insert(request);
                await _RequestRepo.SaveChangesAsync();
                status = AddResultStatus.Added;
            }
            catch (DbException)
            {
                status = AddResultStatus.Error;
            }

            return status;
        }

        public IEnumerable<Request> GetAllRequests()
        {
            return _RequestRepo.GetAll();
        }

        public async Task<Request> GetRequestByIdAsync(int Id)
        {
            return await _RequestRepo.GetEntityByIdAsync(Id);
        }
    }
}
