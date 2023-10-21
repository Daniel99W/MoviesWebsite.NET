using AutoMapper;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiNET.Utils;
using MoviesAPI.Application.Commands.Users;
using MoviesAPI.Application.Queries.Users;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;
using MoviesAPI.Dtos.Users;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IMapper mapper;
        private IMediator mediator;

        public UsersController(IMediator mediator, IMapper mapper)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [HttpGet]
        [Route(ApiRoutes.UsersRoutes.GetUsers)]
        public async Task<ActionResult<IEnumerable<UserGetDto>>> GetUsers()
        {
            var query = new GetAllUsersQuery();
            var result = await mediator.Send(query);
            var mappedResult = this.mapper.Map<List<UserGetDto>>(result);
            return Ok(mappedResult);
        }

        [HttpPost]
        [Route(ApiRoutes.UsersRoutes.CreateUser)]
        public async Task<ActionResult<string>> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var command = new CreateUserCommand()
            {
                Name = createUserDto.Name,
                Email = createUserDto.Email,
                Password = createUserDto.Password
            };
            var result = await mediator.Send(command);
           
            return Ok(result);
        }

    }

}
