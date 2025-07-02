using Hospital_Management.Models;

namespace Hospital_Management.Interfaces
{
    public interface IDepartmentService
    {
        List<Department> departments();
        bool CheckDepartment(string department);
        bool AddDepartment(Department department);

        //void ReloadDepartments();
        //bool CheckDepartment(string departmentname);
        //bool UpdateDepartment(Department department);
        //bool DeleteDepartment(Department department);

    }
}
