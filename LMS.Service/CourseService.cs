using LMS.Contracts;
using LMS.Models.Entities;
using LMS.Service.Contracts;

namespace LMS.Service
{
    public class CourseService : ICourseService

    {

        private readonly IUnitOfWork _uow;
        //private readonly IMapper _mapper;

        public CourseService(IUnitOfWork uow)
        {
            _uow = uow;
            //_mapper = mapper;
        }

        public async Task<Course?> GetCourseAsync(Guid courseId, bool trackChanges = false)
        {
            var course = await _uow.Course.GetCourseAsync(courseId, trackChanges);

            if (course is null) return null!; //ToDo: Fix later

            //_mapper.Map<IEnumerable<CourseDTO>>(courses);
            return course; 
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync( bool trackChanges = false)
        {
            var courses = await _uow.Course.GetCoursesAsync(trackChanges);

            if (courses is null) return null!; //ToDo: Fix later

            return courses; 
            
            //_mapper.Map<IEnumerable<CourseDTO>>(courses);
        }  
        
        
    }
}
