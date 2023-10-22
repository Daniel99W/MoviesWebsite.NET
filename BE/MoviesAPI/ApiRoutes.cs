namespace MoviesAPI
{
    public static class ApiRoutes
    {
        public const string Id = "{Id}";
        public static class UsersRoutes
        {
            public const string GetUsers = "GetUsers";
            public const string CreateUser = "CreateUser";
        }
        public static class MovieRoutes
        {
            public const string GetMovies = "GetMovies";
            public const string CreateMovie = "CreateMovie";
            public const string GetMovieById = $"GetMovieById/{Id}";
            public const string DeleteMovieById = $"DeleteMovieById/{Id}";
            public const string GetMoviesByTitle = "GetMoviesByTitle";
            public const string UpdateViewsCounter = "UpdateViewsCounter";
            public const string UpdateMovieById = $"UpdateMovieById/{Id}";
        }

        public static class VotedMoviesRoutes
        {
            public const string VotedMovieByUserAndMovieID = "VotedMovieByUserAndMovieId";
            public const string DownVotedMovieByUserAndMovieID = "DownVotedMovieByUserIdAndMovieID";
        }
        public static class CategoriesRoutes
        {
            public const string GetCategories = "GetCategories";
        }
    }
}
