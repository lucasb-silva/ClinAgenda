namespace ClinAgenda.src.Application.DTOs.Patient
{
    public class PatientInsertDTO
    {
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required string DocumentNumber { get; set; }
        public required int StatusId { get; set; }
        public required string BirthDate { get; set; }
    }
}