using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Hospital_Management.Models;

namespace Hospital_Management.Services
{
    public class DoctorDepartmentServices : IDoctorDepartment
    {
        private readonly HospitalDbContext hospitalDbContext;
        public DoctorDepartmentServices(HospitalDbContext hospitalDbContext)
        {
            this.hospitalDbContext = hospitalDbContext;
        }

        public List<int> DepartmentIds(int doctorID)
        {
            List<int> departmentids = hospitalDbContext.DoctorDepartments
                        .Where(x => x.DoctorId == doctorID)
                        .Select(x => x.DepartmentId)
                        .ToList();

            return departmentids;
        }

    }
}
