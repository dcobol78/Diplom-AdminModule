using AdminModuleMVC.Data;
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

        // Метод для отображения списка всех оценок
        public ActionResult Index()
        {
            return View();
        }

        // Метод для отображения формы создания новой оценки
        public ActionResult Create()
        {
            return View();
        }

        // Метод для сохранения новой оценки
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewGrade()
        {
            return View();
        }

        // Метод для отображения формы редактирования оценки
        public ActionResult EditGrades(int? id)
        {
            return View();
        }

        // Метод для сохранения изменений после редактирования оценки
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveChanges(int id)
        {
            return View();
        }

        // Метод для отображения частичного представления формы удаления оценки
        public ActionResult Delete(int? id)
        {
            return View();
        }

        // Метод для отображения формы редактирования оценок домашинх заданий
        public ActionResult HomeworkGrades(int? id)
        {
            return View();
        }

        // Метод для отображения формы редактирования оценок тестов
        public ActionResult TestGrades(int? id)
        {
            return View();
        }

        // Метод для удаления оценки
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /*
            var assessment = await _dbContext.Assessments.FindAsync(id);
            _dbContext.Assessments.Remove(assessment);
            await _dbContext.SaveChangesAsync();
            */
            return RedirectToAction(nameof(Index));
            
        }

        // Метод для проверки существования оценки
        private bool AssessmentExists(int id)
        {
            return true;
        }

    }
}
