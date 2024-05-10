using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesTestAppTF.Data;

namespace RazorPagesTestAppTF.Pages.User
{
    public class DeleteUserModel : PageModel
    {
        private readonly ApplicationDbContext _db;

		public DeleteUserModel(ApplicationDbContext db)
		{
			_db = db;
		}

		public async Task<IActionResult> OnGetAsync(string userId)
        {
            var userToDelete = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (userToDelete is null)
            {
                return NotFound();
            }

            _db.Users.Remove(userToDelete);
            _db.SaveChangesAsync();

            return RedirectToPage("/User/GetUsers");
        }
    }
}
