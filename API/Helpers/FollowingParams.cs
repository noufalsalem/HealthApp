namespace API.Helpers
{
    public class FollowingParams : PaginationParams
    {
        public int UserId { get; set; }
        public string Predicate { get; set; }
    }
}