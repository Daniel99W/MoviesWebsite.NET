using MediatR;
using MoviesAPI.Application.Queries.Movies;
using MoviesAPI.Application.Responses;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.QueryHandlers.Movies
{
    public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieById, GetMovieWithVotesCounted>
    {
        private IRepositoryMovie repositoryMovie;
        private IRepositoryVotedMovies repositoryVotedMovies;
        public GetMovieByIdQueryHandler(IRepositoryMovie repositoryMovie,
            IRepositoryVotedMovies repositoryVotedMovies
            
            )
        {
            this.repositoryMovie = repositoryMovie;
            this.repositoryVotedMovies = repositoryVotedMovies;
        } 
        public async Task<GetMovieWithVotesCounted> Handle(GetMovieById request, CancellationToken cancellationToken)
        {
            var movie = await this.repositoryMovie.Read(request.Id);
            if(movie == null)
            {
                throw new HttpRequestException("Movie does not exist");
            }
            var getMovieWithVotesCounted = new GetMovieWithVotesCounted()
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Views = movie.Views,
                VidGuardId = movie.VidGuardId,
                AddedDate = movie.AddedDate,
                Downvotes = await this.repositoryVotedMovies.CountDownvotesByMovieId(movie.Id),
                Upvotes = await this.repositoryVotedMovies.CountVotesByMovieId(movie.Id),
                PosterImageUrl = movie.PosterImageUrl
            };
            return getMovieWithVotesCounted;
        }
    }
}
