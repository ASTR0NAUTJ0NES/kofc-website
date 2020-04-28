using KofCWebSite.Core.Dto.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KofCWebSite.Core.Models
{
    public class NewsletterEditViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [DisplayName("Newsletter")]
        public IFormFile FormFile { get; set; }

        [StringLength(512)]
        [Required]
        public string Description { get; set; }

        public string Filename { get; set; }
    }
}
