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
        public ActionResult AddStudentByEmail(string email)
        {
            var courseId = TempData.Peek("CourseId").ToString();
            Course course = null;
            string result = "NOT Success";
            if (!string.IsNullOrEmpty(courseId))
            {
                var student = _dbContext.Students.Include(s => s.Notifications).FirstOrDefault(s => s.Email == email);
                if (student != null)
                {
                    course = _dbContext.
                    Courses.
                    Include(c => c.Students).
                    First(x => x.Id == courseId);

                    var not = new Notification()
                    {
                        Title = "Invite",
                        Description = $"Это приглашение на курс {course.Name}! Принять <>",
                        ReciverEmail = email
                    };
                    course.Students.Add(student);
                    student.Notifications.Add(not);
                    _dbContext.SaveChanges();
                    result = "Success";
                }
            }

            //NotifyStudent(email);

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
        public ActionResult AddTeacherByEmail(string email)
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
                var teacher = _dbContext.Teachers.Include(s => s.Notifications).FirstOrDefault(s => s.Email == email);
                if (course != null)
                {

                    var not = new Notification()
                    {
                        Title = "Invite",
                        Description = $"Это приглашение на курс {course.Name}",
                        ReciverEmail = email,
                    };
                    _dbContext.TeacherPermissions.Add(new TeacherPermission
                    {
                        Teacher = teacher,
                        TeacherId = teacher.Id,
                        Course = course,
                        CourseId = courseId
                    });
                    _dbContext.Notifications.Add(not);
                    course.Teachers.Add(teacher);
                    result = "Success";
                }
            }

            _dbContext.SaveChanges();

            return RedirectToAction("EditPermissions", "Settings", new { id = course.Id });
        }

        public async Task<IActionResult> EditPermissions(string id)
        {
            var course = await _dbContext.Courses
                .Include(c => c.Teachers).Include(c => c.TeacherPermissions)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            var viewModel = new CourseSettingsViewModel
            {
                CourseId = course.Id,
                CourseName = course.Name,
                TeacherPermissions = course.TeacherPermissions
            };

            return View(viewModel);
        }


        // POST: Settings/UpdatePermissions
        [HttpPost]
        public async Task<IActionResult> UpdatePermissions(CourseSettingsViewModel model)
        {
            if (model != null)
            {
                var courseId = TempData.Peek("CourseId").ToString();
                var course = _dbContext.
                    Courses.
                    Include(c => c.Teachers).
                    First(x => x.Id == courseId);

                var perms = _dbContext.TeacherPermissions.Where(p => p.CourseId == courseId);
                // Update permissions logic here
                foreach (var permission in model.TeacherPermissions)
                {
                    var teacher = await _dbContext.Teachers.FindAsync(permission.TeacherId);
                    if (teacher != null)
                    {
                        var dbPerm = perms.FirstOrDefault(p => p.TeacherId == teacher.Id);
                        if (dbPerm != null)
                        {

                            dbPerm.CanAddStudents = permission.CanAddStudents;
                            dbPerm.CanEditCourse = permission.CanEditCourse;
                            dbPerm.CanEditGrades = permission.CanEditGrades;
                            dbPerm.CanManageContent = permission.CanManageContent;
                            dbPerm.CanRemoveStudents = permission.CanRemoveStudents;
                            dbPerm.CanViewGrades = permission.CanViewGrades;
                        }
                    }
                }

                await _dbContext.SaveChangesAsync();
                return RedirectToAction("EditPermissions", "Settings", new { id = model.CourseId });
            }

            return View("EditPermissions", model);
        }
    }
}
