using Hospital_Management.CommonMethod;
using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class TryController : Controller
    {
        //private readonly IDoctorServices _doctorService;
        //private readonly HospitalDbContext _hospitalDbContext;
        //public TryController(IDoctorServices doctorService, HospitalDbContext hospitalDbContext)
        //{
        //    _doctorService = doctorService;
        //    _hospitalDbContext = hospitalDbContext;
        //}
        //public IActionResult DoctorAdd(AddDoctorViewModel _AddDoctorViewModel)
        //{
        //    var model = new AddDoctorViewModel()
        //    {
        //        Departments = _hospitalDbContext.Departments.ToList()
        //    };
            

        //    var doctor = new Doctor
        //    {
        //        Name = _AddDoctorViewModel.Name,
        //        Phone = _AddDoctorViewModel.Phone,
        //        Email = _AddDoctorViewModel.Email,
        //        Qualification = _AddDoctorViewModel.Qualification,
        //        Specialization = _AddDoctorViewModel.Specialization,
        //        Created = DateTime.Now,
        //        Modified = DateTime.Now,
        //        UserId = 1,
        //        IsActive = true
        //    };
        //    _hospitalDbContext.Add(doctor);
        //    _hospitalDbContext.SaveChanges();

        //    foreach (var deptId in model.SelectedDepartmentId)
        //    {

        //    }

        //        return View(model);
        //}


    }
}
