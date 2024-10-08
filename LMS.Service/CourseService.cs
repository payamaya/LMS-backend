﻿using AutoMapper;

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
        public async Task<CourseDetailedDto?> GetCourseAsync(Guid courseId, bool trackChanges = false)
        {
            var course = await _uow.Course.GetCourseAsync(courseId, trackChanges);
            if (course is null)
            {
                throw new NotFoundException(nameof(GetCourseAsync), courseId);
                //return null; //ToDo: Fix later
            }

            return _mapper.Map<CourseDetailedDto>(course);
        }

        // Bad request should never reach here, sent to Unauthorized middleware
        public async Task<CourseDetailedDto?> GetCourseAsync(ClaimsPrincipal? userClaim, bool trackChanges = false)
        {
            if (userClaim == null)
                throw new BadRequestException($"Bad user claims");

            var userId = _userManager.GetUserId(userClaim);
            var user = await _uow.User.GetUserWithCoursesAsync(userId!, trackChanges);

            if (user == null)
                throw new BadRequestException($"User not found");

            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null || !roles.Contains("Student"))
                throw new BadRequestException($"Role not found");

            var courseId = (Guid)user.Course!.FirstOrDefault()!.Id;

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
            if (courses is null)
                return null!; //ToDo: Fix later

            return _mapper.Map<IEnumerable<CourseDto>>(courses);

        }

        public async Task<CourseDto> PostCourseAsync(CoursePostDto postDto)
        {
            var course = _mapper.Map<Course>(postDto)
                ?? throw new BadRequestException($"Bad input");

            if (postDto.TeacherId.HasValue)
            {
                Guid teacherId = postDto.TeacherId.Value;
                var teacher = await _uow.User.GetUserAsync(postDto.TeacherId.Value, true)
                    ?? throw new NotFoundException($"Teacher not found", postDto);
                if (teacher.IsStudent)
                {
                    throw new Exception("The user is not a teacher");
                }
                if (course.Users == null)
                {
                    course.Users = new List<User>();
                }
                course.Users.Add(teacher);
            }

            await _uow.Course.CreateAsync(course);

            await _uow.CompleteAsync();

            return _mapper.Map<CourseDto>(course);
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
