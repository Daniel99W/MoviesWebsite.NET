using MediatR;
using MoviesAPI.Application.Commands.Categories;
using MoviesAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.CommandsHandler.Categories
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private IRepositoryCategory repositoryCategory;
        public DeleteCategoryCommandHandler(IRepositoryCategory repositoryCategory) 
        {
            this.repositoryCategory = repositoryCategory;
        }
        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await repositoryCategory.Read(request.CategoryId);
            if(category == null)
            {
                throw new Exception("This category does not exist!");
            }
            this.repositoryCategory.Delete(category);
            this.repositoryCategory.SaveChanges();
            return await Task.FromResult(Unit.Value);
        }
    }
}
