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
        public IActionResult tryview()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var department = hospitalDbContext.Departments.ToList();

            list = department.Select(d=> new SelectListItem
            {
                Text = d.DepartmentName,
                Value = d.DepartmentId.ToString(),
            }).ToList();

            ViewBag.Departments = list;

            return View("trynav");
        }

        [HttpPost]
        public IActionResult tryview(Department department)
        {
            var data = department.DepartmentId;
            var name = department.DepartmentName;
            return View("trynav");
        }
    }
}
