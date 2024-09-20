using LMS.Models.Entities;

namespace LMS.Contracts
{
    public interface IModuleRepository
    {
        Task<Module?> GetModuleAsync(Guid id, bool trackChanges);
        Task<IEnumerable<Module>> GetModulesAsync(bool trackChanges);

        Task CreateAsync(Module entity);
        void Update(Module entity);
        void Delete(Module entity);
    }
}