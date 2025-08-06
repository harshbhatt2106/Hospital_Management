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

        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Enter Phone number")]
        [Phone]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone Number must be exactly 10 digits and contain only numbers.")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Enter Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Enter Qualification")]
        public string Qualification { get; set; } = null!;

        [Required(ErrorMessage = "Enter Specialization")]
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
