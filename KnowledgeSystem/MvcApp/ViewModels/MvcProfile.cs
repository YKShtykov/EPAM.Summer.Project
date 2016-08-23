using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
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

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date")]
        [Required(ErrorMessage = "Please enter your birth name")]
        public DateTime BirthDate { get; set; }

        public int Age { get; set; }

        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Relationship status")]
        public RelationshipStatus RelationshipStatus { get; set; }

        [Display(Name = "City")]
        [StringLength(128, ErrorMessage = "Max length - {0} symbols")]
        public string City { get; set; }

        [Display(Name = "AdditionalInfo")]
        [StringLength(500, ErrorMessage = "Max length - {0} symbols")]
        public string AdditionalInfo { get; set; }

        [Display(Name = "Photo")]
        public byte[] Image { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
    }

    public enum Gender
    {
        Unspecified,
        Male,
        Female
    }

    public enum RelationshipStatus
    {
        Unspecified,
        Single,
        Engaged,
        Married,
        Enamored
    }
}