using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcApp.ViewModels
{
    public class MvcProfile
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(128, ErrorMessage = "Max length - {0} symbols")]
        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        [StringLength(128, ErrorMessage = "Max length - {0} symbols")]
        [Required(ErrorMessage = "Please enter your firsrt name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(128, ErrorMessage = "Max length - {0} symbols")]
        public string MiddleName { get; set; }

        [Display(Name ="Contact Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Enter correct email, please")]
        public string ContactEmail { get; set; }

        [Display(Name = "Contact Phone")]
        [RegularExpression(@"\x2B(?<AreaCode>\d{3})\s*(?<Number>\d{2})\s*(?<Number>\d{7})", ErrorMessage = "Enter correct phone in format +--- -- -------, please")]
        public string ContactPhone { get; set; }

        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }

        public int? Age { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Relationship status")]
        public string RelationshipStatus { get; set; }

        [Display(Name = "City")]
        [StringLength(128, ErrorMessage = "Max length - {0} symbols")]
        public string City { get; set; }

        [Display(Name = "AdditionalInfo")]
        [StringLength(500, ErrorMessage = "Max length - {0} symbols")]
        public string AdditionalInfo { get; set; }

        [Display(Name = "Avatar")]
        public byte[] Image { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
    }
}