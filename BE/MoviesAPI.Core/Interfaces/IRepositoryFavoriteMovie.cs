using MoviesAPI.Core.Entities;


namespace MoviesAPI.Core.Interfaces
{
    public interface IRepositoryFavoriteMovie : IRepository<FavoriteMovie>
    {
        public Task<FavoriteMovie?> GetFavoriteMovieByUserAndMovieId(Guid MovieId, Guid UserId);
        public Task<int> GetMovieNumberOfAddedToFavorite(Guid MovieId);
    }
}
