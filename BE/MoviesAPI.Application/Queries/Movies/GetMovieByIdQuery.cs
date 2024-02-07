using MediatR;
using MoviesAPI.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.Queries.Movies
{
    public class GetMovieByIdQuery : IRequest<GetMovieWithVotesCounted>
    {
        public Guid Id { get; set; }
    }
}
