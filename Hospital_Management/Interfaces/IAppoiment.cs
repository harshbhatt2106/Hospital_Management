using Hospital_Management.Models;

namespace Hospital_Management.Interfaces
{
    public interface IAppoiment
    {
        List<Patient> SearchPatient(string patient);
    }
}
