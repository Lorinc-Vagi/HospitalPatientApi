namespace HospitalPatientApi.Entities
{
    public class Treatment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string TreatmentDetails { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        //nav propers
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
