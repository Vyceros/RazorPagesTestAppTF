using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesTestAppTF.Data;
using RazorPagesTestAppTF.Data.ViewModels.User;

namespace RazorPagesTestAppTF.Pages.User
{
    public class EditUserModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
		public EditUserModel(ApplicationDbContext db, UserManager<IdentityUser> userManager)
		{
			_db = db;
			_userManager = userManager;
		}

        [BindProperty]
		public EditUserViewModel UserViewModel { get; set; } = new();

        [Authorize(Roles = "Admin")]
		public async Task<IActionResult> OnGetAsync(string userId)
        {
            var userToEdit = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == userId);
            if (userToEdit is null)
            {
                return NotFound("User not found");
            }

            var roles = await _db.Roles.ToListAsync();
            var userRole = await _db.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId);

            UserViewModel.Id = userToEdit.Id;
            UserViewModel.UserName = userToEdit.UserName;
            UserViewModel.FirstName = userToEdit.FirstName;
            UserViewModel.LastName = userToEdit.LastName;
            UserViewModel.Role = roles.FirstOrDefault(r => r.Id == userRole.RoleId).Name;

            UserViewModel.RoleList = roles.Select(r => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var userToEdit = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == UserViewModel.Id);
                if (userToEdit is null)
                {
                    return NotFound();
                }

                var userRole = await _db.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userToEdit.Id);

                if (userRole != null)
                {
                    var previouseUserRoleName = await _db.Roles.Where(x => x.Id == userRole.RoleId).Select(x => x.Name).FirstOrDefaultAsync();

                    // remove old role
                    await _userManager.RemoveFromRoleAsync(userToEdit, previouseUserRoleName);

                }

                // add new role

                await _userManager.AddToRoleAsync(userToEdit, UserViewModel.Role);
                userToEdit.FirstName = UserViewModel.FirstName;
                userToEdit.LastName = UserViewModel.LastName;
                await _db.SaveChangesAsync();

                return RedirectToPage("/User/GetUsers");
            }

            return Page();
        }
    }
}
