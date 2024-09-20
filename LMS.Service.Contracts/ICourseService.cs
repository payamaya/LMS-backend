using LMS.Infrastructure.Dtos;
using LMS.Models.Entities;

namespace LMS.Service.Contracts
{
    public interface ICourseService
    {
        Task<CourseDto?> GetCourseAsync(Guid courseId, bool trackChanges = false); 
        Task<IEnumerable<CourseDto>> GetCoursesAsync(bool trackChanges = false);
    }
}
