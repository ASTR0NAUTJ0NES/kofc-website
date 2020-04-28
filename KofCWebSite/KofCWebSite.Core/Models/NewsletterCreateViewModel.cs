using KofCWebSite.Core.Dto.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KofCWebSite.Core.Models
{
    public class NewsletterCreateViewModel : BaseViewModel
    {
        [DisplayName("Newsletter")]
        [Required]
        public IFormFile FormFile { get; set; }

        [StringLength(512)]
        [Required]
        public string Description { get; set; }
    }
}
