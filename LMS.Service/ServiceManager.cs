using LMS.Service.Contracts;

namespace LMS.Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthService> _authService;
    private readonly Lazy<ICourseService> _courseService;
    private readonly Lazy<IModuleService> _moduleService;
    private readonly Lazy<IActivityService> _activityService;
    private readonly Lazy<IActivityTypeService> _activityTypeService;

    public IAuthService AuthService => _authService.Value;
    public ICourseService CourseService => _courseService.Value;
    public IModuleService ModuleService => _moduleService.Value;
    public IActivityService ActivityService => _activityService.Value;
    public IActivityTypeService ActivityTypeService => _activityTypeService.Value;

    public ServiceManager(
        Lazy<IAuthService> authService,
        Lazy<ICourseService> courseService,
        Lazy<IModuleService> moduleService,
        Lazy<IActivityService> activityService,
        Lazy<IActivityTypeService> activityTypeService
        )
    {
        _authService = authService;
        _courseService = courseService;
        _moduleService = moduleService;
        _activityService = activityService;
        _activityTypeService = activityTypeService;
    }
}
