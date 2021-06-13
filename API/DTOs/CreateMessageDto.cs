namespace API.DTOs
{
    //DTO only to store/bridge objects/entities, etc
    public class CreateMessageDto
    {
        public string RecipientUsername { get; set; }  
        public string Content { get; set; }
    }
}