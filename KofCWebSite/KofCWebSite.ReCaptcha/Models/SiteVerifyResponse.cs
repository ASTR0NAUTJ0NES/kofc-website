using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace KofCWebSite.ReCaptcha.Models
{
    public class SiteVerifyResponse
    {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "challenge_ts")]
        public DateTime ChallengeTimestamp { get; set; }
        
        [JsonProperty(PropertyName = "hostname")]
        public string HostName { get; set; }
        
        [JsonProperty(PropertyName = "error-codes")]
        public string[] ErrorCodes { get; set; }
    }
}
