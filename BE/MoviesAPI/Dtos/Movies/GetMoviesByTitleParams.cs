namespace MoviesAPI.Dtos.Movies
{
    public class GetMoviesByTitleParams
    {
        public string? Title { get; set; }
        public int Page { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 5;
    }
}
