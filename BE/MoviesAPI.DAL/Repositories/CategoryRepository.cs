using Microsoft.EntityFrameworkCore;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;

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

        public async Task<Category?> GetCategoryByName(string name)
        {
            return await moviesDbContext
                .Categories
                .Where(c => c.Name == name)
                .SingleOrDefaultAsync();
        }
    }
}
