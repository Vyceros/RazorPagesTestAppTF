﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace RazorPagesTestAppTF.Data.ViewModels
{
	public class ResetPasswordViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[DisplayName("Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation passwprd do not match.")]
		public string ConfirmPassword { get; set; }

		public string Code { get; set; }
	}
}
