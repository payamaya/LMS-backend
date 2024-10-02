using AutoMapper;
using LMS.Application.Exceptions;
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
            if (module is null)
                return null; //ToDo: Fix later

            return _mapper.Map<ModuleDto>(module);
        }

        public async Task<IEnumerable<ModuleDto>> GetModulesAsync(bool trackChanges = false)
        {
            var modules = await _uow.Module.GetModulesAsync(trackChanges);
            if (modules is null)
                return null!; //ToDo: Fix later

            return _mapper.Map<IEnumerable<ModuleDto>>(modules);

        }

        public async Task DeleteModuleAsync(Guid id)
        {

            var module = await GetModuleBy(id)
                   ?? throw new NotFoundException("Acitivity", id);
            _uow.Module.Delete(module);
            await _uow.CompleteAsync();
        }
        private async Task<Module?> GetModuleBy(Guid id) =>
            await _uow.Module.GetModuleAsync(id, trackChanges: false);

        public async Task<ModuleDto> PostModuleAsync(ModulePostDto modulePostDto)
        {
            Course course = await _uow.Course.GetCourseAsync(modulePostDto.CourseId, false)
                ?? throw new NotFoundException("Course Not found Exception", modulePostDto.CourseId);

            var module = _mapper.Map<Module>(modulePostDto)
                ?? throw new BadRequestException($"No Module Found");
            await _uow.Module.CreateAsync(module);
            await _uow.CompleteAsync();

            return _mapper.Map<ModuleDto>(module);

        }
    }
}
