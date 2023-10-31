namespace MoviesAPI.Dtos.FavoriteMovies
{
    public class GetFavoriteMoviesParams
    {
        public int Page { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 5;
    }
}
