using AutoMapper;
using MoviesAPI.Core.Entities;
using MoviesAPI.Dtos.Categories;

namespace MoviesAPI.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<GetCategoryDto, Category>().ReverseMap();
        }
    }
}
