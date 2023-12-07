namespace MoviesAPI
{
    public static class ApiRoutes
    {
        public const string Id = "{Id}";
        public static class UsersRoutes
        {
            public const string GetUsers = "GetUsers";
            public const string CreateUser = "CreateUser";
            public const string GetUserByFirebaseId = $"GetUserIdByFirebaseId/{Id}";
        }
        public static class MovieRoutes
        {
            public const string GetMovies = "GetMovies";
            public const string CreateMovie = "CreateMovie";
            public const string GetMovieByTitle = "GetMovieByTitle/{Title}";
            public const string DeleteMovieById = $"DeleteMovieById/{Id}";
            public const string GetMoviesByTitle = "GetMoviesByTitle";
            public const string UpdateViewsCounter = "UpdateViewsCounter";
            public const string UpdateMovieById = $"UpdateMovieById/{Id}";
            public const string GetMovieById = $"GetMovieById/{Id}";
        }
        public static class VotedMoviesRoutes
        {
            public const string VotedMovieByUserAndMovieID = "VoteMovieByUserAndMovieId";
            public const string DownVotedMovieByUserAndMovieID = "DownVoteMovieByUserAndMovieID";
        }
        public static class CategoriesRoutes
        {
            public const string GetCategories = "GetCategories";
            public const string CreateCategory = "CreateCategory";
            public const string DeleteCategoryById = $"DeleteCategoryById/{Id}";
        }
        public static class FavoriteMoviesRoutes
        {
            public const string AddMovieToFavoriteList = "AddMovieToFavoriteList";
            public const string GetFavoriteMoviesByUserId = $"GetFavoriteMoviesByUserId/{Id}";
            public const string DeleteFavoriteMovieByUserIdAndMovieId = "DeleteFavoriteMovieByUserIdAndMovieId";
        }
    }
}
