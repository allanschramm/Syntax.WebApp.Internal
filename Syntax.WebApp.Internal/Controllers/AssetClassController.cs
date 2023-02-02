using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Syntax.WebApp.Internal.Controllers
{
    public class AssetClassController : Controller
    {
        // GET: AssetClassController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AssetClassController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AssetClassController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AssetClassController/Create
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

        // GET: AssetClassController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AssetClassController/Edit/5
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

        // GET: AssetClassController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AssetClassController/Delete/5
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
