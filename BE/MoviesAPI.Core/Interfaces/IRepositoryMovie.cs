using MoviesAPI.Core.Entities;
using MoviesAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Core.Interfaces
{
    public interface IRepositoryMovie : IRepository<Movie>
    {
        public Task<Pagination<Movie>> GetMovies(int ItemsPerPage,
            int Page,
            string? Title,
            List<Guid>? categoriesIds,
            DateTime? BeginAddedDate,
            DateTime? EndAddedDate);

        public Task<Pagination<Movie>> GetMoviesByTitle(int ItemsPerPage,int Page,string? Title, Guid UserId);

        public Task<Movie?> GetMovieByVidGuardId(string Id);

        public Task<Movie?> GetMovieByTitle(string Title);
    }
}
