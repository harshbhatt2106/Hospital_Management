using Hospital_Management.Data;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital_Management.Controllers
{
    public class TryController : Controller
    {
        private readonly HospitalDbContext hospitalDbContext;
        public TryController(HospitalDbContext hospitalDbContext)
        {
            this.hospitalDbContext = hospitalDbContext; 
        }

        public IActionResult tryview(Department department)
        {
            return View("trynav");
        }
    }
}
