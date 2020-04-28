using KofCWebsite.Core.Entities.KofC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KofCWebSite.Core.Models
{
    public class NewsItemCreateViewModel : BaseViewModel
    {
        [StringLength(256)]
        [Required]
        public string Headline { get; set; }

        [DisplayName("Posted On")]
        [Required]
        public DateTime Createdon { get; set; }

        [Required]
        public string Article { get; set; }

        [DisplayName("Is Visible")]
        [Required]
        public bool IsVisible { get; set; }

        [DisplayName("Publish Start")]
        public DateTime? PublishStart { get; set; }

        [DisplayName("Publish Stop")]
        public DateTime? PublishStop { get; set; }

        [DisplayName("Is Pinned")]
        public bool IsPinned { get; set; }

        [StringLength(45)]
        [DisplayName("First Name")]
        [Required]
        public string AuthorFirstName { get; set; }

        [StringLength(45)]
        [DisplayName("Middle Name")]
        public string AuthorMiddleName { get; set; }

        [StringLength(45)]
        [DisplayName("Last Name")]
        [Required]
        public string AuthorLastName { get; set; }
    }
}
