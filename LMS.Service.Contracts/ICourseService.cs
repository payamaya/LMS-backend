using LMS.Models.Entities;

namespace LMS.Service.Contracts
{
    public interface ICourseService
    {
        Task<Course?> GetCourseAsync(Guid courseId, bool trackChanges = false); 
        Task<IEnumerable<Course>> GetCoursesAsync(bool trackChanges = false);
    }
}
