using ClinAgenda.src.Application.DTOs.Doctor;

namespace ClinAgenda.src.Core.Interfaces
{
    public interface IDoctorRepository
    {
        Task<(int total, IEnumerable<DoctorListDTO> doctor)> GetDoctorsAsync(string? name, int? specialtyId, int? statusId, int offset, int pageSize);
    }
}