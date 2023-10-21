using MediatR;
using MoviesAPI.Application.Queries.Movies;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.QueryHandlers.Movies
{
    public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieById, Movie>
    {
        private IRepositoryMovie repositoryMovie;
        public GetMovieByIdQueryHandler(IRepositoryMovie repositoryMovie)
        {
            this.repositoryMovie = repositoryMovie;
        } 
        public async Task<Movie> Handle(GetMovieById request, CancellationToken cancellationToken)
        {
            var movie = await this.repositoryMovie.Read(request.Id);
            return movie;
        }
    }
}
