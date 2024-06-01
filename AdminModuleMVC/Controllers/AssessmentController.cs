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
            var assessments = await _dbContext.Assessments.ToListAsync();
            return View(assessments);
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
            if (ModelState.IsValid)
            {
                fdsaa
                    dfsadsa
                    afds
                    dsafdsaf
                    dfsa
                    dfas
                    dafsf
                    d
                    sdafd
                    as
                    dfasdas
                    dasf
                    dsfa
                    dsa
                    dasf
                _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // Метод для отображения формы редактирования оценки
        public ActionResult EditGrades(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assessment = await _dbContext.Assessments.FindAsync(id);
            if (assessment == null)
            {
                return NotFound();
            }
            return View(assessment);
        }

        // Метод для сохранения изменений после редактирования оценки
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveChanges(int id)
        {
            if (id != assessment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(assessment);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssessmentExists(assessment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(assessment);
        }

        // Метод для отображения частичного представления формы удаления оценки
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assessment = await _dbContext.Assessments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assessment == null)
            {
                return NotFound();
            }

            return View(assessment);
        }

        // Метод для отображения формы редактирования оценок домашинх заданий
        public ActionResult HomeworkGrades(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assessment = await _dbContext.Assessments.FindAsync(id);
            if (assessment == null)
            {
                return NotFound();
            }
            return View(assessment);
        }

        // Метод для отображения формы редактирования оценок тестов
        public ActionResult TestGrades(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assessment = await _dbContext.Assessments.FindAsync(id);
            if (assessment == null)
            {
                return NotFound();
            }
            return View(assessment);
        }

        // Метод для удаления оценки
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var assessment = await _dbContext.Assessments.FindAsync(id);
            _dbContext.Assessments.Remove(assessment);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Метод для проверки существования оценки
        private bool AssessmentExists(int id)
        {
            return _dbContext.Assessments.Any(e => e.Id == id);
        }

    }
}
