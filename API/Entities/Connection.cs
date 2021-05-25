namespace API.Entities
{
    public class Connection
    {
        public Connection()
        {
        }

        public Connection(string connectionId, string username)
        {
            ConnectionId = connectionId;
            Username = username;
        }

        //combo of group name & ConnectionId will automatically be seen as primary key
        public string ConnectionId { get; set; } 
        public string Username { get; set; }
    }
}