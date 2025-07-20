using Hospital_Management.CommonMethod;
using Hospital_Management.Interfaces;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Hospital_Management.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService adminServices;
        private readonly IDoctorServices doctorServices;
        private readonly IDepartmentService departmentServices;
        private readonly IEmailservices emailservices;

        public AdminController(IAdminService _adminServices,
                               IDoctorServices doctorServices,
                               IDepartmentService departmentServices,
                               IEmailservices emailservices)
        {
            this.adminServices = _adminServices;
            this.doctorServices = doctorServices;
            this.departmentServices = departmentServices;
            this.emailservices = emailservices;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Admin/AdminProfile")]
        public IActionResult Profile()
        {
            int? adminID = HttpContext.Session.GetInt32("UserID");

            User? user = null;
            if (adminID != null)
            {
                user = adminServices.GetAdmin((int)adminID);
                ViewBag.departmentCount = departmentServices.CountDepartments();
                ViewBag.doctorCount = doctorServices.countDoctors();
                ViewBag.adminCount = adminServices.adminCount();
            }
            return View("AdminProfile", user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("UserID") != null)
            {
                return View("AdminDashboard");
            }
            return View();
        }
        public IActionResult AdminDashboard()
        {
            return View();
        }

        [HttpPost]
        [Route("Admin/Login")]
        public IActionResult Login(string UserName, string Password)
        {
            //throw new Exception("Just for try");
            try
            {
                User admin = Helper_Method.CheckLogin(Password, UserName);
                if (admin != null)
                {
                    HttpContext.Session.SetInt32("UserID", admin.UserID);
                    //emailservices.SendEmailAsync("24030501004@darshan.ac.in", "Login", "Login");
                    return RedirectToAction("AdminDashboard", "Admin");
                }
                else
                {
                    TempData["IsLogin"] = "Invalid User...";
                    return View("Login");
                }
            }
            catch
            {
                return RedirectToAction("General", "Error");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult UpdatePassword(int userID,string Password, string ConfirmPassword)
        {
            bool Istrue = adminServices.UpdatePasswordofAdmin(userID, Password, ConfirmPassword);
            if (!Istrue)
            {
                TempData["UpdatepasswordMessage"] = "Old Password is Wrong";
                return RedirectToAction("Profile");
            }

            TempData["UpdatepasswordMessage"] = "passsword Updated...";
            return RedirectToAction("Profile");
        }
    }
}
