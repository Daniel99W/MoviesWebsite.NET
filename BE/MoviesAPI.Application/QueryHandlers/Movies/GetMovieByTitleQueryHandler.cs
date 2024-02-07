using MediatR;
using MoviesAPI.Application.Queries.Movies;
using MoviesAPI.Application.Responses;
using MoviesAPI.Core.Interfaces;


namespace MoviesAPI.Application.QueryHandlers.Movies
{
    public class GetMovieByTitleQueryHandler : IRequestHandler<GetMovieByTitle, GetMovieWithVotesCounted>
    {
        private IRepositoryMovie repositoryMovie;
        private IRepositoryVotedMovies repositoryVotedMovies;
        private IRepositoryFavoriteMovie repositoryFavoriteMovie;
        private IRepositoryMovieCategory repositoryMovieCategory;
        private IRepositoryTag repositoryTag;
        public GetMovieByTitleQueryHandler(IRepositoryMovie repositoryMovie,
            IRepositoryVotedMovies repositoryVotedMovies,
            IRepositoryFavoriteMovie repositoryFavoriteMovie,
            IRepositoryMovieCategory repositoryMovieCategory,
            IRepositoryTag repositoryTag
            )
        {
            this.repositoryMovie = repositoryMovie;
            this.repositoryVotedMovies = repositoryVotedMovies;
            this.repositoryFavoriteMovie = repositoryFavoriteMovie;
            this.repositoryMovieCategory = repositoryMovieCategory;
            this.repositoryTag = repositoryTag;
        } 
        public async Task<GetMovieWithVotesCounted> Handle(GetMovieByTitle request, CancellationToken cancellationToken)
        {
            var movie = await this.repositoryMovie.GetMovieByTitle(request.Title);
            if(movie == null)
            {
                throw new HttpRequestException("Movie does not exist");
            }
            var getMovieWithVotesCounted = new GetMovieWithVotesCounted()
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Views = movie.Views,
                VidGuardId = movie.VidGuardId,
                AddedDate = movie.AddedDate,
                Downvotes = await this.repositoryVotedMovies.CountDownvotesByMovieId(movie.Id),
                Upvotes = await this.repositoryVotedMovies.CountVotesByMovieId(movie.Id),
                Likes = await this.repositoryFavoriteMovie.GetMovieNumberOfAddedToFavorite(movie.Id),
                PosterImageUrl = movie.PosterImageUrl,
                Categories = await this.repositoryMovieCategory.GetMovieCategoriesByMovieId(movie.Id),
                Tags = await this.repositoryTag.GetMovieTagByMovieId(movie.Id),
                PosterImageUrlGif = movie.PosterImageUrlGif
            };
            return getMovieWithVotesCounted;
        }
    }
}
