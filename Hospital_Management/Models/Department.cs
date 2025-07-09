using System;
using System.Collections.Generic;

namespace Hospital_Management.Models
{
    public partial class Department
    {
        public Department()
        {
            DoctorDepartments = new HashSet<DoctorDepartment>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<DoctorDepartment> DoctorDepartments { get; set; }
    }
}
