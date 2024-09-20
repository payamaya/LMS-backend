using AutoMapper;
using LMS.Contracts;
using LMS.Infrastructure.Dtos;
using LMS.Models.Entities;
using LMS.Service.Contracts;

namespace LMS.Service
{
    public class CourseService : ICourseService

    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CourseService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<CourseDto?> GetCourseAsync(Guid courseId, bool trackChanges = false)
        {

            var course = await _uow.Course.GetCourseAsync(courseId, trackChanges);
            if (course is null) return null; //ToDo: Fix later

            return _mapper.Map<CourseDto>(course);
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesAsync(bool trackChanges = false)
        {
            var courses = await _uow.Course.GetCoursesAsync(trackChanges);
            if (courses is null) return null!; //ToDo: Fix later

            return _mapper.Map<IEnumerable<CourseDto>>(courses);

        }

    }
}
