using MediatR;
using MoviesAPI.Application.Queries.Movies;
using MoviesAPI.Application.Queries.Users;
using MoviesAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.QueryHandlers.Users
{
    public class GetUserByFirebaseIdQueryHandler : IRequestHandler<GetUserByFirebaseIdQuery, Guid?>
    {
        private IRepositoryUser repositoryUser;
        public GetUserByFirebaseIdQueryHandler(IRepositoryUser repositoryUser)
        {
            this.repositoryUser = repositoryUser;
        }
        public async Task<Guid?> Handle(GetUserByFirebaseIdQuery request, CancellationToken cancellationToken)
        {
            return await this.repositoryUser.GetUserByFirebaseId(request.FirebaseId);
        }
    }
}
