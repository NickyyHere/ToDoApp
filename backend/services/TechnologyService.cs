using ToDoApp.dto;
using ToDoApp.dto.create;
using ToDoApp.factory;
using ToDoApp.mapper;
using ToDoApp.models;
using ToDoApp.repository.interfaces;
using ToDoApp.services.interfaces;

namespace ToDoApp.services
{
    public class TechnologyService : ITechnologyService
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly TechnologyMapper _technologyMapper = new();
        private readonly TechnologyFactory _technologyFactory = new();
        public TechnologyService(ITechnologyRepository technologyRepository)
        {
            _technologyRepository = technologyRepository;
        }
        public async Task AddTechnologyAsync(CreateTechnologyDTO createTechnologyDTO)
        {
            await _technologyRepository.AddAsync(_technologyFactory.Build(createTechnologyDTO));
        }
        public async Task<List<TechnologyDTO>> GetTechnologiesAsync()
        {
            var technologies = await _technologyRepository.GetAllAsync();
            return _technologyMapper.ToDTO(technologies);
        }

        public async Task ImportAsync(TechnologyDTO dto)
        {
            await _technologyRepository.AddAsync(_technologyFactory.Build(dto));
        }
    }
}