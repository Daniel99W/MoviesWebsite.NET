using MoviesAPI.Core.Entities;


namespace MoviesAPI.Core.Interfaces
{
    public interface IRepositoryVotedMovies : IRepository<VotedMovie>
    {
        public Task<VotedMovie?> CheckIfMovieIsVotedByUser(Guid UserId, Guid MovieId);
        public Task<VotedMovie?> CheckIfMovieIsDownVoteByUser(Guid UserId, Guid MovieId);
        public Task<int> CountVotesByMovieId(Guid MovieId);
        public Task<int> CountDownvotesByMovieId(Guid MovieId);
    }
}
