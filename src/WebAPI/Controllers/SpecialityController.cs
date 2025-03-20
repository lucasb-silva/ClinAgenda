using ClinAgenda.src.Application.DTOs.Speciality;
using ClinAgendaAPI;
using ClinAgendaAPI.SpecialityUseCase;
using Microsoft.AspNetCore.Mvc;

namespace ClinAgenda.src.WebAPI.Controllers
{
    [ApiController]
    [Route("api/speciality")]
    public class SpecialityController : ControllerBase
    {
        private readonly SpecialityUseCase _specialityUseCase;

        public SpecialityController(SpecialityUseCase service)
        {
            _specialityUseCase = service;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetSpecialityAsync([FromQuery] int itemsPerPage = 10, [FromQuery] int page = 1)
        {
            try
            {
                var specialty = await _specialityUseCase.GetSpecialityAsync(itemsPerPage, page);
                return Ok(specialty);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar speciality: {ex.Message}");
            }
        }

        [HttpGet("listById/{id}")]
        public async Task<IActionResult> GetSpecialityByIdAsync(int id)
        {
            try
            {
                var specialty = await _specialityUseCase.GetSpecialityByIdAsync(id);

                if (specialty == null)
                {
                    return NotFound($"Speciality com ID {id} não encontrado.");
                }

                return Ok(specialty);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar speciality por ID: {ex.Message}");
            }
        }

        [HttpPost("insert")]
        public async Task<IActionResult> CreateSpecialityAsync([FromBody] SpecialityInsertDTO speciality)
        {
            try
            {
                if (speciality == null || string.IsNullOrWhiteSpace(speciality.Name))
                {
                    return BadRequest("Os dados da Especialidade são inválidos.");
                }

                var createdSpeciality = await _specialityUseCase.CreateSpecialityAsync(speciality);
                var infosSpecialityCreated = await _specialityUseCase.GetSpecialityByIdAsync(createdSpeciality);
                
                return CreatedAtAction(nameof(GetSpecialityByIdAsync), new { id = createdSpeciality }, infosSpecialityCreated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar speciality: {ex.Message}");
            }
        }
    }

}