using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.Commands.Categories
{
    public class DeleteCategoryCommand : IRequest<Unit>
    {
        public Guid CategoryId { get; set; }
    }
}
