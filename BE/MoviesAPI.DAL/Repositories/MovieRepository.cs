using Microsoft.EntityFrameworkCore;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;
using MoviesAPI.Dtos;


namespace MoviesAPI.DAL.Repositories
{
    public class MovieRepository : Repository<Movie>, IRepositoryMovie
    {
        public MovieRepository(MoviesDbContext moviesDbContext)
            : base(moviesDbContext)
        {

        }

        public async Task<Movie?> GetMovieByVidGuardId(string Id)
        {
            return await moviesDbContext
                .Movies
                .Where(m => m.VidGuardId == Id)
                .SingleOrDefaultAsync();
        }

        public Task<Pagination<Movie>> GetMovies(int ItemsPerPage, 
            int Page,
            string? Title,
            List<Guid>? categoriesIds, 
            DateTime? BeginAddedDate, 
            DateTime? EndAddedDate)
        {
            IQueryable<Movie> movies = this.moviesDbContext.Movies;
            movies =
                movies.Include(m => m.VotedMovies);

            if(Title != null && Title != "")
            {
                movies = movies.Where(m => m.Title.Contains(Title));
            }

            if(categoriesIds != null && categoriesIds.Count > 0)
            {
                movies =
                    movies
                    .Include(m => m.MovieCategories)
                    .Where(m => m.MovieCategories.Where(c => categoriesIds.Contains(c.CategoryId)).Any());
            }

            if(BeginAddedDate != null && EndAddedDate != null)
            {
                movies =
                    movies.Where(m => m.AddedDate >= BeginAddedDate && m.AddedDate <= EndAddedDate);
            }
            else if(BeginAddedDate != null)
            {
                movies =
                    movies.Where(m => m.AddedDate >= BeginAddedDate);
            }
            else if(EndAddedDate  != null)
            {
                movies =
                    movies.Where(m => m.AddedDate <= EndAddedDate);
            }

            return movies.Paginate<Movie>(Page, ItemsPerPage);
        }

        public async Task<Pagination<Movie>> GetMoviesByTitle(int ItemsPerPage, int Page, string? Title)
        {
            IQueryable<Movie> movies = moviesDbContext.Movies;
            if(Title != null)
            {
                movies = movies.Where(m => m.Title.Contains(Title));
            }
            return await movies.Paginate<Movie>(Page, ItemsPerPage);
        }
    }
}
