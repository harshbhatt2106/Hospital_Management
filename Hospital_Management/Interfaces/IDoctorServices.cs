using Hospital_Management.Models;

namespace Hospital_Management.Interfaces
{
    public interface IDoctorServices
    {
        List<Doctor> GetAllDoctors();
        Doctor GetDoctorById(int doctorId);
        bool AddDoctorWithDepartment(Doctor doctor, List<int> selectedDepartmentID, int userID);
        bool UpdateDoctor(Doctor doctor);
        bool DeleteDoctor(int doctorId);
    }
}
