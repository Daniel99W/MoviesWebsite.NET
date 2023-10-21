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
    public class MovieRepository : Repository<Movie>, IRepositoryMovie
    {
        public MovieRepository(MoviesDbContext moviesDbContext)
            : base(moviesDbContext)
        {

        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return await this.moviesDbContext.Movies.ToListAsync();
        }
    }
}
