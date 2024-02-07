using MoviesAPI.Core.Entities;


namespace MoviesAPI.Core.Interfaces
{
    public interface IRepositoryUser : IRepository<User>
    {
        public  Task<IEnumerable<User>> GetAllUsers();
        public  Task<Guid?> GetUserByFirebaseId(string id);
    }
}
