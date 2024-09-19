using LMS.Contracts;
using LMS.Persistance;

namespace LMS.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Lazy<ICourseRepository> _courseRepository;
        //private readonly Lazy<IActorRepository> _actorRepository;

        public ICourseRepository Course => _courseRepository.Value;
        // public IActorRepository Actor => _actorRepository.Value;


        public UnitOfWork(ApplicationDbContext context, Lazy<ICourseRepository> courseRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _courseRepository = courseRepository;
           // _actorRepository = actorRepository;
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
