namespace Hospital_Management.Models
{
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentStatus { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string SpecialRemarks { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int UserId { get; set; }
        public decimal? TotalConsultedAmount { get; set; }
        public virtual Doctor Doctor { get; set; } = null!;
        public virtual Patient Patient { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
