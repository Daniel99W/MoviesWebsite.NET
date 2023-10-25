using MoviesAPI.Dtos.MovieTags;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Dtos.Movies
{
    public class CreateMovieDto
    {
        [Required]
        [MaxLength(Constants.Constants.MaxTitleSize)]
        public string Title { get; set; }
        [Required]
        [MaxLength(Constants.Constants.MaxDescriptionSize)]
        public string Description { get; set; }
        [Required]
        public string PosterImageUrl { get; set; }
        [Required]
        public DateTime AddedDate { get; set; }
        [Required]
        public string VidGuardId { get; set; }
        [Required]
        public List<Guid> CategoriesIds { get; set; }
        public List<MovieTagDto>? Tags { get; set; }
    }

}
