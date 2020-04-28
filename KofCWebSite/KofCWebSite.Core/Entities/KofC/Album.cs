using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KofCWebsite.Core.Entities.KofC
{
    public partial class Album
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayName("Is Visible")]
        [Required]
        public bool IsVisible { get; set; }

        public AlbumImage[] AlbumImages { get; set; }
    }
}
