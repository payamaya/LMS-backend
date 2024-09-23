using AutoMapper;
using LMS.Contracts;
using LMS.Infrastructure.Dtos;
using LMS.Models.Entities;
using LMS.Service.Contracts;

namespace LMS.Service
{
    public class ModuleService : IModuleService

    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ModuleService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ModuleDto?> GetModuleAsync(Guid courseId, bool trackChanges = false)
        {

            var module = await _uow.Module.GetModuleAsync(courseId, trackChanges);
            if (module is null) return null; //ToDo: Fix later

            return _mapper.Map<ModuleDto>(module);
        }

        public async Task<IEnumerable<ModuleDto>> GetModulesAsync(bool trackChanges = false)
        {
            var modules = await _uow.Module.GetModulesAsync(trackChanges);
            if (modules is null) return null!; //ToDo: Fix later

            return _mapper.Map<IEnumerable<ModuleDto>>(modules);

        }

    }
}
