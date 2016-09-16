﻿using System.ComponentModel.DataAnnotations;
using MvcApp.Infrastructure.ValidationAttributes;

namespace MvcApp.ViewModels
{
    public class RegisterModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Enter your Login.  ")]
        [Required(ErrorMessage = "The field can not be empty!")]
        public string Login { get; set; }

        [Display(Name = "Enter your e-mail.  ")]
        [Required(ErrorMessage = "The field can not be empty!")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Enter correct email, please.  ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [StringLength(100, ErrorMessage = "The password must contain at least {2} characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm the password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm the password.  ")]
        [Compare("Password", ErrorMessage = "Passwords must match.  ")]
        public string ConfirmPassword { get; set; }

        [MustBeTrue(ErrorMessage = "You need to confirm your agreement.")]
        public bool Terms { get; set; }
    }
}