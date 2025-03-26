namespace ClinAgenda.src.Application.DTOs.Doctor
{
    public class SpecialtyDoctorDTO
    {
        public int DoctorId { get; set; }
         public int SpecialtyId { get; set; }
         public required string SpecialtyName { get; set; }
         public int ScheduleDuration { get; set; }
    }
}