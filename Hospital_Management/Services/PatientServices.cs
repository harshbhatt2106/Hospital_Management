
namespace Hospital_Management.Services
{
    public class PatientServices : IPatient
    {
        private readonly HospitalDbContext _context;

        public PatientServices(HospitalDbContext _context)
        {
            this._context = _context;
        }
        public int addPatient(Patient patient)
        {
            try
            {
                _context.Patients.Add(patient);
                _context.SaveChanges();
                return patient.PatientId;
            }
            catch
            {
                throw;
            }
        }

        public List<Patient> GetAllpatients()
        {
            return _context.Patients.ToList();
        }

        public Patient GetPatientByID(int id)
        {
            Patient ?patient = _context.Patients.
                          Where(phonenumber => phonenumber.PatientId == id)
                          .FirstOrDefault();

            return patient;
        }

        public int GetPatientID(string phone)
        {
            int patientID = _context.Patients.
                            Where(phonenumber => phonenumber.Phone == phone)
                            .Select(p => p.PatientId)
                            .FirstOrDefault();
            return patientID;
        }


        public bool IsPatientExits(string phone)
        {
            try
            {
                return _context.Patients.Any(number => number.Phone == phone);
            }
            catch
            {
                throw new Exception("From patient");
            }
        }
    }
}
