using System;
using System.ComponentModel.DataAnnotations;

namespace Chushka.Web.Models
{
    public class RegisterFormModel
    {
        [Required(ErrorMessage = "Username is required")]
        [MinLength(6)]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Confirm Password")]
        [MinLength(8)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter your full name")]
        [MinLength(1)]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        public string Email { get; set; }
    }

    public class LoginFormModel
    {
        [Required(ErrorMessage = "Username is required")]
        [MinLength(6)]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
