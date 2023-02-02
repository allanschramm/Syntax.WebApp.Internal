using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Syntax.WebApp.Internal.Controllers
{
    public class TransactionClassController : Controller
    {
        // GET: TransactionClassController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TransactionClassController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TransactionClassController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransactionClassController/Create
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

        // GET: TransactionClassController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TransactionClassController/Edit/5
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

        // GET: TransactionClassController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TransactionClassController/Delete/5
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
