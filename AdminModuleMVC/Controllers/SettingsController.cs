using AdminModuleMVC.Data;
using CourseShared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminModuleMVC.Controllers
{
    public class SettingsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CourseDbContext _dbContext;
        private readonly IWebHostEnvironment _appEnvironment;

        public SettingsController(CourseDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment appEnvironment)
        {
            _dbContext = context;
            _userManager = userManager;
            _appEnvironment = appEnvironment;
        }

        public ActionResult Index()
        {
            var currentUser = User;
            var userId = _userManager.GetUserId(currentUser);

            // Получение списка курсов из БД
            var model = _dbContext.
                Courses.
                Where(c => c.AutorId == userId).
                ToList();

            return View(model);
        }

        public ActionResult EditAccess(string courseId) 
        {
            if (!string.IsNullOrEmpty(courseId))
            {
                var course = _dbContext.
                    Courses.
                    Include(c => c.Students).
                    Include(c => c.Teachers).
                    First(c => c.Id == courseId);
                if (course != null)
                {
                    TempData["CourseId"] = courseId;
                    return View(course);
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult StudentList(string courseId)
        {
            //var courseId = TempData.Peek("CourseId").ToString();
            Course course = null;
            if (!string.IsNullOrEmpty(courseId))
            {
                course = _dbContext.
                    Courses.
                    Include(c => c.Students).
                    First(x => x.Id == courseId);
                if (course != null)
                {
                    return View(course.Students);
                }
            }
            return View();
        }

        public ActionResult StudentDetails(string studentId)
        {

            if (string.IsNullOrEmpty(studentId))
            {
                var student = _dbContext.
                    Students.
                    Include(s => s.TestGrades).
                    Include(s => s.HomeworkGrades).
                    Include(s => s.Courses).
                    FirstOrDefault(s => s.Id == studentId);
                if (student != null)
                {
                    return View(student);
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Нужно как-то уведомлять клиента
        public ActionResult AddStudent(string email)
        {
            var courseId = TempData.Peek("CourseId").ToString();
            Course course = null;
            string result = "NOT Success";
            if (!string.IsNullOrEmpty(courseId))
            {
                course = _dbContext.
                    Courses.
                    Include(c => c.Students).
                    First(x => x.Id == courseId);
                if (course != null)
                {
                    var not = new Notification()
                    {
                        Title = "Invite",
                        CourseId = courseId,
                        Description = $"Это приглашение на курс {course.Name}",
                        ReciverEmail = email
                    };

                    _dbContext.Notifications.Add(not);
                    result = "Success";
                }
            }

            NotifyStudent(email);

            return PartialView(result);
        }

        public ActionResult TeacherList(string courseId)
        {
            //var courseId = TempData.Peek("CourseId").ToString();
            Course course = null;
            if (!string.IsNullOrEmpty(courseId))
            {
                course = _dbContext.
                    Courses.
                    Include(c => c.Teachers).
                    First(x => x.Id == courseId);
                if (course != null)
                {
                    return View(course.Teachers);
                }
            }
            return View();
        }

        public ActionResult TeacherDetails(string teacherId)
        {
            if (string.IsNullOrEmpty(teacherId))
            {
                var student = _dbContext.
                    Teachers.
                    Include(t => t.RightsTypes).
                    FirstOrDefault(s => s.Id == teacherId);
                if (student != null)
                {
                    return View(student);
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Нужно как-то уведомлять учителя
        public ActionResult AddTeacher(string email)
        {
            var courseId = TempData.Peek("CourseId").ToString();
            Course course = null;
            string result = "NOT Success";
            if (!string.IsNullOrEmpty(courseId))
            {
                course = _dbContext.
                    Courses.
                    Include(c => c.Teachers).
                    First(x => x.Id == courseId);
                if (course != null)
                {
                    var not = new Notification()
                    {
                        Title = "Invite",
                        CourseId = courseId,
                        Description = $"Это приглашение на курс {course.Name}",
                        ReciverEmail = email,
                        NotificationType = false
                    };

                    _dbContext.Notifications.Add(not);
                    result = "Success";
                }
            }

            NotifyTeacher(email);

            return PartialView(result);
        }

        private void NotifyStudent(string studentEmail)
        {

        }

        private void NotifyTeacher(string teacherEmail)
        {

        }
    }
}
