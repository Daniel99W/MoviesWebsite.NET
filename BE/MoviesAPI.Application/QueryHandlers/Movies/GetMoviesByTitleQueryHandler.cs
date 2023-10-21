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
    public class GetMoviesByTitleQueryHandler : IRequestHandler<GetMoviesByTitleQuery, Pagination<Movie>>
    {
        private IRepositoryMovie repositoryMovie;
        public GetMoviesByTitleQueryHandler(IRepositoryMovie repositoryMovie)
        {
            this.repositoryMovie = repositoryMovie;
        }
            

        public async Task<Pagination<Movie>> Handle(GetMoviesByTitleQuery request, CancellationToken cancellationToken)
        {
            return await repositoryMovie.GetMoviesByTitle(request.ItemsPerPage,request.Page,request.Title);
        }
    }
}
