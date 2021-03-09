namespace API.Entities
{
    public class AppUser //named AppUser to specify user type
    {
        //USING ENTITY FRAMEWORK FOR AUTO-ENABLED PROPERTIES (DATABASE)

        /*For properties, recommended use same labels 
        so entity framework recognizes it as the right field.**/
        public int Id { get; set; } //Identity property

        public string UserName { get; set; } //UserName property
        

    }
}