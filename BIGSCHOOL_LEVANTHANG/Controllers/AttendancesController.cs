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
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _dbContext;

        public AttendancesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDTOs attendanceDtos)
        {

            var userId = User.Identity.GetUserId();
            if (_dbContext.Attendances.Any(a => a.AttendeeId == userId && a.CourseId == attendanceDtos.CourseId))
                return BadRequest("The Attendance already exists!");

            var attendance = new Attendance
            {
                CourseId = attendanceDtos.CourseId,
                AttendeeId = userId
            };

            _dbContext.Attendances.Add(attendance);
            _dbContext.SaveChanges();

            return Ok();

        }
    }
}
