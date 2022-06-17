using BIGSCHOOL_LEVANTHANG.DTOs;
using BIGSCHOOL_LEVANTHANG.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BIGSCHOOL_LEVANTHANG.Controllers
{
 
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;

        public FollowingsController()
        {
            _dbContext = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDTOs followingDTOs)
        {
            var userId = User.Identity.GetUserId();
            if (_dbContext.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == followingDTOs.FolloweeId))
                return BadRequest("The Attendance already exists!");


            var following = new Following
            {
                FolloweeId = followingDTOs.FolloweeId,
                FollowerId = userId
            };

            _dbContext.Followings.Add(following);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
