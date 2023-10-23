using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.Responses
{
    public class GetMovieWithVotesCounted 
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? PosterImageUrl { get; set; }
        public int Views { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public int Likes { get; set; }
        public DateTime AddedDate { get; set; }
        public string VidGuardId { get; set; }

    }
}
