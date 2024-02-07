using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.Queries.Users
{
    public class GetUserByFirebaseIdQuery : IRequest<Guid?>
    {
        public string FirebaseId { get; set; }
    }
}
