﻿using Hospital_Management.Models;

namespace Hospital_Management.Interfaces
{
    public interface IDoctorServices
    {
        List<Doctor> GetAllDoctors();        
        Doctor GetDoctorById(int doctorId);        
        bool AddDoctorWithDepartment(Doctor doctor, List<int> selectedDepartmentID, int userID);
        void DeleteDoctor(int doctorId);
        bool updateDoctorWithDepartment(Doctor doctor, List<int> selectedDepartmentID);
        string isDoctorExits(string doctorName,string phone);
        int countDoctors();
    }
}
