using Microsoft.EntityFrameworkCore;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;

namespace MoviesAPI.DAL.Repositories
{
    public class FavoriteMovieRepository : Repository<FavoriteMovie>,IRepositoryFavoriteMovie
    {
        public FavoriteMovieRepository(MoviesDbContext moviesDbContext)
            :base(moviesDbContext)
        {

        }

        public async Task<FavoriteMovie?> GetFavoriteMovieByUserAndMovieId(Guid MovieId, Guid UserId)
        {
            return await this.moviesDbContext
                .FavoriteMovies
                .Where(fm => fm.UserId == UserId && fm.MovieId == MovieId)
                .SingleOrDefaultAsync();
        }

        public async Task<int> GetMovieNumberOfAddedToFavorite(Guid MovieId)
        {
            return await this.moviesDbContext
                .FavoriteMovies
                .Where(fm => fm.MovieId == MovieId)
                .CountAsync();
        }
    }
}
