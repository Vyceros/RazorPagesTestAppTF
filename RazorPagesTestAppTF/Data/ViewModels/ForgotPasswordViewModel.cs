using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesTestAppTF.Data.ViewModels
{
	public class ForgotPasswordViewModel
	{
		[System.ComponentModel.DataAnnotations.Required]
		[EmailAddress]
        public string Email { get; set; }
    }
}
