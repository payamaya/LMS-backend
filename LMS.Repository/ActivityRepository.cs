using LMS.Contracts;
using LMS.Models.Entities;
using LMS.Persistance;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repository
{
    //public class CourseRepository : RepositoryBase<User>, ICourseRepository
    public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository

    {
        public ActivityRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Activity?> GetActivityAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(a => a.Id.Equals(id), trackChanges).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Activity>> GetActivitiesAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).ToListAsync();
        }

        public void DeleteActivity(Activity activity)
        {
            Delete(activity); // This will use the generic delete method in your repository base class
        }

    }
}
