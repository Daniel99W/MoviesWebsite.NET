using MoviesAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
