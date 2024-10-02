using LMS.Infrastructure.Dtos;

namespace LMS.Service.Contracts
{
    public interface IModuleService
    {
        Task<ModuleDto?> GetModuleAsync(Guid courseId, bool trackChanges = false);
        Task<IEnumerable<ModuleDto>> GetModulesAsync(bool trackChanges = false);

        Task DeleteModuleAsync(Guid id);
        Task<ModuleDto> PostModuleAsync(ModulePostDto modulePostDto);
    }
}
