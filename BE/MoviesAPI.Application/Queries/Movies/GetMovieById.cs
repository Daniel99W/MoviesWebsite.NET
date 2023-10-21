using MediatR;
using MoviesAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.Queries.Movies
{
    public class GetMovieById : IRequest<Movie>
    {
        public Guid Id { get; set; }
    }
}
