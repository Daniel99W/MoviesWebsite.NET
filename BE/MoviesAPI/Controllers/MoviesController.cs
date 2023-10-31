using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Application.Commands.Movies;
using MoviesAPI.Application.Queries.Movies;
using MoviesAPI.Core.Entities;
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
            var mappedResult = mapper.Map<Pagination<GetMovieFeedDto>>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route(ApiRoutes.MovieRoutes.GetMovieById)]
        public async Task<IActionResult> GetMovieById(string Id)
        {
            var query = new GetMovieByIdQuery()
            {
                Id = Guid.Parse(Id)
            };
            var result = await mediator.Send(query);
            var mappedResult = this.mapper.Map<GetMovieDto>(result);
            return Ok(mappedResult);
        }

        [HttpPost]
        [Route(ApiRoutes.MovieRoutes.CreateMovie)]
        [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> CreateMovie([FromBody] CreateMovieDto createMovieDto)
        {
            var tags = mapper.Map<List<Tag>>(createMovieDto.Tags);

            var command = new CreateMovieCommand()
            {
                Title = createMovieDto.Title,
                Description = createMovieDto.Description,
                AddedDate = createMovieDto.AddedDate,
                VidGuardId = createMovieDto.VidGuardId,
                CategoriesIds = createMovieDto.CategoriesIds,
                PosterImageUrl = createMovieDto.PosterImageUrl,
                Tags = tags,
                PosterImageUrlGif = createMovieDto.PosterImageUrlGif,
                FirebaseId = createMovieDto.FirebaseId
            };
            var result = await mediator.Send(command);
            var mappedResult = mapper.Map<GetMovieDto>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route(ApiRoutes.MovieRoutes.GetMovieByTitle)]
        public async Task<IActionResult> GetMovieByTitle(string Title)
        {
            var command = new GetMovieByTitle()
            {
                Title = Title
            };
            var result = await mediator.Send(command);
            var mappedResult = mapper.Map<GetMovieDto>(result);
            return Ok(mappedResult);
        }

        [HttpDelete]
        [Route(ApiRoutes.MovieRoutes.DeleteMovieById)]
        [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> DeleteMovieId(string Id)
        {
            var command = new DeleteMovieByIdCommand()
            {
                Id = Guid.Parse(Id)
            };
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [Route(ApiRoutes.MovieRoutes.GetMoviesByTitle)]
        [Authorize(Roles ="ADMIN")]
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
        public async Task<IActionResult> UpdateViewsCounter([FromBody] UpdateViewsCounterDto updateViewsCounterDto)
        {
            
            var command = new UpdateMovieViewsCounterCommand()
            {
                Id = Guid.Parse(updateViewsCounterDto.Id)
            };
            var result = await mediator.Send(command);
            var mappedResult = mapper.Map<GetMovieDto>(result);
            return Ok(mappedResult);
        }

        [HttpPatch]
        [Route(ApiRoutes.MovieRoutes.UpdateMovieById)]
        [Authorize(Roles ="ADMIN")]
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
