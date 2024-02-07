using MediatR;
using MoviesAPI.Application.Commands.Categories;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.CommandsHandler.Categories
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
    {
        private IRepositoryCategory repositoryCategory;
        public CreateCategoryCommandHandler(IRepositoryCategory repositoryCategory)
        {
            this.repositoryCategory = repositoryCategory;
        }
        public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await repositoryCategory.GetCategoryByName(request.Name);
            if(category != null)
            {
                throw new Exception("This category already exist!");
            }
            category = new Category(request.Name);
            repositoryCategory.Create(category);
            repositoryCategory.SaveChanges();
            return category;
        }
    }
}
