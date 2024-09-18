using LMS.Models.Entities;

namespace LMS.Contracts
{
    public interface ICourseRepository
    {

        Task<Course?> GetCourseAsync(Guid id, bool trackChanges);
        Task<IEnumerable<Course>> GetCoursesAsync(Guid id, bool trackChanges);

        Task CreateAsync(Course entity);
        void Update(Course entity);
        void Delete(Course entity);

    }
}
