using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Core.Entities
{
    public class MovieTag : Base
    {
        public Movie Movie { get; set; }
        public Tag Tag { get; set; }
        public Guid MovieId { get; set; }
        public Guid TagId { get; set; }
    }
}
