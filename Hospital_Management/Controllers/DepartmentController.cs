using Hospital_Management.CommonMethod;
using Hospital_Management.Interfaces;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

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
            int? _userid = HttpContext.Session.GetInt32("UserID");
            department.UserID = _userid ?? 0;

            TempData["Message"] = null;
            if (_service.CheckDepartment(department.DepartmentName))
            {
                TempData["Message"] = "Department Already Exits....";
                TempData["Status"] = true;
            }
            else if (ModelState.IsValid)
            {
                _service.AddDepartment(department);
                TempData["Message"] = "SuccessFaully Added...";
                TempData["Status"] = false;
            }
            return View("AddDepartment");
        }

        [Route("/Department/Edit")]
        public IActionResult EditDepartment(int id)
        {
            var data = _service.departments().Find(x => x.DepartmentID == id);
            return View(data);
        }

        [HttpGet]
        [Route("/Department/DepartmentExits/{departmentName}")]
        public IActionResult DepartmentExits(string departmentName)
        {
            bool data = _service.CheckDepartment(departmentName);
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
        [Route("/Department/Delete/{id?}")]
        public IActionResult DeleteDepartment(int id)
        {
            _service.DeleteDepartment(id);
            return RedirectToAction("DepartmentList", "Department");
        }

    }
}
