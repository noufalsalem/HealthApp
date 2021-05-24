using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class FollowingController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IFollowingRepository _followingRepository;
        public FollowingController(IUserRepository userRepository, IFollowingRepository followingRepository)
        {
            _followingRepository = followingRepository;
            _userRepository = userRepository;
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddFollow(string username)
        {
            var sourceUserId = User.GetUserId();
            var followedUser = await _userRepository.GetUserByUsernameAsync(username);
            var sourceUser = await _followingRepository.GetUserWithFollowing(sourceUserId);

            if (followedUser == null) return NotFound(); 

            if (sourceUser.UserName == username) return BadRequest("You cannot follow yourself");

            var userFollow = await _followingRepository.GetUserFollow(sourceUserId, followedUser.Id);

            if (userFollow != null) return BadRequest("You already followed this user");

            userFollow = new UserFollow
            {
                SourceUserId = sourceUserId,
                FollowedUserId = followedUser.Id
            };

            sourceUser.FollowedUsers.Add(userFollow);

            if (await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to follow user");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FollowDto>>> GetUserFollowing([FromQuery]FollowingParams followingParams)
        {
            followingParams.UserId = User.GetUserId();
            var users = await _followingRepository.GetUserFollowing(followingParams);

            Response.AddPaginationHeader(users.CurrentPage, 
                users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(users);
        }

    }
}