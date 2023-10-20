using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Core.Entities
{
    public class Movie : Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public int Upvote { get; set; }
        public int Downvote { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
