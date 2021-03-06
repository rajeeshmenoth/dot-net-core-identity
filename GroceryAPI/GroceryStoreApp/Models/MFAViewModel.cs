using System.ComponentModel.DataAnnotations;

namespace IdentityCore.Models
{
    public class MFAViewModel
    {
        [Required]
        public string MfaToken { get; set; }
        public string MfaCode { get; set; }
        public string QrCodeUrl { get; set; }
    }
}
