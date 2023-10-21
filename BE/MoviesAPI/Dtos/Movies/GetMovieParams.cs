namespace MoviesAPI.Dtos.Movies
{
    public class GetMovieParams
    {
        public int ItemsPerPage { get; set; }
        public int Page { get; set; }
        public string Title { get; set; }
        public List<Guid> CategoriesIds { get; set; }
        public DateTime BeginAddedDate { get; set; }
        public DateTime EndAddedDate { get; set;}
    }
}
