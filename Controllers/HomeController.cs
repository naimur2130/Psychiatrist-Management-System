using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Psychiatrist_Management_System.Data;
using Psychiatrist_Management_System.Models;
using System.Diagnostics;

namespace Psychiatrist_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser>userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }

            var user = await _userManager.GetUserAsync(User);

            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admins" });
            }
            else if (await _userManager.IsInRoleAsync(user, "Psychiatrist"))
            {
                return RedirectToAction("Index", "Dashboard", new { area = "PsychiatristArea" });
            }
            else if (await _userManager.IsInRoleAsync(user, "Patient"))
            {
                return RedirectToAction("Index", "Home", new { area = "PatientArea" });
            }
            return View("AccessDenied");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult About()
        {
            return View(); // Looks for Views/Home/About.cshtml
        }
    }
}
