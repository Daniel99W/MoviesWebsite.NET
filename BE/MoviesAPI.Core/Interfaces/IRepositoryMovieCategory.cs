using MoviesAPI.Core.Entities;


namespace MoviesAPI.Core.Interfaces
{
    public interface IRepositoryMovieCategory : IRepository<MovieCategory>
    {
        public Task DeleteMovieCategoriesByMovieId(Guid movieId);

        public Task<List<Category>> GetMovieCategoriesByMovieId(Guid movieId);
    }
}
