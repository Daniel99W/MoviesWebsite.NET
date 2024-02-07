using MediatR;
using MoviesAPI.Application.Commands.FavoriteMovies;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.CommandsHandler.FavoriteMovies
{
    public class CreateFavoriteMovieCommandHandler : IRequestHandler<CreateFavoriteMovieCommand, Unit>
    {
        private IRepositoryFavoriteMovie repositoryFavoriteMovie;
        private IRepositoryMovie repositoryMovie;
        private IRepositoryUser repositoryUser;
        public CreateFavoriteMovieCommandHandler(
            IRepositoryMovie repositoryMovie,
            IRepositoryUser repositoryUser,
            IRepositoryFavoriteMovie repositoryFavoriteMovie)
        {
            this.repositoryMovie = repositoryMovie;
            this.repositoryUser = repositoryUser;
            this.repositoryFavoriteMovie = repositoryFavoriteMovie;
        }
        public async Task<Unit> Handle(CreateFavoriteMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await this.repositoryMovie.Read(request.MovieId);
            var user = await this.repositoryUser.Read(request.UserId);
            if(movie == null)
            {
                throw new HttpRequestException("Movie does not exist");
            }
            if(user == null)
            {
                throw new HttpRequestException("User does not exist");
            }
            var favMovie =
                await this.repositoryFavoriteMovie.GetFavoriteMovieByUserAndMovieId(request.MovieId, request.UserId);
            if(favMovie != null)
            {
                this.repositoryFavoriteMovie.Delete(favMovie);
                await this.repositoryFavoriteMovie.SaveChangesAsync();
                return Unit.Value;
            }
            var favoriteMovie = new FavoriteMovie()
            {
                UserId = request.UserId,
                MovieId = request.MovieId,
            };
            this.repositoryFavoriteMovie.Create(favoriteMovie);
            await this.repositoryFavoriteMovie.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
