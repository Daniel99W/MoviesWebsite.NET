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
    public class DeleteMovieByIdCommandHandler : IRequestHandler<DeleteMovieByIdCommand, Unit>
    {
        private IRepositoryMovie repositoryMovie;
        public DeleteMovieByIdCommandHandler(IRepositoryMovie repositoryMovie) 
        { 
            this.repositoryMovie = repositoryMovie;
        }
        public async Task<Unit> Handle(DeleteMovieByIdCommand request, CancellationToken cancellationToken)
        {
            var movie = await this.repositoryMovie.Read(request.Id);
            if(movie == null)
            {
                throw new HttpRequestException("Movies does not exist");
            }
            this.repositoryMovie.Delete(movie);
            await this.repositoryMovie.SaveChangesAsync();
            return await Task.FromResult(Unit.Value);
        }
    }
}
