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
    public class MovieCategoryRepository : Repository<MovieCategory>,IRepositoryMovieCategory
    {
        public MovieCategoryRepository(MoviesDbContext moviesDbContext)
            :base(moviesDbContext)

        {

        }

        public async Task DeleteMovieCategoriesByMovieId(Guid movieId)
        {
           await this.moviesDbContext.MoviesCategories
                .Where(mc => mc.MovieId == movieId)
                .ExecuteDeleteAsync();
        }
    }
}
