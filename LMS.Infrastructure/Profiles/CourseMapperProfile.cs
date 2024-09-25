using AutoMapper;
using LMS.Infrastructure.Dtos;
using LMS.Models.Entities;

using System.Linq.Expressions;

namespace LMS.Infrastructure.Profiles
{
    public class CourseMapperProfile : Profile
    {
        public CourseMapperProfile()
        {
            CreateMap<Course, CourseDetailedDto>()
                .ForMember(
                    dest => dest.Teacher,
                    opt => opt.MapFrom(
                        src => src.Users!
                            .Where(u => !u.IsStudent).FirstOrDefault()))
                .ForMember(
                    dest => dest.Students,
                    opt => opt.MapFrom(
                        src => src.Users!
                            .Where(u => u.IsStudent).ToList()));

            CreateMap<Course, CourseDto>()
                .ForMember(
                    dest => dest.Teacher,
                    opt => opt.MapFrom(
                        src => src.Users!
                            .Where(u => !u.IsStudent).FirstOrDefault()));

            CreateMap<User, UserDto>();

            CreateMap<Module, ModuleDto>()
                .ForMember(
                    dest => dest.State,
                    opt => opt.MapFrom(src => GetCurrentState(src)));

            CreateMap<Activity, ActivityDto>();

            CreateMap<ActivityType, ActivityTypeDto>();
        }

        private static string GetCurrentState(Module module)
        {
            var currentDate = DateTime.UtcNow;

            if (currentDate < module.StartDate)
            {
                return "not-started";
            }
            else if (currentDate > module.EndDate)
            {
                return "completed";
            }
            else
            {
                return "in-progress";
            }
        }
    }
}
