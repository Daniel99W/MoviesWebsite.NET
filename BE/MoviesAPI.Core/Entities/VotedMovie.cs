using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Core.Entities
{
    public class VotedMovie : Base
    {
        public Movie Movie { get; set; }
        public User User { get; set; }

        public Guid MovieId { get; set; }
        public Guid UserId { get; set; }

        public bool Upvote { get; set; } = false;
        public bool Downvote { get; set; } = false;
        public VotedMovie()
        {

        }
        public VotedMovie(Guid UserId,
            Guid MovieId,
            bool Upvote,
            bool Downvote)
        {
            this.UserId = UserId;
            this.MovieId = MovieId;
            this.Upvote = Upvote;
            this.Downvote = Downvote;
        }

        public static VotedMovie CreateVotedMovie(Guid UserId, Guid MovieId, bool Upvote, bool Downvote)
        {
            return new VotedMovie(UserId, MovieId, Upvote, Downvote);
        }

    }
}
