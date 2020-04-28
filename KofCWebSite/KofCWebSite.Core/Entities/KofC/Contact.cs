using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KofCWebsite.Core.Entities.KofC
{
    [Serializable]
    public partial class Contact
    {
        public int Id { get; set; }

        [DisplayName("First Name")]
        [Required]
        public string Firstname { get; set; }

        [DisplayName("Middle Name")]
        public string Middlename { get; set; }

        [DisplayName("Last Name")]
        [Required]
        public string Lastname { get; set; }

        [DisplayName("Suffix")]
        public string Suffix { get; set; }

        [DisplayName("Occupation")]
        public string Occupation { get; set; }

        [DisplayName("Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [DisplayName("Email Address")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public int? AlbumImageId { get; set; }

        public int? ContactTypeId { get; set; }

        public char? Status { get; set; }

        public ContactAddress ContactAddress { get; set; }

        [DisplayName("Home Phone")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Home Phone must be a Valid Phone Number")]
        public string HomePhone { get; set; }

        [DisplayName("Cell Phone")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Cell Phone must be a Valid Phone Number")]
        public string CellPhone { get; set; }

        public ContactType ContactType { get; set; }

        [DisplayName("Contact Picture")]
        public AlbumImage AlbumImage { get; set; }

        public string GetFirstname()
        {
            return Firstname;
        }

        public string FormattedDateOfBirth => DateOfBirth?.ToString("MM/dd/yyyy") ?? "";
    }
}
