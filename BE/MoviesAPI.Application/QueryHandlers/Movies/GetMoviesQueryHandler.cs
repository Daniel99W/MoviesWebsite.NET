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
    public class GetMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, IEnumerable<Movie>>
    {
        private IRepositoryMovie movieRepository;
        public GetMoviesQueryHandler(IRepositoryMovie repositoryMovie) 
        {
            this.movieRepository = repositoryMovie;
        }
        public async Task<IEnumerable<Movie>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            var movies = await this.movieRepository.GetMovies();
            return movies;
        }
    }
}
