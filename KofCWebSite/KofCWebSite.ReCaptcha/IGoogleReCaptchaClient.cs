using KofCWebSite.ReCaptcha.Dto.Posts;
using KofCWebSite.ReCaptcha.Dto.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KofCWebSite.ReCaptcha
{
    public interface IGoogleReCaptchaClient
    {
        Task<SiteVerifyResult> VerifyAsync(SiteVerifyPost post);
    }
}
