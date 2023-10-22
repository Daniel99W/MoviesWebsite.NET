using MediatR;
using MoviesAPI.Application.Commands.VotedMovies;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.CommandsHandler.VotedMovies
{
    public class CreateDownVotedMovieCommandHandler : IRequestHandler<CreateDownVotedMovieCommand, Unit>
    {
        private IRepositoryMovie repositoryMovie;
        private IRepositoryVotedMovies repositoryVotedMovies;
        private IRepositoryUser repositoryUser;
        public CreateDownVotedMovieCommandHandler(IRepositoryVotedMovies repositoryVotedMovies,
            IRepositoryMovie repositoryMovie,
            IRepositoryUser repositoryUser
            )
        {
            this.repositoryVotedMovies = repositoryVotedMovies;
            this.repositoryMovie = repositoryMovie;
            this.repositoryUser = repositoryUser;
        }
        public async Task<Unit> Handle(CreateDownVotedMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await this.repositoryMovie.Read(request.MovieId);
            var user = await this.repositoryUser.Read(request.UserId);
            if (movie == null)
            {
                throw new HttpRequestException("Movie does not exist");
            }
            if (user == null)
            {
                throw new HttpRequestException("User does not exist");
            }
            var movieIsUpvoted =
                await this.repositoryVotedMovies.CheckIfMovieIsVotedByUser(request.UserId, request.MovieId);
            var movieIsDownvoted =
                await this.repositoryVotedMovies.CheckIfMovieIsDownVoteByUser(request.UserId, request.MovieId);
            if(movieIsDownvoted != null)
            {
                this.repositoryVotedMovies.Delete(movieIsDownvoted);
                await this.repositoryVotedMovies.SaveChangesAsync();
            }
            if(movieIsUpvoted != null)
            {
                movieIsUpvoted.Upvote = false;
                movieIsUpvoted.Downvote = true;
                this.repositoryVotedMovies.Update(movieIsUpvoted);
                await this.repositoryVotedMovies.SaveChangesAsync();
                return Unit.Value;
            }
            var votedMovie = VotedMovie.CreateVotedMovie(request.UserId, request.MovieId, true, false);
            this.repositoryVotedMovies.Create(votedMovie);
            await this.repositoryVotedMovies.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
