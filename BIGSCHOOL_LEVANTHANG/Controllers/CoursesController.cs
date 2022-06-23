using BIGSCHOOL_LEVANTHANG.Models;
using BIGSCHOOL_LEVANTHANG.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIGSCHOOL_LEVANTHANG.Controllers
{

    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CoursesController() {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Courses
        [Authorize]
        public ActionResult Create()
        {
            var newModel = new CourseViewModels { categories = _dbContext.Categories.ToList() };
            return View(newModel);
        }
        // POST: Course
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModels viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.categories = _dbContext.Categories.ToList();
                return View("Create", viewModel);
            }
            var course = new Course
            {   
                LecturerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Place = viewModel.Place

            };
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var courses = _dbContext.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Course)
                .Include(l => l.Lecturer)
                .Include(l => l.Category)
                .ToList();

            var viewModel = new CourseViewModels
            {
                UpcommingCourse = courses,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }

        
    }
}