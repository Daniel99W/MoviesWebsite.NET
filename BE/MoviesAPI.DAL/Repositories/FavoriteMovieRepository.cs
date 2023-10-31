using Microsoft.EntityFrameworkCore;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;
using MoviesAPI.Dtos;

namespace MoviesAPI.DAL.Repositories
{
    public class FavoriteMovieRepository : Repository<FavoriteMovie>, IRepositoryFavoriteMovie
    {
        public FavoriteMovieRepository(MoviesDbContext moviesDbContext)
            : base(moviesDbContext)
        {

        }

        public async Task<FavoriteMovie?> GetFavoriteMovieByUserAndMovieId(Guid MovieId, Guid UserId)
        {
            return await this.moviesDbContext
                .FavoriteMovies
                .Where(fm => fm.UserId == UserId && fm.MovieId == MovieId)
                .SingleOrDefaultAsync();
        }

        public async Task<Pagination<Movie>> GetFavoriteMoviesByUserId(int Page, int ItemsPerPage, Guid UserId)
        {
            return await moviesDbContext
                .Movies
                .Include(m => m.Users)
                .Where(m => m.Users.Where(u => u.UserId == UserId).Any())
                .Paginate(Page, ItemsPerPage);
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
