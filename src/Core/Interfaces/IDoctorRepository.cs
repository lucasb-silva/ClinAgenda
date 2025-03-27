using ClinAgenda.src.Application.DTOs.Doctor;

namespace ClinAgenda.src.Core.Interfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<DoctorListDTO>> GetDoctorsAsync(string? name, int? specialtyId, int? statusId, int offset, int pageSize);
        Task<IEnumerable<SpecialtyDoctorDTO>> GetDoctorSpecialtiesAsync(int[] doctorIds);
    }
}