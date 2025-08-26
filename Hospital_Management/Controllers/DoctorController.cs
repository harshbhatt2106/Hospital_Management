namespace Hospital_Management.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IDepartmentService _departementServices;
        private readonly IDoctorDepartment doctorDepartment;

        public DoctorController(IDoctorService doctorService,
                                IDepartmentService departmentService,
                                IDoctorDepartment doctorDepartment)
        {
            this.doctorDepartment = doctorDepartment;
            _departementServices = departmentService;
            _doctorService = doctorService;
        }

        public IActionResult DoctorDashboard()
        {
            return View("DoctorDashboard");
        }

        public IActionResult AddDoctor()
        {
            // list departments
            var Departments = _departementServices.departments().ToList();


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

            int? _userid = SessionUtility.GetCurrentUserID();

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
                TempData["DoctorAddMessgae"] = "Doctor Added SuccessFully";
            }
            else
            {
                TempData["DoctorAddMessgae"] = "Doctor Added Failed";
                var Departments = _departementServices.departments().ToList();

                var DepartmentDDL = new AddDoctorViewModel()
                {
                    Departments = Departments.Select(d => new SelectListItem
                    {
                        Text = d.DepartmentName,
                        Value = d.DepartmentId.ToString()
                    }).ToList()
                };
                return View("AddDoctor");
            }
            return RedirectToAction("ShowDoctors");
        }


        public IActionResult ShowDoctors()
        {
            var doctor = _doctorService.GetAllDoctors();
            return View("ShowDoctorWithDepartment", doctor);
        }

        public IActionResult Update(int doctorID)
        {
            AddDoctorViewModel addDoctorViewModel = new AddDoctorViewModel();

            //Load All SeletedDepartment From DD table
            addDoctorViewModel.SelectedDepartmentId = doctorDepartment.DepartmentIds(doctorID);

            // Get All Department
            var DepartmentList = _departementServices.departments().ToList();

            foreach (var item in DepartmentList)
            {
                // Check Already Selected DepartmentID
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
            var doctor = _doctorService.GetAllDoctors().FirstOrDefault(x => x.DoctorId == doctorID);

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
            int? _userid = SessionUtility.GetCurrentUserID();

            // Fill Doctor Updated Data
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

                // Get All  DepartmentAgain If Doctor Add Faild
                var Departments = _departementServices.departments().ToList();

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

        [HttpPost]
        [Route("/Doctor/isDoctorExitsOrNot")]
        public JsonResult IsDoctorNameExists([FromBody] Dictionary<string,string> keyValuePairs)
        {
            var (EmailExits, PhoneExits) = _doctorService.isDoctorExits(keyValuePairs["GmailID"], keyValuePairs["Phone"]);
            return Json(new { emailExists = EmailExits, phoneExists = PhoneExits });
        }
    }
}