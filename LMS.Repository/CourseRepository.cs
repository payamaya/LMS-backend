using LMS.Contracts;
using LMS.Models.Entities;
using LMS.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Repository
{
    //public class CourseRepository : RepositoryBase<User>, ICourseRepository
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository

    {
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<Course?> GetCourseAsync(Guid id, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Course>> GetCoursesAsync(Guid id, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
