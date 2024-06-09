using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminModuleMVC.Controllers
{
    public class GamififcationController : Controller
    {
        // GET: GamififcationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GamififcationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GamififcationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GamififcationController/Create
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

        // GET: GamififcationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GamififcationController/Edit/5
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

        // GET: GamififcationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GamififcationController/Delete/5
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
