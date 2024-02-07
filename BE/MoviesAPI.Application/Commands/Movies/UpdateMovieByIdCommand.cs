using MediatR;
using MoviesAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.Commands.Movies
{
    public class UpdateMovieByIdCommand : IRequest<Movie>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Guid> CategoriesIds { get; set; }
        public Guid Id { get; set; }
    }
}
