using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KofCWebSite.Core.Models
{
    public class EventEditViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [DisplayName("Event Start")]
        [Required]
        public DateTime EventStart { get; set; }

        [DisplayName("Event End")]
        [Required]
        public DateTime EventEnd { get; set; }

        public List<SelectListItem> Contacts { get; set; }

        [Required]
        public string Description { get; set; }

        [StringLength(512)]
        public string Location { get; set; }

        [StringLength(50)]
        [DisplayName("Address 1")]
        public string Address1 { get; set; }

        [StringLength(50)]
        [DisplayName("Address 2")]
        public string Address2 { get; set; }

        [StringLength(45)]
        public string City { get; set; }

        [StringLength(2)]
        public string State { get; set; }

        [StringLength(10)]
        [RegularExpression(@"^\d{5}(-?\d{4})?$", ErrorMessage = "Not a valid Zip code.")]
        public string Zip { get; set; }

        [DisplayName("Contact Id")]
        public int? ContactId { get; set; }

        [DisplayName("Event Title")]
        [Required]
        [StringLength(128)]
        public string EventTitle { get; set; }
    }
}
