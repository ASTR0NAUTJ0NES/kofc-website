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
    public class ContactUsViewModel : BaseViewModel
    {
        public ContactUsViewModel()
        {

        }

        public ContactUsViewModel(HttpContext context)
        {
            this.IsAuthenticated = context.User.Identity.IsAuthenticated;
            this.UserName = context.User.Identity.Name;
        }

        [Required]
        public ContactUsMessage ContactUsMessage { get; set; }

        public string ReCaptchaSiteKey { get; set; }

        public string ReCaptchaResponse { get; set; }
    }
}
