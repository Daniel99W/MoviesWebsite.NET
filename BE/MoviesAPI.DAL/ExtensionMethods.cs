using Microsoft.EntityFrameworkCore;
using MoviesAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.DAL
{
    public static class ExtensionMethods
    {
        public static async Task<Pagination<T>> Paginate<T>(this IQueryable<T> query, int page, int itemsPerPage)
        {
            int rowsToBeSkiped = itemsPerPage * page - itemsPerPage;

            int totalItems = query.Count();

            List<T> results = await query
                .Skip(rowsToBeSkiped)
                .Take(itemsPerPage)
                .ToListAsync();

            Pagination<T> paginated = new()
            {
                Page = page,
                TotalPages =
                Convert.ToInt16(Math.Ceiling((double)totalItems / itemsPerPage)),
                Results = results
            };

            return paginated;

        }
    }
}
