namespace Hospital_Management.Models
{
    public partial class DoctorDepartment
    {
        public int DoctorDepartmentId { get; set; }
        public int DoctorId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int UserId { get; set; }
        public virtual Department Department { get; set; } = null!;
        public virtual Doctor Doctor { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
