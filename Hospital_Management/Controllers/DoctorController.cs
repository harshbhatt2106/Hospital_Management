using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorServices _doctorService;
        private readonly HospitalDbContext _hospitalDbContext;
        public DoctorController(IDoctorServices doctorService, HospitalDbContext hospitalDbContext)
        {
            _doctorService = doctorService;
            _hospitalDbContext = hospitalDbContext;
        }

        public IActionResult DoctorDashboard()
        {
            return View("DoctorDashboard");
        }

        public IActionResult AddDoctor()
        {
            var DepartmentDDL = new AddDoctorViewModel()
            {
                Departments = _hospitalDbContext.Departments.ToList()
            };
            return View(DepartmentDDL);
        }

        [HttpPost]
        [Route("/Doctor/AddDoctor/")]
        public IActionResult AddDoctor(AddDoctorViewModel addDoctorViewModel)
        {
            
            int? _userid = HttpContext.Session.GetInt32("UserID");

            var Doctors = new Doctor()
            {
                Name = addDoctorViewModel.Name,
                Phone = addDoctorViewModel.Phone,
                Email = addDoctorViewModel.Email,
                Qualification = addDoctorViewModel.Qualification,
                Specialization = addDoctorViewModel.Specialization,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                UserId = _userid ?? 1
            };

            bool isAdded = _doctorService.AddDoctorWithDepartment(Doctors, addDoctorViewModel.SelectedDepartmentId, _userid ?? 1);
            if (isAdded)
            {
                TempData["DoctorAddMessgae"] = "doctor Added SuccessFully";
            }
            else
            {
                TempData["DoctorAddMessgae"] = "Doctor add failed";
                addDoctorViewModel.Departments = _hospitalDbContext.Departments.ToList();
            }
            return View("DoctorDashboard");
        }


        public IActionResult ShowDoctors()
        {
            var doctor = _hospitalDbContext.Doctors
                .Include(d => d.DoctorDepartments)
                .ThenInclude(d => d.Department)
                .ToList();
            return View(doctor);
        }


    }
}
