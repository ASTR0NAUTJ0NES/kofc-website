using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Dto.Enums;
using KofCWebSite.Core.Dto.Results;
using KofCWebSite.Core.Interfaces;
using KofCWebSite.UI.Extension;
using KofCWebSite.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using KofCWebSite.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace KofCWebSite.UI.Controllers
{
    public class EventsController : Controller
    {
        private readonly IAlbumsService _AlbumsService;
        private readonly IEventsService _EventsService;
        private readonly IContactsService _ContactsService;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        private readonly SignInManager<IdentityUser> _SignInManager;
        private readonly UserManager<IdentityUser> _UserManager;

        public EventsController(IAlbumsService albumsService,
                                IEventsService eventsService,
                                IContactsService contactsService,
                                IWebHostEnvironment hostEnvironment,
                                SignInManager<IdentityUser> signInManager,
                                UserManager<IdentityUser> userManager)
        {
            _AlbumsService = albumsService;
            _EventsService = eventsService;
            _ContactsService = contactsService;
            _WebHostEnvironment = hostEnvironment;
            _SignInManager = signInManager;
            _UserManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        [Authorize]
        public IActionResult List()
        {
            var model = new EventsListViewModel()
            {
                Title = "Events List",
                ThemeName = "Bootstrap"
            };

            model.ShowAdminButtons = _SignInManager.IsSignedIn(User) &&
                User.HasClaim("Admin", "Admin");
            model.Events = _EventsService.GetAllEvents().Take(10);

            return View(model);
        }

        [HttpPost]
        public string GetJqPageData(string jsonData)
        {
            if (jsonData is null)
            {
                throw new ArgumentNullException(nameof(jsonData));
            }

            dynamic postObj = JsonConvert.DeserializeObject(jsonData);
            int pagesize = postObj.pagesize;
            int pagenum = postObj.pagenum;
            string sortorder = postObj.sortorder;
            string sortfield = postObj.sortfield;

            var events = _EventsService.GetAllEvents();
            int count = events.ToList().Count();

            if (string.IsNullOrWhiteSpace(sortfield))
            {
                events = sortorder != null && sortorder != ""
                        ? sortorder == "asc"
                            ? events.OrderBy(o => o.GetType().GetProperty(sortfield).GetValue(o, null))
                            : events.OrderByDescending(o => o.GetType().GetProperty(sortfield).GetValue(o, null))
                        : events;
            }
            else
            {
                events = events.OrderBy(o => o.EventStart);
            }

            dynamic data = new
            {
                TotalRecords = count,
                Events = events.Skip(pagesize * pagenum).Take(pagesize).ToArray<Event>()
            };

            var jsonStr = JsonConvert.SerializeObject(data);
            return jsonStr;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = new EventDetailsViewModel
            {
                Event = await _EventsService.GetEventByIdAsync(id)
            };

            model.Contacts = new List<SelectListItem>();
            model.Contacts.Add(new SelectListItem("", "", true, false));
            var contacts = _ContactsService.GetAllContacts();

            foreach (var c in contacts)
            {
                model.Contacts.Add(new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Firstname + " " + c.Lastname,
                    Selected = c.Id == model.Event.Contactid,
                });
            }
            ViewData["Title"] = "Event Details";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var _event = await _EventsService.GetEventByIdAsync(id);
            EventEditViewModel model = _event != null
                ? _event.ToEditViewModel()
                : new EventEditViewModel();

            model.Contacts = new List<SelectListItem>();
            model.Contacts.Add(new SelectListItem("", "", true, false));
            var contacts = _ContactsService.GetAllContacts();

            foreach (var c in contacts)
            {
                model.Contacts.Add(new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Firstname + " " + c.Lastname,
                    Selected = c.Id == model.ContactId,
                });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EventEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _EventsService.UpdateEventAsync(model);
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new EventCreateViewModel()
            {
                Title = "Create An Event"
            };

            model.Contacts = new List<SelectListItem>();
            model.Contacts.Add(new SelectListItem("", "", true, false));
            var contacts = _ContactsService.GetAllContacts();

            foreach (var c in contacts)
            {
                model.Contacts.Add(new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Firstname + " " + c.Lastname
                });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _EventsService.InsertEventAsync(model);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult<BaseResult> DeleteById([FromForm] int EventId)
        {

            var result = new BaseResult("success");
            try
            {
                _EventsService.DeleteEventByIdAsync(EventId);
            }
            catch (Exception)
            {
                result.ResultMessage = "error";
            }

            return result;
        }
    }
}
