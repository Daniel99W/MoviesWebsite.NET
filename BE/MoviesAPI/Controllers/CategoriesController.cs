using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Application.Commands.Categories;
using MoviesAPI.Application.Queries.Categories;
using MoviesAPI.Dtos.Categories;
using System.Runtime.CompilerServices;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private IMediator mediator;
        private IMapper mapper;
        public CategoriesController(IMediator mediator, IMapper mapper)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [HttpGet]
        [Route(ApiRoutes.CategoriesRoutes.GetCategories)]
        public async Task<IActionResult> GetCategories()
        {
            var comannd = new GetCategoriesQuery();
            var result = await mediator.Send(comannd);
            var mappedResult = mapper.Map<List<GetCategoryDto>>(result);
            return Ok(mappedResult);
        }

        [HttpPost]
        [Route(ApiRoutes.CategoriesRoutes.CreateCategory)]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var command = new CreateCategoryCommand()
            {
                Name = createCategoryDto.Name,
            };
            var caregory = await mediator.Send(command);
            var mappedResult = mapper.Map<GetCategoryDto>(caregory);
            return Ok(mappedResult);
        }

        [HttpDelete]
        [Route(ApiRoutes.CategoriesRoutes.DeleteCategoryById)]
        public async Task<IActionResult> DeleteCategoryById(string Id)
        {
            var command = new DeleteCategoryCommand()
            {
                CategoryId = Guid.Parse(Id)
            };
            await mediator.Send(command);
            return NoContent();
        }


    }
}
