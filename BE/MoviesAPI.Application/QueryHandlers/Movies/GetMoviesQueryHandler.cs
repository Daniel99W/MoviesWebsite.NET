using MediatR;
using MoviesAPI.Application.Queries.Movies;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;
using MoviesAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.QueryHandlers.Movies
{
    public class GetMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, Pagination<Movie>>
    {
        private IRepositoryMovie movieRepository;
        public GetMoviesQueryHandler(IRepositoryMovie repositoryMovie) 
        {
            this.movieRepository = repositoryMovie;
        }
        public async Task<Pagination<Movie>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            var movies = await this.movieRepository.GetMovies(
                request.ItemsPerPage,
                request.Page,
                request.Title,
                request.CategoriesIds,
                request.BeginAddedDate,
                request.EndAddedDate);
            return movies;
        }
    }
}
