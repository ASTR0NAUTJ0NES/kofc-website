using KofCWebsite.Core.Entities.KofC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KofCWebSite.Core.Models
{
    public class ContactEditViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [StringLength(45)]
        [DisplayName("First Name")]
        [Required]
        public string Firstname { get; set; }

        [StringLength(45)]
        [DisplayName("Middle Name")]
        public string Middlename { get; set; }

        [StringLength(45)]
        [DisplayName("Last Name")]
        [Required]
        public string Lastname { get; set; }

        [StringLength(15)]
        [DisplayName("Suffix")]
        public string Suffix { get; set; }

        [StringLength(128)]
        [DisplayName("Occupation")]
        public string Occupation { get; set; }

        [DisplayName("Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [DisplayName("Email Address")]
        [EmailAddress]
        [Required]
        [StringLength(128)]
        public string Email { get; set; }

        [StringLength(10)]
        [DisplayName("Home Phone")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Home Phone must be a Valid Phone Number")]
        public string HomePhone { get; set; }

        [StringLength(10)]
        [DisplayName("Cell Phone")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Cell Phone must be a Valid Phone Number")]
        public string CellPhone { get; set; }

        [StringLength(50)]
        [Required]
        [DisplayName("Address 1")]
        public string Address1 { get; set; }

        [StringLength(50)]
        [DisplayName("Address 2")]
        public string Address2 { get; set; }

        [StringLength(45)]
        [Required]
        public string City { get; set; }

        [StringLength(2)]
        [Required]
        public string State { get; set; }

        [StringLength(10)]
        [Required]
        public string Zip { get; set; }

        [DisplayName("Contact Picture")]
        public AlbumImage AlbumImage { get; set; }

        public IFormFile Photo { get; set; }
    }
}
