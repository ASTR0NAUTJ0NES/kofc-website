using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using KofCWebSite.Data.Dto.Posts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KofCWebSite.Core.Models
{
    public class PrayerRequestViewModel : BaseViewModel
    {
        public PrayerRequestViewModel()
        {

        }

        public PrayerRequestViewModel(HttpContext context)
        {
            this.IsAuthenticated = context.User.Identity.IsAuthenticated;
            this.UserName = context.User.Identity.Name;
        }

        [Required]
        public PrayerRequest PrayerRequest { get; set; }

        public string ReCaptchaSiteKey { get; set; }

        public string ReCaptchaResponse { get; set; }

    }
}
