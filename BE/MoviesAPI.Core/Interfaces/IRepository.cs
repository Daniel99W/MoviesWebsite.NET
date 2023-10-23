
namespace MoviesAPI.Core.Interfaces
{
    public interface IRepository<T>
    {
        public Task<T?> Read(Guid id);
        public T Update(T entity);
        public void Delete(T obj);
        public T Create(T entity);
        public Task SaveChangesAsync();
        public void SaveChanges();
    }
}
