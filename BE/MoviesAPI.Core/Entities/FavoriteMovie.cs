using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Core.Entities
{
    public class FavoriteMovie : Base
    {
        public Movie Movie { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public Guid MovieId { get; set; }
    }
}
