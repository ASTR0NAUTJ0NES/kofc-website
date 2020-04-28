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
    public class EventCreateViewModel : BaseViewModel
    {
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

        [DisplayName("Address 1")]
        [StringLength(50)]
        public string Address1 { get; set; }

        [DisplayName("Address 2")]
        [StringLength(50)]
        public string Address2 { get; set; }

        [StringLength(45)]
        public string City { get; set; }

        [StringLength(2)]
        public string State { get; set; }

        [StringLength(10)]
        [RegularExpression(@"^\d{5}(-?\d{4})?$",ErrorMessage = "Not a valid Zip code.")]
        public string Zip { get; set; }

        [DisplayName("Contact Id")]
        public int? ContactId { get; set; }

        [DisplayName("Event Title")]
        [Required]
        [StringLength(128)]
        public string EventTitle { get; set; }
    }
}
