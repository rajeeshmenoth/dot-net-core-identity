using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IdentityCore.Models
{
    public class TwoFactorModel
    {
        [Required]
        [DataType(DataType.Text, ErrorMessage = "Authentication code is required.")]
        [DisplayName("Authentication Code")]
        public string TwoFactorAuthCode { get; set; }
        public bool RememberMe { get; set; }
    }
}