using MediatR;
using MoviesAPI.Application.Commands.Movies;
using MoviesAPI.Core.Comparators;
using MoviesAPI.Core.Entities;
using MoviesAPI.Core.Interfaces;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Application.CommandsHandler.Movies
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Movie>
    {
        private IRepositoryMovie repositoryMovie;
        private IRepositoryCategory repositoryCategory;
        private IRepositoryMovieCategory repositoryMovieCategory;
        private IRepositoryTag repositoryTag;
        private IRepositoryMovieTag repositoryMovieTag;
        private IRepositoryUser repositoryUser;

        public CreateMovieCommandHandler(
            IRepositoryMovie repositoryMovie,
            IRepositoryCategory repositoryCategory,
            IRepositoryMovieCategory repositoryMovieCategory,
            IRepositoryTag repositoryTag,
            IRepositoryMovieTag repositoryMovieTag,
            IRepositoryUser repositoryUser
            )
        {
            this.repositoryMovie = repositoryMovie;
            this.repositoryCategory = repositoryCategory;
            this.repositoryMovieCategory = repositoryMovieCategory;
            this.repositoryTag = repositoryTag;
            this.repositoryMovieTag = repositoryMovieTag;
            this.repositoryUser = repositoryUser;
        }
        public async Task<Movie> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var movieExist = await this.repositoryMovie.GetMovieByVidGuardId(request.VidGuardId);
            var movieExistByTitle = await this.repositoryMovie.GetMovieByTitle(request.Title);
            var userId = await this.repositoryUser.GetUserByFirebaseId(request.FirebaseId);
            if (movieExist != null)
            {
                throw new HttpRequestException("This movie already exist in database");
            }
            if(userId == null)
            {
                throw new HttpRequestException("This user does not exist in database");
            }
            if(movieExistByTitle != null)
            {
                throw new HttpRequestException("This movie title already exist, choose another one");
            }
            var user = await this.repositoryUser.Read((Guid)userId);
            var movie =
                Movie.CreateMovie(request.Title,
                request.Description,
                request.AddedDate,
                request.VidGuardId,
                request.PosterImageUrl,
                request.PosterImageUrlGif,
                user!.Id
                );
            AddCategoriesToMovie(request.CategoriesIds, movie);
            
            await AddTags(request.Tags);
            await AddTagsToMovie(request.Tags,movie);
            this.repositoryMovie.Create(movie);
            await this.repositoryMovie.SaveChangesAsync();
            return movie;
        }


        public void AddCategoriesToMovie(List<Guid> categoriesIds, Movie movie)
        {
            List<MovieCategory> moviesCategories = new List<MovieCategory>();
            foreach (var categoryId in categoriesIds)
            {
                moviesCategories.Add(new MovieCategory()
                {
                    MovieId = movie.Id,
                    CategoryId = categoryId
                });
            }

            movie.MovieCategories = moviesCategories;
        }

        public async Task AddTags(List<Tag> tags)
        {
            List<Tag> movieTags = new();
            List<Tag> existedTags = new();
            foreach (var tag in tags)
            {
                var exTag = await this.repositoryTag.FindByName(tag.Name);
                if (exTag != null)
                {
                    existedTags.Add(exTag);
                }
            }
            movieTags = tags.ExceptBy(existedTags.Select(tag => tag.Name), tag => tag.Name).ToList();
                
            foreach (var tag in movieTags)
            {
                this.repositoryTag.Create(tag);
                this.repositoryTag.SaveChanges();
            }
        }

        public async Task AddTagsToMovie(List<Tag> tags, Movie movie)
        {
            List<MovieTag> movieTags = new List<MovieTag>();
            await this.repositoryMovieTag.DeleteMovieTagsByMovie(movie.Id);
            foreach (var tag in tags)
            {
                var existedTag = await this.repositoryTag.FindByName(tag.Name);
                if(existedTag != null)
                {
                    movieTags.Add(new MovieTag()
                    {
                        MovieId = movie.Id,
                        TagId = existedTag.Id,
                    });
                }
            }
            movie.MovieTags = movieTags;
        }


    }
}
