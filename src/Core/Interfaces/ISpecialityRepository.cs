using ClinAgenda.src.Application.DTOs.Speciality;

namespace ClinAgenda.src.Core.Interfaces
{
    public interface ISpecialityRepository
    {
        Task<SpecialityDTO> GetByIdAsync(int id);
        Task<int> DeleteSpecialityAsync(int id);
        Task<int> InsertSpecialityAsync(SpecialityInsertDTO specialityInsertDTO);
        Task<(int total, IEnumerable<SpecialityDTO> specialtys)> GetAllAsync(int? itemsPerPage, int? page);
    }
}