using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly DataContext _context;
        public FollowingRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserFollow> GetUserFollow(int sourceUserId, int followedUserId)
        {
            return await _context.Following.FindAsync(sourceUserId, followedUserId);
                    
        }

        public async Task<PagedList<FollowDto>> GetUserFollowing(FollowingParams followingParams)
        {
            var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
            var following = _context.Following.AsQueryable();

            if (followingParams.Predicate == "followed")
            {
                following = following.Where(follow => follow.SourceUserId == followingParams.UserId);
                users = following.Select(follow => follow.FollowedUser);
            }

            if (followingParams.Predicate == "followedBy")
            {
                following = following.Where(follow => follow.FollowedUserId == followingParams.UserId);
                users = following.Select(follow => follow.SourceUser);
            }

            var followedUsers = users.Select(user => new FollowDto
            {
                Username = user.UserName,
                KnownAs = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain).Url,
                City = user.City,
                Id = user.Id
            });

            return await PagedList<FollowDto>.CreateAsync(followedUsers, 
                followingParams.PageNumber, followingParams.PageSize);
        }

        public async Task<AppUser> GetUserWithFollowing(int userId)
        {
            return await _context.Users
                .Include(x => x.FollowedUsers)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}