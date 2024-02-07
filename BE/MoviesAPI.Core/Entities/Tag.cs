using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Core.Entities
{
    public class Tag : Base
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        List<MovieTag> MovieTags { get; set; }
    }
}
