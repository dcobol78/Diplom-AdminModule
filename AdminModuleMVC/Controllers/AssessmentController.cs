using AdminModuleMVC.Data;
using CourseShared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminModuleMVC.Controllers
{
    public class AssessmentController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CourseDbContext _dbContext;
        private readonly IWebHostEnvironment _appEnvironment;

        public AssessmentController(CourseDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment appEnvironment)
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
        public ActionResult EditGrades(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var course = _dbContext.
                    Courses.
                    Include(c => c.Students).
                    Include(c => c.Teachers).
                    First(c => c.Id == id);
                if (course != null)
                {
                    TempData["CourseId"] = id;
                    return View(course.Students);
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(string id)
        {
            var student = await _dbContext.Students.Include(s => s.HomeworkGrades).ThenInclude(hg => hg.Homework)
                                                 .Include(s => s.TestGrades).ThenInclude(tg => tg.Test)
                                                 .FirstOrDefaultAsync(s => s.Id == id);

            var courseId = TempData.Peek("CourseId").ToString();
            var tests = _dbContext.Tests.Where(t => t.ParentId == courseId);

            var homeworks = _dbContext.Homeworks.Where(t => t.ParentId == courseId);

            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        public async Task<IActionResult> EditHomeworkGrade(string id)
        {
            var homeworkGrade = await _dbContext.HomeworkGrades.Include(hg => hg.Homework)
                                                             .FirstOrDefaultAsync(hg => hg.Id == id);
            if (homeworkGrade == null)
            {
                return NotFound();
            }
            return View(homeworkGrade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHomeworkGrade(HomeworkGrade homeworkGrade)
        {
            var courseId = TempData.Peek("CourseId").ToString();
            if (ModelState.IsValid)
            {
                _dbContext.Update(homeworkGrade);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = courseId });
            }
            return View(homeworkGrade);
        }

        public async Task<IActionResult> EditTestGrade(string id)
        {
            var testGrade = await _dbContext.TestGrades.Include(tg => tg.Test)
                                                     .FirstOrDefaultAsync(tg => tg.Id == id);
            if (testGrade == null)
            {
                return NotFound();
            }
            return View(testGrade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTestGrade(TestGrade testGrade)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Update(testGrade);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = testGrade.Test.ParentId });
            }
            return View(testGrade);
        }
    }
}
