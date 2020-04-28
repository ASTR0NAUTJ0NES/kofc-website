using KofCWebSite.Core.Dto.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KofCWebSite.Core.Models
{
    public class AlbumEditViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }

        [DisplayName("Is Visible")]
        [Required]
        public bool IsVisible { get; set; }

        IFormFile[] Images { get; set; }
    }
}
