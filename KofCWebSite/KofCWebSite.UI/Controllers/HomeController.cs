using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Interfaces;
using KofCWebSite.Core.Models;
using KofCWebSite.Core.Models.Popup;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace KofCWebSite.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactsService _ContactsService;
        private readonly IAlbumsService _AlbumsService;
        private readonly IEventsService _EventsService;
        private readonly INewslettersService _NewslettersService;
        private readonly INewsItemsService _NewsItemsService;
        private readonly SignInManager<IdentityUser> _SignInManager;
        private readonly UserManager<IdentityUser> _UserManager;
        public HomeController(IContactsService contactsService,
                              IAlbumsService albumsService,
                              IEventsService eventsService,
                              INewslettersService newslettersService,
                              INewsItemsService newsItemsService,
                              SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager)
        {
            _ContactsService = contactsService;
            _AlbumsService = albumsService;
            _EventsService = eventsService;
            _NewslettersService = newslettersService;
            _NewsItemsService = newsItemsService;
            _SignInManager = signInManager;
            _UserManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string popupJson)
        {
            var model = new HomeViewModel();
            if (!string.IsNullOrWhiteSpace(popupJson))
            {
                var popupModel = JsonSerializer.Deserialize<PopupModel>(popupJson);
                model.PopupAlert = popupModel;
            }

            model.VisiblePinnedNewsItems = await _NewsItemsService.GetAllVisiblePinnedNewsItems();
            model.UpcomingEvents = await _EventsService.GetAllRecentAndUpcomingEvents(2);
            model.Newsletter = await _NewslettersService.GetMostRecentNewsletterAsync();

            return View(model);
        }

        public IActionResult Contacts()
        {
            var model = new ContactsListViewModel()
            {
                Contacts = _ContactsService.GetAllContacts()
            };
            return View(model);
        }

        public async Task<IActionResult> ContactDetailsAsync(int id)
        {
            Contact model = await _ContactsService.GetContactByIdAsync(id);
            return View(model);
        }

        public IActionResult Privacy()
        {
            ViewData["Version"] = "1.0.0.0";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
