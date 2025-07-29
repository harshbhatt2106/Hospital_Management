using Hospital_Management.CommonMethod;
using Hospital_Management.Interfaces;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult UpdatePassword(int UserID, string Password, string ConfirmPassword)
        {
            bool Istrue = adminServices.UpdatePasswordfAdmin(UserID, ConfirmPassword, Password);
            if (!Istrue)
            {
                TempData["UpdatepasswordMessage"] = "Old Password is Wrong";
                return RedirectToAction("Profile");
            }

            TempData["UpdatepasswordMessage"] = "passsword Updated...";
            return RedirectToAction("Profile");
        }

        [HttpPost]
        [Route("/Admin/Forgetpassword")]
        public IActionResult Forgetpassword(string Email)
        {
            var IsGamilValid = adminServices.

            if (IsGamilValid.Email != Email)
            {
                TempData["ValidGmail"] = "This Email Is Not Registerd";
                return View("AdminProfile", IsGamilValid);
            }
            else
            {
                // Generate OTP
                int OTP = new Random().Next(100000, 999999);
                HttpContext.Session.SetInt32("AdminOTP", OTP);
                HttpContext.Session.SetString("OTPTime", DateTime.Now.AddMinutes(1).ToString());
                HttpContext.Session.SetString("ForgetPasswordProgress", "True");
                string Subject_of_ForgetEmail = "Your Admin Account OTP for Password Reset"; ;
                string body = $@"
                        <h2>Password Reset Request</h2>
                        <p>Dear Admin,</p>
                        <p>We received a request to reset the password for your admin account.</p>
                        <p><strong>Your OTP is: <span style='color:blue; font-size:18px;'>{OTP}</span></strong></p>
                        <p>Please enter this OTP on the verification screen to proceed.</p>
                        <p>If you did not request this, please ignore this email.</p>
                        <p>This OTP valid Until {DateTime.Now.AddMinutes(1)}</p>
                        <br/>
                        <p>Thanks,<br/>Hospital Management Team</p>
                    ";
                // send into Email
                emailservices.SendEmailAsync(IsGamilValid.Email, Subject_of_ForgetEmail, body);
                return View();
            }

        }

        [HttpGet]
        [Route("/Admin/ResetPassword")]
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(string Password)
        {

            int? userID = HttpContext.Session.GetInt32("UserID");
            bool result = adminServices.UpdatePasswordfAdmin(userID.Value, Password, null, true);
            if (result)
            {
                return RedirectToAction("Profile", "Admin");
            }
            else
            {
                TempData["UpdatePasswordMessage"] = "Your Password Not be Updated";
            }
            return View();
        }

        [Route("/Admin/VerifyOTP/{_otp}")]
        public JsonResult VerifyOTP(int _otp = 12)
        {
            int _EnterdOTP = _otp;
            int AdminOTP = HttpContext.Session.GetInt32("AdminOTP") ?? 0;
            var expiryTime = HttpContext.Session.GetString("OTPTime");

            DateTime et = DateTime.Parse(expiryTime);

            if (AdminOTP == AdminOTP && et > DateTime.Now)
            {
                return Json(new
                {
                    success = true
                });
            }
            else
            {
                return Json(new
                {
                    success = false
                });
            }
        }
    }
}

