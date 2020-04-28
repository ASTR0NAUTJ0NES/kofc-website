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
    public class AboutController : Controller
    {
        private readonly IRequestsService _RequestsService;
        private readonly IReCaptchaService _ReCaptchaService;

        public AboutController(IRequestsService requestsService,
                                   IReCaptchaService reCaptchaService)
        {
            _RequestsService = requestsService;
            _ReCaptchaService = reCaptchaService;
        }

        public IActionResult FourPrinciples()
        {
            var model = new BaseViewModel()
            {
                Title = "Four Principles of The Knights"
            };
            return View(model);
        }

        public IActionResult CouncilFounders()
        {
            var model = new BaseViewModel()
            {
                Title = "Council Founders"
            };
            return View(model);
        }

        public IActionResult History()
        {
            var model = new BaseViewModel()
            {
                Title = "History of The Knights"
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult ContactUs()
        {
            var context = HttpContext;
            var model = new ContactUsViewModel(context)
            {
                ReCaptchaSiteKey = "6LdO2OQUAAAAADb3Nim540MD-o81QEZ5UxTGkgDe"
            };
            model.Title = "Contact Us";
            model.ContactUsMessage = new ContactUsMessage();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ContactUs(ContactUsViewModel requestModel)
        {
            requestModel.Title = "Contact Us";
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

            await _RequestsService.InsertAContactUsMessageAsync(requestModel.ContactUsMessage);

            var msg = "Thank you for your contact request.  Your request will be reviewed by a knight and addressed as soon as possible. ";

            var postSavePopup = new PopupModel()
            {
                Title = "Contact Request Submitted",
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

        public IActionResult Links()
        {
            var model = new BaseViewModel()
            {
                Title = "Links"
            };
            return View(model);
        }
    }
}
