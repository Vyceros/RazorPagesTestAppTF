using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorPagesTestAppTF.Data.ViewModels.User
{
	public class EditUserViewModel
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Role { get; set; }
		public IEnumerable<SelectListItem>? RoleList { get; set; }
	}
}
