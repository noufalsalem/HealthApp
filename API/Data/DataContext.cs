using API.Entities;
using Microsoft.EntityFrameworkCore; 

namespace API.Data
{
    //this class will act as a bridge
    public class DataContext : DbContext //derive DbContext from EntityFramework
    {
        //Generate DataContext constructor 
        public DataContext(DbContextOptions options) : base(options)
        { //
        }

        public DbSet<AppUser> Users { get; set; } //Users: Database table

        
    }
}