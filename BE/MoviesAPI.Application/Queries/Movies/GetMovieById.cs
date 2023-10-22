using MediatR;
using MoviesAPI.Application.Responses;
using MoviesAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.Queries.Movies
{
    public class GetMovieById : IRequest<GetMovieWithVotesCounted>
    {
        public Guid Id { get; set; }
    }
}
