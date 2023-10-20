using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Core.Entities
{
    public class Comment : Base
    {
        public Movie Movie { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
    }
}
