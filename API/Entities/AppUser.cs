using System;
using System.Collections.Generic;
using API.Extensions;

namespace API.Entities
{
    public class AppUser //named AppUser to specify user type
    {
        //USING ENTITY FRAMEWORK FOR AUTO-ENABLED PROPERTIES (DATABASE)

        /*For properties, recommended use same labels 
        so entity framework recognizes it as the right field.**/
        public int Id { get; set; } //Identity property
        public string UserName { get; set; } //UserName property
        public byte[] PasswordHash {get; set;}
        public byte[] PasswordSalt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string ExperiencedWith { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }

        public ICollection<UserFollow> FollowedByUsers { get; set; } //who follows them
        public ICollection<UserFollow> FollowedUsers { get; set; } //who you follow
    }
}