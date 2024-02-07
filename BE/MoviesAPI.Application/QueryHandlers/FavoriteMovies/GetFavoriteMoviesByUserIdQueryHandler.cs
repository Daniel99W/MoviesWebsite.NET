using MediatR;
using MoviesAPI.Application.Queries.FavoriteMovies;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;
using MoviesAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.QueryHandlers.FavoriteMovies
{
    public class GetFavoriteMoviesByUserIdQueryHandler : IRequestHandler<GetFavoriteMoviesByUserIdQuery, Pagination<Movie>>
    {
        private IRepositoryFavoriteMovie repositoryFavoriteMovie;
        private IRepositoryUser repositoryUser;
        public GetFavoriteMoviesByUserIdQueryHandler(IRepositoryFavoriteMovie repositoryFavoriteMovie,
            IRepositoryUser repositoryUser
            )
        {
            this.repositoryFavoriteMovie = repositoryFavoriteMovie;
            this.repositoryUser = repositoryUser;
        }
        public async Task<Pagination<Movie>> Handle(GetFavoriteMoviesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userId = await this.repositoryUser.GetUserByFirebaseId(request.FirebaseId);
            if (userId == null)
            {
                throw new HttpRequestException("This user doest not exist");
            }
         
            return 
                await 
                this.repositoryFavoriteMovie.GetFavoriteMoviesByUserId(request.Page, request.ItemsPerPage, (Guid)userId);
        }
    }
}
