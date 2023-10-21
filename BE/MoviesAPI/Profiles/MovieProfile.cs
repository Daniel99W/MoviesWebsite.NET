using AutoMapper;
using MoviesAPI.Core.Entities;
using MoviesAPI.Dtos.Movies;

namespace MoviesAPI.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, GetMovieDto>().ReverseMap();
        }
    }
}
