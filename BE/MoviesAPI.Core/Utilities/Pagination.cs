namespace MoviesAPI.Dtos
{
    public class Pagination<T>
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public List<T> Results { get; set; }
    }
}
