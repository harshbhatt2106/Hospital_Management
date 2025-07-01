using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class TryController : Controller
    {
        public IActionResult Index()
        {
            return View("trynav");
        }
    }
}
