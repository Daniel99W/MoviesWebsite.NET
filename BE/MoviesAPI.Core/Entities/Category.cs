using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Core.Entities
{
    public class Category : Base
    {
        public string Name { get; set; }
        public List<MovieCategory> Movies { get; set; }
        public Category()
        {

        }
        public Category(string name)
        {
            this.Name = name;
        }
    }
}
