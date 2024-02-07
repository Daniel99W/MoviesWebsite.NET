

namespace MoviesAPI.Core.Entities
{
    public class Movie : Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? PosterImageUrl { get; set; }
        public string? PosterImageUrlGif { get; set; }
        public int Views { get; set; }
        public string VidGuardId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime AddedDate { get; set; }
        public List<MovieCategory> MovieCategories { get; set; }
        public List<Comment> Comments { get; set; }
        public List<FavoriteMovie> Users { get; set; }
        public List<VotedMovie> VotedMovies { get; set; }
        public List<MovieTag> MovieTags { get; set; }

        public Movie()
        {

        }

        public Movie(string title,
            string description,
            DateTime AddedDate,
            string vidguardId,
            string? posterImageUrl,
            string? posterImageUrlGif,
            Guid userId
            )
        {
            this.Title = title;
            this.Description = description;
            this.AddedDate = AddedDate;
            this.VidGuardId = vidguardId;
            this.PosterImageUrl = posterImageUrl;
            this.PosterImageUrlGif = posterImageUrlGif;
            this.Views = 0;
            this.UserId = userId;
        }

        public static Movie CreateMovie(
            string title,
            string description,
            DateTime AddedDate,
            string vidguardId,
            string? posterImageUrl,
            string? posterImageUrlGif,
            Guid userId
            )
        {
            return new Movie(title, description, AddedDate, vidguardId, posterImageUrl, posterImageUrlGif, userId);
        }

        public Movie UpdateViewsCounter()
        {
            this.Views += 1;
            return this;
        }
    }
}
