using AutoMapper;
using LMS.Infrastructure.Dtos;
using LMS.Models.Entities;

namespace LMS.Infrastructure.Profiles
{
    public class CourseMapperProfile : Profile
    {
        public CourseMapperProfile()
        {
            CreateMap<Course, CourseDto>()
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
            //.ConstructUsing(src => new CourseDto
            //{
            //    Id = src.Id,
            //    CourseName = src.CourseName,
            //    Description = src.Description,
            //    StartDate = src.StartDate,
            //    Modules = src.Modules.Select,
            //    Students = src.Students
            //}); ;
            CreateMap<User, UserDto>();
            CreateMap<Module, ModuleDto>();
            CreateMap<Activity, ActivityDto>();
            CreateMap<ActivityType, ActivityTypeDto>();
            
        }
    }
}
