namespace MoviesAPI.Dtos.Movies
{
    public class UpdateMovieById
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Guid> CategoriesIds { get; set; }
        //to be implemented later
        //public string VidGuardId { get; set; }
    }
}
