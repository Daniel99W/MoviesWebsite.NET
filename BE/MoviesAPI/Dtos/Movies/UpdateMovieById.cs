using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Dtos.Movies
{
    public class UpdateMovieById
    {
        [Required]
        [MaxLength(Constants.Constants.MaxTitleSize)]
        public string Title { get; set; }
       
        [Required]
        [MaxLength(Constants.Constants.MaxDescriptionSize)]
        public string Description { get; set; }
        public List<Guid> CategoriesIds { get; set; }
        //to be implemented later
        //public string VidGuardId { get; set; }
    }
}
