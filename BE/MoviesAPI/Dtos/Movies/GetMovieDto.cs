namespace MoviesAPI.Dtos.Movies
{
    public class GetMovieDto
    {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public int Upvote { get; set; }
        public int Downvote { get; set; }
        public DateTime AddedDate { get; set; }
        public string VidGuardId { get; set; }
    }
}
