using Microsoft.AspNetCore.Mvc;

namespace Authorization.Areas.Psychologist.Controllers
{
    [Area("PsychiatristArea")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
