using MediatR;
using MoviesAPI.Application.Commands.Movies;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;

namespace MoviesAPI.Application.CommandsHandler.Movies
{
    public class UpdateMovieIdCommandHandler : IRequestHandler<UpdateMovieByIdCommand, Movie>
    {
        private IRepositoryMovie repositoryMovie;
        private IRepositoryCategory repositoryCategory;
        private IRepositoryMovieCategory repositoryMovieCategory;
        public UpdateMovieIdCommandHandler(
            IRepositoryMovie repositoryMovie,
            IRepositoryCategory repositoryCategory,
            IRepositoryMovieCategory repositoryMovieCategory
            )
        {
            this.repositoryMovie = repositoryMovie;
            this.repositoryCategory = repositoryCategory;
            this.repositoryMovieCategory = repositoryMovieCategory;
        }
        public async Task<Movie> Handle(UpdateMovieByIdCommand request, CancellationToken cancellationToken)
        {
            var movie = await this.repositoryMovie.Read(request.Id);
            if(movie == null)
            {
                throw new HttpRequestException("Movie does not exist");
            }
            List<MovieCategory> movieCategories = new List<MovieCategory>();
            await this.repositoryMovieCategory.DeleteMovieCategoriesByMovieId(movie.Id);
            foreach(Guid categoryId in request.CategoriesIds)
            {
                movieCategories.Add(new MovieCategory()
                {
                    MovieId = movie.Id,
                    CategoryId = categoryId
                });
            }
            movie.MovieCategories = movieCategories;
            movie.Title = request.Title;
            movie.Description = request.Description;
            movie = this.repositoryMovie.Update(movie);
            await this.repositoryMovie.SaveChangesAsync();
            return movie;
        }
    }
}
