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
        public CreateMovieCommandHandler(IRepositoryMovie repositoryMovie,IRepositoryCategory repositoryCategory)
        {
            this.repositoryMovie = repositoryMovie;
            this.repositoryCategory = repositoryCategory;
        }
        public async Task<Movie> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            List<Category> categories =
                (await repositoryCategory.GetCategoriesByGuidList(request.CategoriesIds)).ToList();
            var movie =
                Movie.CreateMovie(request.Title,
                request.Description,
                request.AddedDate,
                request.VidGuardId,
                categories
                );
            
            this.repositoryMovie.Create(movie);
            await this.repositoryMovie.SaveChangesAsync();
            return movie;
        }
    }
}
