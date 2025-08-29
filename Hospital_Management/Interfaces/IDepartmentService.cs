using Hospital_Management.Models;

namespace Hospital_Management.Interfaces
{
    public interface IDepartmentService
    {
        List<Department> departments();
        bool CheckDepartment(string department);
        bool AddDepartment(Department department);
        bool setDepartmentStatus(int departmentID);
        bool DeleteDepartment(int departmentId);
        bool UpdateDepartment(Department department);
        int CountDepartments();
        List<Department> SearchDepartmentByDoctorID(int DoctorID);
    }
}
