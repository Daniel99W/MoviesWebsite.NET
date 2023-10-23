using MediatR;
using MoviesAPI.Application.Commands.VotedMovies;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;


namespace MoviesAPI.Application.CommandsHandler.VotedMovies
{
    public class CreateVotedMovieCommandHandler : IRequestHandler<CreateVotedMovieCommand, Unit>
    {
        private IRepositoryVotedMovies repositoryVotedMovies;
        private IRepositoryUser repositoryUser;
        private IRepositoryMovie repositoryMovie;
        public CreateVotedMovieCommandHandler(
            IRepositoryUser repositoryUser,
            IRepositoryMovie repositoryMovie,
            IRepositoryVotedMovies repositoryVotedMovies)
        {
            this.repositoryVotedMovies = repositoryVotedMovies;
            this.repositoryMovie = repositoryMovie;
            this.repositoryUser = repositoryUser;
        }
        public async Task<Unit> Handle(CreateVotedMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await this.repositoryMovie.Read(request.MovieId);
            var user = await this.repositoryUser.Read(request.UserId);
            if(movie == null)
            {
                throw new HttpRequestException("Movie does not exist");
            }
            if(user == null)
            {
                throw new HttpRequestException("User does not exist");
            }
            var movieIsUpvoted = 
                await this.repositoryVotedMovies.CheckIfMovieIsVotedByUser(request.UserId, request.MovieId);
            var movieIsDownvoted = 
                await this.repositoryVotedMovies.CheckIfMovieIsDownVoteByUser(request.UserId, request.MovieId);
            if(movieIsUpvoted != null)
            {
                this.repositoryVotedMovies.Delete(movieIsUpvoted);
                await this.repositoryVotedMovies.SaveChangesAsync();
                return Unit.Value;
            }
            if(movieIsDownvoted != null)
            {
                movieIsDownvoted.Upvote = true;
                movieIsDownvoted.Downvote = false;
                this.repositoryVotedMovies.Update(movieIsDownvoted);
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
