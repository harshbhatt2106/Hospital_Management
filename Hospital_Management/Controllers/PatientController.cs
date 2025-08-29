using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatient _patientservices;
        public PatientController(IPatient patient)
        {
            _patientservices = patient;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/Patient/AddPatient")]
        public IActionResult AddPatient()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddPatient(Patient patient)
        {
            patient.UserId = SessionUtility.GetCurrentUserID();
            int result = _patientservices.addPatient(patient);
            if (result > 0)
            {
                TempData["PatientAddMessage"] = "Patient add SuccessFully";
                TempData["MessageType"] = "Success";
            }
            else
            {
                TempData["PatientAddMessage"] = "Patient not Added please try again";
                TempData["MessageType"] = "Failed";
            }
            return View();
        }

        [HttpGet]
        public IActionResult ShowPatient()
        {
            var paatientList = _patientservices.GetAllpatients();
            return View(paatientList);
        }

    }
}
