using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var Departments = _hospitalDbContext.Departments.ToList();
            var DepartmentDDL = new AddDoctorViewModel()
            {
                Departments = Departments.Select(d => new SelectListItem
                {
                    Text = d.DepartmentName,
                    Value = d.DepartmentId.ToString()
                }).ToList()
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
                var Departments = _hospitalDbContext.Departments.ToList();

                var DepartmentDDL = new AddDoctorViewModel()
                {
                    Departments = Departments.Select(d => new SelectListItem
                    {
                        Text = d.DepartmentName,
                        Value = d.DepartmentId.ToString()
                    }).ToList()
                };
            }
            return View("DoctorDashboard");
        }


        public IActionResult ShowDoctors()
        {
            var doctor = _hospitalDbContext.Doctors
                .Include(d => d.DoctorDepartments)
                .ThenInclude(d => d.Department)
                .ToList();

            return View("ShowDoctorWithDepartment", doctor);
        }

        public IActionResult Update(int doctorID)
        {
            AddDoctorViewModel addDoctorViewModel = new AddDoctorViewModel();

            //Load All SeletedDepartment From DD table
            addDoctorViewModel.SelectedDepartmentId =
                        _hospitalDbContext.DoctorDepartments
                        .Where(x => x.DoctorId == doctorID)
                        .Select(x => x.DepartmentId)
                        .ToList();

            // get all department
            var DepartmentList = _hospitalDbContext.Departments.ToList();

            foreach (var item in DepartmentList)
            {
                if (addDoctorViewModel.SelectedDepartmentId.Contains(item.DepartmentId))
                {
                    addDoctorViewModel.Departments.Add(new SelectListItem
                    {
                        Text = item.DepartmentName,
                        Value = item.DepartmentId.ToString(),
                        Selected = true
                    });

                }
                else
                {
                    addDoctorViewModel.Departments.Add(new SelectListItem
                    {
                        Text = item.DepartmentName,
                        Value = item.DepartmentId.ToString(),
                    });
                }
            }
            // get doctor Details For Update
            var doctor = _hospitalDbContext.Doctors.FirstOrDefault(x => x.DoctorId == doctorID);

            if (doctor != null)
            {
                addDoctorViewModel.DoctorId = doctorID;
                addDoctorViewModel.Qualification = doctor.Qualification;
                addDoctorViewModel.Specialization = doctor.Specialization;
                addDoctorViewModel.Name = doctor.Name;
                addDoctorViewModel.Email = doctor.Email;
                addDoctorViewModel.Phone = doctor.Phone;
            }
            return View("UpdateDoctor", addDoctorViewModel);
        }

        [HttpPost]
        public IActionResult Update(AddDoctorViewModel addDoctorViewModel)
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
                UserId = _userid ?? 1,
                DoctorId = addDoctorViewModel.DoctorId,
                IsActive = true,
            };

            bool isAdded = _doctorService.updateDoctorWithDepartment(Doctors, addDoctorViewModel.SelectedDepartmentId);

            if (isAdded)
            {
                TempData["DoctorAddMessgae"] = "doctor Added SuccessFully";
            }
            else
            {
                TempData["DoctorAddMessgae"] = "Doctor add failed";
             
                // get department From database 
                var Departments = _hospitalDbContext.Departments.ToList();

                var DepartmentDDL = new AddDoctorViewModel()
                {
                    Departments = Departments.Select(d => new SelectListItem
                    {
                        Text = d.DepartmentName,
                        Value = d.DepartmentId.ToString()
                    }).ToList()
                };
            }
            return RedirectToAction("ShowDoctors");

        }

        [HttpGet]
        public IActionResult Delete(int doctorID)
        {
            _doctorService.DeleteDoctor(doctorID);
            return RedirectToAction("ShowDoctors");
        }

        [HttpGet]
        [Route("/Doctor/isDoctorExitsOrNot/{doctorName}/{Phone}")]
        public IActionResult IsDoctorNameExists(string doctorName, string Phone)
        {
            string isDoctore = _doctorService.isDoctorExits(doctorName, Phone);
            return Ok(new { messgae = isDoctore });
        }
    }
}
