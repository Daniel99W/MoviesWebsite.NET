using Microsoft.EntityFrameworkCore;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;

namespace MoviesAPI.DAL.Repositories
{
    public class UserRepository : Repository<User>,IRepositoryUser
    {
        public UserRepository(MoviesDbContext moviesDbContext)
            :base(moviesDbContext)
        {

        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await moviesDbContext.Users.ToListAsync();
        }

        public async Task<Guid?> GetUserByFirebaseId(string id)
        {
            return await moviesDbContext
                .Users
                .Where(u => u.FirebaseId == id)
                .Select(u => u.Id)
                .SingleOrDefaultAsync();
        }
    }
}
