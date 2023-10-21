using MediatR;
using MoviesAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.Queries.Users
{
    public class GetAllUsersQuery : IRequest<IEnumerable<User>>
    {
    }
}
