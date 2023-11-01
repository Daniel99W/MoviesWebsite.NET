namespace MoviesAPI.Dtos.Movies
{
    public class GetMovieFeedDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PosterImageUrl { get; set; }
        public string PosterImageUrlGif { get; set; }
        public DateTime AddedDate { get; set; }
        public int Views { get; set; }
    }
}
