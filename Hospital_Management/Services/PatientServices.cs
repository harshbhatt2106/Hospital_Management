
namespace Hospital_Management.Services
{
    public class PatientServices : IPatient
    {
        private readonly HospitalDbContext _context;

        public PatientServices(HospitalDbContext _context)
        {
            this._context = _context;
        }
        public bool addPatient(Patient patient)
        {
            try
            {
                _context.Patients.Add(patient);
                return _context.SaveChanges() > 0;
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
    }
}
