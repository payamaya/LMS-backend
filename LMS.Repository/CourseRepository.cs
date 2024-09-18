using LMS.Contracts;
using LMS.Models.Entities;
using LMS.Persistance;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repository
{
    //public class CourseRepository : RepositoryBase<User>, ICourseRepository
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository

    {
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Course?> GetCourseAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(a => a.Id.Equals(id), trackChanges).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync(Guid id, bool trackChanges)
        {
            return await FindAll(trackChanges).ToListAsync();
        }
    }
}
