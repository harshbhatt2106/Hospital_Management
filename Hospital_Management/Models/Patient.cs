using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Created = DateTime.Now;
            Modified = DateTime.Now;
            Appointments = new HashSet<Appointment>();
        }
        public int PatientId { get; set; }
        [Required]
        public string? Name { get; set; } = null!;
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        public string? Gender { get; set; } = null!;
        public string? Email { get; set; } = null!;

        [Required(ErrorMessage = "Enter Phone number")]
        [Phone]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone Number must be exactly 10 digits and contain only numbers.")]

        public string? Phone { get; set; } = null!;

        [Required]
        public string? Address { get; set; } = null!;
        [Required]
        public string? City { get; set; } = null!;
        [Required]
        public string? State { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
