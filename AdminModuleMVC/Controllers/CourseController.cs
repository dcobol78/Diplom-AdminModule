// Разделить контроллер на несколько файлов? Для читаемости.
// Зачем 3 модели и 3 таблицы в бд под файлы если можно использовать 1?

using AdminModuleMVC.Data;
using AdminModuleMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IO;

namespace AdminModuleMVC.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CourseDbContext _dbContext;
        private readonly IWebHostEnvironment _appEnvironment;

        public CourseController(CourseDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment appEnvironment)
        {
            _dbContext = context;
            _userManager = userManager;
            _appEnvironment = appEnvironment;
        }

        public ActionResult Index()
        {

            CourseIndexViewModel model = new CourseIndexViewModel();

            // Получение списка курсов из БД
            model.Courses = _dbContext.Courses.ToList();

            return View(model);
        }

        #region Course

        // GET: CourseController
        public ActionResult EditCourse(string courseId)
        {
            // Проверка на то что пользователь имеет право редактировать курс

            if (!string.IsNullOrEmpty(courseId))
            {
                EditCourseViewModel model = new EditCourseViewModel();

                var course = _dbContext.Courses.Include(c => c.Sections).Include(c => c.Homework).FirstOrDefault(x => x.Id == courseId);
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
            var currentUser = this.User;
            var userId = _userManager.GetUserId(currentUser);
            Course course = new Course(userId);

            //Занесение курса в БД
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // Здесь используется Request.Form может использовать модель?
        // Еще здесь используется TempData в других подобных частях используется модель, че за бред?
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCourseFiles(IFormFileCollection uploads)
        {
            var courseId = TempData["CourseId"].ToString();
            foreach (var uploadedFile in uploads)
            {
                if (!string.IsNullOrEmpty(courseId))
                {
                    if (uploadedFile != null)
                    {
                        // путь к папке Files
                        string path = "/files/coursefiles/" + uploadedFile.FileName;
                        // сохраняем файл в папку Files в каталоге wwwroot
                        using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                        {
                            uploadedFile.CopyTo(fileStream);
                        }
                        CourseFile file = new CourseFile { Name = uploadedFile.FileName, ParentId = courseId, Path = path, Description = ""};
                        _dbContext.CourseFiles.Add(file);
                        _dbContext.SaveChanges();
                    }
                }
            }
            return RedirectToAction("EditCourse", new { courseId = courseId });
        }

        public ActionResult CreateCourseHomework()
        {
            var courseId = TempData["CourseId"].ToString();
            if (!string.IsNullOrEmpty(courseId))
            {
                var course = _dbContext
                 .Courses
                 .Include(c => c.Homework)
                 .First(x => x.Id == courseId);

                course.Homework = new Homework(courseId);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("EditCourse", new { courseId = courseId });
        }

        public ActionResult SaveCourseHomework(IFormFile file)
        {
            var courseId = TempData["CourseId"].ToString();
            if (!string.IsNullOrEmpty(courseId))
            {
                var form = Request.Form;
                var course = _dbContext
                 .Courses
                 .Include(c => c.Homework)
                 .First(x => x.Id == courseId);

                var homework = course.Homework;
                homework.Duration = int.Parse(form["duration"]);
                homework.Description = form["content"];
                homework.Name = form["name"];
                homework.HomeWorkFile = (CourseFile)form.Files[0];
                _dbContext.SaveChanges();
            }
            return RedirectToAction("EditCourse", new { courseId = courseId });
        }

        public ActionResult DeleteCourse()
        {
            return View();
        }

        #endregion

        #region Sector

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

        // Принимает courseId, но курс courseId есть в TeampData?
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSectorFiles(IFormFileCollection uploads)
        {
            //Использовать модель или поменять везде на TempData
            //Работает неверно
            var sectorId = TempData["SectorId"].ToString();
            foreach (var uploadedFile in uploads)
            {
                if (!string.IsNullOrEmpty(sectorId))
                {
                    if (uploadedFile != null)
                    {
                        var sector = _dbContext.Sections.Include(s => s.SectionFiles).First(s => s.Id == sectorId);
                        // путь к папке Files
                        string path = "/files/sectorfiles/" + uploadedFile.FileName;
                        // сохраняем файл в папку Files в каталоге wwwroot
                        using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                        {
                            uploadedFile.CopyTo(fileStream);
                        }
                        sector.SectionFiles.Add(new CourseFile { Name = uploadedFile.FileName, ParentId = sectorId, Path = path });
                        _dbContext.SaveChanges();
                    }
                }
            }
            return RedirectToAction("EditSector", new { sectorId = sectorId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSectorHomeWork(IFormFile upload)
        {
            var sectorId = TempData["SectorId"].ToString();
            if (!string.IsNullOrEmpty(sectorId))
            {
                var form = Request.Form;
                var sector = _dbContext.Sections.Include(s => s.Homework).First(s => s.Id == sectorId);
                if (form != null && sector != null) 
                {
                    // Добавить из курса
                }
            }

            return RedirectToAction("EditSector", new { sectorId = sectorId });
        }

        public ActionResult DeleteSector()
        {
            return View();
        }

        #endregion

        #region Theme
        public ActionResult EditTheme(string themeId)
        {
            if (!string.IsNullOrEmpty(themeId))
            {
                EditThemeViewModel model = new EditThemeViewModel();

                var theme = _dbContext.Themes.FirstOrDefault(x => x.Id == themeId);
                model.Theme = theme;

                TempData["ThemeId"] = theme.Id;
                return View(model);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTheme(string? sectorId)
        {
            var sector = _dbContext
                .Sections
                .Include(s => s.Themes)
                .First(x => x.Id == sectorId);
            var themes = sector.Themes;

            if (themes == null) themes = new List<Theme>();

            themes.Add(new Theme(sectorId, themes.Count));
            _dbContext.SaveChanges();

            return RedirectToAction("EditSector", new { sectorId = sectorId });
        }

        // Здесь используется Request.Form может использовать модель?
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveTheme()
        {
            var themeId = TempData["ThemeId"].ToString();
            if (!string.IsNullOrEmpty(themeId))
            {
                var form = Request.Form;
                var theme = _dbContext.Themes.First(x => x.Id == themeId);
                if (form != null && theme != null)
                {
                    // Добавить проверку на то что пользователь авторизовани ?? А нужно?
                    theme.Name = form["themeName"];
                    theme.Duration = int.Parse(form["duration"]);
                    theme.Content = form["content"];

                    // Запрос к БД для сохранения
                    _dbContext.SaveChanges();
                }
            }
            return RedirectToAction("EditTheme", new { themeId = themeId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddThemeFiles(IFormFileCollection uploads)
        {
            //Использовать модель или поменять везде на TempData
            //Работает неверно
            var themeId = TempData["ThemeId"].ToString();
            foreach (var uploadedFile in uploads)
            {
                if (!string.IsNullOrEmpty(themeId))
                {
                    if (uploadedFile != null)
                    {
                        // путь к папке Files
                        string path = "/files/themefiles/" + uploadedFile.FileName;
                        // сохраняем файл в папку Files в каталоге wwwroot
                        using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                        {
                            uploadedFile.CopyTo(fileStream);
                        }
                        CourseFile file = new CourseFile { Name = uploadedFile.FileName, ParentId = themeId, Path = path };
                        _dbContext.CourseFiles.Add(file);
                        _dbContext.SaveChanges();
                    }
                }
            }
            return RedirectToAction("EditTheme", new { themeId = themeId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTheme()
        {
            return View();
        }

        #endregion

    }
}
