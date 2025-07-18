using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Hospital_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Services
{
    public class DoctorService : IDoctorServices
    {
        private readonly HospitalDbContext _hospitalDbContext;
        public DoctorService(HospitalDbContext _hospitalDbContext)
        {
            this._hospitalDbContext = _hospitalDbContext;
        }
        public bool AddDoctorWithDepartment(Doctor doctor, List<int> SelectedDepartmentId, int UserId)
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

        public void DeleteDoctor(int doctorId)
        {
            var doctordID = _hospitalDbContext.DoctorDepartments
                            .ToList()
                            .Where(x => x.DoctorId == doctorId);
            _hospitalDbContext.RemoveRange(doctordID);
            _hospitalDbContext.SaveChanges();
        }

        public List<Doctor> GetAllDoctors()
        {
            return _hospitalDbContext.Doctors.ToList();
        }

        public Doctor GetDoctorById(int doctorId)
        {
            throw new NotImplementedException();
        }

        public bool updateDoctorWithDepartment(Doctor doctor, List<int> selectedDepartmentID)
        {

            var oldDepartments = _hospitalDbContext.DoctorDepartments
                .Where(x => x.DoctorId == doctor.DoctorId)
                .ToList();

            _hospitalDbContext.DoctorDepartments.RemoveRange(oldDepartments);
           
            _hospitalDbContext.Doctors.Update(doctor);

            foreach (var deptId in selectedDepartmentID)
            {
                var doctorDepartment = new DoctorDepartment
                {
                    UserId = doctor.UserId,
                    DoctorId = doctor.DoctorId,
                    DepartmentId = deptId
                };

                _hospitalDbContext.DoctorDepartments.Add(doctorDepartment);
            }

            _hospitalDbContext.SaveChanges();

            return true;
        }
    }
}

