using MediatR;
using MoviesAPI.Application.Commands.FavoriteMovies;
using MoviesAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.CommandsHandler.FavoriteMovies
{
    public class DeleteFavoriteMovieCommandHandler : IRequestHandler<RemoveMovieFromFavoriteListCommand, Unit>
    {
        private IRepositoryMovie repositoryMovie;
        private IRepositoryFavoriteMovie repositoryFavoriteMovie;
        private IRepositoryUser repositoryUser;
        public DeleteFavoriteMovieCommandHandler(IRepositoryFavoriteMovie repositoryFavoriteMovie,
            IRepositoryMovie repositoryMovie,
            IRepositoryUser repositoryUser
            ) 
        { 
            this.repositoryMovie = repositoryMovie;
            this.repositoryFavoriteMovie = repositoryFavoriteMovie;
            this.repositoryUser = repositoryUser;
        }
        public async Task<Unit> Handle(RemoveMovieFromFavoriteListCommand request, CancellationToken cancellationToken)
        {
            var userId = await this.repositoryUser.GetUserByFirebaseId(request.FirebaseId);
            if (userId == null)
            {
                throw new HttpRequestException("User does not exist");
            }
            var movie = await this.repositoryMovie.Read(request.MovieId);
            if (movie == null)
            {
                throw new HttpRequestException("Movie does not exist");
            }

            await this.repositoryFavoriteMovie.DeleteFavoriteMovieByUserIdAndMovieId(request.MovieId, (Guid)userId);
            await this.repositoryFavoriteMovie.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
