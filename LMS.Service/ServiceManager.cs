using LMS.Service.Contracts;

namespace LMS.Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthService> _authService;
    private readonly Lazy<ICourseService> _courseService;


    public IAuthService AuthService => _authService.Value;
    public ICourseService CourseService => _courseService.Value;

    public ServiceManager(Lazy<IAuthService> authService, Lazy<ICourseService> courseService)
    {
        _authService = authService;
        _courseService = courseService;
        
    }
}
