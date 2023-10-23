using Microsoft.EntityFrameworkCore;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;


namespace MoviesAPI.DAL.Repositories
{
    public class VotedMoviesRepository : Repository<VotedMovie>,IRepositoryVotedMovies
    {
        public VotedMoviesRepository(MoviesDbContext moviesDbContext)
            :base(moviesDbContext)
        {

        }

        public async Task<VotedMovie?> CheckIfMovieIsDownVoteByUser(Guid UserId, Guid MovieId)
        {
            return await this.moviesDbContext
                .VotedMovies
                .Where(vm => vm.MovieId == MovieId && vm.UserId == UserId && vm.Downvote == true)
                .SingleOrDefaultAsync();
        }

        public async Task<VotedMovie?> CheckIfMovieIsVotedByUser(Guid UserId, Guid MovieId)
        {
            return await this.moviesDbContext
                .VotedMovies
                .Where(vm => vm.MovieId == MovieId && vm.UserId == UserId && vm.Upvote == true)
                .SingleOrDefaultAsync();
                
        }

        public async Task<int> CountDownvotesByMovieId(Guid MovieId)
        {
            return await moviesDbContext
                .VotedMovies
                .Where(m => m.MovieId == MovieId && m.Downvote == true)
                .CountAsync();
        }

        public async Task<int> CountVotesByMovieId(Guid MovieId)
        {
            return await moviesDbContext
                .VotedMovies
                .Where(m => m.MovieId == MovieId && m.Upvote == true)
                .CountAsync();
        }
    }
}
