using System.ComponentModel.DataAnnotations;

namespace RazorPagesTestAppTF.Data.ViewModels
{
    public class VerifyAuthenticatorViewModel
    {
        [Required]
        public string? Code { get; set; }
        public string? Returnurl { get; set; }

        [Display(Name ="Remember me")]
        public bool RememberMe { get; set; }
    }
}
