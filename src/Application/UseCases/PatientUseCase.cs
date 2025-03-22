using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinAgenda.src.Application.DTOs.Patient;
using ClinAgenda.src.Application.DTOs.Status;
using ClinAgenda.src.Core.Interfaces;

namespace ClinAgenda.src.Application.UseCases
{
    public class PatientUseCase
    {
        private readonly IPatientRepository _patientRepository;
        public PatientUseCase(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public async Task<object> GetPatientsAsync(string? name, string? documentNumber, int? statusId, int itemsPerPage, int page)
        {
            var (total, rawData) = await _patientRepository.GetPatientsAsync(name, documentNumber, statusId, itemsPerPage, page);

            var patients = rawData
                .Select(p => new PatientListReturnDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    PhoneNumber = p.PhoneNumber,
                    DocumentNumber = p.DocumentNumber,
                    BirthDate = p.BirthDate,
                    Status = new StatusDTO
                    {
                        Id = p.StatusId,
                        Name = p.StatusName
                    }
                })
                .ToList();

            return new { total, items = patients };
        }
    }
}