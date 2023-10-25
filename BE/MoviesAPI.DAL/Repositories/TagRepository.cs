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
    public class TagRepository : Repository<Tag>,IRepositoryTag
    {
        public TagRepository(MoviesDbContext moviesDbContext)
            :base(moviesDbContext)
        {

        }

        public async Task<Tag?> FindByName(string name)
        {
            return await this
                .moviesDbContext
                .Tags
                .Where(t => t.Name == name)
                .SingleOrDefaultAsync();
        }

        public async Task<List<Tag>> GetMovieTagByMovieId(Guid movieId)
        {
            return await this
                .moviesDbContext
                .MovieTags
                .Where(mt => mt.MovieId == movieId)
                .Select(mt => new Tag()
                {
                    Id = mt.Id,
                    Name = mt.Tag.Name
                })
                .ToListAsync();
        }
    }
}
