using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Psychiatrist_Management_System.Data;
using Psychiatrist_Management_System.Models;
using Psychiatrist_Management_System.ViewModel;

namespace Psychiatrist_Management_System.Areas.PsychiatristArea.Controllers
{
    [Area("PsychiatristArea")]
    public class PsychiatristController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PsychiatristController(UserManager<IdentityUser> userManager, 
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> CompleteProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new PsychiatristViewModel
            {
                PsychoEmail = user.Email 
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteProfile(PsychiatristViewModel model)
        {
            if (ModelState.IsValid)
            {

                var userId = _userManager.GetUserId(User);
                string uniqueFileName = null;

                if (model.PsychoImage != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, @"images\psychos");
                    Directory.CreateDirectory(uploadsFolder); 
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PsychoImage.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.PsychoImage.CopyToAsync(fileStream);
                    }
                }
                var psychiatrist = new PsychiatristProfile
                {
                    UserId = userId,
                    PsychoEmail = model.PsychoEmail,
                    PsychoName = model.PsychoName,
                    PsychoAge = model.PsychoAge,
                    PsychoGender = model.PsychoGender,
                    PsychoPhone = model.PsychoPhone,
                    HospitalName = model.HospitalName,
                    Specialization = model.Specialization,
                    PsychoImage = @"\images\products\" + uniqueFileName
                };
                
                _context.PsychiatristProfile.Add(psychiatrist);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Dashboard", new { area = "PsychiatristArea" });
            }
            return View(model);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
