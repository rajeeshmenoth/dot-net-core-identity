using System.ComponentModel.DataAnnotations;

namespace GroceryStoreApp.Models
{
    public class SigninViewModel
    {
        [Required(ErrorMessage = "User name must be required.")]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password must be required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
