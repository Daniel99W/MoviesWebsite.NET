using MediatR;
using MoviesAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.Queries.Movies
{
    public class GetAllMoviesQuery : IRequest<IEnumerable<Movie>>
    {
        public int ItemsPerPage { get; set; }
        public int Page { get; set; }
        public string Title { get; set; }
        public List<Guid> CategoriesIds { get; set; }
        public DateTime BeginAddedDate { get; set; }
        public DateTime EndAddedDate { get; set; }
    }
}
