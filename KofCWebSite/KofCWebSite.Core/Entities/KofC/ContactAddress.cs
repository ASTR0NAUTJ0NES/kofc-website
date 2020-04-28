using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KofCWebsite.Core.Entities.KofC
{
    public class ContactAddress
    {
        public int Id { get; set; }

        [Required]
        public int ContactId { get; set; }

        [Required]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Zip { get; set; }

        public char Status { get; set; }

        [Required]
        public bool IsPrimary { get; set; }

        [JsonIgnore]
        public Contact Contact { get; set; }
    }
}
