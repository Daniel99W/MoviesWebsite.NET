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
    public class MovieTagRepository : Repository<MovieTag>,IRepositoryMovieTag
    {
        public MovieTagRepository(MoviesDbContext moviesDbContext)
            :base(moviesDbContext)
        {

        }

        public async Task DeleteMovieTagsByMovie(Guid movieId)
        {
            await this.moviesDbContext
                .MovieTags
                .Where(mt => mt.MovieId == movieId)
                .ExecuteDeleteAsync();
        }
    }
}
