using MediatR;
using MoviesAPI.Core.Entities;
using MoviesAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.Queries.FavoriteMovies
{
   public class GetFavoriteMoviesByUserIdQuery : IRequest<Pagination<Movie>>
    {
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public string FirebaseId { get; set; }
    }
}
