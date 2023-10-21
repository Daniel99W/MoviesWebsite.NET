using MediatR;
using MoviesAPI.Application.Queries.Users;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.QueryHandlers.Users
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
    {
        private IRepositoryUser repositoryUser;
        public GetAllUsersQueryHandler(IRepositoryUser repositoryUser)
        {
            this.repositoryUser = repositoryUser;
        }

        public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await this.repositoryUser.GetAllUsers();
        }
    }
}
