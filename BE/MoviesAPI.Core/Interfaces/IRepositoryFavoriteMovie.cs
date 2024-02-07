using MoviesAPI.Core.Entities;
using MoviesAPI.Dtos;

namespace MoviesAPI.Core.Interfaces
{
    public interface IRepositoryFavoriteMovie : IRepository<FavoriteMovie>
    {
        public Task<FavoriteMovie?> GetFavoriteMovieByUserAndMovieId(Guid MovieId, Guid UserId);
        public Task<int> GetMovieNumberOfAddedToFavorite(Guid MovieId);
        public Task<Pagination<Movie>> GetFavoriteMoviesByUserId(int Page, int ItemsPerPage, Guid UserId);

        public Task DeleteFavoriteMovieByUserIdAndMovieId(Guid MovieId, Guid UserId);
    }
}
