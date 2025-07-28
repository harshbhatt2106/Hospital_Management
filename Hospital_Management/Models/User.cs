namespace Hospital_Management.Models
{
    public partial class User
    {
        public User()
        {
            Appointments = new HashSet<Appointment>();
            Departments = new HashSet<Department>();
            DoctorDepartments = new HashSet<DoctorDepartment>();
            Doctors = new HashSet<Doctor>();
            Patients = new HashSet<Patient>();
        }
        public int UserID { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string MobileNo { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime? Created { get; set; }
        public DateTime Modified { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<DoctorDepartment> DoctorDepartments { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
