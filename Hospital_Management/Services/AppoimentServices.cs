
using NuGet.Common;

namespace Hospital_Management.Services
{
    public class AppoimentServices : IAppoiment
    {

        private readonly HospitalDbContext _context;


        public AppoimentServices(HospitalDbContext hospitalDbContext, ILogger<AdminService> logger)
        {
            _context = hospitalDbContext;
        }

        public List<Patient> SearchPatient(string patient)
        {
            var patientList = _context.Patients.
                              Where(p => p.Name.StartsWith(patient))
                              .ToList();
            return patientList;
        }
    }
}
