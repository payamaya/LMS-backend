using LMS.Contracts;
using LMS.Models.Entities;
using LMS.Persistance;
using Microsoft.EntityFrameworkCore;

namespace LMS.Repository
{
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository

    {
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Course?> GetCourseAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(a => a.Id.Equals(id), trackChanges)
                .Include(c => c.Modules)
                    .ThenInclude(m => m.Activities)
                    .ThenInclude(a => a.ActivityType)
                .Include(c => c.Users)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Include(c => c.Users)
                .ToListAsync();
        }

        public void DeleteCourse(Course course)
        {
            Delete(course);
        }
    }
}
