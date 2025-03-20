using ClinAgenda.src.Application.DTOs.Speciality;
using ClinAgenda.src.Core.Interfaces;

namespace ClinAgendaAPI.SpecialityUseCase
{
    public class SpecialityUseCase
    {
        // Declaração de um campo somente leitura (_specialityRepository) que armazenará a instância do repositório de speciality.
        private readonly ISpecialityRepository _specialityRepository;

        // Construtor da classe SpecialityUseCase, que recebe uma implementação de ISpecialityRepository como dependência.
        public SpecialityUseCase(ISpecialityRepository specialityRepository)
        {
            _specialityRepository = specialityRepository; // Atribui o repositório injetado ao campo privado.
        }

        // Método assíncrono que obtém uma lista paginada de speciality.
        public async Task<object> GetSpecialityAsync(int itemsPerPage, int page)
        {
            // Chama o repositório para obter os dados paginados e o total de registros.
            var (total, rawData) = await _specialityRepository.GetAllAsync(itemsPerPage, page);

            // Retorna um objeto anônimo contendo o total de itens e a lista de speciality formatada.
            return new
            {
                total,
                items = rawData.ToList()
            };
        }

        // Método assíncrono que obtém um speciality pelo seu ID.
        public async Task<SpecialityDTO?> GetSpecialityByIdAsync(int id)
        {
            // Chama o repositório para buscar um speciality específico pelo ID.
            return await _specialityRepository.GetByIdAsync(id);
        }

        // Método assíncrono que cria um novo speciality e retorna o ID gerado.
        public async Task<int> CreateSpecialityAsync(SpecialityInsertDTO specialityDTO)
        {
            // Cria uma nova instância de SpecialityInsertDTO com os dados fornecidos.
            var speciality = new SpecialityInsertDTO
            {
                Name = specialityDTO.Name,
                ScheduleDuration = specialityDTO.ScheduleDuration
            };

            // Chama o repositório para inserir o novo speciality e obtém o ID gerado.
            var newSpecialityId = await _specialityRepository.InsertSpecialityAsync(speciality);

            return newSpecialityId; // Retorna o ID do novo speciality criado.
        }
    }
}
