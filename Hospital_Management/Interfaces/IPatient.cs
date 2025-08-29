namespace Hospital_Management.Interfaces
{
    public interface IPatient
    {
        public int addPatient(Patient patient);
        public List<Patient> GetAllpatients();
        bool IsPatientExits(string phone);

        int GetPatientID(string phone);

        Patient GetPatientByID(int id);
    }
}
