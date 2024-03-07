using AdminModuleMVC.Data;
using AdminModuleMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminModuleMVC.Controllers
{
    public class CourseController : Controller
    {
        private ApplicationDbContext dbContext;
        public CourseController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        // GET: CourseController
        public ActionResult Course()
        {
            //CourseViewModel viewModel = new CourseViewModel();
            //viewModel.Course = new Course();
            return View();
        }

        public ActionResult Course(int courseId)
        {
            //CourseViewModel viewModel = new CourseViewModel();
            //viewModel.Course = new Course();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Course(CourseViewModel viewModel)
        {
            var form = Request.Form;
            Course course = new Course();
            // Проверка есть ли у курса id, если id нет, создать id и добавить курс в базу, если есть изменить курс
            if (string.IsNullOrEmpty(viewModel.Course.Id))
            {
                course.Id = Guid.NewGuid().ToString();
            }
            if (form != null)
            {
                
                course.Name = form["courseName"];
                course.AutorName = form["authorName"];
                course.Duration = int.Parse(form["duration"]);
                course.Description = form["content"];
                course.IsCoherent = !string.IsNullOrEmpty(form["sequential"]);
                course.IsPublic = !string.IsNullOrEmpty(form["open"]);
                //course.AutorId 
            }
            return View();
        }

        public ActionResult EditCourse(int courseId)
        {
            return View();
        }

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
