using AutoMapper;
using LMS.Infrastructure.Dtos;
using LMS.Models.Entities;

namespace LMS.Infrastructure.Profiles
{
    public class CourseMapperProfile : Profile
    {
        public CourseMapperProfile()
        {
            CreateMap<Course, CourseDTO>();
        }
    }
}
