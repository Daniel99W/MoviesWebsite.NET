using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Core.Entities
{
    public class MovieCategory : Base
    {
        public Movie Movie { get; set; }
        public Category Category { get; set; }
        public Guid MovieId { get; set; }
        public Guid CategoryId { get; set; }

    }
}
