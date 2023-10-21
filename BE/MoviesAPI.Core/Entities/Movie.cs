using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Core.Entities
{
    public class Movie : Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public string VidGuardId { get; set; }
        public DateTime AddedDate { get; set; }
        public List<Category> Categories { get; set; }
        public List<Comment> Comments { get; set; }
        public List<FavoriteMovie> Users { get; set; }
        public List<VotedMovie> VotedMovies { get; set; }

        public Movie()
        {

        }

        public Movie(string title,
            string description,
            DateTime AddedDate,
            string vidguardId,
            List<Category> categories
            )
        {
            this.Title = title;
            this.Description = description;
            this.AddedDate = AddedDate;
            this.VidGuardId = vidguardId;
            this.Categories = categories;
            this.Views = 0;
        }

        public static Movie CreateMovie(string title,
            string description,
            DateTime AddedDate,
            string vidguardId,
            List<Category> categories
            )
        {
            return new Movie(title, description, AddedDate, vidguardId,categories);
        }

        public Movie UpdateViewsCounter()
        {
            this.Views += 1;
            return this;
        }


    }
}
