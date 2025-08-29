
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

        public List<AppoimentViewModel> ListAppointments()
        {
            var data = _context.Appointments.Include(d => d.Doctor)
                                                .ThenInclude(d => d.DoctorDepartments)
                                                    .ThenInclude(d => d.Department)
                                            .Include(a => a.Patient)
                                            .Include(a => a.User)
                                            .Select(data => new AppoimentViewModel
                                            {
                                                doctors = new List<Doctor> { data.Doctor },
                                                departments = data.Doctor.DoctorDepartments
                                                                .Select(dd => dd.Department)
                                                                .ToList(),

                                                Patient = data.Patient,
                                                Appointment = data,
                                                AppointmentTime = data.AppointmentDate.TimeOfDay,

                                            })
                                            .ToList();


            return data;
        }

        public bool BookAppoinment(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            return _context.SaveChanges() > 0;
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
