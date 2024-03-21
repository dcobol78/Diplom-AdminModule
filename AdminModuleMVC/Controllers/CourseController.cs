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

        #region Index
        public ActionResult Index()
        {
            CourseIndexViewModel model = new CourseIndexViewModel();

            // Получение списка курсов из БД
            model.Courses = _dbContext.Courses.ToList();

            return View(model);
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

        #endregion

        #region Course

        // GET: CourseController
        public ActionResult EditCourse(string courseId)
        {
            // Проверка на то что пользователь имеет право редактировать курс

            if (!string.IsNullOrEmpty(courseId))
            {
                var course = _dbContext.
                    Courses.
                    Include(course => course.Homework).
                    Include(course => course.Homework.HomeworkFile).
                    Include(course => course.CourseFiles).
                    Include(course => course.Sections).
                    First(c => c.Id == courseId);
                if (course != null)
                {
                    TempData["CourseId"] = courseId;
                    return View(course);
                }
            }

            return RedirectToAction("Index");
        }

        // Здесь используется Request.Form может использовать модель?
        // Еще здесь используется TempData в других подобных частях используется модель, че за бред?
        // Сделать частичные представлениями, не будет дрочки с моделями!
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCourse(Course model)
        {
            var courseId = TempData["CourseId"].ToString();
            if (!string.IsNullOrEmpty(courseId))
            {
                Course course = _dbContext.Courses.First(x => x.Id == courseId);
                if (model != null && course != null)
                {
                    // Добавить проверку на то что пользователь авторизовани ?? А нужно?
                    course.Name = model.Name;
                    course.AutorName = model.AutorName;
                    course.Duration = model.Duration;
                    course.Description = model.Description;
                    course.IsCoherent = model.IsCoherent;
                    course.IsPublic = model.IsPublic;

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
            if (!string.IsNullOrEmpty(courseId))
            {
                var course = _dbContext.
                    Courses.
                    Include(course => course.CourseFiles).
                    First(c => c.Id == courseId);
                foreach (var uploadedFile in uploads)
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
                        CourseFile courseFile = new CourseFile { Name = uploadedFile.FileName, ParentId = courseId, Path = path, Description = "" };
                        course.CourseFiles.Add(courseFile);
                        _dbContext.SaveChanges();
                    }
                }
            }
            return RedirectToAction("EditCourse", new { courseId = courseId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCourseHomework(Homework model, IFormFile FormFile)
        {
            var courseId = TempData["CourseId"].ToString();
            if (!string.IsNullOrEmpty(courseId))
            {
                var course = _dbContext
                 .Courses
                 .Include(c => c.Homework)
                 .First(x => x.Id == courseId);

                var homework = course.Homework;
                homework.Duration = model.Duration;
                homework.Description = model.Description.IsNullOrEmpty() ? "" : model.Description;
                homework.Name = model.Name;
                var file = model.FormFile;
                if (FormFile != null) file = FormFile;
                if (file != null)
                {
                    // путь к папке Files
                    string path = "/files/coursefiles/" + file.FileName;
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    CourseFile courseFile = new CourseFile { Name = file.FileName, ParentId = homework.Id, Path = path, Description = "" };
                    homework.HomeworkFile = courseFile;
                }
                _dbContext.SaveChanges();
            }
            return RedirectToAction("EditCourse", new { courseId = courseId });
        }

        public ActionResult DeleteCourse()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSector()
        {
            var courseId = TempData["CourseId"].ToString();
            if (!string.IsNullOrEmpty(courseId))
            {
                var course = _dbContext
                .Courses
                .Include(c => c.Sections)
                .First(x => x.Id == courseId);
                var sections = course.Sections;

                if (sections == null) sections = new List<Section>();

                sections.Add(new Section(courseId, (sections.Count+1)));
                _dbContext.SaveChanges();
                return RedirectToAction("EditCourse", new { courseId = courseId });
            }
            return View();
        }

        #endregion

        #region Sector

        // GET: CourseController
        public ActionResult EditSector(string sectorId)
        {
            // Проверка на то что пользователь имеет право редактировать курс

            if (!string.IsNullOrEmpty(sectorId))
            {
                var sector = _dbContext.
                    Sections.
                    Include(sector => sector.Homework).
                    Include(sector => sector.Homework.HomeworkFile).
                    Include(sector => sector.SectionFiles).
                    Include(sector => sector.Themes).
                    First(c => c.Id == sectorId);
                if (sector != null)
                {
                    TempData["SectorId"] = sectorId;
                    return View(sector);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSector(Section model)
        {
            var sectorId = TempData["SectorId"].ToString();
            if (!string.IsNullOrEmpty(sectorId))
            {
                Section sector = _dbContext.Sections.First(x => x.Id == sectorId);
                if (model != null && sector != null)
                {
                    // Добавить проверку на то что пользователь авторизовани ?? А нужно?
                    sector.Name = model.Name;
                    sector.Duration = model.Duration;
                    sector.Content = model.Content;

                    // Запрос к БД для сохранения
                    _dbContext.SaveChanges();
                }
            }
            return RedirectToAction("EditSector", new { sectorId = sectorId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSectorFiles(IFormFileCollection uploads)
        {
            var sectorId = TempData["SectorId"].ToString();
            if (!string.IsNullOrEmpty(sectorId))
            {
                var sector = _dbContext.
                    Sections.
                    Include(sector => sector.SectionFiles).
                    First(c => c.Id == sectorId);
                foreach (var uploadedFile in uploads)
                {
                    if (uploadedFile != null)
                    {
                        // путь к папке Files
                        string path = "/files/sectorfiles/" + uploadedFile.FileName;
                        // сохраняем файл в папку Files в каталоге wwwroot
                        using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                        {
                            uploadedFile.CopyTo(fileStream);
                        }
                        CourseFile courseFile = new CourseFile { Name = uploadedFile.FileName, ParentId = sectorId, Path = path, Description = "" };
                        sector.SectionFiles.Add(courseFile);
                        _dbContext.SaveChanges();
                    }
                }
            }
            return RedirectToAction("EditSector", new { sectorId = sectorId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSectorHomework()
        {
            var sectorId = TempData["SectorId"].ToString();
            if (!string.IsNullOrEmpty(sectorId))
            {
                var sector = _dbContext
                 .Sections
                 .Include(c => c.Homework)
                 .First(x => x.Id == sectorId);

                sector.Homework = new Homework(sectorId);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("EditSector", new { sectorId = sectorId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSectorHomework(Homework model, IFormFile FormFile)
        {
            var sectorId = TempData["SectorId"].ToString();
            if (!string.IsNullOrEmpty(sectorId))
            {
                var sector = _dbContext
                 .Sections
                 .Include(c => c.Homework)
                 .First(x => x.Id == sectorId);

                var homework = sector.Homework;
                homework.Duration = model.Duration;
                homework.Description = model.Description.IsNullOrEmpty() ? "" : model.Description;
                homework.Name = model.Name;
                var file = model.FormFile;
                if (FormFile != null) file = FormFile;
                if (file != null)
                {
                    // путь к папке Files
                    string path = "/files/sectorfiles/" + file.FileName;
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    CourseFile courseFile = new CourseFile { Name = file.FileName, ParentId = homework.Id, Path = path, Description = "" };
                    homework.HomeworkFile = courseFile;
                }
                _dbContext.SaveChanges();
            }
            return RedirectToAction("EditSector", new { sectorId = sectorId });
        }

        public ActionResult DeleteSector()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTheme()
        {
            var sectorId = TempData["SectorId"].ToString();
            if (!string.IsNullOrEmpty(sectorId))
            {
                var sector = _dbContext
                .Sections
                .Include(c => c.Themes)
                .First(x => x.Id == sectorId);
                var themes = sector.Themes;

                if (themes == null) themes = new List<Theme>();

                themes.Add(new Theme(sectorId, (themes.Count+1)));
                _dbContext.SaveChanges();
                return RedirectToAction("EditSector", new { sectorId = sectorId });
            }
            return View();
        }

        #endregion

        #region Theme
        public ActionResult EditTheme(string themeId)
        {
            // Проверка на то что пользователь имеет право редактировать курс

            if (!string.IsNullOrEmpty(themeId))
            {
                var theme = _dbContext.
                    Themes.
                    Include(theme => theme.Homework).
                    Include(theme => theme.Homework.HomeworkFile).
                    Include(theme => theme.ThemeFiles).
                    First(c => c.Id == themeId);
                if (theme != null)
                {
                    TempData["ThemeId"] = themeId;
                    return View(theme);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveTheme(Section model)
        {
            var themeId = TempData["ThemeId"].ToString();
            if (!string.IsNullOrEmpty(themeId))
            {
                var theme = _dbContext.Themes.First(x => x.Id == themeId);
                if (model != null && theme != null)
                {
                    // Добавить проверку на то что пользователь авторизовани ?? А нужно?
                    theme.Name = model.Name;
                    theme.Duration = model.Duration;
                    theme.Content = model.Content;

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
            var themeId = TempData["ThemeId"].ToString();
            if (!string.IsNullOrEmpty(themeId))
            {
                var theme = _dbContext.
                    Themes.
                    Include(theme => theme.ThemeFiles).
                    First(c => c.Id == themeId);
                foreach (var uploadedFile in uploads)
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
                        CourseFile courseFile = new CourseFile { Name = uploadedFile.FileName, ParentId = themeId, Path = path, Description = "" };
                        theme.ThemeFiles.Add(courseFile);
                        _dbContext.SaveChanges();
                    }
                }
            }
            return RedirectToAction("EditTheme", new { themeId = themeId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateThemeHomework()
        {
            var themeId = TempData["ThemeId"].ToString();
            if (!string.IsNullOrEmpty(themeId))
            {
                var theme = _dbContext
                 .Themes
                 .Include(c => c.Homework)
                 .First(x => x.Id == themeId);

                theme.Homework = new Homework(themeId);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("EditTheme", new { themeId = themeId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveThemeHomework(Homework model, IFormFile FormFile)
        {
            var themeId = TempData["ThemeId"].ToString();
            if (!string.IsNullOrEmpty(themeId))
            {
                var theme = _dbContext
                 .Themes
                 .Include(c => c.Homework)
                 .First(x => x.Id == themeId);

                var homework = theme.Homework;
                homework.Duration = model.Duration;
                homework.Description = model.Description.IsNullOrEmpty() ? "" : model.Description;
                homework.Name = model.Name;
                var file = model.FormFile;
                if (FormFile != null) file = FormFile;
                if (file != null)
                {
                    // путь к папке Files
                    string path = "/files/themefiles/" + file.FileName;
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    CourseFile courseFile = new CourseFile { Name = file.FileName, ParentId = homework.Id, Path = path, Description = "" };
                    homework.HomeworkFile = courseFile;
                }
                _dbContext.SaveChanges();
            }
            return RedirectToAction("EditTheme", new { themeId = themeId });
        }

    }

    #endregion

}
