using FirebaseAdmin.Auth;
using MediatR;
using MiNET.Utils;
using MoviesAPI.Application.Commands.Users;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Enums;
using MoviesAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.CommandsHandler.Users
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private IRepositoryUser repositoryUser;
        public CreateUserCommandHandler(IRepositoryUser repositoryUser)
        {
            this.repositoryUser = repositoryUser;
        }
        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userRecordArgs = new UserRecordArgs()
            {
                DisplayName = request.Name,
                Email = request.Email,
                Password = request.Password
            };
            var claims = new Dictionary<string, object>
            {
                {ClaimTypes.Role, Roles.USER.ToString() },
                {ClaimTypes.Expiration, 3600 }
            };

            UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userRecordArgs);
            var user = User.CreateUser(userRecord.Uid, userRecordArgs.DisplayName, userRecordArgs.Email);
            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(userRecord.Uid, claims);
            repositoryUser.Create(user);
            await repositoryUser.SaveChangesAsync();
            return userRecord.Uid;
        }


    }
}
