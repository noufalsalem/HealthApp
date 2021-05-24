using API.Entities;
using Microsoft.EntityFrameworkCore; 

namespace API.Data
{
    //this class will act as a bridge
    public class DataContext : DbContext //derive DbContext from EntityFramework
    {
        //Generate DataContext constructor 
        public DataContext(DbContextOptions options) : base(options)
        { 
        }

        public DbSet<AppUser> Users { get; set; } //Users: Database table
        public DbSet<UserFollow> Following { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserFollow>()
                .HasKey(k => new{k.SourceUserId, k.FollowedUserId}); //key: combo of FollowedUserId & SourceUserId

            //the following configures the one-to-many relationship
            builder.Entity<UserFollow>()
                .HasOne(s => s.SourceUser)
                .WithMany(f => f.FollowedUsers)
                .HasForeignKey(s => s.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade); //if u delete a user, u delete related entities

            builder.Entity<UserFollow>()
                .HasOne(s => s.FollowedUser)
                .WithMany(f => f.FollowedByUsers)
                .HasForeignKey(s => s.FollowedUserId)
                .OnDelete(DeleteBehavior.Cascade); //if SQL server changed set it to DeleteBehavior.NoAction

            builder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);

        }
        
    }
}