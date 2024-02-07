using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Application.Commands.FavoriteMovies;
using MoviesAPI.Application.Queries.FavoriteMovies;
using MoviesAPI.Dtos;
using MoviesAPI.Dtos.FavoriteMovies;
using MoviesAPI.Dtos.Movies;
using System.Security.Claims;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteMoviesController : ControllerBase
    {
        private IMediator mediator;
        private IMapper mapper;

        public FavoriteMoviesController(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [HttpPost]
        [Route(ApiRoutes.FavoriteMoviesRoutes.AddMovieToFavoriteList)]
        [Authorize]
        public async Task<IActionResult> AddMoviesToFavoriteList([FromBody] CreateFavoriteMovie createFavoriteMovie)
        {
            var command = new CreateFavoriteMovieCommand()
            {
                MovieId = Guid.Parse(createFavoriteMovie.MovieId),
                UserId = Guid.Parse(createFavoriteMovie.UserId)
            };
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [Route(ApiRoutes.FavoriteMoviesRoutes.GetFavoriteMoviesByUserId)]
        [Authorize]
        public async Task<IActionResult> GetFavoriteMoviesByUserId([FromQuery] GetFavoriteMoviesParams getFavoriteMoviesParams, 
            string Id)
        {
            var query = new GetFavoriteMoviesByUserIdQuery()
            {
                Page = getFavoriteMoviesParams.Page,
                ItemsPerPage = getFavoriteMoviesParams.ItemsPerPage,
                FirebaseId = Id
            };

            var result = await mediator.Send(query);
            var mappedResult = mapper.Map<Pagination<GetMovieFeedDto>>(result);
            return Ok(mappedResult);
        }

        [HttpDelete]
        [Route(ApiRoutes.FavoriteMoviesRoutes.DeleteFavoriteMovieByUserIdAndMovieId)]
        [Authorize]
        public async Task<IActionResult> DeleteFavoriteMovieByUserIdAndMovieId([FromQuery] DeleteFavoriteMovieParamsDto deleteFavoriteMovieParamsDto)
        {
            var command = new RemoveMovieFromFavoriteListCommand()
            {
                FirebaseId = deleteFavoriteMovieParamsDto.FirebaseId,
                MovieId = Guid.Parse(deleteFavoriteMovieParamsDto.MovieId)
            };
            await mediator.Send(command);
            return NoContent();
        }
    }
}
