using LMS.Contracts;
using LMS.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
