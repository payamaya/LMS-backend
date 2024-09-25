using LMS.Contracts;
using LMS.Persistance;

namespace LMS.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Lazy<ICourseRepository> _courseRepository;
        private readonly Lazy<IActivityTypeRepository> _activityTypeRepository;
        private readonly Lazy<IActivityRepository> _activityRepository;
        private readonly Lazy<IModuleRepository> _moduleRepository;
        private readonly Lazy<IUserRepository> _userRepository;

        //private readonly Lazy<IActorRepository> _actorRepository;

        public ICourseRepository Course => _courseRepository.Value;
        public IModuleRepository Module => _moduleRepository.Value;
        public IActivityRepository Activity => _activityRepository.Value;
        public IActivityTypeRepository ActivityType => _activityTypeRepository.Value;
        public IUserRepository User => _userRepository.Value;
        // public IActorRepository Actor => _actorRepository.Value;


        public UnitOfWork(
            ApplicationDbContext context,
            Lazy<ICourseRepository> courseRepository,
            Lazy<IActivityTypeRepository> activityTypeRepository,
            Lazy<IActivityRepository> activityRepository,
            Lazy<IModuleRepository> moduleRepository,
            Lazy<IUserRepository> userRepository)
        {
            _context = context
                ?? throw new ArgumentNullException(nameof(context));

            _courseRepository = courseRepository
                ?? throw new ArgumentNullException(nameof(courseRepository));

            _activityTypeRepository = activityTypeRepository
                ?? throw new ArgumentNullException(nameof(activityTypeRepository));

            _activityRepository = activityRepository
                ?? throw new ArgumentNullException(nameof(activityRepository));

            _moduleRepository = moduleRepository
                ?? throw new ArgumentNullException(nameof(moduleRepository));

            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task CompleteAsync()
        {
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
