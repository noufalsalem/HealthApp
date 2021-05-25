using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Group
    {
        //entity framework needs empty constructor to work
        public Group()
        {
        }

        public Group(string name)
        {
            Name = name;
        }

        [Key]
        public string Name { get; set; } //group name will act as primary key
        public ICollection<Connection> Connections { get; set; } = new List<Connection>();
    }
}