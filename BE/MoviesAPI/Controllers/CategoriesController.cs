using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Application.Queries.Categories;
using MoviesAPI.Dtos.Categories;

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
    }
}
