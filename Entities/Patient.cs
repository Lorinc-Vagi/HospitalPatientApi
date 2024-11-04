namespace HospitalPatientApi.Entities
{
    public class Patient
    {
        public int Id { set;get; }
        public string Name { set;get; }
        public DateTime DateOfBirth { set; get; }
        public string Gender { set; get; }
        public string ContactInfo { set; get; }
    }
}
