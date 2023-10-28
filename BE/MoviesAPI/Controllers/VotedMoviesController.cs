using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Application.Commands.VotedMovies;
using MoviesAPI.Dtos.VotedMovies;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotedMoviesController : ControllerBase
    {
        private IMapper mapper;
        private IMediator mediator;

        public VotedMoviesController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route(ApiRoutes.VotedMoviesRoutes.VotedMovieByUserAndMovieID)]
        [Authorize]
        public async Task<IActionResult> VoteMovieByUserAndMovieId([FromBody] CreateVotedMovieDto createVotedMovieDto)
        {
            var command = new CreateVotedMovieCommand()
            {
                UserId = Guid.Parse(createVotedMovieDto.UserId),
                MovieId = Guid.Parse(createVotedMovieDto.MovieId)
            };
            await this.mediator.Send(command);
            return Ok();
        }

        [HttpPost]
        [Route(ApiRoutes.VotedMoviesRoutes.DownVotedMovieByUserAndMovieID)]
        [Authorize]
        public async Task<IActionResult> DownVotedMovieByUserAndMovieId([FromBody] CreateVotedMovieDto createVotedMovieDto)
        {
            var command = new CreateDownVotedMovieCommand()
            {
                UserId = Guid.Parse(createVotedMovieDto.UserId),
                MovieId = Guid.Parse(createVotedMovieDto.MovieId)
            };
            await this.mediator.Send(command);
            return Ok();
        }

    }
}
