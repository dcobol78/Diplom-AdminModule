using AdminModuleMVC.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminModuleMVC.Controllers
{
    public class MessagesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CourseDbContext _dbContext;

        public MessagesController(CourseDbContext context, UserManager<IdentityUser> userManager)
        {
            _dbContext = context;
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CourseChat(string courseId)
        {
            return View();
        }

        public ActionResult Chat(string chatId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(string chatId, string messageContent)
        {

            return RedirectToAction("Index");
        }
    }

    /*
    public class MessagesController : Controller
    {
        // GET: Messages
        public ActionResult Index()
        {
            return View();
        }

        // GET: Messages/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Messages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Messages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: Messages/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Messages/Edit/5
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

        // GET: Messages/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Messages/Delete/5
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
    */
}
