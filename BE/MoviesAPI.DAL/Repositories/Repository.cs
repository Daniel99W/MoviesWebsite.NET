using MoviesAPI.Core.Interfaces;

namespace MoviesAPI.DAL.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected MoviesDbContext moviesDbContext;
        public Repository(MoviesDbContext moviesDbContext)
        {
            this.moviesDbContext = moviesDbContext;
        }
        public virtual T Create(T entity)
        {
            return moviesDbContext.Add(entity).Entity;
        }

        public virtual void Delete(T obj)
        {
            moviesDbContext.Remove(obj);
        }

        public virtual async Task<T?> Read(Guid id)
        {
            return await moviesDbContext.FindAsync<T>(id);
        }

        public void SaveChanges()
        {
            moviesDbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await moviesDbContext.SaveChangesAsync();
        }

        public virtual T Update(T entity)
        {
            return moviesDbContext.Update(entity).Entity;
        }
        
    }
}
