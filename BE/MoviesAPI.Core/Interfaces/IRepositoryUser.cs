using MoviesAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Core.Interfaces
{
    public interface IRepositoryUser : IRepository<User>
    {
        public  Task<IEnumerable<User>> GetAllUsers();

    }
}
