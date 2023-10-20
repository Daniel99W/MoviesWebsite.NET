using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Core.Entities
{
    public class User : Base
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<FavoriteMovie> Movies { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
