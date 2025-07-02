using Hospital_Management.CommonMethod;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class TryController : Controller
    {
        public static List<Department> departmentList = Helper_Method.GetDepartmentList();
        public IActionResult Index()
        {
            return View("trynav",departmentList);
        }

        [HttpPost]
        public IActionResult SelectdList(List<int> seletedlist)
        {
            return View();
        }
    }
}
