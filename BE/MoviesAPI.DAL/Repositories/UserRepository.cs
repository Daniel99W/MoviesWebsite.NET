using Microsoft.EntityFrameworkCore;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
