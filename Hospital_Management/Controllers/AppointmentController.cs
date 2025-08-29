namespace Hospital_Management.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppoiment appoimentService;
        private readonly IDoctorService doctorService;
        private readonly IDepartmentService departmentService;
        private readonly IPatient patientservices;

        public AppointmentController(IAppoiment appoiment, IDoctorService doctorService, IDepartmentService departmentService, IPatient patientservices)
        {
            this.patientservices = patientservices;
            this.appoimentService = appoiment;
            this.doctorService = doctorService;
            this.departmentService = departmentService;
        }

        [HttpGet]
        [Route("/Appointment/MakeAppoinment")]
        public IActionResult MakeAppoinment()
        {
            List<Doctor> doctors = doctorService.GetAllDoctors();
            var appoimentViewModel = new AppoimentViewModel
            {
                doctors = doctors
            };
            return View("AddAppoinment", appoimentViewModel);
        }

        [Route("/Appointment/searchPatient")]
        [HttpGet]
        public JsonResult SearchPatient(string patientName)
        {
            var PatientNameList = appoimentService.SearchPatient(patientName);
            var formatted = PatientNameList.Select(p => new
            {
                patientID = p.PatientId,
                name = p.Name,
                city = p.City,
                address = p.Address
            });
            return Json(formatted);
        }


        [Route("/Appointment/GetAllDepartmentByDoctor")]
        public JsonResult GetAllDepartmentByDoctor(int DoctorID)
        {
            List<Department> department = departmentService.SearchDepartmentByDoctorID(DoctorID);
            return Json(department);
        }


        public IActionResult BookAppointment(AppoimentViewModel appoimentViewModel)
        {

            bool IsPatientExits = patientservices.IsPatientExits(appoimentViewModel.Patient.Phone);
            int patientID = 0;

            #region Converion of DateTime 
            DateTime dateTime = appoimentViewModel.Appointment.AppointmentDate;
            TimeSpan timeSpan = TimeSpan.Parse(appoimentViewModel.AppointmentTime.ToString());
            DateTime AppoimentTime = dateTime.Date.Add(timeSpan);

            #endregion

            if (!IsPatientExits)
            {
                // add patient
                Patient patient = new Patient
                {
                    Name = appoimentViewModel.Patient.Name,
                    DateOfBirth = appoimentViewModel.Patient.DateOfBirth,
                    Gender = appoimentViewModel.Patient.Gender,
                    Email = appoimentViewModel.Patient.Email,
                    Phone = appoimentViewModel.Patient.Phone,
                    Address = appoimentViewModel.Patient.Address,
                    State = appoimentViewModel.Patient.State,
                    City = appoimentViewModel.Patient.City,
                    UserId = SessionUtility.GetCurrentUserID()
                };
                patientID = patientservices.addPatient(patient);
            }
            else
            {
                patientID = patientservices.GetPatientID(appoimentViewModel.Patient.Phone);
            }

            // only add appoiment
            Appointment appointment = new Appointment
            {
                DoctorId = appoimentViewModel.DoctorId,
                PatientId = patientID,
                AppointmentDate = AppoimentTime,
                AppointmentStatus = "Pending",
                Description = appoimentViewModel.Appointment.Description,
                SpecialRemarks = appoimentViewModel.Appointment.SpecialRemarks,
                UserId = SessionUtility.GetCurrentUserID(),
                Created = DateTime.Now,                 
                Modified = DateTime.Now,
            };
            
            if (appoimentService.BookAppoinment(appointment))
            {
                TempData["AppoimentStatus"] = "Appointment Taken SucessFully";
            }
            else
            {
                TempData["AppoimentStatus"] = "Failed....Retry";
            }

            return RedirectToAction("ShowAllAppoiment");
        }


        [Route("/Appointment/ShowAllAppoiment")]

        public IActionResult ShowAllAppoiment()
        {
            var data = appoimentService.ListAppointments();
            return View(data);
        }

        [Route("/Appointment/getPatientByID")]
        public JsonResult getPatientByID(int id)
        {
             Patient patient =  patientservices.GetPatientByID(id);
            return Json(patient);
        }
    }
}
