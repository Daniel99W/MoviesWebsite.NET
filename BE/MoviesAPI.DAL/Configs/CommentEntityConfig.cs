using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.DAL.Configs
{
    public class UserEntityConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder) 
        {
            builder
                 .HasOne<User>(c => c.User)
                 .WithMany(u => u.Comments)
                 .OnDelete(DeleteBehavior.ClientCascade);

            builder
                 .HasOne<Movie>(u => u.Movie)
                 .WithMany(m => m.Comments)
                 .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
