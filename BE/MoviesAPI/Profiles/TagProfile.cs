using AutoMapper;
using MoviesAPI.Core.Entities;
using MoviesAPI.Dtos.MovieTags;

namespace MoviesAPI.Profiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, MovieTagDto>().ReverseMap();
        }
    }
}
