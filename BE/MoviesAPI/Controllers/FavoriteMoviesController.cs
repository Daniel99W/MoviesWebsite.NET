using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Application.Commands.FavoriteMovies;
using MoviesAPI.Dtos.FavoriteMovies;

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
    }
}
