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
        private IRepositoryFavoriteMovie repositoryFavoriteMovie;
        private IRepositoryUser repositoryUser;

        public GetMoviesByTitleQueryHandler(IRepositoryMovie repositoryMovie,
            IRepositoryVotedMovies movieVotedMovies,
            IRepositoryFavoriteMovie repositoryFavoriteMovie,
            IRepositoryUser repositoryUser
            )
        {
            this.repositoryMovie = repositoryMovie;
            this.repositoryVotedMovies = movieVotedMovies;
            this.repositoryFavoriteMovie = repositoryFavoriteMovie;
            this.repositoryUser = repositoryUser;
        }

        public async Task<Pagination<GetMovieWithVotesCounted>> Handle(GetMoviesByTitleQuery request, CancellationToken cancellationToken)
        {
            var userId = await repositoryUser.GetUserByFirebaseId(request.FirebaseId);
            if(userId == null)
            {
                throw new Exception("User does not exist");
            }
            Pagination<Movie> moviesPaginated = 
                await repositoryMovie.GetMoviesByTitle(
                request.ItemsPerPage,request.Page,
                request.Title,
                (Guid)userId);

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
                    Downvotes = await this.repositoryVotedMovies.CountDownvotesByMovieId(movie.Id),
                    Likes = await this.repositoryFavoriteMovie.GetMovieNumberOfAddedToFavorite(movie.Id)
                });
            }
            return movieWithVotesPaginated;
        }
    }
}
