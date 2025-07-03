using Hospital_Management.Models;

namespace Hospital_Management.Interfaces
{
    public interface IDepartmentService
    {
        List<Department> departments();
        bool CheckDepartment(string department);
        bool AddDepartment(Department department);

        bool DeleteDepartment(int departmentId);
        void ReloadDepartments();
        bool UpdateDepartment(Department department);

    }
}
