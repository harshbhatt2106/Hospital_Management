using Hospital_Management.Models;

namespace Hospital_Management.Interfaces
{
    public interface IDoctorService
    {
        Doctor GetDoctorById(int doctorId);
        bool AddDoctorWithDepartment(Doctor doctor, List<int> selectedDepartmentID, int userID);
        void DeleteDoctor(int doctorId);
        bool updateDoctorWithDepartment(Doctor doctor, List<int> selectedDepartmentID);
        string isDoctorExits(string doctorName, string phone);
        int countDoctors();

        List<Doctor> GetAllDoctors();
    }
}
