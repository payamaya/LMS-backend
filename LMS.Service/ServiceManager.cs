using LMS.Service.Contracts;

namespace LMS.Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthService> _authService;
    public IAuthService AuthService => _authService.Value;

    public ServiceManager(Lazy<IAuthService> authService)
    {
        _authService = authService;
    }
}
