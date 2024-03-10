using AdminModuleMVC.Data;
using AdminModuleMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

                var course = _dbContext.Courses.FirstOrDefault(x => x.Id == courseId);

                model.Course = course;
                // Получение курса из бд или из модели или из TempData
                return View(model);
            }
            
            return RedirectToAction("Index");
        }
        
        // Добавить изменение записи в бд
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourse(string courseId)
        {
            // Проверка на то что пользователь имеет право редактировать курс

            if (!string.IsNullOrEmpty(courseId))
            {
                EditCourseViewModel model = new EditCourseViewModel();

                var course = _dbContext.Courses.FirstOrDefault(x => x.Id == courseId);

                model.Course = course;
                // Получение курса из бд или из модели или из TempData
                return View(model);
            }

            return RedirectToAction("Index");
        }
        */

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

        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse(CourseViewModel courseModel)
        {

            var form = Request.Form;
            Course course = new Course();
            // Проверка есть ли у курса id, если id нет, создать id и добавить курс в базу, если есть изменить курс
            if (!TempData.ContainsKey("Id"))
            {
                course.Id = Guid.NewGuid().ToString();
            }
            else
            {
                course.Id = TempData["Id"].ToString();
            }
            if (form != null)
            {
                // Добавить проверку на то что пользователь авторизовани ?? А нужно?
                System.Security.Claims.ClaimsPrincipal currentUser = this.User;
                course.Name = form["courseName"];
                course.AutorName = form["authorName"];
                course.Duration = int.Parse(form["duration"]);
                course.Description = form["content"];
                course.IsCoherent = !string.IsNullOrEmpty(form["sequential"]);
                course.IsPublic = !string.IsNullOrEmpty(form["open"]);
                course.AutorId = _userManager.GetUserId(currentUser);

                // Запрос к БД для сохранения
            }
            return View();
        }
        */

        public ActionResult EditTheme(int themeId)
        {
            return View();
        }

        public ActionResult EditSector(int sectorId)
        {
            return View();
        }

        // GET: CourseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create()
        {
            string formValue;
            if (!string.IsNullOrEmpty(Request.Form["content"]))
            {
                formValue = Request.Form["content"];
            }
            return View();
        }

        /*
        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            int i = 0;
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        */

        // GET: CourseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CourseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
