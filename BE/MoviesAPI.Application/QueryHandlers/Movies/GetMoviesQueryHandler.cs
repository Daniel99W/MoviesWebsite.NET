using MediatR;
using MoviesAPI.Application.Queries.Movies;
using MoviesAPI.Application.Responses;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;
using MoviesAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.QueryHandlers.Movies
{
    public class GetMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, Pagination<GetMovieWithVotesCounted>>
    {
        private IRepositoryMovie repositoryMovie;
        private IRepositoryVotedMovies repositoryVotedMovies;
        public GetMoviesQueryHandler(IRepositoryMovie repositoryMovie,
            IRepositoryVotedMovies repositoryVotedMovies
            ) 
        {
            this.repositoryMovie = repositoryMovie;
            this.repositoryVotedMovies = repositoryVotedMovies;
        }
        public async Task<Pagination<GetMovieWithVotesCounted>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            Pagination<Movie> moviesPaginated =
                await repositoryMovie.GetMovies(request.ItemsPerPage,
                request.Page,request.Title,
                request.CategoriesIds,
                request.BeginAddedDate,
                request.EndAddedDate);

            Pagination<GetMovieWithVotesCounted> movieWithVotesPaginated =
                new Pagination<GetMovieWithVotesCounted>();
            movieWithVotesPaginated.TotalPages = moviesPaginated.TotalPages;
            movieWithVotesPaginated.Page = moviesPaginated.Page;
            movieWithVotesPaginated.Results = new List<GetMovieWithVotesCounted>();
            foreach (var movie in moviesPaginated.Results)
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
