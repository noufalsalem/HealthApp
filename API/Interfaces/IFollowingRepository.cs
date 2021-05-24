using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IFollowingRepository
    {
        Task<UserFollow> GetUserFollow(int sourceUserId, int followedUserId);
        Task<AppUser> GetUserWithFollowing(int userId);
        Task<PagedList<FollowDto>> GetUserFollowing(FollowingParams followingParams);
        
    }
}