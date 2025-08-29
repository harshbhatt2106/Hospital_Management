using Hospital_Management.Models;
using Microsoft.CodeAnalysis;

namespace Hospital_Management.Interfaces
{
    public interface IAppoiment
    {
        List<Patient> SearchPatient(string patient);

        bool BookAppoinment(Appointment appointment);

        List<AppoimentViewModel> ListAppointments();
    }
}
