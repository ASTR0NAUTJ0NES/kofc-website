using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KofCWebsite.Core.Entities.KofC
{
    public partial class AlbumImage
    {
        public AlbumImage()
        {
        }

        public int Id { get; set; }

        [DisplayName("File Type")]
        [Required]
        public string Filetype { get; set; }

        [DisplayName("Uploaded Image")]
        [Required]
        public byte[] ImageBytes { get; set; }

        [DisplayName("File Name")]
        [Required]
        public string Filename { get; set; }

        [DisplayName("Album")]
        public int? AlbumId { get; set; }
    }
}
