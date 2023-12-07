using MediatR;
using MoviesAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.Commands.Categories
{
    public class CreateCategoryCommand : IRequest<Category>
    {
        public string Name { get; set; }
    }
}
