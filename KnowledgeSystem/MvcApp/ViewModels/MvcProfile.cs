using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcApp.ViewModels
{
    /// <summary>
    /// Class-model for mvc layout user
    /// </summary>
    public class MvcProfile
    {
        /// <summary>
        /// User identify number
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// User lastnme
        /// </summary>
        [Display(Name = "Last Name")]
        [StringLength(128, ErrorMessage = "Max length - {0} symbols")]
        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; }

        /// <summary>
        /// User first name
        /// </summary>
        [Display(Name = "First Name")]
        [StringLength(128, ErrorMessage = "Max length - {0} symbols")]
        [Required(ErrorMessage = "Please enter your firsrt name")]
        public string FirstName { get; set; }

        /// <summary>
        /// User Middlename
        /// </summary>
        [Display(Name = "Middle Name")]
        [StringLength(128, ErrorMessage = "Max length - {0} symbols")]
        public string MiddleName { get; set; }

        /// <summary>
        /// user contact mail
        /// </summary>
        [Display(Name ="Contact Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Enter correct email, please")]
        public string ContactEmail { get; set; }

        /// <summary>
        /// User contact phone
        /// </summary>
        [Display(Name = "Contact Phone")]
        [RegularExpression(@"\x2B(?<AreaCode>\d{3})\s*(?<Number>\d{2})\s*(?<Number>\d{7})", ErrorMessage = "Enter correct phone in format +--- -- -------, please")]
        public string ContactPhone { get; set; }

        /// <summary>
        /// user birth date
        /// </summary>
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// User age
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// User gender
        /// </summary>
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        /// <summary>
        /// User relationship status
        /// </summary>
        [Display(Name = "Relationship status")]
        public string RelationshipStatus { get; set; }

        /// <summary>
        /// user status
        /// </summary>
        [Display(Name = "City")]
        [StringLength(128, ErrorMessage = "Max length - {0} symbols")]
        public string City { get; set; }

        /// <summary>
        /// User additional information
        /// </summary>
        [Display(Name = "AdditionalInfo")]
        [StringLength(500, ErrorMessage = "Max length - {0} symbols")]
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// User photo
        /// </summary>
        [Display(Name = "Avatar")]
        public byte[] Image { get; set; }

        /// <summary>
        /// User photo MIME type
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
    }
}