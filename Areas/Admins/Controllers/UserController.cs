using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Psychiatrist_Management_System.Data;
using Psychiatrist_Management_System.ViewModel;

namespace Psychiatrist_Management_System.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        public UserController(UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var getUser = _userManager.Users.ToList();

            var userWithRole = new List<UserRoleViewModel>();
            foreach (var i in getUser)
            {
                var roles = await _userManager.GetRolesAsync(i);
                userWithRole.Add(new UserRoleViewModel
                {
                    UserId = i.Id,
                    Email = i.Email,
                    PhoneNumber = i.PhoneNumber,
                    UserRoles = roles.ToList()
                });
            }
            return new JsonResult(userWithRole);
        }
        [HttpGet]
        public async Task<IActionResult> GetRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();
            var userRoles = await _userManager.GetRolesAsync(user);

            return new JsonResult(new
            {
                userId = user.Id,
                email = user.Email,
                allRoles,
                userRoles
            });
        }
        [HttpPost]
        public async Task<IActionResult> EditRole([FromForm] string userId, [FromForm] List<string> selectedRoles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { message = "User not found." });

            var currentRoles = await _userManager.GetRolesAsync(user);

            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                return BadRequest(new { message = "Failed to remove existing roles." });
            }

            var addResult = await _userManager.AddToRolesAsync(user, selectedRoles ?? new List<string>());
            if (!addResult.Succeeded)
            {
                return BadRequest(new { message = "Failed to add new roles." });
            }

            return Ok(new { message = "Role updated successfully." });
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(new { message = "Failed to delete user." });
            }
            return Ok(new { message = "User deleted successfully." });
        }
        [HttpGet]
        public async Task<IActionResult> Pending()
        {
            var pendingList = await _context.PsychiatristProfile
        .Where(p => !p.IsApproved)
        .ToListAsync();

            return View(pendingList);
        }
        [HttpGet]
        public async Task<IActionResult> Doctors()
        {
            var pendingList = await _context.PsychiatristProfile
        .Where(p => p.IsApproved)
        .ToListAsync();

            return View(pendingList);
        }
        [HttpPost]
        public async Task<IActionResult> ApprovePsychiatrist(int id)
        {
            var profile = await _context.PsychiatristProfile.FindAsync(id);
            if (profile != null)
            {
                profile.IsApproved = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Pending","User", new {area="Admins"});
        }

    }
}
