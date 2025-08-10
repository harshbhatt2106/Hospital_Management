namespace Hospital_Management.Services
{
    public class DoctorService : IDoctorService
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

        public int countDoctors()
        {
            int count = _hospitalDbContext.Doctors.Where(x => x.IsActive == true).Count();
            return count;
        }

        public void DeleteDoctor(int doctorId)
        {
            var doctordID = _hospitalDbContext.DoctorDepartments
                            .ToList()
                            .Where(x => x.DoctorId == doctorId);
            _hospitalDbContext.RemoveRange(doctordID);
            _hospitalDbContext.SaveChanges();
        }

        public (bool EmailExits, bool PhoneExits) isDoctorExits(string gmail, string phone)
        {
            try
            {
                bool IsgmailExits = !string.IsNullOrWhiteSpace(gmail) && _hospitalDbContext.Doctors.Any(x => x.Email.ToLower() == gmail.ToLower());
                bool IsphoneExits = !string.IsNullOrWhiteSpace(phone) && _hospitalDbContext.Doctors.Any(x => x.Phone.ToLower() == phone.ToLower());
                return (IsgmailExits, IsgmailExits);
            }
            catch
            {
                throw;
            }
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

        public List<Doctor> GetAllDoctors()
        {
            List<Doctor> doctor = _hospitalDbContext.Doctors
               .Include(d => d.DoctorDepartments)
               .ThenInclude(d => d.Department)
               .ToList();

            return doctor;
        }

        public Doctor GetDoctorById(int doctorId)
        {
            throw new NotImplementedException();
        }
    }
}

