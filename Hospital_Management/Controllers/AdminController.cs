using Hospital_Management.CommonMethod;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
      
        [Route("/Admin/AdminProfile")]
        public IActionResult Profile()
        {
            return View("AdminProfile");
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
        public IActionResult Login(string UserName,string Password)
        {           
            try
            {
                Admin admin = Helper_Method.CheckLogin(Password, UserName);
                if (admin != null)
                {
                    HttpContext.Session.SetInt32("UserID", admin.UserID);
                    return View("AdminDashboard");
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
    }
}
