using AutoMapper;
using FreeCourse.Service.Catalog.Dtos;
using FreeCourse.Service.Catalog.Models;

namespace FreeCourse.Service.Catalog.Mapping
{
    internal class GeneralMapping:Profile
    {
        internal GeneralMapping()
        {
            CreateMap<Category,CategoryDto>().ReverseMap();

            CreateMap<Feature,FeatureDto>().ReverseMap();

            CreateMap<Course,CourseDto>().ReverseMap();
            CreateMap<Course,CourseCreateDto>().ReverseMap();
            CreateMap<Course,CourseUpdateDto>().ReverseMap();
        }
    }
}
