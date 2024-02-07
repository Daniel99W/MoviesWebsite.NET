

using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Dtos.FavoriteMovies
{
    public class CreateFavoriteMovie
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string MovieId { get; set; }
    }
}
