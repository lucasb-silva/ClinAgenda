using ClinAgenda.src.Application.DTOs.Specialty;
using ClinAgenda.src.Application.DTOs.Status;

namespace ClinAgenda.src.Application.DTOs.Doctor
{
    public class DoctorListReturnDTO
    {
        public int Id { get; set; }
         public required string Name { get; set; }
        public required string StatusName { get; set; }
        public required StatusDTO Status { get; set; }
        public required SpecialtyDTO Specialty { get; set; }
    }
}