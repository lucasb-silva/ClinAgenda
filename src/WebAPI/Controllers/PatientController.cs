using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinAgenda.src.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace ClinAgenda.src.WebAPI.Controllers
{
    public class PatientController : ControllerBase
    {
        private readonly PatientUseCase _patientUseCase;

        public PatientController(PatientUseCase service)
        {
            _patientUseCase = service;
        }
        [HttpGet("list")]
        public async Task<IActionResult> GetPatientsAsync([FromQuery] string? name, [FromQuery] string? documentNumber, [FromQuery] int? statusId, [FromQuery] int itemsPerPage = 10, [FromQuery] int page = 1)
        {
            try
            {
                var result = await _patientUseCase.GetPatientsAsync(name, documentNumber, statusId, itemsPerPage, page);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}