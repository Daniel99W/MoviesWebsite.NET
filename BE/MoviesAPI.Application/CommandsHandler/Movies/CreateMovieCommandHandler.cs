using MediatR;
using MoviesAPI.Application.Commands.Movies;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.CommandsHandler.Movies
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Movie>
    {
        private IRepositoryMovie repositoryMovie;
        private IRepositoryCategory repositoryCategory;
        private IRepositoryMovieCategory repositoryMovieCategory;
        public CreateMovieCommandHandler(
            IRepositoryMovie repositoryMovie,
            IRepositoryCategory repositoryCategory,
            IRepositoryMovieCategory repositoryMovieCategory
            )
        {
            this.repositoryMovie = repositoryMovie;
            this.repositoryCategory = repositoryCategory;
            this.repositoryMovieCategory = repositoryMovieCategory;
        }
        public async Task<Movie> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var movieExist = await this.repositoryMovie.GetMovieByVidGuardId(request.VidGuardId);
            if (movieExist != null)
            {
                throw new HttpRequestException("This movie already exist in database");
            } 
            var movie =
                Movie.CreateMovie(request.Title,
                request.Description,
                request.AddedDate,
                request.VidGuardId,
                request.PosterImageUrl
                );
            List<MovieCategory> moviesCategories = new List<MovieCategory>();
            foreach (var categoryId in request.CategoriesIds)
            {
                moviesCategories.Add(new MovieCategory()
                {
                    MovieId = movie.Id,
                    CategoryId = categoryId
                });
            }
            movie.MovieCategories = moviesCategories;
            this.repositoryMovie.Create(movie);
            await this.repositoryMovie.SaveChangesAsync();
            return movie;
        }


    }
}
