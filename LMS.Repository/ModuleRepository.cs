using LMS.Contracts;
using LMS.Models.Entities;
using LMS.Persistance;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repository
{
    //public class CourseRepository : RepositoryBase<User>, ICourseRepository
    public class ModuleRepository : RepositoryBase<Module>, IModuleRepository

    {
        public ModuleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Module?> GetModuleAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(a => a.Id.Equals(id), trackChanges).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Module>> GetModulesAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).ToListAsync();
        }
        public void DeleteModule(Module module)
        {
            Delete(module);
        }
    }
}
