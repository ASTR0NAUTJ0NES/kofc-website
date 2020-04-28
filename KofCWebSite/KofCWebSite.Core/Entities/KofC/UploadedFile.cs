using KofCWebSite.Core.Dto.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KofCWebsite.Core.Entities.KofC
{
    public partial class UploadedFile
    {
        public UploadedFile()
        {
        }

        public int Id { get; set; }

        [DisplayName("File Type")]
        [Required]
        public string Filetype { get; set; }

        [DisplayName("Uploaded File")]
        [Required]
        public byte[] FileBytes { get; set; }

        [DisplayName("File Name")]
        [Required]
        public string Filename { get; set; }

        [DisplayName("Category")]
        [Required]
        public UploadedFileCategory CategoryId { get; set; }

        [DisplayName("Uploaded On")]
        [Required]
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }
    }
}
