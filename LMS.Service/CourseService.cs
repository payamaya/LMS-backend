using AutoMapper;

using LMS.Application.Exceptions;
using LMS.Contracts;
using LMS.Infrastructure.Dtos;
using LMS.Models.Entities;
using LMS.Service.Contracts;

using Microsoft.AspNetCore.Identity;

using System.Security.Claims;

namespace LMS.Service
{
    public class CourseService : ICourseService

    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CourseService(
            UserManager<User> userManager,
            IUnitOfWork uow,
            IMapper mapper)
        {
            _userManager = userManager;
            _uow = uow;
            _mapper = mapper;
        }

<<<<<<< HEAD
        public async Task<CourseDetailedDto?> GetCourseAsync(Guid courseId, bool trackChanges = false)
=======
     

        public async Task<CourseDto?> GetCourseAsync(Guid courseId, bool trackChanges = false)
>>>>>>> 6284b88 (Delete Method for All controller except user and activityType not finished yet)
        {
            var course = await _uow.Course.GetCourseAsync(courseId, trackChanges);
            if (course is null)
            {
                throw new NotFoundException(nameof(GetCourseAsync), courseId);
                //return null; //ToDo: Fix later
            }

            return _mapper.Map<CourseDetailedDto>(course);
        }

        public async Task<CourseDetailedDto?> GetCourseAsync(ClaimsPrincipal? userClaim, bool trackChanges = false)
        {
            //ClaimsPrincipal user = User;
            if (userClaim == null) throw new BadRequestException($"Bad user claims");

            var user = await _userManager.GetUserAsync(userClaim);
            if (user == null) throw new BadRequestException($"User not found");

            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null || !roles.Contains("Student"))
                throw new BadRequestException($"Role not found");
            
            var courseId = (Guid)user.CourseId!;
            
            var course = await _uow.Course.GetCourseAsync(courseId, trackChanges);
            if (course is null)
            {
                throw new NotFoundException(nameof(GetCourseAsync), courseId);
                //return null; //ToDo: Fix later
            }

            return _mapper.Map<CourseDetailedDto>(course);
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesAsync(bool trackChanges = false)
        {
            var courses = await _uow.Course.GetCoursesAsync(trackChanges);
            if (courses is null) return null!; //ToDo: Fix later

            return _mapper.Map<IEnumerable<CourseDto>>(courses);

        }

        public async Task DeleteCourseAsync(Guid id)
        {

            var course = await GetCourseBy(id)
                ?? throw new NotFoundException("Course", id);
            _uow.Course.Delete(course);
            await _uow.CompleteAsync();
            
        }
        private async Task<Course?> GetCourseBy(Guid id) => 
            await _uow.Course.GetCourseAsync(id, trackChanges: false);

    }
}
