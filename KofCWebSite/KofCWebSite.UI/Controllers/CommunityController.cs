using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KofCWebSite.Data.Dto.Posts;
using KofCWebSite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using KofCWebSite.Core.Interfaces;
using KofCWebSite.Core.Models.Popup;
using Microsoft.AspNetCore.Routing;
using System.Text.Json;
using KofCWebSite.ReCaptcha.enums;
using KofCWebSite.ReCaptcha.Dto.Results;

namespace KofCWebSite.UI.Controllers
{
    public class CommunityController : Controller
    {
        private readonly IRequestsService _RequestsService;
        private readonly IReCaptchaService _ReCaptchaService;
        private readonly INewsItemsService _NewsItemsService;

        public CommunityController(IRequestsService requestsService,
                                   IReCaptchaService reCaptchaService,
                                   INewsItemsService newsItemsService)
        {
            _RequestsService = requestsService;
            _ReCaptchaService = reCaptchaService;
            _NewsItemsService = newsItemsService;
        }

        public async Task<IActionResult> NewsList()
        {
            var model = new NewsItemsCommunityListViewModel()
            {
                Title = "News",
                ThemeName = "Bootstrap"
            };

            model.VisibleNewsItems = await _NewsItemsService.GetAllVisibleNewsItems();

            return View(model);
        }

        [HttpGet]
        public IActionResult PrayerRequest()
        {
            var context = HttpContext;
            var model = new PrayerRequestViewModel(context)
            {
                ReCaptchaSiteKey = "6LdO2OQUAAAAADb3Nim540MD-o81QEZ5UxTGkgDe"
            };
            model.Title = "Submit a Prayer Request";
            model.PrayerRequest = new PrayerRequest();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PrayerRequest(PrayerRequestViewModel requestModel)
        {
            requestModel.Title = "Submit a Prayer Request";
            SiteVerifyResult reCaptchaVerifyResult = null;

            if (requestModel == null)
                ModelState.AddModelError("", "requestModel was Null");
            else if (requestModel.ReCaptchaResponse == null)
                ModelState.AddModelError("", "requestModel.ReCaptchaResponse was Null");
            else
            {
                try
                {
                    reCaptchaVerifyResult = await _ReCaptchaService.VerifyReCaptchaAsync(requestModel.ReCaptchaResponse);
                }
                catch (Exception)
                {

                }
            }


            if (reCaptchaVerifyResult != null)
            {
                if (reCaptchaVerifyResult.ResultStatus != ReCaptcha.enums.ResultStatus.Ok || reCaptchaVerifyResult.IsVerified == false)
                {
                    var resultCd = reCaptchaVerifyResult.ResultStatus switch
                    {
                        ResultStatus.Ok => "Ok",
                        ResultStatus.BadRequest => "Bad Request",
                        ResultStatus.Conflict => "Conflict",
                        ResultStatus.Error => "Error",
                        ResultStatus.NotFound => "Not Found",
                        ResultStatus.Unauthorized => "Unauthorized",
                        _ => "Unknown"
                    };

                    ModelState.AddModelError(string.Empty, resultCd + " " + reCaptchaVerifyResult.ResultStatusMessages[0].Message);
                }
            }

            if (!ModelState.IsValid)
                return View(requestModel);

            await _RequestsService.InsertAPrayerRequestAsync(requestModel.PrayerRequest);

            var msg = "Thank you for your prayer request.  Your request will be reviewed by a knight and included in our group prayer at our council meeting. ";
            msg += "If your request was marked urgent, your request may be forwarded to brother knights to include with their individual prayer intentions.";

            var postSavePopup = new PopupModel()
            {
                Title = "Prayer Request Submitted",
                Body = msg,
                Buttons = new PopupButtonModel[]
                {
                    new PopupButtonModel(Core.Enums.ButtonType.Close)
                }
            };

            var _popupJson = JsonSerializer.Serialize(postSavePopup);

            return RedirectToAction("Index", new RouteValueDictionary(
                new { controller = "Home", action = "Index", popupJson = _popupJson }));
        }

    }
}
