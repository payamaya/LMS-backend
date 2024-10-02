using LMS.Infrastructure.Dtos;

using System.Security.Claims;

namespace LMS.Service.Contracts
{
    public interface ICourseService
    {
        Task<CourseDetailedDto?> GetCourseAsync(Guid courseId, bool trackChanges = false);
        Task<CourseDetailedDto?> GetCourseAsync(ClaimsPrincipal? user, bool trackChanges = false);
        Task<IEnumerable<CourseDto>> GetCoursesAsync(bool trackChanges = false);

        Task DeleteCourseAsync(Guid id);
        Task<CourseDto> PostCourseAsync(CourseDto course);
    }
}
