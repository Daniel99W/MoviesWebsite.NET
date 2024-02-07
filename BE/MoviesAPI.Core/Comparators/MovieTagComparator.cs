using MoviesAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Core.Comparators
{
    public class MovieTagComparator : IEqualityComparer<MovieTag>
    {
        public bool Equals(MovieTag? x, MovieTag? y)
        {
            return x.Tag.Name == y.Tag.Name;
        }

        public int GetHashCode([DisallowNull] MovieTag obj)
        {
            throw new NotImplementedException();
        }
    }
}
