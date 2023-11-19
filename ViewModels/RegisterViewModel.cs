﻿using System.ComponentModel.DataAnnotations;

namespace _3_Asp.Net_MVC.ViewModels
{
    public class RegisterViewModel
    {
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Không chứa ký tự đặc biệt")]
        [Required]
        [MinLength(6, ErrorMessage = "Tối thiểu 6 ký tự")]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Không chứa ký tự đặc biệt")]
        [Required]
        [MinLength(6, ErrorMessage = "Tối thiểu 6 ký tự")]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
