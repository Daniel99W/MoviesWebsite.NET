using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MoviesAPI.Dtos.Categories;
using MoviesAPI.Dtos.MovieTags;
using MoviesAPI.Dtos.VotedMovies;

namespace MoviesAPI.Dtos.Movies
{
    public class GetMovieDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PosterImageUrl { get; set; }
        public int Views { get; set; }
        public int Upvotes { get; set; }
        public int Likes { get; set; }
        public int Downvotes { get; set; }
        public DateTime AddedDate { get; set; }
        public string VidGuardId { get; set; }
        public List<GetCategoryDto> Categories { get; set; }
        public List<MovieTagDto> Tags { get; set; }
    }
}
