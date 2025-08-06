namespace Hospital_Management.Services
{
    public class DoctorDepartmentService : IDoctorDepartment
    {
        private readonly HospitalDbContext hospitalDbContext;
        public DoctorDepartmentService(HospitalDbContext hospitalDbContext)
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
