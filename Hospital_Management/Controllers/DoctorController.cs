using Hospital_Management.Interfaces;
using Hospital_Management.Models;
using Hospital_Management.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Hospital_Management.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorServices _doctorService;
        public DoctorController(IDoctorServices doctorService)
        {
            _doctorService = doctorService;
        }

        public IActionResult Index()
        {
            return View("DoctorDashboard");
        }

        public IActionResult AddDoctor()
        {
            return View();
        }

        [HttpPost]
        [Route("/Doctor/AddDoctor")]
        public IActionResult AddDoctor(Doctor doctor)
        {
            int? _userid = HttpContext.Session.GetInt32("UserID");
            doctor.UserId = _userid ?? 1;
            bool isAdded = _doctorService.AddDoctor(doctor);
            if (isAdded)
            {
                TempData["DoctorAddMessgae"] = "doctor Added SuccessFully";
            }
            else
            {
                TempData["DoctorAddMessgae"] = "Doctor add failed";
            }
            return View("AddDoctor");
        }

    }
}
