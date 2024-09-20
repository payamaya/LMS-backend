using LMS.Contracts;
using LMS.Models.Entities;
using LMS.Persistance;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repository
{
    //public class CourseRepository : RepositoryBase<User>, ICourseRepository
    public class ActivityTypeRepository : RepositoryBase<ActivityType>, IActivityTypeRepository
    {
        public ActivityTypeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ActivityType?> GetActivityTypeAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(a => a.Id.Equals(id), trackChanges).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ActivityType>> GetActivityTypesAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).ToListAsync();
        }
    }
}
