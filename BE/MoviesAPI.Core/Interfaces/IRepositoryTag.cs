using MoviesAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Core.Interfaces
{
    public interface IRepositoryTag : IRepository<Tag>
    {
        public Task<Tag?> FindByName(string name);
        public Task<List<Tag>> GetMovieTagByMovieId(Guid movieId);
    }
}
