using AutoMapper;
using LMS.Infrastructure.Dtos;
using LMS.Models.Entities;

namespace LMS.Infrastructure.Profiles
{
    public class CourseMapperProfile : Profile
    {
        public CourseMapperProfile()
        {
            CreateMap<Course, CourseDto>();
            CreateMap<User, UserDto>();
            CreateMap<Module, ModuleDto>();
            CreateMap<Activity, ActivityDto>();
            CreateMap<ActivityType, ActivityTypeDto>();
            
        }
    }
}
