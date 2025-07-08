using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Models
{
    public class Department
    {
        public int DepartmentID{ get; set; }
        
        [Required]
        public string ?DepartmentName { get; set; }

        [Required]
        public string ?Description { get; set; }
        public DateTime? Modified { get; set; }
        public int UserID{ get; set; }

        public bool IsActive { get; set; }
    }
}
