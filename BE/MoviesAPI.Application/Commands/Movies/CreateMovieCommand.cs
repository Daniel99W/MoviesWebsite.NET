using MediatR;
using MoviesAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.Commands.Movies
{
    public class CreateMovieCommand : IRequest<Movie>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PosterImageUrl { get; set; }
        public DateTime AddedDate { get; set; }
        public string VidGuardId { get; set; }
        public List<Guid> CategoriesIds { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
