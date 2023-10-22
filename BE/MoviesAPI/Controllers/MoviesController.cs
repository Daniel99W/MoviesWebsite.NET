using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Application.Commands.Movies;
using MoviesAPI.Application.Queries.Movies;
using MoviesAPI.Dtos;
using MoviesAPI.Dtos.Movies;


namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private IMapper mapper;
        private IMediator mediator;
        public MoviesController(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [HttpGet]
        [Route(ApiRoutes.MovieRoutes.GetMovies)]
        public async Task<IActionResult> GetMovies([FromQuery] GetMovieParams getMovieParams)
        {
            var query = new GetAllMoviesQuery()
            {
                Title = getMovieParams.Title,
                ItemsPerPage = getMovieParams.ItemsPerPage,
                Page = getMovieParams.Page,
                CategoriesIds = getMovieParams.CategoriesIds,
                BeginAddedDate = getMovieParams.BeginAddedDate,
                EndAddedDate = getMovieParams.EndAddedDate
            };
            var result = await mediator.Send(query);
            var mappedResult = mapper.Map<Pagination<GetMovieDto>>(result);
            return Ok(mappedResult);
        }

        [HttpPost]
        [Route(ApiRoutes.MovieRoutes.CreateMovie)]
        public async Task<IActionResult> CreateMovie([FromBody] CreateMovieDto createMovieDto)
        {
            var command = new CreateMovieCommand()
            {
                Title = createMovieDto.Title,
                Description = createMovieDto.Description,
                AddedDate = createMovieDto.AddedDate,
                VidGuardId = createMovieDto.VidGuardId,
                CategoriesIds = createMovieDto.CategoriesIds,
                PosterImageUrl = createMovieDto.PosterImageUrl
            };
            var result = await mediator.Send(command);
            var mappedResult = mapper.Map<GetMovieDto>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route(ApiRoutes.MovieRoutes.GetMovieById)]
        public async Task<IActionResult> GetMovieById(Guid Id)
        {
            var command = new GetMovieByIdCommand()
            {
                Id = Id
            };
            var result = await mediator.Send(command);
            var mappedResult = mapper.Map<GetMovieDto>(result);
            return Ok(mappedResult);
        }

        [HttpDelete]
        [Route(ApiRoutes.MovieRoutes.DeleteMovieById)]
        public async Task<IActionResult> DeleteMovieId(Guid Id)
        {
            var command = new DeleteMovieByIdCommand()
            {
                Id = Id
            };
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [Route(ApiRoutes.MovieRoutes.GetMoviesByTitle)]
        public async Task<IActionResult> GetMoviesByTitle([FromQuery] GetMoviesByTitleParams getMoviesByTitleParams)
        {
            var query = new GetMoviesByTitleQuery()
            {
                Page = getMoviesByTitleParams.Page,
                ItemsPerPage = getMoviesByTitleParams.ItemsPerPage,
                Title = getMoviesByTitleParams.Title
            };
            var result = await mediator.Send(query);
            var mappedResult = mapper.Map<Pagination<GetMovieDto>>(result);
            return Ok(mappedResult);
        }

        [HttpPatch]
        [Route(ApiRoutes.MovieRoutes.UpdateViewsCounter)]
        public async Task<IActionResult> UpdateViewsCounter(Guid Id)
        {
            var command = new UpdateMovieViewsCounterCommand()
            {
                Id = Id
            };
            var result = await mediator.Send(command);
            var mappedResult = mapper.Map<GetMovieDto>(result);
            return Ok(mappedResult);
        }

        [HttpPatch]
        [Route(ApiRoutes.MovieRoutes.UpdateMovieById)]
        public async Task<IActionResult> UpdateMoviesById(Guid Id, [FromBody] UpdateMovieById updateMovieById)
        {
            var command = new UpdateMovieByIdCommand()
            {
                Id = Id,
                Title = updateMovieById.Title,
                Description = updateMovieById.Description,
                CategoriesIds = updateMovieById.CategoriesIds
            };
            var result = await mediator.Send(command);
            var mappedResult = mapper.Map<GetMovieDto>(result);
            return Ok(mappedResult);
        }















    }
}
