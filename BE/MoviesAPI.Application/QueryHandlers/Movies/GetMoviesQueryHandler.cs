using MediatR;
using MoviesAPI.Application.Queries.Movies;
using MoviesAPI.Application.Responses;
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
        private IRepositoryMovie repositoryMovie;
        private IRepositoryVotedMovies repositoryVotedMovies;
        private IRepositoryFavoriteMovie repositoryFavoriteMovie;
        public GetMoviesQueryHandler(IRepositoryMovie repositoryMovie,
            IRepositoryVotedMovies repositoryVotedMovies,
            IRepositoryFavoriteMovie repositoryFavoriteMovie
            ) 
        {
            this.repositoryMovie = repositoryMovie;
            this.repositoryVotedMovies = repositoryVotedMovies;
            this.repositoryFavoriteMovie = repositoryFavoriteMovie;
        }
        public async Task<Pagination<Movie>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            Pagination<Movie> moviesPaginated =
                await repositoryMovie.GetMovies(request.ItemsPerPage,
                request.Page,request.Title,
                request.CategoriesIds,
                request.BeginAddedDate,
                request.EndAddedDate);


            return moviesPaginated;
        }
    }
}
