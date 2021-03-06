using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityCore.Models
{
    public class SignupViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
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

        public List<SelectListItem> Countries { get; } = new List<SelectListItem>
        {

                new SelectListItem() { Text = "--Select--", Value = string.Empty },
                new SelectListItem() { Text = "India", Value = "India" },
                new SelectListItem() { Text = "Australia", Value = "Australia" },
                new SelectListItem() { Text = "USA", Value = "USA" },
                new SelectListItem() { Text = "UK", Value = "UK" }

        };
    }
}
