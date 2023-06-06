﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace CarRentals.Models.Auth
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "FirstName is required.")]
        [MinLength(10, ErrorMessage = "The minimum lenght is 10.")]
        [MaxLength(25)]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "LastName is required.")]
        [MinLength(10, ErrorMessage = "The minimum lenght is 10.")]
        [MaxLength(25)]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(11, ErrorMessage = "The minimum lenght is 11.")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Please Enter A valid email!!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required (ErrorMessage ="Please Enter Your Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("Password", ErrorMessage =
            "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
