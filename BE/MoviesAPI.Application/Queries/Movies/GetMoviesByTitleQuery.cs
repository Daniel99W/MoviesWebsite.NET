using MediatR;
using MoviesAPI.Core.Entities;
using MoviesAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.Queries.Movies
{
    public class GetMoviesByTitleQuery : IRequest<Pagination<Movie>>
    {
        public int Page { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 5;
        public string? Title { get; set; }
    }
}
