using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Syntax.Models;
using System.Net.Http.Headers;

namespace Syntax.WebApp.Internal.Controllers
{
    public class TransactionClassController : Controller
    {
        HttpClient client;

        public TransactionClassController(IHttpClientFactory factory)
        {
            client = factory.CreateClient();
        }

        // GET: TransactionClassController
        public async Task<ActionResult> Index()
        {
            client.BaseAddress = new Uri("http://localhost:5069");
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = client.GetAsync("api/transactionclass").Result;
                if (response.IsSuccessStatusCode)
                {
                    var listTC = await response.Content.ReadAsAsync<TransactionClass[]>();
                    return View(listTC.ToList());
                }
                else
                {
                    throw new Exception("Ocorreu um erro na listagem!");
                }
            }
            catch (Exception ex)
            {
                return View("_Erro", ex);
            }
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
