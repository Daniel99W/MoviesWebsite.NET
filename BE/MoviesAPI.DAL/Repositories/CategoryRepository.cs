using Microsoft.EntityFrameworkCore;
using MiNET.Net;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.DAL.Repositories
{
    public class CategoryRepository : Repository<Category>, IRepositoryCategory
    {
        public CategoryRepository(MoviesDbContext moviesDbContext)
            : base(moviesDbContext)
        {

        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await moviesDbContext.Categories.ToListAsync();
        }
    }
}
