using System.ComponentModel.DataAnnotations;

namespace GroceryStoreApp.Models
{
    public class TwoFactorModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string TwoFactorAuthCode { get; set; }
        public bool RememberMe { get; set; }
    }
}
