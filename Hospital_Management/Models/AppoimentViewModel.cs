namespace Hospital_Management.Models
{
    public class AppoimentViewModel
    {
        public List<Doctor> doctors { get; set; }
        public List<Department> departments { get; set; }
        public int DepartmentId { get; set; }  
        public int DoctorId { get; set; }
        public Patient Patient { get; set; }
        public Appointment Appointment { get; set; }
        public TimeSpan AppointmentTime { get; set; }
    }
}
