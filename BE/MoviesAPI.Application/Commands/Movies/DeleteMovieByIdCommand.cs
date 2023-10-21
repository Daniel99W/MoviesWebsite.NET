using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.Commands.Movies
{
    public class DeleteMovieByIdCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
