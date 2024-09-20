using LMS.Service.Contracts;

namespace LMS.Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthService> _authService;
    private readonly Lazy<ICourseService> _courseService;
    private readonly Lazy<IActivityService> _activityService;
    private readonly Lazy<IActivityTypeService> _activityTypeService;

    public IAuthService AuthService => _authService.Value;
    public ICourseService CourseService => _courseService.Value;
    public IActivityService ActivityService => _activityService.Value;
    public IActivityTypeService ActivityTypeService => _activityTypeService.Value;

    public ServiceManager(
        Lazy<IAuthService> authService,
        Lazy<ICourseService> courseService,
        Lazy<IActivityService> activityService,
        Lazy<IActivityTypeService> activityTypeService
        )
    {
        _authService = authService;
        _courseService = courseService;
        _activityService = activityService;
        _activityTypeService = activityTypeService;
    }
}
