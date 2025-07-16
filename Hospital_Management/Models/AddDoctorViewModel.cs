namespace Hospital_Management.Models
{
    public class AddDoctorViewModel
    {
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Qualification { get; set; } = null!;
        public string Specialization { get; set; } = null!;

        public List<int> SelectedDepartmentId { get; set; } = new();

        public List<Department> Departments { get; set; } = new();

    }
}
