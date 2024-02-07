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
    public class UpdateViewsCounterCommandHandler : IRequestHandler<UpdateMovieViewsCounterCommand, Movie>
    {
        private IRepositoryMovie repositoryMovie;
        public UpdateViewsCounterCommandHandler(IRepositoryMovie repositoryMovie)
        {
            this.repositoryMovie = repositoryMovie;
        }
        public async Task<Movie> Handle(UpdateMovieViewsCounterCommand request, CancellationToken cancellationToken)
        {
            var movie = await this.repositoryMovie.Read(request.Id);
            if (movie == null)
            {
                throw new HttpRequestException("Movie does not exist");
            }
            movie = movie.UpdateViewsCounter();
            this.repositoryMovie.Update(movie);
            await this.repositoryMovie.SaveChangesAsync();
            return movie;
        }
    }
}
