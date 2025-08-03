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
            User? usersByID = null;
            if (adminID != null)
            {
                usersByID = adminServices.GetAdminByID((int)adminID);
                ViewBag.departmentCount = departmentServices.CountDepartments();
                List<User> users = adminServices.GetAdmins();
                ViewBag.doctorCount = doctorServices.countDoctors();
                ViewBag.adminCount = adminServices.adminCount();
                var model = Tuple.Create(usersByID, users);
                return View("AdminProfile", model);
            }
            return View("AdminProfile", null);
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

        [HttpGet]
        public IActionResult AddAdmin()
        {
            return View();
        }

        [Route("/Admin/AddAdmin")]
        [HttpPost]
        public IActionResult AddAdmin(User user)
        {
            user.Created = DateTime.Now;
            adminServices.AddAdmin(user);
            return View();
        }
    
        [HttpGet]
        public IActionResult Forgetpassword()
        {
            return View();
        }

        [Route("/Admin/SendOTP")]
        [HttpPost]
        public JsonResult SendOTP([FromBody] string Email)
        {

            // Generate OTP
            int OTP = new Random().Next(100000, 999999);
            HttpContext.Session.SetInt32("AdminOTP", OTP);
            HttpContext.Session.SetString("ForgetPasswordProgress", "True");
            string Subject_of_ForgetEmail = "Your Admin Account OTP for Password Reset"; ;
            string body = $@"
                        <h2>Password Reset Request</h2>
                        <p>Dear Admin,</p>
                        <p>We received a request to reset the password for your admin account.</p>
                        <p><strong>Your OTP is: <span style='color:blue; font-size:18px;'>{OTP}</span></strong></p>
                        <p>Please enter this OTP on the verification screen to proceed.</p>
                        <p>If you did not request this, please ignore this email.</p>
                        <p>This OTP valid 1 Time </p> 
                        <br/>
                        <p>Thanks,<br/>Hospital Management Team</p>
                    ";

            // send into Email
            bool IsMailSendSuccess = emailservices.SendEmail(Email, Subject_of_ForgetEmail, body);
            return Json(new { success = IsMailSendSuccess });
        }


        [Route("/Admin/VerifyGmail")]
        [HttpPost]
        public JsonResult VerifyGmail([FromBody] string Email)
        {
            bool IsGamilValid = adminServices.GetAdmins().Any(x => x.Email.ToLower() == Email.ToLower());
            ViewBag.Email = Email;
            return Json(new { success = IsGamilValid });
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
            bool result = adminServices.UpdateUserForgetPassword(ViewBag.Email, Password);
            if (result)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                TempData["UpdatePasswordMessage"] = "Your Password Not be Updated";
            }
            return View();
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

        [Route("/Admin/VerifyOTP")]
        public JsonResult VerifyOTP([FromBody] int userEnterOTP)
        {
            int _EnterdOTP = userEnterOTP;
            int AdminOTP = HttpContext.Session.GetInt32("AdminOTP") ?? 0;

            if (AdminOTP == _EnterdOTP)
            {
                return Json(new
                {
                    _VerifyOTP = true
                });
            }
            else
            {
                return Json(new
                {
                    _VerifyOTP = false
                });
            }
        }
    }
}

