using KofCWebSite.Core.Interfaces;
using KofCWebSite.ReCaptcha;
using KofCWebSite.ReCaptcha.Dto.Posts;
using KofCWebSite.ReCaptcha.Dto.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Services
{
    public class ReCaptchaService : IReCaptchaService
    {
        private readonly IGoogleReCaptchaClient _reCaptchaClient;
        private readonly ReCaptchaClientSettings _reCaptchaClientSettings;

        public ReCaptchaService(IGoogleReCaptchaClient reCaptchaClient)
        {
            _reCaptchaClient = reCaptchaClient;
            _reCaptchaClientSettings = new ReCaptchaClientSettings()
            {
                IsRecaptchaUsed = true,
                ReCaptchaCachingTimeout = 30,
                SiteKey = "[site key here]",
                SecretKey = "[secret key here]"
            };
        }

        public async Task<SiteVerifyResult> VerifyReCaptchaAsync(string gReCaptchaResponse)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(gReCaptchaResponse))
                    return new SiteVerifyResult(ReCaptcha.enums.ResultStatus.BadRequest, new ReCaptcha.Models.ResultErrorMessage("", $"{nameof(gReCaptchaResponse)} must be supplied."));

                var post = new SiteVerifyPost()
                {
                    RecaptchaResponseToken = gReCaptchaResponse,
                    SecretKey = _reCaptchaClientSettings.SecretKey                    
                };

                var result = await _reCaptchaClient.VerifyAsync(post);
                if (result == null)
                    return new SiteVerifyResult(ReCaptcha.enums.ResultStatus.BadRequest, new ReCaptcha.Models.ResultErrorMessage("", "Unable to process request. result was null"));

                if (result.ResultStatus == ReCaptcha.enums.ResultStatus.Error)
                {
                    return new SiteVerifyResult(ReCaptcha.enums.ResultStatus.Error, result.ResultStatusMessages);
                }

                return result;
            }
            catch(Exception ex)
            {
                return new SiteVerifyResult(ReCaptcha.enums.ResultStatus.BadRequest, new ReCaptcha.Models.ResultErrorMessage("", "Unable to process request. " + ex.Message));
            }
        }
    }
}
