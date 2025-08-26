namespace Hospital_Management.Interfaces
{
    public interface IPatient
    {
        public bool addPatient(Patient patient);
        public List<Patient> GetAllpatients();
    }
}
