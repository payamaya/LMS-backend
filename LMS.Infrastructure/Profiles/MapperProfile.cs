using AutoMapper;
using LMS.Infrastructure.Dtos;
using LMS.Models.Entities;

namespace LMS.Infrastructure.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
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
                            .Where(u => !u.IsStudent).FirstOrDefault())).ReverseMap();

            CreateMap<User, UserDto>();

            CreateMap<Module, ModuleDto>()
                .ForMember(
                    dest => dest.State,
                    opt => opt.MapFrom(src => GetCurrentState(src)));

            CreateMap<Activity, ActivityDto>();
            CreateMap<ActivityPostDto, Activity>();

            CreateMap<ActivityType, ActivityTypeDto>();

            CreateMap<ModulePostDto, Module>();
            CreateMap<CoursePostDto, Course>()
                .ForMember(
                    dest => dest.Users,
      opt => opt.Ignore());
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
