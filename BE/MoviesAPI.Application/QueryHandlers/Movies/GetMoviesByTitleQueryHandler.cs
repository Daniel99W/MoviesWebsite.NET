using AutoMapper;
using MediatR;
using MoviesAPI.Application.Queries.Movies;
using MoviesAPI.Application.Responses;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;
using MoviesAPI.Dtos;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.QueryHandlers.Movies
{
    public class GetMoviesByTitleQueryHandler : IRequestHandler<GetMoviesByTitleQuery, Pagination<GetMovieWithVotesCounted>>
    {
        private IRepositoryMovie repositoryMovie;
        private IRepositoryVotedMovies repositoryVotedMovies;

        public GetMoviesByTitleQueryHandler(IRepositoryMovie repositoryMovie,
            IRepositoryVotedMovies movieVotedMovies
            )
        {
            this.repositoryMovie = repositoryMovie;
            this.repositoryVotedMovies = movieVotedMovies;
        }

        public async Task<Pagination<GetMovieWithVotesCounted>> Handle(GetMoviesByTitleQuery request, CancellationToken cancellationToken)
        {
            Pagination<Movie> moviesPaginated = 
                await repositoryMovie.GetMoviesByTitle(request.ItemsPerPage,request.Page,request.Title);

            Pagination<GetMovieWithVotesCounted> movieWithVotesPaginated = 
                new Pagination<GetMovieWithVotesCounted>();
            movieWithVotesPaginated.TotalPages = moviesPaginated.TotalPages;
            movieWithVotesPaginated.Page = moviesPaginated.Page;
            movieWithVotesPaginated.Results = new List<GetMovieWithVotesCounted>();
            foreach(var movie in  moviesPaginated.Results)
            {
                movieWithVotesPaginated.Results.Add(new GetMovieWithVotesCounted()
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Description = movie.Description,
                    AddedDate = movie.AddedDate,
                    VidGuardId = movie.VidGuardId,
                    Views = movie.Views,
                    PosterImageUrl = movie.PosterImageUrl,
                    Upvotes = await this.repositoryVotedMovies.CountVotesByMovieId(movie.Id),
                    Downvotes = await this.repositoryVotedMovies.CountDownvotesByMovieId(movie.Id)
                });
            }
            return movieWithVotesPaginated;
        }
    }
}
