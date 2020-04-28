using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace KofCWebSite.ReCaptcha.Dto.Posts
{
    public class SiteVerifyPost
    {
        [JsonProperty("response")]
        public string RecaptchaResponseToken { get; set; }

        [JsonProperty("secret")]
        public string SecretKey { get; set; }
    }
}
