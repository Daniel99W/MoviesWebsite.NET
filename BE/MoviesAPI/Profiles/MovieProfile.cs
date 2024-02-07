using AutoMapper;
using MoviesAPI.Core.Entities;
using MoviesAPI.Dtos;
using MoviesAPI.Dtos.Movies;

namespace MoviesAPI.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, GetMovieDto>().ReverseMap();
            CreateMap<Movie, GetMovieFeedDto>().ReverseMap();
            CreateMap<FavoriteMovie,GetMovieDto>().ReverseMap();
            CreateMap<Pagination<Movie>, Pagination<GetMovieDto>>();
            CreateMap<Pagination<Movie>, Pagination<GetMovieFeedDto>>();
        }
    }
}
