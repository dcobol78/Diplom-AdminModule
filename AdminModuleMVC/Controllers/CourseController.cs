using AdminModuleMVC.Data;
using AdminModuleMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AdminModuleMVC.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private CourseDbContext _dbContext;

        public CourseController(CourseDbContext context, UserManager<IdentityUser> userManager)
        {
            _dbContext = context;
            _userManager = userManager;
        }

        public ActionResult Index()
        {

            CourseIndexViewModel model = new CourseIndexViewModel();

            // Получение списка курсов из БД
            model.Courses = _dbContext.Courses.ToList();

            return View(model);
        }

        // GET: CourseController
        public ActionResult EditCourse(string courseId)
        {
            // Проверка на то что пользователь имеет право редактировать курс

            if (!string.IsNullOrEmpty(courseId))
            {
                EditCourseViewModel model = new EditCourseViewModel();

                var course = _dbContext.Courses.Include(c => c.Sections).FirstOrDefault(x => x.Id == courseId);
                model.Course = course;

                TempData["CourseId"] = course.Id;
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse()
        {
            var newId = Guid.NewGuid().ToString();
            var currentUser = this.User;
            var userId = _userManager.GetUserId(currentUser);
            Course course = new Course(newId, userId);

            //Занесение курса в БД
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // Здесь используется Request.Form может использовать модель?
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCourse()
        {
            var courseId = TempData["CourseId"].ToString();
            if (!string.IsNullOrEmpty(courseId))
            {
                var form = Request.Form;
                Course course = _dbContext.Courses.First(x => x.Id == courseId);
                if (form != null && course != null)
                {
                    // Добавить проверку на то что пользователь авторизовани ?? А нужно?
                    course.Name = form["courseName"];
                    course.AutorName = form["authorName"];
                    course.Duration = int.Parse(form["duration"]);
                    course.Description = form["content"];
                    course.IsCoherent = !string.IsNullOrEmpty(form["sequential"]);
                    course.IsPublic = !string.IsNullOrEmpty(form["open"]);

                    // Запрос к БД для сохранения
                    _dbContext.SaveChanges();
                }
            }
            return RedirectToAction("EditCourse", new { courseId = courseId });
        }

        public ActionResult DeleteCourse()
        {
            return View();
        }




        public ActionResult EditSector(string? sectorId)
        {

            if (!string.IsNullOrEmpty(sectorId))
            {
                EditSectorViewModel model = new EditSectorViewModel();

                var section = _dbContext.Sections.Include(c => c.Themes).FirstOrDefault(x => x.Id == sectorId);
                model.Section = section;

                TempData["SectorId"] = section.Id;
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSector(string? courseId)
        {
            var course = _dbContext
                .Courses
                .Include(c => c.Sections)
                .First(x => x.Id == courseId);
            var sections = course.Sections;

            if (sections == null) sections = new List<Section>();

            sections.Add(new Section(courseId, sections.Count));
            _dbContext.SaveChanges();

            return RedirectToAction("EditCourse", new { courseId = courseId });
        }

        // Здесь используется Request.Form может использовать модель?
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSector()
        {
            var courseId = TempData["SectorId"].ToString();
            if (!string.IsNullOrEmpty(courseId))
            {
                var form = Request.Form;
                var sector = _dbContext.Sections.First(x => x.Id == courseId);
                if (form != null && sector != null)
                {
                    // Добавить проверку на то что пользователь авторизовани ?? А нужно?
                    sector.Name = form["sectorName"];
                    sector.Duration = int.Parse(form["duration"]);
                    sector.Content = form["content"];

                    // Запрос к БД для сохранения
                    _dbContext.SaveChanges();
                }
            }
            return RedirectToAction("EditSector", new { sectorId = courseId });
        }

        public ActionResult DeleteSector()
        {
            return View();
        }



/*
        public ActionResult EditTheme(string themeId)
        {
            if (!string.IsNullOrEmpty(themeId))
            {
                EditSectorViewModel model = new EditSectorViewModel();

                var section = _dbContext.Sections.Include(c => c.Themes).FirstOrDefault(x => x.Id == sectorId);
                model.Section = section;

                TempData["ThemeId"] = section.Id;
                return View(model);
            }
            return RedirectToAction("Index");
        }
*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTheme()
        {
            return View();
        }

        // Здесь используется Request.Form может использовать модель?
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveTheme()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTheme()
        {
            return View();
        }

    }
}
