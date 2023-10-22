using AutoMapper;
using MoviesAPI.Application.Responses;
using MoviesAPI.Core.Entities;
using MoviesAPI.Dtos;
using MoviesAPI.Dtos.Movies;
using MoviesAPI.Dtos.VotedMovies;

namespace MoviesAPI.Profiles
{
    public class VotedMoviesProfile : Profile
    {
        public VotedMoviesProfile()
        {
            CreateMap<VotedMovie, GetVotedMovieDto>().ReverseMap();
            CreateMap<GetMovieWithVotesCounted, GetMovieDto>().ReverseMap();
            CreateMap<Pagination<GetMovieWithVotesCounted>, Pagination<GetMovieDto>>().ReverseMap();
        }
    }
}
