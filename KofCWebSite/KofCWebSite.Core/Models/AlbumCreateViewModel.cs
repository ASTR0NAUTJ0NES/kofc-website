using KofCWebSite.Core.Dto.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KofCWebSite.Core.Models
{
    public class AlbumCreateViewModel : BaseViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayName("Is Visible")]
        [Required]
        public bool IsVisible { get; set; }
    }
}
