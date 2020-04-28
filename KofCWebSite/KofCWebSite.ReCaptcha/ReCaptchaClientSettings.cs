using System;
using System.Collections.Generic;
using System.Text;

namespace KofCWebSite.ReCaptcha
{
    public class ReCaptchaClientSettings
    {
        public bool IsRecaptchaUsed { get; set; } = true;
        public string Url { get; set; }
        public string SiteKey { get; set; }
        public string SecretKey { get; set; }
        public int ReCaptchaCachingTimeout { get; set; }
    }
}
