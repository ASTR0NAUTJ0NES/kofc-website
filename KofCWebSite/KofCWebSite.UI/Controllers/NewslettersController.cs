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
    public class NewslettersController : Controller
    {
        private readonly INewslettersService _NewslettersService;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        private readonly SignInManager<IdentityUser> _SignInManager;
        private readonly UserManager<IdentityUser> _UserManager;
        public NewslettersController(INewslettersService newslettersService,
                                    IWebHostEnvironment webHostEnvironment,
                                    SignInManager<IdentityUser> signInManager,
                                    UserManager<IdentityUser> userManager)
        {
            _NewslettersService = newslettersService;
            _WebHostEnvironment = webHostEnvironment;
            _SignInManager = signInManager;
            _UserManager = userManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            var model = new NewsletterListViewModel()
            {
                Title = "Newsletters List",
                ThemeName = "Bootstrap"
            };

            model.IsAdmin = _SignInManager.IsSignedIn(User) &&
                User.HasClaim("Admin", "Admin");
            model.IsContributor = _SignInManager.IsSignedIn(User) &&
                User.HasClaim("Contributor", "Contributor");
            model.Newsletters = _NewslettersService.GetAllNewsletters();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new NewsletterCreateViewModel()
            {
                Title = "Upload A Newsletter"
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsletterCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            //if (model.Photo != null && model.Photo.Length > 0)
            //{
            //    if (!model.Photo.IsImage())
            //    {
            //        model.AlertMessage = "The uploaded file is not an image!";
            //        return View(model);
            //    }
            //}

            await _NewslettersService.InsertNewsletterAsync(model);
            return RedirectToAction("List");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var newsletter = await _NewslettersService.GetNewsletterByIdAsync(id);
            NewsletterEditViewModel model = newsletter != null
                ? newsletter.ToEditViewModel()
                : new NewsletterEditViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NewsletterEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _NewslettersService.UpdateNewsletterAsync(model);
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Download(int id)
        {
            var fileData = _NewslettersService.GetNewsletterByIdAsync(id).GetAwaiter().GetResult();

            if (fileData == null)
                return RedirectToAction("List");

            return File(fileData.FileBytes, fileData.Filetype, fileData.Filename);
        }

        [HttpGet]
        [Authorize]
        public ActionResult<BaseResult> DeleteById(int Id)
        {
            var result = new BaseResult("success");
            try
            {
                _NewslettersService.DeleteNewsletterByIdAsync(Id).GetAwaiter().GetResult();

            }
            catch (Exception)
            {
                result.ResultMessage = "error";
            }

            return result;
        }
    }
}
