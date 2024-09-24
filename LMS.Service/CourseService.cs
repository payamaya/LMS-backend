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

        public async Task<CourseDto?> GetCourseAsync(Guid courseId, ClaimsPrincipal? userClaim, bool trackChanges = false)
        {
            //ClaimsPrincipal user = User;
            if (userClaim != null)
            {
                var user = await _userManager.GetUserAsync(userClaim);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null && roles.Contains("Student"))
                    {
                        courseId = (Guid)user.CourseId!;
                    }
                }
            }

            var course = await _uow.Course.GetCourseAsync(courseId, trackChanges);
            if (course is null)
            {
                throw new NotFoundException(nameof(GetCourseAsync), courseId);
                //return null; //ToDo: Fix later
            }

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
