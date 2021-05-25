namespace API.DTOs
{
    public class FollowDto
    {
        public int Id { get; set; } //user id
        public string Username { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public string PhotoUrl { get; set; }
        public string City { get; set; }
    }
}