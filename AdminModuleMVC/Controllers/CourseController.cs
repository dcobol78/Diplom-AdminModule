// Разделить контроллер на несколько файлов? Для читаемости.
// Зачем 3 модели и 3 таблицы в бд под файлы если можно использовать 1?

using AdminModuleMVC.Data;
using CourseShared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace AdminModuleMVC.Controllers
{
    // Курс контроллер 
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
            var currentUser = this.User;
            var userId = _userManager.GetUserId(currentUser);

            // Получение списка курсов из БД
            var model = _dbContext.
                Courses.
                Where(c => c.AutorId == userId).
                ToList();
                
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
            var courses = _dbContext.Courses;
            courses.Add(course);
            _dbContext.SaveChanges();

            return PartialView("PartialIndexCourses", _dbContext.Courses.ToList());
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
                    Include(course => course.Test).
                    Include(course => course.Events).
                    First(c => c.Id == courseId);
                if (course != null)
                {
                    TempData["CourseId"] = courseId;
                    return View(course);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCode()
        {
            var courseId = TempData.Peek("CourseId").ToString();
            if (!string.IsNullOrEmpty(courseId))
            {
                string code = "JD73D9djk3";
                return PartialView("PartialStudentListCode", code);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCourse(Course model)
        {
            var courseId = TempData.Peek("CourseId").ToString();
            Course course = null;
            if (!string.IsNullOrEmpty(courseId))
            {
                course = _dbContext.Courses.First(x => x.Id == courseId);
                if (model != null && course != null)
                {
                    // Добавить проверку на то что пользователь авторизовани ?? А нужно?
                    course.Name = model.Name;
                    course.AutorName = model.AutorName;
                    course.Duration = model.Duration;
                    course.Description = model.Description;
                    course.IsCoherent = model.IsCoherent;
                    course.IsPublic = model.IsPublic;
                    course.StartingDate = model.StartingDate;

                    // Запрос к БД для сохранения
                    _dbContext.SaveChanges();
                }
            }

            return PartialView("PartialCourseDetails", course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCourseFiles(IFormFileCollection uploads)
        {
            var courseId = TempData.Peek("CourseId").ToString();
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
                        CourseFile courseFile = new CourseFile { Name = uploadedFile.FileName, ParentId = courseId, Path = path, Description = "", ParentType = "course" };
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
            var courseId = TempData.Peek("CourseId").ToString();
            if (!string.IsNullOrEmpty(courseId))
            {
                var course = _dbContext
                 .Courses
                 .Include(c => c.Homework)
                 .First(x => x.Id == courseId);

                course.Homework = new Homework(courseId, "course");
                _dbContext.SaveChanges();
            }
            return RedirectToAction("EditCourse", new { courseId = courseId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourseTest()
        {
            var courseId = TempData.Peek("CourseId").ToString();
            if (!string.IsNullOrEmpty(courseId))
            {
                var course = _dbContext
                 .Courses
                 .Include(c => c.Test)
                 .First(x => x.Id == courseId);

                course.Test = new Test(courseId, "course");
                _dbContext.SaveChanges();
            }
            return RedirectToAction("EditTest", new { parentId = courseId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEvent()
        {
            var courseId = TempData.Peek("CourseId").ToString();
            if (!string.IsNullOrEmpty(courseId))
            {
                var course = _dbContext
                 .Courses
                 .Include(c => c.Events)
                 .First(x => x.Id == courseId);
                var events = course.Events;

                if (events != null)
                {
                    events.Add(new Event() { Content = "", Name = "", StartTime = DateTime.Now.Date, Type = EventType.Other });
                    _dbContext.SaveChanges();
                }

                return PartialView("PartialCourseEvents", events);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEvents(List<Event> model)
        {
            var courseId = TempData.Peek("CourseId").ToString();
            if (!string.IsNullOrEmpty(courseId))
            {
                var course = _dbContext
                 .Courses
                 .Include(c => c.Events)
                 .First(x => x.Id == courseId);
                var events = course.Events;

                foreach (var _event in events)
                {
                    var newanswer = model.First(a => a.Id == _event.Id);

                    _event.Name = newanswer.Name;
                    _event.StartTime = newanswer.StartTime;
                    _event.Content = newanswer.Content;
                    _event.Type = newanswer.Type;
                }
                _dbContext.SaveChanges();
                return PartialView("PartialCourseEvents", events);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEvent(string eventId)
        {
            var courseId = TempData.Peek("CourseId").ToString();
            if (!string.IsNullOrEmpty(courseId))
            {
                var course = _dbContext
                 .Courses
                 .Include(c => c.Events)
                 .First(x => x.Id == courseId);

                if (course != null)
                {
                    var eventToDelete = course.Events.First(e => e.Id == eventId);
                    if (eventToDelete != null)
                    {
                        course.Events.Remove(eventToDelete);
                        _dbContext.SaveChanges();
                    }
                }
            }
            return RedirectToAction("EditCourse", new { courseId = courseId });
        }

        public ActionResult DeleteCourse()
        {
            var courseId = TempData.Peek("CourseId").ToString();
            if (!string.IsNullOrEmpty(courseId))
            {
                var course = _dbContext.
                    Courses.
                    Include(course => course.Homework).
                    Include(course => course.Homework.HomeworkFile).
                    Include(course => course.CourseFiles).
                    Include(course => course.Sections).
                        ThenInclude(section => section.Themes).
                    Include(course => course.Test).
                    Include(course => course.Events).
                    First(c => c.Id == courseId);

                _dbContext.Courses.Remove(course);
                _dbContext.SaveChanges();
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSector()
        {
            var courseId = TempData.Peek("CourseId").ToString();
            if (!string.IsNullOrEmpty(courseId))
            {
                var course = _dbContext
                .Courses
                .Include(c => c.Sections)
                .First(x => x.Id == courseId);
                var sections = course.Sections;

                if (sections == null) sections = new List<Sector>();

                sections.Add(new Sector(courseId, (sections.Count + 1)));
                _dbContext.SaveChanges();

                return PartialView("PartialCourseSectors", sections);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCourse(string id)
        {
            string courseId;
            if (string.IsNullOrEmpty(id))
            {
                courseId = TempData.Peek("CourseId").ToString();
            }
            else
            {
                courseId = id;
            }

            if (!string.IsNullOrEmpty(courseId))
            {
                DeleteCourseById(courseId);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
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
                    Include(sector => sector.Test).
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
        public ActionResult SaveSector(Sector model)
        {
            var sectorId = TempData.Peek("SectorId").ToString();
            if (!string.IsNullOrEmpty(sectorId))
            {
                Sector sector = _dbContext.Sections.First(x => x.Id == sectorId);
                if (model != null && sector != null)
                {
                    // Добавить проверку на то что пользователь авторизовани ?? А нужно?
                    sector.Name = model.Name;
                    sector.Duration = model.Duration;
                    sector.Content = model.Content;
                    sector.Exp = model.Exp;

                    // Запрос к БД для сохранения
                    _dbContext.SaveChanges();
                }
                return PartialView("PartialSectorDetails", sector);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSectorFiles(IFormFileCollection uploads)
        {
            var sectorId = TempData.Peek("SectorId").ToString();
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
                        CourseFile courseFile = new CourseFile { Name = uploadedFile.FileName, ParentId = sectorId, Path = path, Description = "", ParentType = "sector" };
                        sector.SectionFiles.Add(courseFile);
                        _dbContext.SaveChanges();
                    }
                }
            }
            return RedirectToAction("EditSector", new { sectorId = sectorId });
        }

        // Вроде уже не исползуется!!!
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSectorHomework()
        {
            var sectorId = TempData.Peek("SectorId").ToString();
            if (!string.IsNullOrEmpty(sectorId))
            {
                var sector = _dbContext
                 .Sections
                 .Include(c => c.Homework)
                 .First(x => x.Id == sectorId);

                sector.Homework = new Homework(sectorId, "sector");
                _dbContext.SaveChanges();
            }
            return RedirectToAction("EditSector", new { sectorId = sectorId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSectorTest()
        {
            var sectorId = TempData.Peek("SectorId").ToString();
            if (!string.IsNullOrEmpty(sectorId))
            {
                var sector = _dbContext
                 .Sections
                 .Include(c => c.Test)
                 .First(x => x.Id == sectorId);

                sector.Test = new Test(sectorId, "sector");
                _dbContext.SaveChanges();
            }
            return RedirectToAction("EditTest", new { parentId = sectorId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTheme()
        {
            var sectorId = TempData.Peek("SectorId").ToString();
            if (!string.IsNullOrEmpty(sectorId))
            {
                var sector = _dbContext
                .Sections
                .Include(c => c.Themes)
                .First(x => x.Id == sectorId);
                var themes = sector.Themes;

                if (themes == null) themes = new List<Theme>();

                themes.Add(new Theme() 
                { 
                    IdSection = sectorId, 
                    Number = themes.Count,
                    Content = " ",
                    Name = "Deafualt Theme"
                });
                _dbContext.SaveChanges();
                return RedirectToAction("EditSector", new { sectorId = sectorId });
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSector(string id)
        {
            string sectorId;
            if (string.IsNullOrEmpty(id))
            {
                sectorId = TempData.Peek("SectorId").ToString();
            }
            else
            {
                sectorId = id;
            }

            if (!string.IsNullOrEmpty(sectorId))
            {
                DeleteSectorById(sectorId);
                _dbContext.SaveChanges();

                var courseId = TempData.Peek("CourseId").ToString();

                return RedirectToAction("EditCourse", new { courseId = courseId });
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
                    Include(theme => theme.Test).
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
        public ActionResult SaveTheme(Sector model)
        {
            var themeId = TempData.Peek("ThemeId").ToString();
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
                return PartialView("PartialThemeDetails", theme);
            }
            return View();
        }

        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTheme()
        {
            var themeId = TempData.Peek("ThemeId").ToString();
            if (!string.IsNullOrEmpty(themeId))
            {
                DeleteTheme(themeId);
                _dbContext.SaveChanges();

                var sectorId = TempData.Peek("SectorId").ToString();

                return RedirectToAction("EditSector", new { sectorId = sectorId });
            }
            return View();
        }
        */

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddThemeFiles(IFormFileCollection uploads)
        {
            var themeId = TempData.Peek("ThemeId").ToString();
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
                        CourseFile courseFile = new CourseFile { Name = uploadedFile.FileName, ParentId = themeId, Path = path, Description = "", ParentType = "theme" };
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
            var themeId = TempData.Peek("ThemeId").ToString();
            if (!string.IsNullOrEmpty(themeId))
            {
                var theme = _dbContext
                 .Themes
                 .Include(c => c.Homework)
                 .First(x => x.Id == themeId);

                theme.Homework = new Homework(themeId, "theme");
                _dbContext.SaveChanges();
            }
            return RedirectToAction("EditTheme", new { themeId = themeId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateThemeTest()
        {
            var themeId = TempData.Peek("ThemeId").ToString();
            if (!string.IsNullOrEmpty(themeId))
            {
                var theme = _dbContext
                 .Themes
                 .Include(c => c.Test)
                 .First(x => x.Id == themeId);

                theme.Test = new Test(themeId, "theme");
                _dbContext.SaveChanges();
            }
            return RedirectToAction("EditTest", new { parentId = themeId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTheme(string id)
        {
            string themeId;
            if (string.IsNullOrEmpty(id))
            {
                themeId = TempData.Peek("ThemeId").ToString();
            }
            else
            {
                themeId = id;
            }

            if (!string.IsNullOrEmpty(themeId))
            {
                DeleteThemeById(themeId);
                _dbContext.SaveChanges();

                var sectorId = TempData.Peek("SectorId").ToString();

                return RedirectToAction("EditSector", new { sectorId = sectorId });
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveThemeHomework(Homework model, IFormFile FormFile)
        {
            var themeId = TempData.Peek("ThemeId").ToString();
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
        #endregion

        #region Homework

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateHomework(Homework model, IFormFile FormFile, string deleteHW, string createHW, string deleteFile)
        {
            Homework homework = null;

            if (!string.IsNullOrEmpty(createHW))
            {
                if (createHW == "course")
                {
                    var courseId = TempData.Peek("CourseId").ToString();
                    if (!string.IsNullOrEmpty(courseId))
                    {
                        var course = _dbContext
                         .Courses
                         .Include(c => c.Homework)
                         .First(x => x.Id == courseId);

                        homework = new Homework(courseId, "course");
                        course.Homework = homework;
                        
                    }
                }
                else if (createHW == "sector")
                {
                    var sectorId = TempData.Peek("SectorId").ToString();
                    if (!string.IsNullOrEmpty(sectorId))
                    {
                        var sector = _dbContext
                         .Sections
                         .Include(c => c.Homework)
                         .First(x => x.Id == sectorId);

                        homework = new Homework(sectorId, "sector");
                        sector.Homework = homework;
                    }
                }
                else if (createHW == "theme")
                {
                    var themeId = TempData.Peek("ThemeId").ToString();
                    if (!string.IsNullOrEmpty(themeId))
                    {
                        var theme = _dbContext
                         .Themes
                         .Include(c => c.Homework)
                         .First(x => x.Id == themeId);

                        homework = new Homework(themeId, "theme");
                        theme.Homework = homework;
                    }
                }

                if (homework != null)
                {
                    _dbContext.SaveChanges();
                    return PartialView("PartialHomework", homework);
                }
                return View();
            }

            string parentId = "";
            string parentType = "";
            DbSet<ICourseStage> context;
            if (model.ParentType == "course")
            {
                parentId = TempData.Peek("CourseId").ToString();
                parentType = "Course";
            }
            else if (model.ParentType == "sector")
            {
                parentId = TempData.Peek("SectorId").ToString();
                parentType = "Sector";
            }
            else if (model.ParentType == "theme")
            {
                parentId = TempData.Peek("ThemeId").ToString();
                parentType = "Theme";
            }

            if (!string.IsNullOrEmpty(parentId))
            {
                ICourseStage parent = null;
                if (model.ParentType == "course")
                {
                    parent = _dbContext
                     .Courses
                     .Include(c => c.Homework)
                        .ThenInclude(h => h.HomeworkFile)
                     .First(x => x.Id == parentId);
                }
                else if (model.ParentType == "sector")
                {
                    parent = _dbContext
                     .Sections
                     .Include(c => c.Homework)
                        .ThenInclude(h => h.HomeworkFile)
                     .First(x => x.Id == parentId);
                }
                else if (model.ParentType == "theme")
                {
                    parent = _dbContext
                     .Themes
                     .Include(c => c.Homework)
                        .ThenInclude(h => h.HomeworkFile)
                     .First(x => x.Id == parentId);
                }

                if (!string.IsNullOrEmpty(deleteHW))
                {
                    homework = parent.Homework;

                    var file = homework.HomeworkFile;
                    if (file != null)
                    {
                        // путь к папке Files
                        string path = _appEnvironment.WebRootPath + file.Path;
                        // dfdsf fdgjdkfl jdfl
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                        _dbContext.CourseFiles.Remove(file);
                        homework.HomeworkFile = null;
                    }

                    _dbContext.Homeworks.Remove(homework);
                    homework = null;
                }
                else if (!string.IsNullOrEmpty(deleteFile))
                {
                    var file = homework.HomeworkFile;
                    if (file != null)
                    {
                        // путь к папке Files
                        string path = _appEnvironment.WebRootPath + $"/files/{model.ParentType}files/" + file.Name;
                        // rodsfijw jfdsk kdfjsoiwje
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                        _dbContext.CourseFiles.Remove(file);
                        homework.HomeworkFile = null;
                    }
                }
                else
                {
                    homework = parent.Homework;
                    homework.Duration = model.Duration;
                    homework.Exp = model.Exp;
                    homework.Cost = model.Cost;
                    homework.Description = model.Description.IsNullOrEmpty() ? "" : model.Description;
                    homework.Name = model.Name;
                    if (homework.HomeworkFile == null)
                    {
                        var req = Request.Form;
                        if (Request.Form.Files.Count != 0)
                        {
                            var file = Request.Form.Files[0];
                            if (file != null)
                            {
                                // путь к папке Files
                                string path = "/files/coursefiles/" + file.FileName;
                                // сохраняем файл в папку Files в каталоге wwwroot
                                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                                {
                                    file.CopyTo(fileStream);
                                }
                                CourseFile courseFile = new CourseFile { Name = file.FileName, ParentId = homework.Id, Path = path, Description = "", ParentType = model.ParentType };
                                homework.HomeworkFile = courseFile;
                            }
                        }
                    }
                }
                _dbContext.SaveChanges();
            }
            return PartialView("PartialHomework", homework);
        }

        #endregion

        #region Test

        // Полный бред, находить экземпляр теста через Id родителя???!!!??? Поправить позже
        public ActionResult EditTest(string parentId)
        {
            // Проверка на то что пользователь имеет право редактировать курс
            if (!string.IsNullOrEmpty((string)parentId))
            {
                var test = _dbContext.
                    Tests.
                    Include(t => t.Questions).
                    First(t => t.ParentId == parentId);
                if (test != null)
                {
                    TempData["TestId"] = test.Id;
                    return View(test);
                }
            }
            return RedirectToAction("Index");
        }

        // Полный бред, находить экземпляр теста через Id родителя в EDIT TEST тут тоже покрутить???!!!??? Поправить позже
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveTest(Test model)
        {
            var testId = TempData.Peek("TestId").ToString();
            if (!string.IsNullOrEmpty(testId))
            {
                var test = _dbContext.
                    Tests.
                    First(t => t.Id == testId);
                test.Exp = model.Exp;
                test.Name = model.Name;
                test.Duration = model.Duration;
                test.Cost = model.Cost;
                test.Description = model.Description;
                test.AttemptsAlowed = model.AttemptsAlowed;
                _dbContext.SaveChanges();

                return RedirectToAction("EditTest", new { parentId = test.ParentId});
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTest()
        {
            var testId = TempData.Peek("TestId").ToString();
            if (!string.IsNullOrEmpty(testId))
            {
                var test = _dbContext
                    .Tests
                    .Include(t => t.Questions)
                        .ThenInclude(q => q.Answers)
                    .First(t => t.Id == testId);

                string parentType = test.ParentType;
                string parentId = test.ParentId;

                foreach (var q in test.Questions)
                {
                    _dbContext.Answers.RemoveRange(q.Answers);
                }
                _dbContext.Questions.RemoveRange(test.Questions);

                if (parentType == "course")
                {
                    var course = _dbContext
                        .Courses
                        .Include(c => c.Test)
                        .First(t => t.Id == parentId);

                    course.Test = null;
                    _dbContext.Tests.Remove(test);
                    _dbContext.SaveChanges();

                    return RedirectToAction("EditCourse", new { courseId = parentId });
                }
                else if (parentType == "sector")
                {
                    var section = _dbContext
                        .Sections
                        .Include(c => c.Test)
                        .First(t => t.Id == parentId);

                    section.Test = null;
                    _dbContext.Tests.Remove(test);
                    _dbContext.SaveChanges();

                    return RedirectToAction("EditSector", new { sectorId = parentId });
                }
                else if (parentType == "theme")
                {
                    var theme = _dbContext
                        .Themes
                        .Include(c => c.Test)
                        .First(t => t.Id == parentId);

                    theme.Test = null;
                    _dbContext.Tests.Remove(test);
                    _dbContext.SaveChanges();

                    return RedirectToAction("EditTheme", new { themeId = parentId });
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddQuestion()
        {
            var testId = TempData.Peek("TestId").ToString();
            List<Question> questions = null;
            if (!string.IsNullOrEmpty(testId))
            {
                var test = _dbContext.
                    Tests.
                    Include(t => t.Questions).
                    First(t => t.Id == testId);
                test.Questions.Add(new Question(test.Questions.Count + 1));
                questions = test.Questions;
                _dbContext.SaveChanges();
            }
            return PartialView("PartialTestQuestions", questions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteQuestion(string questionId)
        {
            var testId = TempData.Peek("TestId").ToString();
            if (!string.IsNullOrEmpty(testId))
            {
                var test = _dbContext.
                    Tests
                    .Include(t => t.Questions
                        .OrderBy(q => q.Number)).
                    First(t => t.Id == testId);

                var questions = test.Questions;

                if (questions != null)
                {
                    var question = _dbContext
                        .Questions
                        .Include(q => q.Answers)
                        .Where(q => q.Id == questionId)
                        .SingleOrDefault();

                    for (int i = 0; i < questions.Count; i++)
                    {
                        if (questions[i].Number > question.Number)
                        {
                            questions[i].Number--;
                        }
                    }

                    _dbContext.Answers.RemoveRange(question.Answers);
                    _dbContext.Questions.Remove(question);
                    _dbContext.SaveChanges();
                }
                return RedirectToAction("EditTest", new { parentId = test.ParentId });
            }
            return View();
        }


        public ActionResult EditQuestion(string questionId)
        {
            // Проверка на то что пользователь имеет право редактировать курс
            if (!string.IsNullOrEmpty((string)questionId))
            {
                var question = _dbContext.
                    Questions.
                    Include(t => t.Answers).
                    First(t => t.Id == questionId);
                if (question != null)
                {
                    TempData["QuestionId"] = question.Id;
                    return View(question);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAnswer()
        {
            var questionId = TempData.Peek("QuestionId").ToString();
            if (!string.IsNullOrEmpty(questionId))
            {
                var question = _dbContext.
                    Questions.
                    Include(q => q.Answers).
                    First(q => q.Id == questionId);
                var answers = question.Answers;
                if (answers != null)
                {
                    answers.Add(new Answer() { Number = answers.Count + 1, Content = "AnswerText", IsCorrect = false });
                    _dbContext.SaveChanges();
                }
                return PartialView("PartialQuestionAnswers", question);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAnswer(string answerId)
        {
            var questionId = TempData.Peek("QuestionId").ToString();
            if (!string.IsNullOrEmpty(questionId))
            {
                var question = _dbContext.
                    Questions.
                    Include(q => q.Answers.OrderBy(a => a.Number)).
                    First(q => q.Id == questionId);
                var answers = question.Answers;
                if (answers != null)
                {
                    var answer = answers.First(a => a.Id == answerId);

                    for (int i = 0; i < answers.Count; i++)
                    {
                        if (answers[i].Number < answer.Number)
                        {
                            answers[i].Number--;
                        }
                    }

                    answers.Remove(answer);
                    _dbContext.SaveChanges();
                }
                return RedirectToAction("EditQuestion", new { questionId = questionId });
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateQuestion(Question model, string DeleteAnswer)
        {
            var questionId = TempData.Peek("QuestionId").ToString();

            if (!string.IsNullOrEmpty(questionId))
            {
                var question = _dbContext.
                    Questions.
                    Include(q => q.Answers.OrderBy(a => a.Number)).
                    First(q => q.Id == questionId);
                if (!string.IsNullOrEmpty(DeleteAnswer))
                {
                    var answers = question.Answers;
                    if (answers != null)
                    {
                        var answer = answers.First(a => a.Id == DeleteAnswer);

                        for (int i = 0; i < answers.Count; i++)
                        {
                            if (answers[i].Number < answer.Number)
                            {
                                answers[i].Number--;
                            }
                        }
                        answers.Remove(answer);
                    }
                }

                question.Content = model.Content;
                question.Cost = model.Cost;
                question.Type = model.Type;

                foreach (var answer in question.Answers)
                {
                    var newanswer = model.Answers.First(a => a.Id == answer.Id);

                    answer.IsCorrect = newanswer.IsCorrect;
                    answer.Number = newanswer.Number;
                    answer.Content = newanswer.Content;
                }
                _dbContext.SaveChanges();
            }
            return RedirectToAction("EditQuestion", new { questionId = questionId });
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFile(string fileId)
        {
            string parentId = "";
            string parentType = "";

            if (!string.IsNullOrEmpty(fileId))
            {
                var file = _dbContext
                    .CourseFiles
                    .First(f => f.Id == fileId);
                parentId = file.ParentId;

                char[] letters = file.ParentType.ToCharArray();
                letters[0] = char.ToUpper(letters[0]);
                parentType = new string(letters);
                parentType = file.ParentType;

                if (file != null)
                {
                    // путь к папке Files
                    string path = _appEnvironment.WebRootPath + file.Path;
                    // rodsfijw jfdsk kdfjsoiwje
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    _dbContext.CourseFiles.Remove(file);
                    _dbContext.SaveChanges();
                }
            }
            return RedirectToAction($"Edit{parentType}", new { courseId = parentId, themeId = parentId, sectorId = parentId });
        }

        public ActionResult Attendance(string eventId)
        {
            var courseId = TempData.Peek("CourseId").ToString();

            if (string.IsNullOrEmpty(eventId) && string.IsNullOrEmpty(courseId))
            {
                var course = _dbContext.
                    Courses.
                    Include(c => c.Students).
                    FirstOrDefault(c => c.Id == courseId);

                TempData["EventId"] = eventId;

                if (course != null)
                {
                    return View(course.Students);
                }    
            }
            return View();
        }

        public ActionResult SaveAttendance()
        {

            return View();
        }

        private void NotifyStudent(string studentEmail)
        {

        }

        private void DeleteTest(Test test)
        {
            if (test != null)
            {
                foreach (var q in test.Questions)
                {
                    _dbContext.Answers.RemoveRange(q.Answers);
                }
                _dbContext.Questions.RemoveRange(test.Questions);

                _dbContext.Tests.Remove(test);
                test = null;
            }
        }

        private void DeleteFiles(List<CourseFile> courseFiles)
        {
            foreach (var courseFile in courseFiles) 
            {
                var fileId = courseFile.Id;

                if (courseFile != null)
                {
                    var file = _dbContext
                    .CourseFiles
                        .First(f => f.Id == fileId);

                    if (file != null)
                    {
                        // путь к папке Files
                        string path = _appEnvironment.WebRootPath + file.Path;
                        // rodsfijw jfdsk kdfjsoiwje
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                        _dbContext.CourseFiles.Remove(file);
                    }
                }
            }
        }

        private void DeleteHomework(Homework homework)
        {
            if (homework != null)
            { 
                var file = homework.HomeworkFile;
                if (file != null)
                {
                    // путь к папке Files
                    string path = _appEnvironment.WebRootPath + file.Path;
                    // dfdsf fdgjdkfl jdfl
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    _dbContext.CourseFiles.Remove(file);
                    homework.HomeworkFile = null;
                }

                _dbContext.Homeworks.Remove(homework);
                homework = null;
            }
        }

        private void DeleteThemeById(string themeId)
        {
            var theme = _dbContext.
                Themes.
                Include(t => t.Homework).
                    ThenInclude(h => h.HomeworkFile).
                Include(t => t.Test).
                    ThenInclude(t => t.Questions).
                        ThenInclude(q => q.Answers).
                Include(t => t.ThemeFiles).
                FirstOrDefault(t => t.Id == themeId);

            if (theme.Test != null)
            {
                DeleteTest(theme.Test);
            }

            if (theme.Homework != null)
            {
                DeleteHomework(theme.Homework);
            }

            if (!theme.ThemeFiles.IsNullOrEmpty())
            {
                DeleteFiles(theme.ThemeFiles);
            }

            _dbContext.Themes.Remove(theme);
            theme = null;
        }

        private void DeleteSectorById(string sectorId)
        {
            var section = _dbContext.
                Sections.
                Include(t => t.Homework).
                    ThenInclude(h => h.HomeworkFile).
                Include(t => t.Test).
                    ThenInclude(t => t.Questions).
                        ThenInclude(q => q.Answers).
                Include(t => t.SectionFiles).
                Include(t => t.Themes).
                FirstOrDefault(t => t.Id == sectorId);

            if (section.Test != null)
            {
                DeleteTest(section.Test);
            }

            if (section.Homework != null)
            {
                DeleteHomework(section.Homework);
            }

            if (!section.SectionFiles.IsNullOrEmpty())
            {
                DeleteFiles(section.SectionFiles);
            }

            if (!section.Themes.IsNullOrEmpty())
            {
                foreach (var theme in section.Themes)
                {
                    DeleteThemeById(theme.Id);
                }
                section.Themes.Clear();
            }

            _dbContext.Sections.Remove(section);
            section = null;
        }

        private void DeleteCourseById(string courseId)
        {
            var course = _dbContext.
                Courses.
                Include(t => t.Homework).
                    ThenInclude(h => h.HomeworkFile).
                Include(t => t.Test).
                    ThenInclude(t => t.Questions).
                        ThenInclude(q => q.Answers).
                Include(t => t.CourseFiles).
                Include(t => t.Events).
                Include(t => t.Sections).
                FirstOrDefault(t => t.Id == courseId);

            if (course.Test != null)
            {
                DeleteTest(course.Test);
            }

            if (course.Homework != null)
            {
                DeleteHomework(course.Homework);
            }

            if (!course.CourseFiles.IsNullOrEmpty())
            {
                DeleteFiles(course.CourseFiles);
            }

            if (!course.Sections.IsNullOrEmpty())
            {
                foreach (Sector section in course.Sections)
                {
                    DeleteSectorById(section.Id);
                }
                course.Sections.Clear();
            }

            if (!course.Events.IsNullOrEmpty())
            {
                _dbContext.Events.RemoveRange(course.Events);
            }

            _dbContext.Courses.Remove(course);
            course = null;
        }

    }
}
