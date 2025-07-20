using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Appointments = new HashSet<Appointment>();
            DoctorDepartments = new HashSet<DoctorDepartment>();
        }
        public int DoctorId { get; set; }
        public string Name { get; set; } = null!;

        [Required]
        [Phone]
        public string Phone { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = null!;

        [Required]
        public string Qualification { get; set; } = null!;
        [Required]
        public string Specialization { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Appointment> Appointments { get; set; }

        [Required]
        public virtual ICollection<DoctorDepartment> DoctorDepartments { get; set; }
    }
}
