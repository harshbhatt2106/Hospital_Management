namespace Hospital_Management.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService adminServices;
        private readonly IDoctorService doctorServices;
        private readonly IDepartmentService departmentServices;
        private readonly IEmailservice emailservices;
        private readonly IPasswordService passwordServices;
        private readonly IotpService otpServices;

        public AdminController(IAdminService _adminServices,
                               IDoctorService doctorServices,
                               IDepartmentService departmentServices,
                               IEmailservice emailservices,
                               IPasswordService passwordServices,
                               IotpService otpServices)
        {
            this.adminServices = _adminServices;
            this.doctorServices = doctorServices;
            this.departmentServices = departmentServices;
            this.emailservices = emailservices;
            this.passwordServices = passwordServices;
            this.otpServices = otpServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Admin/AdminProfile")]
        public IActionResult Profile()
        {
            int? adminID = SessionUtility.GetCurrentUserID();
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
            if (SessionUtility.GetAdminID() > 0)
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
            try
            {
                User admin = Helper_Method.CheckLogin(Password, UserName);
                if (admin != null)
                {
                    HttpContext.Session.SetInt32("UserID", admin.UserID);
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
            SessionUtility.ClearUserSession();
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
            // send into Email
            bool IsMailSendSuccess = otpServices.SendOTP(Email);
            return Json(new { success = IsMailSendSuccess });
        }


        [Route("/Admin/VerifyGmail")]
        [HttpPost]
        public JsonResult VerifyGmail([FromBody] string Email)
        {
            bool IsGamilValid = adminServices.GetAdmins().Any(x => x.Email.ToLower() == Email.ToLower());
            TempData["Gmail"] = Email;
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
            int? userID = SessionUtility.GetAdminID();
            string gmail = TempData["Gmail"].ToString();
            bool result = passwordServices.UpdateUserForgetPassword(gmail, Password);
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
        public IActionResult UpdatePassword(int UserID, string Password, string ConfirmPassword, string EnterdOldPassword)
        {
            bool Istrue = passwordServices.UpdatePasswordfAdmin(UserID, ConfirmPassword, Password, EnterdOldPassword);
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
            int generatedOTPBySystem = SessionUtility.GetOPT();

            if (otpServices.VerifyOTP(generatedOTPBySystem, userEnterOTP))
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

        [HttpGet]
        public IActionResult UpdateAdmin(int id)
        {
            var ExistingData = adminServices.GetAdminByID(id);
            return View(ExistingData);
        }
        [HttpPost]
        public IActionResult UpdateAdmin(User user, int id)
        {
            int result = adminServices.UpdateAdmin(user, id);
            if (result > 0)
            {
                return RedirectToAction("AdminProfile", "Admin");
            }
            else
            {
                return Ok("data not Updated");   
            }
        }
    }
}

