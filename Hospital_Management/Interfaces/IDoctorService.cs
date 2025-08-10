using Hospital_Management.Models;

namespace Hospital_Management.Interfaces
{
    public interface IDoctorService
    {
        Doctor GetDoctorById(int doctorId);
        bool AddDoctorWithDepartment(Doctor doctor, List<int> selectedDepartmentID, int userID);
        void DeleteDoctor(int doctorId);
        bool updateDoctorWithDepartment(Doctor doctor, List<int> selectedDepartmentID);
        (bool EmailExits, bool PhoneExits) isDoctorExits(string phone, string GmailID);
        int countDoctors();

        List<Doctor> GetAllDoctors();
    }
}
