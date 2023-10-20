using AutoMapper;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;
using MoviesAPI.Dtos.Users;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class UsersController : ControllerBase
    {
        private IRepositoryUser repositoryUser;
        private IMapper mapper;

        public UsersController(IRepositoryUser repositoryUser, IMapper mapper)
        {
            this.repositoryUser = repositoryUser;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route(ApiRoutes.UsersRoutes.GetUsers)]
        public async Task<ActionResult<IEnumerable<UserGetDto>>> GetUsers()
        {
            var result = await this.repositoryUser.GetAllUsers();
            var mappedResult = this.mapper.Map<List<UserGetDto>>(result);
            return Ok(mappedResult);
        }

        [HttpPost]
        [Route(ApiRoutes.UsersRoutes.CreateUser)]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var userRecordArgs = new UserRecordArgs()
            {
                DisplayName = createUserDto.Name,
                Email = createUserDto.Email,
                Password = createUserDto.Password,
            };
            var user = await FirebaseAuth.DefaultInstance.CreateUserAsync(userRecordArgs);
            return Ok(user.Uid);
        }

    }

}
