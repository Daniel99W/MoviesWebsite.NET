using MediatR;
using MoviesAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.Commands.Movies
{
    public class UpdateMovieViewsCounterCommand : IRequest<Movie>
    {
        public Guid Id { get; set; }
    }
}
