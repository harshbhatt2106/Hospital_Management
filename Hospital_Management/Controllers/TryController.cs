using Hospital_Management.CommonMethod;
using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class TryController : Controller
    {
        public IActionResult tryview()
        {
            return View("trynav");
        }


    }
}
