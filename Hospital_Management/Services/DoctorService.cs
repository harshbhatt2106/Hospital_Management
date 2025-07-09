using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Hospital_Management.Models;

namespace Hospital_Management.Services
{
    public class DoctorService : IDoctorServices
    {
        private readonly HospitalDbContext _hospitalDbContext;
        public DoctorService(HospitalDbContext _hospitalDbContext)
        {
            this._hospitalDbContext = _hospitalDbContext;
        }
        public bool AddDoctor(Doctor doctor)
        {
            _hospitalDbContext.Doctors.Add(doctor);
            return _hospitalDbContext.SaveChanges() > 0;
        }

        public bool DeleteDoctor(int doctorId)
        {
            throw new NotImplementedException();
        }

        public List<Doctor> GetAllDoctors()
        {
            return _hospitalDbContext.Doctors.ToList();
        }

        public Doctor GetDoctorById(int doctorId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }
    }
}
