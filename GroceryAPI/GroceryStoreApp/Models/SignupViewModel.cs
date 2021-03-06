using System.ComponentModel.DataAnnotations;

namespace IdentityCore.Models
{
    public class SignupViewModel
    {
        [Required]
        [DataType(DataType.Text, ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email address is invalid or missing.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "Incorrect or missing password.")]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } = "User";

        [Required]
        public string Country { get; set; }
    }
}
