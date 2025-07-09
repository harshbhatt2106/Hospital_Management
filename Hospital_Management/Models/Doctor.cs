using System;
using System.Collections.Generic;

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
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Qualification { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<DoctorDepartment> DoctorDepartments { get; set; }
    }
}
