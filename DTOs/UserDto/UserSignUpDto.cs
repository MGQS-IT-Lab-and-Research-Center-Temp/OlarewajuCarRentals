using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CarRentals.DTOs.UserDto
{
    public class UserSignUpDto
    {

        [Required(ErrorMessage = "FirstName is required.")]
        [MinLength(3, ErrorMessage = "The minimum length is 3.")]
        [MaxLength(25)]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "LastName is required.")]
        [MinLength(3, ErrorMessage = "The minimum lenght is 3.")]
        [MaxLength(25)]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(11, ErrorMessage = "The minimum lenght is 11.")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Please Enter A valid email!!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please Enter Your Password")]
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
