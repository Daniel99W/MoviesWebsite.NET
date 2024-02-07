using MoviesAPI.Core.Entities;


namespace MoviesAPI.Core.Interfaces
{
    public interface IRepositoryCategory : IRepository<Category>
    {
        public Task<IEnumerable<Category>> GetAllCategories();

        public Task<Category?> GetCategoryByName(string name);
    }
}
