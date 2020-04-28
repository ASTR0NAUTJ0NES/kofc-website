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
    public class NewsItemsController : Controller
    {
        private readonly INewsItemsService _NewsItemsService;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        private readonly SignInManager<IdentityUser> _SignInManager;
        private readonly UserManager<IdentityUser> _UserManager;

        public NewsItemsController(INewsItemsService newsItemsService,
                                IWebHostEnvironment hostEnvironment,
                                SignInManager<IdentityUser> signInManager,
                                UserManager<IdentityUser> userManager)
        {
            _NewsItemsService = newsItemsService;
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
            var model = new NewsItemsListViewModel()
            {
                Title = "News Items List",
                ThemeName = "Bootstrap"
            };

            model.ShowAdminButtons = _SignInManager.IsSignedIn(User) &&
                User.HasClaim("Admin", "Admin");
            model.NewsItems = _NewsItemsService.GetAllNewsItems().Take(10);

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

            var newsItems = _NewsItemsService.GetAllNewsItems();
            int count = newsItems.ToList().Count();

            if (string.IsNullOrWhiteSpace(sortfield))
            {
                newsItems = sortorder != null && sortorder != ""
                        ? sortorder == "asc"
                            ? newsItems.OrderBy(o => o.GetType().GetProperty(sortfield).GetValue(o, null))
                            : newsItems.OrderByDescending(o => o.GetType().GetProperty(sortfield).GetValue(o, null))
                        : newsItems;
            }
            else
            {
                newsItems = newsItems.OrderByDescending(o => o.Createdon);
            }

            dynamic data = new
            {
                TotalRecords = count,
                NewsItems = newsItems.Skip(pagesize * pagenum).Take(pagesize).ToArray<NewsItem>()
            };

            var jsonStr = JsonConvert.SerializeObject(data);
            return jsonStr;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = new NewsItemDetailsViewModel
            {
                NewsItem = await _NewsItemsService.GetNewsItemByIdAsync(id)
            };

            ViewData["Title"] = "News Item Details";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var newsItem = await _NewsItemsService.GetNewsItemByIdAsync(id);
            NewsItemEditViewModel model = newsItem != null
                ? newsItem.ToEditViewModel()
                : new NewsItemEditViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NewsItemEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _NewsItemsService.UpdateNewsItemAsync(model);
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new NewsItemCreateViewModel()
            {
                Title = "Create A News Item"
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsItemCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _NewsItemsService.InsertNewsItemAsync(model);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult<BaseResult> DeleteById([FromForm] int NewsItemId)
        {

            var result = new BaseResult("success");
            try
            {
                _NewsItemsService.DeleteNewsItemByIdAsync(NewsItemId);
            }
            catch (Exception)
            {
                result.ResultMessage = "error";
            }

            return result;
        }
    }
}
