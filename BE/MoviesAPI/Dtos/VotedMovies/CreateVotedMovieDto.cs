namespace MoviesAPI.Dtos.VotedMovies
{
    public class CreateVotedMovieDto
    {
        public Guid UserId { get; set; }
        public Guid MovieId { get; set; }
    }
}
