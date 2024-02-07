using MediatR;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.Commands.FavoriteMovies
{
    public class RemoveMovieFromFavoriteListCommand : IRequest<Unit>
    {
        public string FirebaseId { get; set; }
        public Guid MovieId { get; set; }
    }
}
