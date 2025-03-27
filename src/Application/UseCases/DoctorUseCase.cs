using System;
using System.Collections.Generic;
using ClinAgenda.src.Application.DTOs.Doctor;
using ClinAgenda.src.Application.DTOs.Specialty;
using ClinAgenda.src.Application.DTOs.Status;
using ClinAgenda.src.Core.Interfaces;

namespace ClinAgenda.src.Application.UseCases
{
    public class DoctorUseCase
    {
        private readonly IDoctorRepository _doctorRepository;
        public DoctorUseCase(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public async Task<object> GetDoctorsAsync(string? name, int? specialtyId, int? statusId, int itemsPerPage, int page)
        {
            int offset = (page - 1) * itemsPerPage;

            var doctors = (await _doctorRepository.GetDoctorsAsync(name, specialtyId, statusId, offset, itemsPerPage)).ToList();

            if(!doctors.Any())
                return new {total = 0, items = new List<DoctorListReturnDTO>()};
            
            var doctorIds = doctors.Select(d => d.Id).ToArray();
            var specialties = (await _doctorRepository.GetDoctorSpecialtiesAsync(doctorIds)).ToList();

            var result = doctors.Select(d => new DoctorListReturnDTO
            {
                Id = d.Id,
                Name = d.Name,
                Status = new StatusDTO
                {
                    Id = d.StatusId,
                    Name = d.StatusName
                },
                Specialty = specialties.Where(s => s.DoctorId == d.Id)
                .Select(s => new SpecialtyDTO 
                {
                    Id = s.SpecialtyId,
                    Name = s.SpecialtyName,
                    ScheduleDuration = s.ScheduleDuration
                })
                .ToList()
            });

            return new
            {
                total = result.Count(),
                items = result.ToList()
            };
        }
    }
}