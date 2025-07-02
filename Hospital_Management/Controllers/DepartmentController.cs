using Hospital_Management.CommonMethod;
using Hospital_Management.Interfaces;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Hospital_Management.Controllers
{

    public class DepartmentController : Controller
    {
        public static List<Department> departmentList = Helper_Method.GetDepartmentList();

        private readonly IDepartmentService _service;
        public DepartmentController(IDepartmentService departmentService)
        {
            _service = departmentService;
        }
        [Route("/Department/DepartmentList")]
        public IActionResult DepartmentList()
        {
            departmentList.Clear();
            departmentList = Helper_Method.GetDepartmentList();
            return View(departmentList);
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
            var data = departmentList.Find(x => x.DepartmentID == id);
            return View(data);
        }

        [HttpPost]
        [Route("/Department/Update")]
        public IActionResult EditDepartment(Department dept)
        {
            string procedure = "SP_Update_Departement";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter ("@Description",dept.Description),
                new SqlParameter("@DepartmentName",dept.DepartmentName),
                new SqlParameter("@DepartmentID",dept.DepartmentID),
            };
            bool IsUpdated = DBHelper.ExecuteNonQuery(procedure, sqlParameters);
            if (IsUpdated)
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
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@DID",id)
            };
            string Procedure = "SP_Delete_Department";
            DBHelper.ExecuteNonQuery(Procedure, sqlParameters);
            return RedirectToAction("DepartmentList", "Department");
        }

    }
}
