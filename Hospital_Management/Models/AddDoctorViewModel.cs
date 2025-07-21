using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Models
{
    public class AddDoctorViewModel : Doctor
    {
        [Required(ErrorMessage = "Please select Department name ")]
        public List<int> SelectedDepartmentId { get; set; } = new();
        public List<SelectListItem> Departments { get; set; } = new();

    }
}
