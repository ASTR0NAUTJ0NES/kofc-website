using KofCWebSite.ReCaptcha.Dto.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Interfaces
{
    public interface IReCaptchaService
    {
        Task<SiteVerifyResult> VerifyReCaptchaAsync(string gReCaptchaResponse);
    }
}
