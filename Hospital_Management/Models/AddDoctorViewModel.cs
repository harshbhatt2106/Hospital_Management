namespace Hospital_Management.Models
{
    public class AddDoctorViewModel : Doctor
    {        
        public List<int> SelectedDepartmentId { get; set; } = new();

        public List<Department> Departments { get; set; } = new();

    }
}
