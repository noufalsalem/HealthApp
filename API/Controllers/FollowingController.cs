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
        private readonly IUnitOfWork _unitOfWork;
        public FollowingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddFollow(string username)
        {
            var sourceUserId = User.GetUserId();
            var followedUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
            var sourceUser = await _unitOfWork.FollowingRepository.GetUserWithFollowing(sourceUserId);

            if (followedUser == null) return NotFound();

            if (sourceUser.UserName == username) return BadRequest("You cannot follow yourself");

            var userFollow = await _unitOfWork.FollowingRepository.GetUserFollow(sourceUserId, followedUser.Id);

            if (userFollow != null) return BadRequest("You already followed this user");

            userFollow = new UserFollow
            {
                SourceUserId = sourceUserId,
                FollowedUserId = followedUser.Id
            };

            sourceUser.FollowedUsers.Add(userFollow);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to follow user");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FollowDto>>> GetUserFollowing([FromQuery] FollowingParams followingParams)
        {
            followingParams.UserId = User.GetUserId();
            var users = await _unitOfWork.FollowingRepository.GetUserFollowing(followingParams);

            Response.AddPaginationHeader(users.CurrentPage,
                users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(users);
        }

    }
}