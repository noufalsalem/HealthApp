using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    //IdentityUser usually takes string & so we don't refactor too much, <int>
    public class AppUser : IdentityUser<int> 
    {
        //USING ENTITY FRAMEWORK FOR AUTO-ENABLED PROPERTIES (DATABASE)

        // For properties, recommended use same labels 
        // so entity framework recognizes it as the right field.
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

        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }

    }
}