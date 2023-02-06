using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Syntax.Models;
using System.Net.Http.Headers;

namespace Syntax.WebApp.Internal.Controllers
{
    public class AssetClassController : Controller
    {
        HttpClient client;

        public AssetClassController(IHttpClientFactory factory)
        {
            client = factory.CreateClient();
        }

        // GET: AssetClassController
        public async Task<ActionResult> Index()
        {
            client.BaseAddress = new Uri("http://localhost:5069");
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = client.GetAsync("api/assetclass").Result;
                if (response.IsSuccessStatusCode)
                {
                    var listAC = await response.Content.ReadAsAsync<AssetClass[]>();
                    return View(listAC.ToList());
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
