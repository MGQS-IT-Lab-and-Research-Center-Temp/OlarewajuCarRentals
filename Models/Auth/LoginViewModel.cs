using System.ComponentModel.DataAnnotations;

namespace CarRentals.Models.Auth
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
