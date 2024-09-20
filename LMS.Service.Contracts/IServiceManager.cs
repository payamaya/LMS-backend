namespace LMS.Service.Contracts;

public interface IServiceManager
{
    IAuthService AuthService { get; }
    ICourseService CourseService { get; }
    IActivityTypeService ActivityTypeService { get; }
}