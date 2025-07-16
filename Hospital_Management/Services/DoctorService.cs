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
        public bool AddDoctorWithDepartment(Doctor doctor,List<int> SelectedDepartmentId,int UserId)
        {
            _hospitalDbContext.Doctors.Add(doctor);
            _hospitalDbContext.SaveChanges();

            // selected DepartmentID by Admin for Doctor
            foreach (var DepartmentID in SelectedDepartmentId)
            {
                var DoctorDepartment = new DoctorDepartment()
                {
                    DepartmentId = DepartmentID,
                    DoctorId = doctor.DoctorId,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    UserId = UserId
                };
                _hospitalDbContext.DoctorDepartments.Add(DoctorDepartment);
            }                       
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
