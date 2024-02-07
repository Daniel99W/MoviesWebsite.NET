using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.Commands.FavoriteMovies
{
    public class CreateFavoriteMovieCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public Guid MovieId { get; set; }
    }
}
