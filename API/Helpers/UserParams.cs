namespace API.Helpers
{
    public class UserParams : PaginationParams
    {
        public string CurrentUsername { get; set; }
        public int MinAge { get; set; } = 15;
        public int MaxAge { get; set; } = 100;
        public string OrderBy { get; set; } = "lastActive";
    }
}