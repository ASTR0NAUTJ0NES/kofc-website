using KofCWebsite.Core.Entities.KofC;
using KofCWebSite.Core.Dto.Enums;
using KofCWebSite.Core.Dto.Results;
using KofCWebSite.Core.Interfaces;
using KofCWebSite.UI.Extension;
using KofCWebSite.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using KofCWebSite.Core.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace KofCWebSite.UI.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactsService _ContactsService;
        private readonly IAlbumsService _AlbumsService;
        private readonly IImagesService _ImagesService;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        private readonly SignInManager<IdentityUser> _SignInManager;
        private readonly UserManager<IdentityUser> _UserManager;

        public ContactsController(IContactsService contactsService,
                                  IAlbumsService albumsService, 
                                  IImagesService imagesService,
                                  IWebHostEnvironment hostEnvironment,
                                  SignInManager<IdentityUser> signInManager,
                                  UserManager<IdentityUser> userManager)
        {
            _ContactsService = contactsService;
            _AlbumsService = albumsService;
            _ImagesService = imagesService;
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
            var model = new ContactsListViewModel()
            {
                Title = "Contacts List",
                ThemeName = "Bootstrap"
            };

            model.ShowAdminButtons = _SignInManager.IsSignedIn(User) &&
                User.HasClaim("Admin", "Admin");
            model.Contacts = _ContactsService.GetAllContacts().Take(10);

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

            var contacts = _ContactsService.GetAllContacts();
            int count = contacts.ToList().Count();

            if (string.IsNullOrWhiteSpace(sortfield))
            {
                contacts = sortorder != null && sortorder != ""
                        ? sortorder == "asc"
                            ? contacts.OrderBy(o => o.GetType().GetProperty(sortfield).GetValue(o, null))
                            : contacts.OrderByDescending(o => o.GetType().GetProperty(sortfield).GetValue(o, null))
                        : contacts;
            }
            else
            {
                contacts = contacts.OrderBy(o => o.Lastname).ThenBy(o => o.Firstname);
            }

            dynamic data = new 
            {
                TotalRecords = count,
                Contacts = contacts.Skip(pagesize * pagenum).Take(pagesize).ToArray<Contact>()
            };


            return JsonConvert.SerializeObject(data);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = new ContactDetailsViewModel
            {
                Contact = await _ContactsService.GetContactByIdAsync(id)
            };
            ViewData["Title"] = "Contact Details";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _ContactsService.GetContactByIdAsync(id);
            ContactEditViewModel model = contact != null
                ? contact.ToEditViewModel()
                : new ContactEditViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ContactEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _ContactsService.UpdateContactAsync(model);
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new ContactCreateViewModel()
            {
                Title = "Create A Contact or Member"
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Photo != null && model.Photo.Length > 0)
            {
                if (!model.Photo.IsImage())
                {
                    model.AlertMessage = "The uploaded file is not an image!";
                    return View(model);
                }
            }

            await _ContactsService.InsertContactAsync(model);
            return RedirectToAction("List");

            //var result = _ContactsService.Add(model.Contact);
            //if (result.Status != AddResultStatus.Added)
            //{
            //    model.AlertMessage = result.Status == AddResultStatus.AlreadyExists
            //        ? "A person with that name or Email already exists!"
            //        : result.Status == AddResultStatus.Error
            //            ? "An error occured when attempting to add this contact!"
            //            : "";                

            //    return View(model);
            //}

        }

        [HttpPost]
        public ActionResult<BaseResult> DeleteById([FromForm] int ContactId)
        {

            var result = new BaseResult("success");
            try
            {
               _ContactsService.DeleteContactByIdAsync(ContactId);
            }
            catch (Exception)
            {
                result.ResultMessage = "error";
            }

            return result;
        }
    }
}
