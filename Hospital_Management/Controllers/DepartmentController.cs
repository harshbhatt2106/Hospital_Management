using Hospital_Management.Interfaces;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{

    public class DepartmentController : Controller
    {

        private readonly IDepartmentService _service;
        public DepartmentController(IDepartmentService departmentService)
        {
            _service = departmentService;
        }
        [Route("/Department/DepartmentList")]
        public IActionResult DepartmentList()
        {
            var data = _service.departments();
            return View(data);
        }

        public IActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        [Route("/Department/AddDepartment")]
        public IActionResult AddDepartment(Department department)
        {
            int? _userid = SessionUtility.GetCurrentUserID();
                department.UserId = _userid ?? 1;

            TempData["Message"] = null;
            if (_service.CheckDepartment(department.DepartmentName ?? " "))
            {
                TempData["Message"] = "Department Already Exits....";
                TempData["Status"] = true;
            }

            bool isAdded = _service.AddDepartment(department);
            if (isAdded)
            {
                TempData["Message"] = "SuccessFaully Added...";
                TempData["Status"] = false;
            }
            else
            {
                TempData["Message"] = "DepartmenrAdd Process Failed";
                TempData["Status"] = true;
            }


            return View("AddDepartment");
        }

        [Route("/Department/Edit")]
        public IActionResult EditDepartment(int id)
        {
            var data = _service.departments().Find(x => x.DepartmentId == id);
            return View(data);
        }


        [HttpGet]
        [Route("/Department/DepartmentExits/{departmentName}")]
        public IActionResult DepartmentExits(string departmentname)
        {
            bool data = _service.departments().Any(x => x.DepartmentName == departmentname);
            return Json(data);
        }


        [HttpPost]
        [Route("/Department/Update")]
        public IActionResult EditDepartment(Department dept)
        {
            bool isUpdate = _service.UpdateDepartment(dept);

            if (isUpdate)
            {
                TempData["Department_Update_Message"] = "Department Data Updated SuccessFully.....";
            }
            else
            {
                TempData["Department_Update_Message"] = "Updated failed....";
            }
            return RedirectToAction("DepartmentList");
        }


        [HttpPost]
        [Route("/Department/UpdateDepartmentStatus/{id?}")]
        public IActionResult UpdateDepartmentStatus(int id)
        {
            bool result = _service.setDepartmentStatus(id);
            return Json(new
            {
                success = result
            });
        }

    }
}
