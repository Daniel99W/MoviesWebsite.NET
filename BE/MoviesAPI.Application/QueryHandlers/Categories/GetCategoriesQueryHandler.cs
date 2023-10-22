using MediatR;
using MoviesAPI.Application.Queries.Categories;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.QueryHandlers.Categories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<Category>>
    {
        private IRepositoryCategory repositoryCategory;
        public GetCategoriesQueryHandler(IRepositoryCategory repositoryCategory) 
        {
            this.repositoryCategory = repositoryCategory;
        }

        public async Task<IEnumerable<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await this.repositoryCategory.GetAllCategories();
        }
    }
}
