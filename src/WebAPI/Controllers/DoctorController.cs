using ClinAgenda.src.Application.UseCases;
using ClinAgendaAPI;
using ClinAgendaAPI.StatusUseCase;
using Microsoft.AspNetCore.Mvc;

namespace ClinAgenda.src.WebAPI.Controllers
{
    [ApiController]
    [Route("api/doctor")]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorUseCase _doctorUseCase;

        private readonly StatusUseCase _statusUseCase;

        private readonly SpecialtyUseCase _specialtyUseCase;

        public DoctorController(DoctorUseCase doctorUseCase, StatusUseCase statusUseCase, SpecialtyUseCase specialtyUseCase)
        {
            _doctorUseCase = doctorUseCase;
            _statusUseCase = statusUseCase;
            _specialtyUseCase = specialtyUseCase;
        }
        [HttpGet ("list")]
        public async Task<IActionResult> GetDoctors([FromQuery] string? name, [FromQuery] int? specialtyId, [FromQuery] int? statusId, [FromQuery] int itemsPerPage = 10, [FromQuery] int page = 1)
        {
            try
            {
                var result = await _doctorUseCase.GetDoctorsAsync(name, specialtyId, statusId, itemsPerPage, page);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

    }
}