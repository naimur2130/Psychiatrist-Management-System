using Microsoft.AspNetCore.Mvc;

namespace Psychiatrist_Management_System.Areas.PatientArea.Controllers
{
    [Area("PatientArea")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
