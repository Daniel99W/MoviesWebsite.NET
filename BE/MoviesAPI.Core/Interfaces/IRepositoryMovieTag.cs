using MoviesAPI.Core.Entities;

namespace MoviesAPI.Core.Interfaces
{
    public interface IRepositoryMovieTag : IRepository<MovieTag>
    {
        public Task DeleteMovieTagsByMovie(Guid movieId);
    }
}
