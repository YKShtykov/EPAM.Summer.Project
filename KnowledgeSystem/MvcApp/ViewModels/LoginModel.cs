using System.ComponentModel.DataAnnotations;

namespace MvcApp.ViewModels
{
    /// <summary>
    /// Class-model for login
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// User mail or login
        /// </summary>
        [Display(Name = "Email or Login")]
        [StringLength(128, ErrorMessage = "Maximum length - 128 symbols")]
        [Required(ErrorMessage = "Please enter your email or Login")]
        public string EmailOrLogin { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(128, ErrorMessage = "Minimum lengt - 6 symbols, maximum length - 128 symbols", MinimumLength = 6)]
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }

        /// <summary>
        /// Remember mark
        /// </summary>
        [Display(Name = "Remember me")]
        public bool IsRemember { get; set; }
    }
}