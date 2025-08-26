namespace Hospital_Management.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppoiment appoiment;
        private readonly IDoctorService doctorService;
        public AppointmentController(IAppoiment appoiment, IDoctorService doctorService)
        {
            this.appoiment = appoiment;
            this.doctorService = doctorService;
        }

        

        [HttpGet]
        [Route("/Appointment/MakeAppoinment")]
        public IActionResult MakeAppoinment()
        {
            List<Doctor> doctors = doctorService.GetAllDoctors();
            ViewBag.DoctorsDDL = doctors;  
            return View("AddAppoinment");
        }

        [Route("/Appointment/searchPatient")]
        [HttpGet]
        public JsonResult SearchPatient(string patientName)
        {
            var PatientNameList = appoiment.SearchPatient(patientName);
            var formatted = PatientNameList.Select(p => new
            {
                patientID = p.PatientId,
                name = p.Name,
                city = p.City,
                address = p.Address
            });
            return Json(formatted);   
        }

    }
}
