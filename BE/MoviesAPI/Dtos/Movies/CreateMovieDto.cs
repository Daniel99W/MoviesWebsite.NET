namespace MoviesAPI.Dtos.Movies
{
    public class CreateMovieDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AddedDate { get; set; }
        public string VidGuardId { get; set; }
        public List<Guid> CategoriesIds { get; set; }
    }

}
